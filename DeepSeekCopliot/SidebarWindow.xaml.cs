	using KBEHtool;
	using Microsoft.Web.WebView2.Wpf;
	using System.Runtime.InteropServices;
	using System.Text.Json;
	using System.Windows;
	using System.Windows.Input;
	using System.Windows.Interop;
	using System.Windows.Media;
	using System.Windows.Media.Animation;
	using System.Windows.Threading;

	namespace deepseek_copilot;

	public partial class SidebarWindow : Window
	{
		private const double BaseAnimationDurationMs = 250;
		private readonly AppSettings _settings;
		private double _sidebarWidth = 450;
		private DispatcherTimer? _unloadTimer;
		private WebView2? _webView;
		private bool _isOpeningSettings;
		private TaskCompletionSource<bool>? _slideInTcs;

		private void OnWebMessageReceived(object? sender,
			Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
		{
			string msg;
			try { msg = e.TryGetWebMessageAsString(); }
			catch { return; }

			if (msg != "send") return;

			// 让页面里的脚本去点击发送按钮
			Dispatcher.BeginInvoke(async () =>
			{
				try
				{
					await _webView!.CoreWebView2!.ExecuteScriptAsync(
						"window.__dsCopilotClickSend && window.__dsCopilotClickSend();");
				}
				catch { }
			});
		}

		private const string SwapEnterShiftEnterScript = """
		(() => {
			if (window.__dsCopilotEnterSwapped) return;
			window.__dsCopilotEnterSwapped = true;

			const isInput = (el) =>
				el && (el.tagName === 'TEXTAREA' ||
					   (el.tagName === 'INPUT' && el.type === 'text'));

			// 点击发送按钮（供 C# 调用）
			window.__dsCopilotClickSend = () => {
				const btn = document.querySelector(
					'div[role="button"].ds-button--primary.ds-button--filled') ||
					document.querySelector('div[role="button"].ds-button--primary');
				if (btn) {
					btn.click();
					return true;
				}
				return false;
			};

			document.addEventListener('keydown', (e) => {
				if (e.key !== 'Enter' && e.code !== 'Enter') return;
				if (!isInput(e.target)) return;
				if (e.isComposing || e.keyCode === 229) return;
				if (!e.isTrusted) return;

				e.stopImmediatePropagation();
				e.preventDefault();

				if (!e.shiftKey) {
					// Enter → 换行
					const t = e.target;
					const start = t.selectionStart;
					const end = t.selectionEnd;
					const value = t.value;
					const newValue = value.substring(0, start) + '\n' + value.substring(end);

					const proto = t instanceof HTMLTextAreaElement
						? HTMLTextAreaElement.prototype
						: HTMLInputElement.prototype;
					Object.getOwnPropertyDescriptor(proto, 'value').set.call(t, newValue);

					t.selectionStart = t.selectionEnd = start + 1;
					t.dispatchEvent(new Event('input', { bubbles: true }));
				} else {
					// Shift+Enter → 通知 C# 去点发送按钮
					window.chrome.webview.postMessage('send');
				}
			}, true);
		})();
		""";

		public bool IsAnimating { get; private set; }
		public event Action? SettingsClicked;

		protected override void OnClosed(EventArgs e)
		{
			_unloadTimer?.Stop();
			DestroyWebView();
			base.OnClosed(e);
		}

		public SidebarWindow(AppSettings settings)
		{
			_settings = settings;
			InitializeComponent();

			Topmost = true;   // ← 永远浮在所有窗口上面

			Icon = IconHelper.RenderToBitmapSource(16);
			TitleIcon.Source = IconHelper.RenderToBitmapSource(18);

			var workArea = SystemParameters.WorkArea;
			Left = workArea.Right;
			Top = workArea.Top;
			Height = workArea.Height;

			PreviewKeyDown += OnPreviewKeyDown;
			Loaded += OnLoaded;
		}

		public void ApplyTheme(ThemeMode theme)
		{
			_settings.SetThemeMode(theme);
			ApplyOpacity(_settings.SidebarOpacity);

			var isDark = _settings.IsDarkTheme;
			var textColor = isDark ? Colors.White : System.Windows.Media.Color.FromRgb(0x1A, 0x1A, 0x2E);
			TitleText.Foreground = new SolidColorBrush(textColor);

			var btnColor = isDark ? Colors.White : System.Windows.Media.Color.FromRgb(0x66, 0x66, 0x66);
			SettingsButton.Foreground = new SolidColorBrush(btnColor);
			CloseButton.Foreground = new SolidColorBrush(btnColor);

			SetWebViewColorScheme(isDark);
		}

		public void ApplyOpacity(double opacity)
		{
			var isDark = _settings.IsDarkTheme;
			var baseColor = isDark
				? System.Windows.Media.Color.FromRgb(0x15, 0x15, 0x17)
				: System.Windows.Media.Color.FromRgb(0xFF, 0xFF, 0xFF);
			var alpha = (byte)(opacity * 255);
			var bgColor = System.Windows.Media.Color.FromArgb(alpha, baseColor.R, baseColor.G, baseColor.B);

			MainBorder.Background = new SolidColorBrush(bgColor);
			TitleBar.Background = new SolidColorBrush(bgColor);
		}

		private void SetWebViewColorScheme(bool isDark)
		{
			if (_webView?.CoreWebView2 == null) return;
			_webView.CoreWebView2.Profile.PreferredColorScheme =
				isDark
					? Microsoft.Web.WebView2.Core.CoreWebView2PreferredColorScheme.Dark
					: Microsoft.Web.WebView2.Core.CoreWebView2PreferredColorScheme.Light;
		}

		public void ApplyWidth(double width)
		{
			_sidebarWidth = width;
			Width = width;
			var workArea = SystemParameters.WorkArea;
			BeginAnimation(LeftProperty, null);
			Left = workArea.Right - width;
		}

		private async void OnLoaded(object sender, RoutedEventArgs e)
		{
			await CreateWebViewAsync();
		}

		private async Task CreateWebViewAsync()
		{
			if (_webView != null) return;

			var env = await Microsoft.Web.WebView2.Core.CoreWebView2Environment.CreateAsync(
				null,
				System.IO.Path.Combine(
					System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
					"DeepSeekCopilot", "WebView2"));

			_webView = new WebView2();
			WebViewHost.Children.Add(_webView);

			await _webView.EnsureCoreWebView2Async(env);
			_webView.CoreWebView2!.DocumentTitleChanged += OnTitleChanged;

			// 接收 JS 发来的 "send" 消息
			_webView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;

			// 每次页面导航完成后注入按键交换脚本
			_webView.CoreWebView2.NavigationCompleted += async (_, _) =>
			{
				try
				{
					await _webView.CoreWebView2.ExecuteScriptAsync(SwapEnterShiftEnterScript);
				}
				catch { }
			};

			SetWebViewColorScheme(_settings.IsDarkTheme);
			_webView.CoreWebView2.Navigate(_settings.ChatUrl);
			TitleText.Text = _webView.CoreWebView2.DocumentTitle;
		}

		private void OnTitleChanged(object? sender, object e)
		{
			Dispatcher.Invoke(() =>
			{
				TitleText.Text = _webView?.CoreWebView2?.DocumentTitle ?? "DeepSeek";
			});
		}

		private void DestroyWebView()
		{
			_unloadTimer = null;

			if (_webView == null) return;

			_webView.CoreWebView2!.DocumentTitleChanged -= OnTitleChanged;
			WebViewHost.Children.Remove(_webView);
			_webView.Dispose();
			_webView = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public async void SlideIn()
		{
			if (IsAnimating) return;
			_unloadTimer?.Stop();

			_slideInTcs?.TrySetCanceled();
			_slideInTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
			IsAnimating = true;

			if (_webView == null)
			{
				Show();
				Activate();
				await CreateWebViewAsync();
				AnimateIn();
			}
			else
			{
				Show();
				Activate();
				AnimateIn();
			}
		}

		public Task WaitForSlideInAsync()
		{
			if (_slideInTcs != null) return _slideInTcs.Task;

			_slideInTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
			if (!IsAnimating && Visibility == Visibility.Visible)
				_slideInTcs.TrySetResult(true);
			return _slideInTcs.Task;
		}

		public async Task PasteClipboardImageAsync()
		{
			if (_webView?.CoreWebView2 == null) return;

			const string script =
				"(()=>{const i=document.querySelector('textarea,input');if(i){i.focus();}" +
				"const v=document.querySelector('[data-model-type=\"vision\"][role=\"radio\"]');" +
				"if(v&&v.getAttribute('aria-checked')!=='true'){v.click();}" +
				"return i?true:false;})()";

			for (var i = 0; i < 40; i++)
			{
				try
				{
					var result = await _webView.CoreWebView2.ExecuteScriptAsync(script);
					if (result == "true")
					{
						await Task.Delay(250);
						break;
					}
				}
				catch { }
				await Task.Delay(150);
			}

			try
			{
				await _webView.CoreWebView2.ExecuteScriptAsync(
					"(()=>{const v=document.querySelector('[data-model-type=\"vision\"][role=\"radio\"]');" +
					"if(v&&v.getAttribute('aria-checked')!=='true'){v.click();}return true;})()");
			}
			catch { }

			await Task.Delay(200);

			try
			{
				KeyAction.PressKey(new[] { KeyCode.LeftControl, KeyCode.V }, 60);
			}
			catch { }
		}

		public async Task<bool> AskAsync(string question)
		{
			if (_webView?.CoreWebView2 == null) return false;

			var encoded = JsonSerializer.Serialize(question);

			const string focusScript =
				"(()=>{const e=document.querySelector('textarea,input');if(e){e.focus();}return e?true:false;})()";

			var setScript =
				"(()=>{" +
				"const el=document.querySelector('textarea,input');" +
				"if(!el)return false;" +
				"const proto=el instanceof HTMLTextAreaElement?HTMLTextAreaElement.prototype:HTMLInputElement.prototype;" +
				"Object.getOwnPropertyDescriptor(proto,'value').set.call(el," + encoded + ");" +
				"el.dispatchEvent(new Event('input',{bubbles:true}));" +
				"return true;})()";

			var found = false;
			for (var i = 0; i < 40; i++)
			{
				try
				{
					var ok = await _webView.CoreWebView2.ExecuteScriptAsync(setScript);
					if (ok == "true") { found = true; break; }
				}
				catch { }
				await Task.Delay(150);
			}

			if (!found) return false;

			try { await _webView.CoreWebView2.ExecuteScriptAsync(focusScript); } catch { }
			await Task.Delay(200);

			const string enterScript =
				"(()=>{" +
				"const el=document.querySelector('textarea,input');" +
				"if(!el)return false;" +
				"const down=new KeyboardEvent('keydown',{key:'Enter',code:'Enter',keyCode:13,which:13,bubbles:true,cancelable:true});" +
				"const up=new KeyboardEvent('keyup',{key:'Enter',code:'Enter',keyCode:13,which:13,bubbles:true,cancelable:true});" +
				"el.dispatchEvent(down);el.dispatchEvent(up);return true;})()";

			try { await _webView.CoreWebView2.ExecuteScriptAsync(enterScript); } catch { }
			await Task.Delay(150);

			// 用脚本里定义好的 __dsCopilotClickSend 点发送
			try
			{
				await _webView.CoreWebView2.ExecuteScriptAsync(
					"window.__dsCopilotClickSend && window.__dsCopilotClickSend();");
			}
			catch { }

			return true;
		}

		private void AnimateIn()
		{
			var workArea = SystemParameters.WorkArea;
			var anim = new DoubleAnimation
			{
				From = workArea.Right,
				To = workArea.Right - _sidebarWidth,
				Duration = TimeSpan.FromMilliseconds(BaseAnimationDurationMs / _settings.AnimationSpeed),
				EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
			};
			anim.Completed += async (_, _) =>
			{
				IsAnimating = false;
				_slideInTcs?.TrySetResult(true);
				if (_webView != null)
				{
					Keyboard.Focus(_webView);
					try
					{
						await _webView.CoreWebView2!.ExecuteScriptAsync(
							"try{let e=document.querySelector('textarea,input');e?.focus()}catch{}");
					}
					catch { }
				}
			};
			BeginAnimation(LeftProperty, anim);
		}

		public void SlideOut()
		{
			if (IsAnimating) return;

			IsAnimating = true;
			var anim = new DoubleAnimation
			{
				From = Left,
				To = SystemParameters.WorkArea.Right,
				Duration = TimeSpan.FromMilliseconds(BaseAnimationDurationMs / _settings.AnimationSpeed),
				EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
			};
			anim.Completed += (_, _) =>
			{
				IsAnimating = false;
				Hide();
			};
			BeginAnimation(LeftProperty, anim);
		}

		private void OnSettingsClick(object sender, RoutedEventArgs e)
		{
			_isOpeningSettings = true;
			SettingsClicked?.Invoke();
			_isOpeningSettings = false;
		}

		private void OnPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Escape)
			{
				SlideOut();
			}
		}

		private void OnCloseClick(object sender, RoutedEventArgs e)
		{
			SlideOut();
		}
	}