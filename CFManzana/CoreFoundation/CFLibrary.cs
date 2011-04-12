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
using CFURLRef = System.IntPtr;
#endregion
namespace CoreFoundation
{
    public class CFLibrary
    {
        #region CFArray

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFArrayGetCount(CFArrayRef theArray);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFArrayRef CFArrayGetValueAtIndex(CFArrayRef theArray, int index);
        #endregion
        #region CFBoolean

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFBooleanGetValue(CFBooleanRef theBoolean);
        #endregion
        #region CFData

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFDataGetLength(CFDataRef theData);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFDataRef CFDataGetBytePtr(CFDataRef theData);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void CFDataGetBytes(CFDataRef theData, CFRange range, IntPtr buffer);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFDataRef CFDataCreate(IntPtr theAllocator, IntPtr bytes, int bytelength);
        #endregion
        #region CFDictionary

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFDictionaryRef CFDictionaryGetValue(CFDictionaryRef theDict, IntPtr key);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFDictionaryGetCount(CFDictionaryRef theDict);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFDictionaryRef CFDictionaryGetKeysAndValues(CFDictionaryRef theDict, IntPtr[] keys, IntPtr[] values);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFDictionaryRef CFDictionaryCreate(IntPtr allocator, IntPtr[] keys, IntPtr[] values, int numValues, IntPtr kcall, IntPtr vcall);
        #endregion
        #region CFNumber

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFNumberRef CFNumberCreate(IntPtr theAllocator, CFNumber.CFNumberType theType, int* valuePtr);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFNumberGetValue(CFNumberRef theNumber, IntPtr pint, IntPtr valuePtr);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFNumberRef CFNumberGetType(CFNumberRef theNumber);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFNumberGetByteSize(CFNumberRef theNumber);
        #endregion
        #region CFPropertyList

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFPropertyListIsValid(CFPropertyListRef theList, IntPtr theFormat);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFPropertyListRef CFPropertyListCreateXMLData(CFPropertyListRef theAllocator, IntPtr theList);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFPropertyListRef CFPropertyListCreateFromXMLData(CFPropertyListRef theAllocator, IntPtr xmlData,IntPtr mutability,IntPtr errorString);
        #endregion
        #region CFString

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFStringRef __CFStringMakeConstantString(string cstring);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFStringGetLength(CFStringRef handle);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFStringRef CFStringGetCharactersPtr(CFStringRef handle);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFStringRef CFStringGetCharacters(CFStringRef handle, CFRange range, IntPtr buffer);
        #endregion
        #region CFType
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFGetTypeID(CFTypeRef typeRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]        
        public static unsafe extern CFStringRef CFCopyDescription(CFTypeRef typeRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern CFStringRef CFCopyTypeIDDescription(int typeID);
        #endregion        
    }
}
