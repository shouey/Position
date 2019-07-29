using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Position.Properties
{
	// Token: 0x02000009 RID: 9
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002D8A File Offset: 0x00000F8A
		internal Resources()
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002D94 File Offset: 0x00000F94
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager temp = new ResourceManager("Position.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002DDC File Offset: 0x00000FDC
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002DF3 File Offset: 0x00000FF3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000021 RID: 33
		private static ResourceManager resourceMan;

		// Token: 0x04000022 RID: 34
		private static CultureInfo resourceCulture;
	}
}
