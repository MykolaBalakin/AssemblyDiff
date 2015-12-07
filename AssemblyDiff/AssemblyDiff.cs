using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class AssemblyDiff : DiffBase {
        public static AssemblyDiff Calculate(String assembly1Path, String assembly2Path) {
            return Calculate(AssemblyDefinition.ReadAssembly(assembly1Path), AssemblyDefinition.ReadAssembly(assembly2Path));
        }

        public static AssemblyDiff Calculate(AssemblyDefinition assembly1, AssemblyDefinition assembly2) {
            var children = new List<IDiff>();
            children.Add(CustomAttributesDiff.Calculate(assembly1.CustomAttributes, assembly2.CustomAttributes));
            return new AssemblyDiff(children);
        }


        protected AssemblyDiff(Different different)
            : base(different) {
        }

        protected AssemblyDiff(IEnumerable<IDiff> children)
            : base(children) {
        }
    }
}
