using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using iMobileDevice;

namespace Position
{
	// Token: 0x02000002 RID: 2
	[ComVisible(true)]
	public partial class Main : Form
	{
		// Token: 0x06000001 RID: 1
		public Main()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000002 RID: 2
		private void frmMain_Load(object sender, EventArgs e)
		{
			NativeLibraries.Load();
			this.service = LocationService.GetInstance();
			this.service.PrintMessageEvent = new Action<string>(this.PrintMessage);
			this.service.ListeningDevice();
			Cef.Initialize(new CefSettings
			{
				PersistSessionCookies = true,
				CachePath = "cache"
			});

            //
            string path = Application.StartupPath;
            path = path + "\\pos.ini";
            INIFile file = new INIFile(path);
            string addr = file.Read("Position", "url", "http://www.163.com/");

            //
			this.m_chromeBrowser = new ChromiumWebBrowser(addr);
			this.panel1.Controls.Add(this.m_chromeBrowser);
			this.m_chromeBrowser.RegisterJsObject("ckPos", this, true);
		}

		// Token: 0x06000003 RID: 3
		public void PrintMessage(string msg)
		{
			if (this.rtbxLog.InvokeRequired)
			{
				base.Invoke(new Action<string>(this.PrintMessage), new object[]
				{
					msg
				});
				return;
			}
			this.rtbxLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + "：\r\n" + msg + "\r\n");
		}

		// Token: 0x06000004 RID: 4
		private void frmMain_Paint(object sender, PaintEventArgs e)
		{
		}

		// Token: 0x06000005 RID: 5
		private void Main_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6
		// (set) Token: 0x06000007 RID: 7
		public Location Location1 { get; set; } = new Location();

		// Token: 0x06000008 RID: 8
		public void savePos(string lat, string lng, string b_0)
		{
			this.Location1.Longitude = double.Parse(lng);
			this.Location1.Latitude = double.Parse(lat);
			this.service.UpdateLocation(this.Location1);
		}

		// Token: 0x06000009 RID: 9
		public void resetPos()
		{
			this.service.ClearLocation();
		}

		// Token: 0x0600000A RID: 10
		private void clear_Click(object sender, EventArgs e)
		{
			this.service.ClearLocation();
		}

		// Token: 0x0600000B RID: 11
		private void reload_Click(object sender, EventArgs e)
		{
			this.m_chromeBrowser.Reload();
		}

		// Token: 0x04000001 RID: 1
		private Form frm;

		// Token: 0x04000002 RID: 2
		private bool _Right;

		// Token: 0x04000003 RID: 3
		private LocationService service;

		// Token: 0x04000004 RID: 4
		private ChromiumWebBrowser m_chromeBrowser;

		// Token: 0x0200000B RID: 11
		public class JsCallback
		{
			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600003E RID: 62
			// (set) Token: 0x0600003F RID: 63
			private Form ContainerForm { get; set; }

			// Token: 0x06000040 RID: 64
			public JsCallback(Form containerForm)
			{
				this.ContainerForm = containerForm;
			}

			// Token: 0x06000041 RID: 65
			public void minWin()
			{
				this.ContainerForm.WindowState = FormWindowState.Minimized;
			}
		}
	}
}
