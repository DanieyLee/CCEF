using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Security.Permissions;

namespace BY2._0
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class Main : Form
    {
        public static ChromiumWebBrowser browser;
        public static string saveSwitch;
        public static string login;
        public static string password;
        public static IniFiles ini = new IniFiles(Application.StartupPath + @"\Config.ini");
        public Main()
        {
            InitializeComponent();
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            InitBrowser();
        }
        public void InitBrowser()
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.Locale = "zh_CN";
            Cef.Initialize(settings);
            //browser = new ChromiumWebBrowser("http://upick.booymp.com:8088/#/login");
            //browser = new ChromiumWebBrowser("file:///C:/Users/add/Desktop/123.html");
            browser = new ChromiumWebBrowser("http://192.168.1.225:8088/#/login");

            this.Controls.Add(browser);
            //browser.FrameLoadEnd += OpenDev;
            browser.RegisterAsyncJsObject("bound", new _Event());
            browser.Dock = DockStyle.Fill;
        }
        public void OpenDev(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }
    }
    public class _Event
    {
        public void setPassword(string saveSwitch,string login,string password)
        {
            //if (!Main.ini.ExistINIFile())
            Main.ini.IniWriteValue("inf","save", saveSwitch);
            Main.ini.IniWriteValue("inf", "login", login);
            Main.ini.IniWriteValue("inf", "pass", password);
            //MessageBox.Show("set收到调用"+saveSwitch + "," + login + "," + password);
        }
        public string getPassword()
        {
            if (!Main.ini.ExistINIFile()) return "0,,";


            return Main.ini.IniReadValue("inf", "save")+ "," +
                Main.ini.IniReadValue("inf", "login") + "," +
                Main.ini.IniReadValue("inf", "pass");
        }
    }
}
