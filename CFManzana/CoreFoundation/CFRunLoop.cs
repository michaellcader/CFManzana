using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFRunLoop
    {
        internal IntPtr theRunLoop;

        public CFRunLoop() { }
        public CFRunLoop(IntPtr Number){theRunLoop = Number;}
    }
}
