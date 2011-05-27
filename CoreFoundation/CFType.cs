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

namespace CoreFoundation
{
    public class CFType
    {
        public const int _CFString=7;
        public const int _CFDictionary=17;
        public const int _CFArray=18;
        public const int _CFData=19;
        public const int _CFBoolean=21;
        public const int _CFNumber=22;
        public const int _CFError=27;
        public const int _CFUUID=31;
        
        internal IntPtr typeRef;

        public CFType() { }
        
        public CFType(IntPtr handle){this.typeRef = handle;}
        
        /// <summary>
        /// Returns the unique identifier of an opaque type to which a CoreFoundation object belongs
        /// </summary>
        /// <returns></returns>
        public int GetTypeID()
        {
            return CFLibrary.CFGetTypeID(typeRef);
        }
        /// <summary>
        /// Returns a textual description of a CoreFoundation object
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return new CFString(CFLibrary.CFCopyDescription(typeRef)).ToString();
        }

        /// <summary>
        /// Returns the CFObject type as a string
        /// </summary>
        /// <returns></returns>
        
        public string CFObjectType()
        {            
            switch (CFLibrary.CFGetTypeID(typeRef))
            {
                case _CFString:
                    return "CFString";
                case _CFDictionary:
                    return "CFDictionary";
                case _CFArray:
                    return "CFArray";
                case _CFData:
                    return "CFData";
                case _CFBoolean:
                    return "CFBoolean";
                case _CFNumber:
                    return "CFNumber";
                case _CFError:
                    return "CFError";
                case _CFUUID:
                    return "CFUUID";
            }
            return String.Empty;  
        }

        static public string extractCFDictionary(string plist)
        {
            string ret = "\0";
            int end = plist.IndexOf(@"<plist version=" + (char)(34) + "1.0" + (char)(34) + ">") + 21;
            ret = plist.Remove(0, end);
            ret = ret.Remove(ret.IndexOf(@"</plist>"));
            return ret;
        }

        public override string ToString()
        {
            switch (CFLibrary.CFGetTypeID(typeRef))
            {
                case _CFString:
                    return new CFString(typeRef).ToString();
                case _CFDictionary:
                    return new CFDictionary(typeRef).ToString();
                case _CFArray:
                    return new CFArray(typeRef).getCount().ToString();
                case _CFData:
                    return "CFData";
                case _CFBoolean:
                    return new CFBoolean(typeRef).ToBoolean().ToString();
                case _CFNumber:
                    return new CFNumber(typeRef).ToString();
                case _CFError:
                    return "CFError";
                case _CFUUID:
                    return "CFUUID";
            }
            return null;
        }
    }
}
