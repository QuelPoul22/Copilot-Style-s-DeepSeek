using System.Runtime.InteropServices;

namespace KBEHtool.Base;

public static class ByteVirtualKey
{
    // 鼠标
    public const byte vbKeyLButton = 0x01;
    public const byte vbKeyRButton = 0x02;
    public const byte vbKeyCancel = 0x03;
    public const byte vbKeyMButton = 0x04;

    // 控制
    public const byte vbKeyBack = 0x08;
    public const byte vbKeyTab = 0x09;
    public const byte vbKeyClear = 0x0C;
    public const byte vbKeyReturn = 0x0D;
    public const byte vbKeyShift = 0x10;
    public const byte vbKeyControl = 0x11;
    public const byte vbKeyAlt = 0x12;
    public const byte vbKeyPause = 0x13;
    public const byte vbKeyCapital = 0x14;
    public const byte vbKeyEscape = 0x1B;
    public const byte vbKeySpace = 0x20;
    public const byte vbKeyPageUp = 0x21;
    public const byte vbKeyPageDown = 0x22;
    public const byte vbKeyEnd = 0x23;
    public const byte vbKeyHome = 0x24;
    public const byte vbKeyLeft = 0x25;
    public const byte vbKeyUp = 0x26;
    public const byte vbKeyRight = 0x27;
    public const byte vbKeyDown = 0x28;
    public const byte vbKeySelect = 0x29;
    public const byte vbKeyPrint = 0x2A;
    public const byte vbKeyExecute = 0x2B;
    public const byte vbKeySnapshot = 0x2C;
    public const byte vbKeyInsert = 0x2D;
    public const byte vbKeyDelete = 0x2E;
    public const byte vbKeyHelp = 0x2F;

    // 字母
    public const byte vbKeyA = 65;
    public const byte vbKeyB = 66;
    public const byte vbKeyC = 67;
    public const byte vbKeyD = 68;
    public const byte vbKeyE = 69;
    public const byte vbKeyF = 70;
    public const byte vbKeyG = 71;
    public const byte vbKeyH = 72;
    public const byte vbKeyI = 73;
    public const byte vbKeyJ = 74;
    public const byte vbKeyK = 75;
    public const byte vbKeyL = 76;
    public const byte vbKeyM = 77;
    public const byte vbKeyN = 78;
    public const byte vbKeyO = 79;
    public const byte vbKeyP = 80;
    public const byte vbKeyQ = 81;
    public const byte vbKeyR = 82;
    public const byte vbKeyS = 83;
    public const byte vbKeyT = 84;
    public const byte vbKeyU = 85;
    public const byte vbKeyV = 86;
    public const byte vbKeyW = 87;
    public const byte vbKeyX = 88;
    public const byte vbKeyY = 89;
    public const byte vbKeyZ = 90;

    // 主键盘数字
    public const byte vbKey0 = 48;
    public const byte vbKey1 = 49;
    public const byte vbKey2 = 50;
    public const byte vbKey3 = 51;
    public const byte vbKey4 = 52;
    public const byte vbKey5 = 53;
    public const byte vbKey6 = 54;
    public const byte vbKey7 = 55;
    public const byte vbKey8 = 56;
    public const byte vbKey9 = 57;

    // 小键盘
    public const byte vbKeyNumpad0 = 0x60;
    public const byte vbKeyNumpad1 = 0x61;
    public const byte vbKeyNumpad2 = 0x62;
    public const byte vbKeyNumpad3 = 0x63;
    public const byte vbKeyNumpad4 = 0x64;
    public const byte vbKeyNumpad5 = 0x65;
    public const byte vbKeyNumpad6 = 0x66;
    public const byte vbKeyNumpad7 = 0x67;
    public const byte vbKeyNumpad8 = 0x68;
    public const byte vbKeyNumpad9 = 0x69;
    public const byte vbKeyMultiply = 0x6A;
    public const byte vbKeyAdd = 0x6B;
    public const byte vbKeySeparator = 0x6C;
    public const byte vbKeySubtract = 0x6D;
    public const byte vbKeyDecimal = 0x6E;
    public const byte vbKeyDivide = 0x6F;

