using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class TypesDiff : DiffBase {
        public static TypesDiff Calculate(ICollection<TypeDefinition> types1, ICollection<TypeDefinition> types2) {
            if (types1.NullOrEmpty() && types2.NullOrEmpty()) {
                return new TypesDiff(Different.Same);
            }

            var children = new List<IDiff>();

            var namespaces1 = types1?.Select(t => t.Namespace);
            var namespaces2 = types2?.Select(t => t.Namespace);
            var allNamespaces = namespaces1.UnionIfNotNull(namespaces2).Distinct().OrderBy(n => n).ToList();
            var allTypes = allNamespaces.ToDictionary(
                n => n,
                n => Tuple.Create(
                    types1?.Where(t => t.Namespace == n).ToList(),
                    types2?.Where(t => t.Namespace == n).ToList()
                    )
                );
            foreach (var ns in allNamespaces) {
                children.Add(NamespaceDiff.Calculate(allTypes[ns].Item1, allTypes[ns].Item2));
            }
            return new TypesDiff(children);
        }

        protected TypesDiff(Different different)
            : base(different) {
        }

        protected TypesDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public override DiffType DiffType => DiffType.Type | DiffType.Group;
    }
}
