using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFError
    {
        internal IntPtr theError;

        public CFError() { }
        public CFError(IntPtr Number){theError = Number;}
    }
}
