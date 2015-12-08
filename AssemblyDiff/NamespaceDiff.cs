using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class NamespaceDiff : DiffBase {
        public static NamespaceDiff Calculate(ICollection<TypeDefinition> types1, ICollection<TypeDefinition> types2) {
            if (types1.NullOrEmpty() && types2.NullOrEmpty()) {
                return new NamespaceDiff(Different.Same);
            }

            var children = new List<IDiff>();

            var namespaces1 = types1?.Select(t => t.Namespace);
            var namespaces2 = types1?.Select(t => t.Namespace);
            var allNamespaces = namespaces1.UnionIfNotNull(namespaces2).Distinct();
            var ns = allNamespaces.Single();

            var rootTypes1 = types1?.Where(t => !t.IsNested).ToList() ?? new List<TypeDefinition>();
            var rootTypes2 = types2?.Where(t => !t.IsNested).ToList() ?? new List<TypeDefinition>();

            var allNames = rootTypes1.Select(t => t.Name).Union(rootTypes2.Select(t => t.Name)).Distinct().OrderBy(n => n);
            foreach (var typeName in allNames) {
                var type1 = rootTypes1.SingleOrDefault(t => t.Name == typeName);
                var type2 = rootTypes2.SingleOrDefault(t => t.Name == typeName);

                children.Add(TypeDiff.Calculate(type1, type2));
            }

            return new NamespaceDiff(children) {
                Namespace = ns
            };
        }

        protected NamespaceDiff(Different different)
             : base(different) {
        }

        protected NamespaceDiff(IEnumerable<IDiff> children)
             : base(children) {
        }

        public String Namespace { get; private set; }
        public override DiffType DiffType => DiffType.Type | DiffType.Group;
    }
}
