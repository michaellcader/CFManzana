// Software License Agreement (BSD License)
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
        public static unsafe extern CFDictionaryRef CFDictionaryCreate(IntPtr allocator, IntPtr[] keys, IntPtr[] values, int numValues, ref CFDictionaryKeyCallBacks kcall, ref CFDictionaryValueCallBacks vcall);
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