    // F1-F24
    public const byte vbKeyF1 = 0x70;
    public const byte vbKeyF2 = 0x71;
    public const byte vbKeyF3 = 0x72;
    public const byte vbKeyF4 = 0x73;
    public const byte vbKeyF5 = 0x74;
    public const byte vbKeyF6 = 0x75;
    public const byte vbKeyF7 = 0x76;
    public const byte vbKeyF8 = 0x77;
    public const byte vbKeyF9 = 0x78;
    public const byte vbKeyF10 = 0x79;
    public const byte vbKeyF11 = 0x7A;
    public const byte vbKeyF12 = 0x7B;
    public const byte vbKeyF13 = 0x7C;
    public const byte vbKeyF14 = 0x7D;
    public const byte vbKeyF15 = 0x7E;
    public const byte vbKeyF16 = 0x7F;
    public const byte vbKeyF17 = 0x80;
    public const byte vbKeyF18 = 0x81;
    public const byte vbKeyF19 = 0x82;
    public const byte vbKeyF20 = 0x83;
    public const byte vbKeyF21 = 0x84;
    public const byte vbKeyF22 = 0x85;
    public const byte vbKeyF23 = 0x86;
    public const byte vbKeyF24 = 0x87;

    // 修饰键
    public const byte vbKeyLShift = 0xA0;
    public const byte vbKeyRShift = 0xA1;
    public const byte vbKeyLControl = 0xA2;
    public const byte vbKeyRControl = 0xA3;
    public const byte vbKeyLAlt = 0xA4;
    public const byte vbKeyRAlt = 0xA5;
    public const byte vbKeyLWin = 0x5B;
    public const byte vbKeyRWin = 0x5C;

    // OEM 符号键
    public const byte vbKeyOem1 = 0xBA;   // ;
    public const byte vbKeyOemPlus = 0xBB;   // =
    public const byte vbKeyOemComma = 0xBC;  // ,
    public const byte vbKeyOemMinus = 0xBD;  // -
    public const byte vbKeyOemPeriod = 0xBE; // .
    public const byte vbKeyOem2 = 0xBF;   // /
    public const byte vbKeyOem3 = 0xC0;   // `
    public const byte vbKeyOem4 = 0xDB;   // [
    public const byte vbKeyOem5 = 0xDC;   // \
    public const byte vbKeyOem6 = 0xDD;   // ]
    public const byte vbKeyOem7 = 0xDE;   // '
    public const byte vbKeyOem8 = 0xDF;

    // 杂项
    public const byte vbKeyNumlock = 0x90;
    public const byte vbKeyScroll = 0x91;
    public const byte vbKeySleep = 0x5F;
    public const byte vbKeyApps = 0x5D;

    // 多媒体键 (VK 常量来自 WinUser.h)
    public const byte vbKeyVolumeMute = 0xAD;
    public const byte vbKeyVolumeDown = 0xAE;
    public const byte vbKeyVolumeUp = 0xAF;
    public const byte vbKeyMediaNext = 0xB0;
    public const byte vbKeyMediaPrev = 0xB1;
    public const byte vbKeyMediaStop = 0xB2;
    public const byte vbKeyMediaPlayPause = 0xB3;

    // 浏览器键
    public const byte vbKeyBrowserBack = 0xA6;
    public const byte vbKeyBrowserForward = 0xA7;
    public const byte vbKeyBrowserRefresh = 0xA8;
    public const byte vbKeyBrowserSearch = 0xAA;
    public const byte vbKeyBrowserFavorites = 0xAB;
    public const byte vbKeyBrowserHome = 0xAC;

    // 应用键
    public const byte vbKeyLaunchMail = 0xB4;
    public const byte vbKeyLaunchMedia = 0xB5;
    public const byte vbKeyLaunchApp1 = 0xB6;
    public const byte vbKeyLaunchApp2 = 0xB7;

    // Windows 11 Copilot 键 (VK=F23)
    public const byte vbKeyCopilot = 0x86;

    [DllImport("user32.dll")]
    public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
}
