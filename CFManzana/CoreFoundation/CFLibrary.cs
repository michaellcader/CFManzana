#region decl
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using CFStringRef = System.IntPtr;
using CFDictionaryRef = System.IntPtr;
using CFTypeRef = System.IntPtr;
using CFDataRef = System.IntPtr;
using CFPropertyListRef = System.IntPtr;
using CFArrayRef = System.IntPtr;
using CFBooleanRef = System.IntPtr;
using CFNumberRef = System.IntPtr;
using CFStringEncodingRef = System.IntPtr;
#endregion
namespace CoreFoundation
{
    public class CFLibrary
    {
        #region CFType
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFGetTypeID(CFTypeRef typeRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]        
        public static unsafe extern CFStringRef CFCopyDescription(CFTypeRef typeRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFStringRef CFCopyTypeIDDescription(int typeID);
        #endregion
        #region CFString
        
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr __CFStringMakeConstantString(string cstring);
        
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFStringGetLength(CFStringRef handle);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFStringGetCharactersPtr(CFStringRef handle);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFStringGetCharacters(CFStringRef handle,CFRange range, IntPtr buffer);
        #endregion
        #region CFData
        
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFDataGetLength(CFDataRef theData);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFDataGetBytePtr(CFDataRef theData);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void CFDataGetBytes(CFDataRef theData,CFRange range,IntPtr buffer);
        #endregion
        #region CFDictionary
        
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFDictionaryGetValue(CFDictionaryRef theDict, IntPtr key);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFDictionaryGetCount(CFDictionaryRef theDict);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFDictionaryGetKeysAndValues(CFDictionaryRef theDict,IntPtr[] keys,IntPtr[] values);
        #endregion
        #region CFPropertyList
        
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFPropertyListIsValid(CFPropertyListRef theList,IntPtr theFormat);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern string CFPropertyListCreateXMLData(CFPropertyListRef theAllocator,IntPtr theList);
        #endregion
        #region CFNumber
        
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFNumberGetValue(CFNumberRef theNumber,IntPtr pint, IntPtr valuePtr);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFNumberGetType(CFNumberRef theNumber);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFNumberGetByteSize(CFNumberRef theNumber);
        #endregion
        #region CFBoolean
        
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFBooleanGetValue(CFBooleanRef theBoolean);
        #endregion
        #region CFArray
             
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFArrayGetCount(CFArrayRef theArray);
        #endregion
    }
}
