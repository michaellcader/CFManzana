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

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace CoreFoundation
{
   
    public class CFString 
    {       
        internal IntPtr theString;        
        
        public CFString()
        {
            
        }       
        private void getcha(IntPtr CFThing)
        {
            theString = CFThing;
        }
        /// <summary>
        /// Creates an immutable string from a constant compile-time string
        /// </summary>
        /// <param name="str"></param>
        unsafe public CFString(string str)
        {
            theString = CFLibrary.__CFStringMakeConstantString(str);           
        }

        public CFString(IntPtr myHandle) 
        { 
            this.theString = myHandle; 
        }
        /// <summary>
        /// Returns the CFString object as a System.IntPtr object
        /// </summary>
        /// <returns></returns>
        public IntPtr ToIntPtr()
        {
            return theString;
        }
        /// <summary>
        /// Converts the CFString object to a C String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            

            if (theString == IntPtr.Zero)
                return null;
            
            string str;
            int length = CFLibrary.CFStringGetLength(theString);        
            IntPtr u = CFLibrary.CFStringGetCharactersPtr(theString);
            IntPtr buffer = IntPtr.Zero;
            if (u == IntPtr.Zero)
            {
                CFRange range = new CFRange(0, length);
                buffer = Marshal.AllocCoTaskMem(length * 2);
                CFLibrary.CFStringGetCharacters(theString, range, buffer);
                u = buffer;
            }
            unsafe
            {
               str = new string((char*)u, 0, length);
            }
            if (buffer != IntPtr.Zero)
                Marshal.FreeCoTaskMem(buffer);
            return str;
        }

        /// <summary>
        /// Checks if the object is a valid CFString object
        /// </summary>
        /// <returns></returns>
        public bool isCFString()
        {
            return CFLibrary.CFGetTypeID(theString) == CFType._CFString;                     
        }

        public static implicit operator CFString(IntPtr value)
        {
            return new CFString(value);
        }

        public static implicit operator IntPtr(CFString value)
        {
            return value.theString;
        }

        public static implicit operator string(CFString value)
        {
            return value.ToString();
        }

        public static implicit operator CFString(string value)
        {            
            return new CFString(value);
        }
    }         
}
