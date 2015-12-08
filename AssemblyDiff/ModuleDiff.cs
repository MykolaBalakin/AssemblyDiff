using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class ModuleDiff : DiffBase {
        public static ModuleDiff Calculate(ModuleDefinition module1, ModuleDefinition module2) {
            if (module1 == null && module2 == null) {
                return new ModuleDiff(Different.Same);
            }

            // TODO: Check names

            var children = new List<IDiff>();
            children.Add(CustomAttributesDiff.Calculate(module1?.CustomAttributes, module2?.CustomAttributes));
            children.Add(TypesDiff.Calculate(module1?.Types, module2?.Types));

            return new ModuleDiff(children);
        }

        protected ModuleDiff(Different different) : base(different) {
        }

        protected ModuleDiff(IEnumerable<IDiff> children) : base(children) {
        }

        public override DiffType DiffType => DiffType.Module;
    }
}