using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace CoreFoundation
{
    public class CFBoolean
    {
        internal IntPtr theBoolean;

        public CFBoolean() { }
        public CFBoolean(IntPtr Number){theBoolean = Number;}

        /// <summary>
        /// Returns the value of a CFBoolean object as a standard C type Boolean
        /// </summary>
        /// <returns></returns>
        public bool ToBoolean()
        {
            return CFLibrary.CFBooleanGetValue(theBoolean);
        }
    }
}
