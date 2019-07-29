using System;

namespace Position
{
	// Token: 0x02000007 RID: 7
	public class Location
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002C8A File Offset: 0x00000E8A
		public Location()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C94 File Offset: 0x00000E94
		public Location(double lo, double la)
		{
			this.Longitude = lo;
			this.Latitude = la;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public Location(string location)
		{
			string[] arry = location.Split(new char[]
			{
				','
			});
			this.Longitude = double.Parse(arry[0]);
			this.Latitude = double.Parse(arry[1]);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002CF5 File Offset: 0x00000EF5
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002CFD File Offset: 0x00000EFD
		public double Longitude { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002D06 File Offset: 0x00000F06
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002D0E File Offset: 0x00000F0E
		public double Latitude { get; set; }
	}
}
