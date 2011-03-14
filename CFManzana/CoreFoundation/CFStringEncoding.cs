using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFoundation
{
    public class CFStringEncoding
    {
        internal IntPtr theEncoding;
        public CFStringEncoding(IntPtr encoding){this.theEncoding = encoding;}
        public CFStringEncoding(){}
    }
}
