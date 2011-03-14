using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFGregorianDate
    {
        internal IntPtr theGregorianDate;

        public CFGregorianDate() { }
        public CFGregorianDate(IntPtr Number){theGregorianDate = Number;}
    }
}
