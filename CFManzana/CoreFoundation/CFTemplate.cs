using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFTemplate
    {
        internal IntPtr theTemplate;

        public CFTemplate() { }
        public CFTemplate(IntPtr Number){theTemplate = Number;}
    }
}
