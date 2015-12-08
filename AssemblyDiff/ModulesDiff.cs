using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class ModulesDiff : DiffBase {
        public static ModulesDiff Calculate(ICollection<ModuleDefinition> modules1, ICollection<ModuleDefinition> modules2) {
            if (modules1 == null && modules2 == null) {
                return new ModulesDiff(Different.Same);
            }

            var children = new List<IDiff>();

            var names1 = modules1?.Select(m => m.Name);
            var names2 = modules2?.Select(m => m.Name);
            var allNames = names1.UnionIfNotNull(names2).Distinct().OrderBy(n => n).ToList();
            foreach (var name in allNames) {
                var module1 = modules1?.SingleOrDefault(m => m.Name == name);
                var module2 = modules2?.SingleOrDefault(m => m.Name == name);

                children.Add(ModuleDiff.Calculate(module1, module2));
            }

            return new ModulesDiff(children);
        }

        protected ModulesDiff(Different different)
            : base(different) {
        }

        protected ModulesDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public override DiffType DiffType => DiffType.Module | DiffType.Group;
    }
}
