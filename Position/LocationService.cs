using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using iMobileDevice;
using iMobileDevice.iDevice;
using iMobileDevice.Lockdown;
using iMobileDevice.Plist;
using iMobileDevice.Service;

namespace Position
{
	// Token: 0x02000005 RID: 5
	public class LocationService
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000266C File Offset: 0x0000086C
		private LocationService()
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026C3 File Offset: 0x000008C3
		public static LocationService GetInstance()
		{
			LocationService result;
			if ((result = LocationService._instance) == null)
			{
				result = (LocationService._instance = new LocationService());
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026DC File Offset: 0x000008DC
		public void ListeningDevice()
		{
			int num = 0;
			ReadOnlyCollection<string> devices;
			iDeviceError deviceError = this.iDevice.idevice_get_device_list(out devices, ref num);
			bool flag = deviceError > iDeviceError.Success;
			if (flag)
			{
				this.PrintMessage("无法继续.可能本工具权限不足, 或者未正确安装iTunes工具.");
			}
			else
			{
				Action<DeviceModel> <>9__1;
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					for (;;)
					{
						List<DeviceModel> devices;
						deviceError = this.iDevice.idevice_get_device_list(out devices, ref num);
						bool flag2 = devices.Count > 0;
						if (flag2)
						{
							List<string> lst = (from s in this.Devices
							select s.UDID).ToList<string>().Except(devices).ToList<string>();
							List<string> dst = devices.Except(from s in this.Devices
							select s.UDID).ToList<string>();
							foreach (string udid in dst)
							{
								iDeviceHandle iDeviceHandle;
								this.iDevice.idevice_new(out iDeviceHandle, udid).ThrowOnError();
								LockdownClientHandle lockdownClientHandle;
								this.lockdown.lockdownd_client_new_with_handshake(iDeviceHandle, out lockdownClientHandle, "Quamotion").ThrowOnError("无法读取设备Quamotion");
								string deviceName;
								this.lockdown.lockdownd_get_device_name(lockdownClientHandle, out deviceName).ThrowOnError("获取设备名称失败.");
								this.lockdown.lockdownd_client_new_with_handshake(iDeviceHandle, out lockdownClientHandle, "waua").ThrowOnError("无法读取设备waua");
								PlistHandle node;
								this.lockdown.lockdownd_get_value(lockdownClientHandle, null, "ProductVersion", out node).ThrowOnError("获取设备系统版本失败.");
								string version;
								LibiMobileDevice.Instance.Plist.plist_get_string_val(node, out version);
								iDeviceHandle.Dispose();
								lockdownClientHandle.Dispose();
								DeviceModel device = new DeviceModel
								{
									UDID = udid,
									Name = deviceName,
									Version = version
								};
								this.PrintMessage("发现设备: " + deviceName + "  " + version);
								this.LoadDevelopmentTool(device);
								this.Devices.Add(device);
							}
						}
						else
						{
							devices = this.Devices;
							Action<DeviceModel> action;
							if ((action = <>9__1) == null)
							{
								action = (<>9__1 = delegate(DeviceModel itm)
								{
									this.PrintMessage(string.Concat(new string[]
									{
										"设备 ",
										itm.Name,
										" ",
										itm.Version,
										" 已断开连接."
									}));
								});
							}
							devices.ForEach(action);
							this.Devices.Clear();
						}
						Thread.Sleep(1000);
					}
				});
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000274C File Offset: 0x0000094C
		public bool GetDevice()
		{
			this.Devices.Clear();
			int num = 0;
			ReadOnlyCollection<string> readOnlyCollection;
			iDeviceError iDeviceError = this.iDevice.idevice_get_device_list(out readOnlyCollection, ref num);
			bool flag = iDeviceError == iDeviceError.NoDevice;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				iDeviceError.ThrowOnError();
				foreach (string udid in readOnlyCollection)
				{
					iDeviceHandle iDeviceHandle;
					this.iDevice.idevice_new(out iDeviceHandle, udid).ThrowOnError();
					LockdownClientHandle lockdownClientHandle;
					this.lockdown.lockdownd_client_new_with_handshake(iDeviceHandle, out lockdownClientHandle, "Quamotion").ThrowOnError();
					string deviceName;
					this.lockdown.lockdownd_get_device_name(lockdownClientHandle, out deviceName).ThrowOnError();
					string version = "";
					PlistHandle node;
					bool flag2 = this.lockdown.lockdownd_client_new_with_handshake(iDeviceHandle, out lockdownClientHandle, "waua") == LockdownError.Success && this.lockdown.lockdownd_get_value(lockdownClientHandle, null, "ProductVersion", out node) == LockdownError.Success;
					if (flag2)
					{
						LibiMobileDevice.Instance.Plist.plist_get_string_val(node, out version);
					}
					iDeviceHandle.Dispose();
					lockdownClientHandle.Dispose();
					DeviceModel device = new DeviceModel
					{
						UDID = udid,
						Name = deviceName,
						Version = version
					};
					this.PrintMessage(string.Concat(new string[]
					{
						"发现设备: ",
						deviceName,
						"  ",
						version,
						"  ",
						udid
					}));
					this.LoadDevelopmentTool(device);
					this.Devices.Add(device);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002904 File Offset: 0x00000B04
		public void LoadDevelopmentTool(DeviceModel device)
		{
			string shortVersion = string.Join(".", device.Version.Split(new char[]
			{
				'.'
			}).Take(2));
			this.PrintMessage("为设备加载驱动版本 " + shortVersion + "。");
			string basePath = AppDomain.CurrentDomain.BaseDirectory + "/drivers/";
			bool flag = !File.Exists(basePath + shortVersion + "/inject.dmg");
			if (flag)
			{
				this.PrintMessage("未找到 " + shortVersion + " 驱动版本,请下载驱动后重新加载设备。");
				this.PrintMessage("请前往：https://pan.baidu.com/s/1MPUTYJTdv7yXEtE8nMIRbQ 提取码：p9ep 按版本自行下载后放入drivers目录。");
			}
			else
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = "injecttool",
					UseShellExecute = false,
					RedirectStandardOutput = false,
					CreateNoWindow = true,
					Arguments = ".\\drivers\\" + shortVersion + "\\inject.dmg"
				}).WaitForExit();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000029F0 File Offset: 0x00000BF0
		public void UpdateLocation(Location location)
		{
			bool flag = this.Devices.Count == 0;
			if (flag)
			{
				this.PrintMessage("修改失败! 未发现任何设备。");
			}
			else
			{
				this.iDevice.idevice_set_debug_level(1);
				string Longitude = location.Longitude.ToString();
				string Latitude = location.Latitude.ToString();
				this.PrintMessage("尝试修改位置。");
				this.PrintMessage(string.Format("经度:{0}", location.Longitude));
				this.PrintMessage(string.Format("纬度:{0}", location.Latitude));
				byte[] size = BitConverter.GetBytes(0u);
				Array.Reverse(size);
				this.Devices.ForEach(delegate(DeviceModel itm)
				{
					this.PrintMessage("开始修改设备 " + itm.Name + " " + itm.Version);
					uint num = 0u;
					iDeviceHandle device;
					this.iDevice.idevice_new(out device, itm.UDID);
					LockdownClientHandle client;
					this.lockdown.lockdownd_client_new_with_handshake(device, out client, "com.alpha.jailout").ThrowOnError();
					LockdownServiceDescriptorHandle service2;
					this.lockdown.lockdownd_start_service(client, "com.apple.dt.simulatelocation", out service2).ThrowOnError();
					ServiceClientHandle client2;
					ServiceError se = this.service.service_client_new(device, service2, out client2);
					se = this.service.service_send(client2, size, 4u, ref num);
					num = 0u;
					byte[] bytesLocation = Encoding.ASCII.GetBytes(Latitude);
					size = BitConverter.GetBytes((uint)Latitude.Length);
					Array.Reverse(size);
					se = this.service.service_send(client2, size, 4u, ref num);
					se = this.service.service_send(client2, bytesLocation, (uint)bytesLocation.Length, ref num);
					bytesLocation = Encoding.ASCII.GetBytes(Longitude);
					size = BitConverter.GetBytes((uint)Longitude.Length);
					Array.Reverse(size);
					se = this.service.service_send(client2, size, 4u, ref num);
					se = this.service.service_send(client2, bytesLocation, (uint)bytesLocation.Length, ref num);
					this.PrintMessage(string.Concat(new string[]
					{
						"设备 ",
						itm.Name,
						" ",
						itm.Version,
						" 位置修改完成。"
					}));
				});
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public void ClearLocation()
		{
			bool flag = this.Devices.Count == 0;
			if (flag)
			{
				this.PrintMessage("修改失败! 未发现任何设备.");
			}
			else
			{
				this.iDevice.idevice_set_debug_level(1);
				this.PrintMessage("发起还原位置.");
				this.Devices.ForEach(delegate(DeviceModel itm)
				{
					this.PrintMessage("开始还原设备 " + itm.Name + " " + itm.Version);
					uint num = 0u;
					iDeviceHandle device;
					this.iDevice.idevice_new(out device, itm.UDID);
					LockdownClientHandle client;
					LockdownError lockdowndError = this.lockdown.lockdownd_client_new_with_handshake(device, out client, "com.alpha.jailout");
					LockdownServiceDescriptorHandle service2;
					lockdowndError = this.lockdown.lockdownd_start_service(client, "com.apple.dt.simulatelocation", out service2);
					ServiceClientHandle client2;
					ServiceError se = this.service.service_client_new(device, service2, out client2);
					se = this.service.service_send(client2, new byte[4], 4u, ref num);
					se = this.service.service_send(client2, new byte[]
					{
						0,
						0,
						0,
						1
					}, 4u, ref num);
					device.Dispose();
					client.Dispose();
					this.PrintMessage(string.Concat(new string[]
					{
						"设备 ",
						itm.Name,
						" ",
						itm.Version,
						" 还原成功."
					}));
				});
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B38 File Offset: 0x00000D38
		public void PrintMessage(string msg)
		{
			Action<string> printMessageEvent = this.PrintMessageEvent;
			if (printMessageEvent != null)
			{
				printMessageEvent(msg);
			}
		}

		// Token: 0x04000012 RID: 18
		private bool d_r_0;

		// Token: 0x04000013 RID: 19
		private List<DeviceModel> Devices = new List<DeviceModel>();

		// Token: 0x04000014 RID: 20
		private IiDeviceApi iDevice = LibiMobileDevice.Instance.iDevice;

		// Token: 0x04000015 RID: 21
		private ILockdownApi lockdown = LibiMobileDevice.Instance.Lockdown;

		// Token: 0x04000016 RID: 22
		private IServiceApi service = LibiMobileDevice.Instance.Service;

		// Token: 0x04000017 RID: 23
		private static LocationService _instance;

		// Token: 0x04000018 RID: 24
		public Action<string> PrintMessageEvent = null;

		// Token: 0x04000019 RID: 25
		private string u_0;
	}
}
