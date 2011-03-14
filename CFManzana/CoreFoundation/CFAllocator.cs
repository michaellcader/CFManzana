using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFAllocator
    {
        internal IntPtr theAllocator;

        public CFAllocator() { }
        public CFAllocator(IntPtr Number){theAllocator = Number;}
    }
}
