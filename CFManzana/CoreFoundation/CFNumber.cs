using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFNumber
    {
        internal IntPtr theNumber;

        public CFNumber() { }
        public CFNumber(IntPtr Number){theNumber = Number;}
        unsafe public CFNumber(int Number) 
        {
            int* pNumber=&Number;
            theNumber = CFLibrary.CFNumberCreate(IntPtr.Zero, CFNumberType.kCFNumberIntType, pNumber);
        }       
        public enum CFNumberType 
        { 
            kCFNumberSInt8Type = 1, 
            kCFNumberSInt16Type = 2, 
            kCFNumberSInt32Type = 3, 
            kCFNumberSInt64Type = 4, 
            kCFNumberFloat32Type = 5, 
            kCFNumberFloat64Type = 6, 
            kCFNumberCharType = 7, 
            kCFNumberShortType = 8, 
            kCFNumberIntType = 9, 
            kCFNumberLongType = 10, 
            kCFNumberLongLongType = 11, 
            kCFNumberFloatType = 12, 
            kCFNumberDoubleType = 13, 
            kCFNumberCFIndexType = 14, 
            kCFNumberNSIntegerType = 15,            
            kCFNumberCGFloatType = 16, 
            kCFNumberMaxType = 16 
        };
        public IntPtr ToIntPtr()
        {
            return theNumber;
        }
        /// <summary>
        /// Obtains the value of a CFNumber object as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {            
            IntPtr buffer = Marshal.AllocCoTaskMem(CFLibrary.CFNumberGetByteSize(theNumber));
            bool scs = CFLibrary.CFNumberGetValue(theNumber, CFLibrary.CFNumberGetType(theNumber), buffer);
            if (scs != true)
            {
                return string.Empty;
            }
            int type = (int)CFLibrary.CFNumberGetType(theNumber);            
            switch(type)
            {
                case 1:
                    return Marshal.ReadInt16(theNumber).ToString();
                case 2:
                    return Marshal.ReadInt16(theNumber).ToString();
                case 3:
                    return Marshal.ReadInt32(theNumber).ToString();
                case 4:
                    return Marshal.ReadInt64(theNumber).ToString();
                default:
                    return Enum.GetName(typeof(CFNumberType),type) + " is not supported yet!";
            }            
        }
    }
}
