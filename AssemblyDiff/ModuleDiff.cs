using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class ModuleDiff : DiffBase {
        public static ModuleDiff Calculate(ModuleDefinition module1, ModuleDefinition module2) {
            throw new NotImplementedException();
        }

        public ModuleDiff(Different different) : base(different) {
        }

        public ModuleDiff(IEnumerable<IDiff> children) : base(children) {
        }
    }
}