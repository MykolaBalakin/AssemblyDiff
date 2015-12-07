using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.AssemblyDiff;

namespace ConsoleTest {
    class Program {
        static void Main(string[] args) {
            var diff = AssemblyDiff.Calculate(@"C:\Sources\GitHub\VSOutputEnhancer\2013_Balakin.VSOutputEnhancer.dll", @"C:\Sources\GitHub\VSOutputEnhancer\2015_Balakin.VSOutputEnhancer.dll");
            Debugger.Break();
        }
    }
}
