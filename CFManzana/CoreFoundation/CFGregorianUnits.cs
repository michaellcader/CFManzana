using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFGregorianUnits
    {
        internal IntPtr theGregorianUnits;

        public CFGregorianUnits() { }
        public CFGregorianUnits(IntPtr Number){theGregorianUnits = Number;}
    }
}
