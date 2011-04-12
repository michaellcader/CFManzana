using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFData  
    {
        internal IntPtr theData;

        public CFData(){}
        public CFData(IntPtr Data){this.theData = Data;}
        unsafe public CFData(byte[] Data)
        {            
            byte[] buffer = Data;            
            int len = buffer.Length;            
            fixed (byte* bytePtr = buffer)
                
                theData = CFLibrary.CFDataCreate(IntPtr.Zero, (IntPtr)bytePtr,len);            
        }

        private int gettheLength(byte[] data)
        {
            int ret = 0;
            for (int i = 0; i <= data.Length; i++)
            {
                ret += (int)data[0];
            }
            return ret;
        }
        /// <summary>
        /// Returns the number of bytes contained by the CFData object
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return CFLibrary.CFDataGetLength(theData);
        }
        /// <summary>
        /// Checks if the object is a valid CFData object
        /// </summary>
        /// <returns></returns>
        public bool isData()
        {
            return CFLibrary.CFGetTypeID(theData) == 19;
        }
        /// <summary>
        /// Returns the CFData object as a byte array
        /// </summary>
        /// <returns></returns>
        unsafe public byte[] ToByteArray()
        {            
            int len = Length();
            byte[] buffer = new byte[len];
            fixed (byte* bufPtr = buffer)
                CFLibrary.CFDataGetBytes(theData, new CFRange(0, len), (IntPtr)bufPtr);
            return buffer;            
        }

        public IntPtr ToIntPtr()
        {
            return theData;
        }
        
    }
}
