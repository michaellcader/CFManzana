using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace CoreFoundation
{
   
    public class CFString 
    {       
        internal IntPtr theString;        
        
        public CFString()
        {
            
        }       
        private void getcha(IntPtr CFThing)
        {
            theString = CFThing;
        }
        /// <summary>
        /// Creates an immutable string from a constant compile-time string
        /// </summary>
        /// <param name="str"></param>
        unsafe public CFString(string str)
        {
            theString = CFLibrary.__CFStringMakeConstantString(str);           
        }

        public CFString(IntPtr myHandle) 
        { 
            this.theString = myHandle; 
        }
        /// <summary>
        /// Returns the CFString object as a System.IntPtr object
        /// </summary>
        /// <returns></returns>
        public IntPtr ToIntPtr()
        {
            return theString;
        }
        /// <summary>
        /// Converts the CFString object to a C String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            

            if (theString == IntPtr.Zero)
                return null;
            
            string str;
            int length = CFLibrary.CFStringGetLength(theString);        
            IntPtr u = CFLibrary.CFStringGetCharactersPtr(theString);
            IntPtr buffer = IntPtr.Zero;
            if (u == IntPtr.Zero)
            {
                CFRange range = new CFRange(0, length);
                buffer = Marshal.AllocCoTaskMem(length * 2);
                CFLibrary.CFStringGetCharacters(theString, range, buffer);
                u = buffer;
            }
            unsafe
            {
               str = new string((char*)u, 0, length);
            }
            if (buffer != IntPtr.Zero)
                Marshal.FreeCoTaskMem(buffer);
            return str;
        }

        /// <summary>
        /// Checks if the object is a valid CFString object
        /// </summary>
        /// <returns></returns>
        public bool isCFString()
        {
            return CFLibrary.CFGetTypeID(theString) == CFType._CFString;                     
        }
          
    }
         
}
