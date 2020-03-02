using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CarScraping
{
    internal static class NativeMethods
    {
        public static Int32 STD_OUTPUT_HANDLE = -11;

        /// Return Type: HANDLE->void*
        ///nStdHandle: DWORD->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetStdHandle")]
        public static extern System.IntPtr GetStdHandle(Int32 nStdHandle);

        /// Return Type: BOOL->int
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "AllocConsole")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool AllocConsole();

    }
}
