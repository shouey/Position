using System;
using System.Windows.Forms;

namespace Position
{
	// Token: 0x02000008 RID: 8
	internal static class Program
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002D18 File Offset: 0x00000F18
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Application.Run(new Main());
			}
			catch (Exception ex)
			{
				MessageBox.Show("软件发生异常! " + ex.Message);
			}
		}

		// Token: 0x0400001F RID: 31
		public static string User = "";

		// Token: 0x04000020 RID: 32
		public static double ver = 2.0;
	}
}
