using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFDate
    {
        internal IntPtr theDate;

        public CFDate() { }
        public CFDate(IntPtr Number){theDate = Number;}
    }
}
