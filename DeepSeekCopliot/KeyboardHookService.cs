	using System.Windows.Threading;
	using KBEHtool;

	namespace deepseek_copilot;

	public sealed class KeyboardHookService : IDisposable
	{
		private const int LongPressMs = 500;

		private readonly AppSettings _settings;
		private readonly DispatcherTimer _holdTimer;
		private bool _copilotMapped;
		private bool _copilotDown;
		private bool _copilotLongPressed;

		// 记录本次按下的是哪个热键，抬起时校验一致
		private KeyCode? _activeHotkey;

		public event Action? CopilotKeyPressed;
		public event Action? CopilotLongPressed;
		public event Action? MappingToggled;

		public KeyboardHookService(AppSettings settings)
		{
			_settings = settings;
			KBEH.Start();
			KeyAction.AddKeyDownPreview(OnKeyDown);
			KeyAction.AddKeyUpPreview(OnKeyUp);

			_holdTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(LongPressMs) };
			_holdTimer.Tick += OnHoldTick;
		}

		private static bool IsHotkey(KeyCode kc) =>
			kc == KeyCode.Copilot || kc == KeyCode.RightControl;

		private bool OnKeyDown(KeyCode kc)
		{
			if (!IsHotkey(kc)) return false;

			// Copilot 键按下时释放可能残留的 Win 键
			if (kc == KeyCode.Copilot)
			{
				KeyAction.ReleaseKey(KeyCode.LeftWin);
			}

			_copilotMapped = false;

			// 切换快捷键：左 Ctrl + Copilot（仅 Copilot 生效）
			if (kc == KeyCode.Copilot &&
				_settings.EnableToggleShortcut &&
				KeyAction.IsKeyPressed(KeyCode.LeftControl))
			{
				_settings.MapCopilotToRightCtrl = !_settings.MapCopilotToRightCtrl;
				_settings.Save();
				MappingToggled?.Invoke();
				return true;
			}

			// 映射模式：仅 Copilot 生效
			if (kc == KeyCode.Copilot && _settings.MapCopilotToRightCtrl)
			{
				KeyAction.PressKey(KeyCode.RightControl, -1);
				_copilotMapped = true;
				_activeHotkey = KeyCode.Copilot;
				return true;
			}

			// 常规热键：启动长按计时
			if (!_copilotDown)
			{
				_copilotDown = true;
				_copilotLongPressed = false;
				_activeHotkey = kc;
				_holdTimer.Start();
			}
			return true;
		}

		private void OnHoldTick(object? sender, EventArgs e)
		{
			_holdTimer.Stop();
			if (!_copilotDown) return;

			_copilotLongPressed = true;
			CopilotLongPressed?.Invoke();
		}

		private bool OnKeyUp(KeyCode kc)
		{
			if (!IsHotkey(kc)) return false;

			// 抬起的键必须与按下的键一致
			if (_activeHotkey != kc) return true;

			_holdTimer.Stop();
			_copilotDown = false;
			_activeHotkey = null;

			if (_copilotMapped)
			{
				KeyAction.ReleaseKey(KeyCode.RightControl);
			}
			else if (!_copilotLongPressed)
			{
				CopilotKeyPressed?.Invoke();
			}
			return true;
		}

		public void Dispose()
		{
			_holdTimer.Stop();
			KBEH.Stop();
		}
	}