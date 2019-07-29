using System;
using System.Collections.Generic;

namespace Position
{
	// Token: 0x02000004 RID: 4
	public class RootObject
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000261D File Offset: 0x0000081D
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002625 File Offset: 0x00000825
		public string status { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000262E File Offset: 0x0000082E
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002636 File Offset: 0x00000836
		public string msg { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000263F File Offset: 0x0000083F
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002647 File Offset: 0x00000847
		public string count { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002650 File Offset: 0x00000850
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002658 File Offset: 0x00000858
		public List<Result> result { get; set; }
	}
}
