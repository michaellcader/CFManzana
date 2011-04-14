/// Software License Agreement (BSD License)
// 
// Copyright (c) 2011, iSn0wra1n <isn0wra1ne@gmail.com>
// All rights reserved.
// 
// Redistribution and use of this software in source and binary forms, with or without modification are
// permitted without the copyright owner's permission provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above
//   copyright notice, this list of conditions and the
//   following disclaimer.
// 
// * Redistributions in binary form must reproduce the above
//   copyright notice, this list of conditions and the
//   following disclaimer in the documentation and/or other
//   materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR
// TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using CoreFoundation;
using CFNumberRef = CoreFoundation.CFNumber;

namespace Manzana {
	internal enum AppleMobileErrors
	{

	}

	/// <summary>
	/// Provides the fields representing the type of notification
	/// </summary>
	public enum NotificationMessage {
		/// <summary>The iPhone was connected to the computer.</summary>
		Connected		= 1,
		/// <summary>The iPhone was disconnected from the computer.</summary>
		Disconnected	= 2,

		/// <summary>Notification from the iPhone occurred, but the type is unknown.</summary>
		Unknown			= 3,
	}

	/// <summary>
	/// Structure describing the iPhone - no longer used
	/// </summary>
	/// Just opaque block of memory - give a decent chunk
	/// 
#if false
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	public struct AMDevice {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
		internal byte[]		unknown0;		/* 0 - zero */
		internal uint		device_id;		/* 16 */
		internal uint		product_id;		/* 20 - set to AMD_IPHONE_PRODUCT_ID */
		/// <summary>Write Me</summary>
		public string		serial;			/* 24 - set to AMD_IPHONE_SERIAL */
		internal uint		unknown1;		/* 28 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		internal byte[]		unknown2;		/* 32 */
		internal uint		lockdown_conn;	/* 36 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		internal byte[]		unknown3;		/* 40 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=6*16+1)]
		internal byte[]		unknown4;		/* 48  + in iTunes 8.0, by iFunbox.dev */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		internal byte[]		unknown5;		/* 97  + in iTunes 8.0, by iFunbox.dev */
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMDeviceNotification {
		uint						unknown0;	/* 0 */
		uint						unknown1;	/* 4 */
		uint						unknown2;	/* 8 */
		DeviceNotificationCallback	callback;   /* 12 */ 
		uint						unknown3;	/* 16 */
	}
#endif
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMDeviceNotificationCallbackInfo {
		unsafe public void* dev {
			get {
				return dev_ptr;
			}
		}
		unsafe internal void* dev_ptr;
		public NotificationMessage msg;
	}


	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMRecoveryDevice {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public byte[]	unknown0;			/* 0 */
		public DeviceRestoreNotificationCallback	callback;		/* 8 */
		public IntPtr	user_info;			/* 12 */		
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=12)]
		public byte[]	unknown1;			/* 16 */
		public uint		readwrite_pipe;		/* 28 */
		public byte		read_pipe;          /* 32 */
		public byte		write_ctrl_pipe;    /* 33 */
		public byte		read_unknown_pipe;  /* 34 */
		public byte		write_file_pipe;    /* 35 */
		public byte		write_input_pipe;   /* 36 */
	};



	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void DeviceNotificationCallback(ref AMDeviceNotificationCallbackInfo callback_info);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void DeviceRestoreNotificationCallback(ref AMRecoveryDevice callback_info);

    internal class MobileDevice {

        const string DLLName = "iTunesMobileDevice.dll";
		static readonly FileInfo iTunesMobileDeviceFile = new FileInfo(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Mobile Device Support\Shared", "iTunesMobileDeviceDLL", DLLName).ToString());
		static readonly DirectoryInfo ApplicationSupportDirectory = new DirectoryInfo(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Application Support", "InstallDir", Environment.CurrentDirectory).ToString());

        static MobileDevice() 
        {
            // try to find the dll automatically
            string addpath = iTunesMobileDeviceFile.DirectoryName;
            if (!iTunesMobileDeviceFile.Exists) {
                addpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + @"\Apple\Mobile Device Support\bin";
				if (!File.Exists(addpath + @"\" + DLLName))
					addpath = @"C:\Program Files\Apple\Mobile Device Support\bin";
			}
            Environment.SetEnvironmentVariable("Path", string.Join(";", new String[] { Environment.GetEnvironmentVariable("Path"), addpath, ApplicationSupportDirectory.FullName }));
        }       

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceNotificationSubscribe(DeviceNotificationCallback callback, uint unused1, uint unused2, uint unused3, out void* am_device_notification_ptr);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceConnect(void* device);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceDisconnect(void* device);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceIsPaired(void* device);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceValidatePairing(void* device);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceStartSession(void* device);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceStopSession(void* device);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceGetConnectionID(void* device);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMRestoreModeDeviceCreate(uint unknown0, int connection_id, uint unknown1);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCDirectoryOpen(void* conn, byte[] path, ref void* dir);

		unsafe public static int AFCDirectoryOpen(void* conn, string path, ref void* dir) {
			return AFCDirectoryOpen(conn, Encoding.UTF8.GetBytes(path), ref dir);
		}

		unsafe public static int AFCDirectoryRead(void* conn, void* dir, ref string buffer) {
			int ret;

			void* ptr = null;
			ret = AFCDirectoryRead(conn, dir, ref ptr);
			if ((ret == 0) && (ptr != null)) {
				buffer = Marshal.PtrToStringAnsi(new IntPtr(ptr));
			} else {
				buffer = null;
			}
			return ret;
		}
		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCDirectoryRead(void* conn, void* dir, ref void* dirent);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int AMDeviceActivate(void* device,IntPtr wildcard_ticket);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int AMDeviceDeactivate(void* device);
                
		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCDirectoryClose(void* conn, void* dir);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMRestoreRegisterForDeviceNotifications(
			DeviceRestoreNotificationCallback dfu_connect, 
			DeviceRestoreNotificationCallback recovery_connect, 
			DeviceRestoreNotificationCallback dfu_disconnect,
			DeviceRestoreNotificationCallback recovery_disconnect,
			uint unknown0,
			void* user_info);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceStartService(void* device, void* service_name, ref void* handle, void* unknown);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCConnectionOpen(void* handle, uint io_timeout, ref void* conn);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AFCConnectionIsValid(void* conn);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AFCConnectionInvalidate(void* conn);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AFCConnectionClose(void* conn);

		unsafe public static string AMDeviceCopyValue(void* device, string name) {               
            IntPtr result = AMDeviceCopyValue_IntPtr(device, 0, new CFString(name).ToIntPtr());            
            if (result==IntPtr.Zero)
                return name;
            return new CFType(result).ToString();            
		}

        [DllImport(DLLName, EntryPoint = "AMDeviceCopyValue", CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static IntPtr AMDeviceCopyValue_IntPtr(void* device, uint unknown, IntPtr cfstring);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int AFCFileInfoOpen(void* conn, string path, ref void* dict);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AFCKeyValueRead(void* dict, out void* key, out void* val);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AFCKeyValueClose(void* dict);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AFCRemovePath(void* conn, string path);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCRenamePath(void* conn, string old_path, string new_path);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefOpen(void* conn, string path, int mode, int unknown, out Int64 handle);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefClose(void* conn, Int64 handle);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefRead(void* conn, Int64 handle, byte[] buffer, ref uint len);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefWrite(void* conn, Int64 handle, byte[] buffer, uint len);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFlushData(void* conn, Int64 handle);

		// FIXME - not working, arguments? Always returns 7
		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefSeek(void* conn, Int64 handle, uint pos, uint origin);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefTell(void* conn, Int64 handle, ref uint position);

		// FIXME - not working, arguments?
		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefSetFileSize(void* conn, Int64 handle, uint size);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCDirectoryCreate(void* conn, string path);       
        
	}

}
