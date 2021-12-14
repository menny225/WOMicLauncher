using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOMicLauncher
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, uint wParam, uint lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        public Form1()
        {
            InitializeComponent();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            string file = @"D:\Portable\Portable WOMic\WOMicClient.exe";
            Process.Start(file);
            await Task.Delay(100);
            FindWO();
            await Task.Delay(5);
            Connect();
            await Task.Delay(5);
            SendMessage(FindWindowByCaption(IntPtr.Zero, "WO Mic Client"), 0x0010, 0, 0);
            Close();
        }

        async void FindWO()
        {
            await Task.Run(() =>
            {
                FindWindowByCaption(IntPtr.Zero, "WO Mic Client");
                SendMessage(FindWindowByCaption(IntPtr.Zero, "WO Mic Client"), 0x0111, 1000, 1000);
            });
        }

        async void Connect()
        {
            await Task.Run(async () =>
            {
                SendMessage(FindWindowEx(FindWindowByCaption(IntPtr.Zero, "Connect"), IntPtr.Zero, "Button", "&OK"), 0x0203, 0, 0);
                SendMessage(FindWindowEx(FindWindowByCaption(IntPtr.Zero, "Connect"), IntPtr.Zero, "Button", "&OK"), 0x0202, 0, 0);
                await Task.Delay(1000);
            });
        }

    }
}
