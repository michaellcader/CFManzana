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
using System.Runtime.InteropServices;
using Plist;
using System.Xml;

namespace CoreFoundation
{
    public class CFDictionary 
    {
        internal IntPtr theDict;
        public CFDictionary() { }

        public CFDictionary(IntPtr dictionary){this.theDict = dictionary;}

        unsafe public CFDictionary(string[] keys,IntPtr[] values)
        {
            IntPtr[] keyz = new IntPtr[keys.Length];            
            int i;
            for (i = 0; i < keys.Length; i++)
            {
                keyz[i] = new CFString(keys[i]).ToIntPtr();                
            }
                        
            theDict = CFLibrary.CFDictionaryCreate(IntPtr.Zero,keyz,values,keys.Length,IntPtr.Zero,IntPtr.Zero);            
        }
       
        public IntPtr getDataValue(string value)
        {            
            return CFLibrary.CFDictionaryGetValue(theDict, new CFString(value).ToIntPtr());            
        }
        /// <summary>
        /// Returns the number of key-value pairs in a dictionary
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return CFLibrary.CFDictionaryGetCount(theDict);            
        }

        /// <summary>
        /// Returns the Keys and Values from the Dictionary as a C-String
        /// </summary>
        public override string ToString()
        {            
            IntPtr[] keys = new IntPtr[Length()];
            IntPtr[] values = new IntPtr[Length()];
            CFLibrary.CFDictionaryGetKeysAndValues(theDict, keys, values);
            string[] keyz = new string[Length()];
            string[] valuez = new string[Length()];            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Length(); i++)
            {                
                keyz[i] = new CFString(keys[i]).ToString();
                sb.Append(keyz[i] + "\n");
                valuez[i] = parseValue(values[i]);                
                sb.Append(valuez[i] + "\n"); 
            }
            return sb.ToString();
        }
        public IntPtr ToIntPtr()
        {
            return this.theDict;
        }
        private string parseValue(IntPtr typeRef)
        {
            if (typeRef == IntPtr.Zero)
                return string.Empty;
            string desc = new CFType(typeRef).CFObjectType();
            switch (desc)
            {
                case "CFArray":
                    return null; //refer to cf tinyumbrella
                case "CFBoolean":
                    return new CFBoolean(typeRef).ToBoolean().ToString();                
                case "CFString":
                    return new CFString(typeRef).ToString();
                case "CFNumber":
                    return new CFNumber(typeRef).ToString();
                case "CFData":
                    return Convert.ToBase64String(new CFData(typeRef).ToByteArray());
                case "CFDictionary":
                    return null;
                case "CFDate":
                    return null;
                default:
                    throw new ArgumentException("Unknown Value:" + desc + ":" + typeRef);

            }            
        }
    }

            
        public delegate IntPtr CFDictionaryRetainCallBack(IntPtr allocator, IntPtr type); 
        public delegate IntPtr CFDictionaryReleaseCallBack(IntPtr allocator, IntPtr type);
        public delegate IntPtr CFDictionaryCopyDescriptionCallBack(IntPtr type);
        public delegate IntPtr CFDictionaryEqualCallBack(IntPtr type1,IntPtr type2);
        public delegate int CFDictionaryHashCallBack(IntPtr type);
        public struct CFDictionaryKeyCallBacks
        {
            CFIndex version;
            CFDictionaryRetainCallBack retain;
            CFDictionaryReleaseCallBack release;
            CFDictionaryCopyDescriptionCallBack copyDescription;
            CFDictionaryEqualCallBack equal;
            CFDictionaryHashCallBack hash;
        };

        public struct CFDictionaryValueCallBacks
        {
            CFIndex version;
            CFDictionaryRetainCallBack retain;
            CFDictionaryReleaseCallBack release;
            CFDictionaryCopyDescriptionCallBack copyDescription;
            CFDictionaryEqualCallBack equal;
        };

        

    
}
