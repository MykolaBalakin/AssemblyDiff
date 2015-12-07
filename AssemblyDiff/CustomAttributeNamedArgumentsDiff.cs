using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public abstract class CustomAttributeNamedArgumentsDiff : DiffBase {
        internal static IEnumerable<IDiff> CalculateChildrenDiffs(ICollection<CustomAttributeNamedArgument> arguments1, ICollection<CustomAttributeNamedArgument> arguments2) {
            if (arguments1.NullOrEmpty() && arguments2.NullOrEmpty()) {
                yield break;
            }

            var arguments1Dict = arguments1?.ToDictionary(a => a.Name, a => a) ?? new Dictionary<String, CustomAttributeNamedArgument>();
            var arguments2Dict = arguments2?.ToDictionary(a => a.Name, a => a) ?? new Dictionary<String, CustomAttributeNamedArgument>();

            var allNames = arguments1Dict.Keys.Union(arguments2Dict.Keys).OrderBy(n => n).ToList();
            foreach (var name in allNames) {
                var argument1 = arguments1Dict.GetOrNullableNull(name);
                var argument2 = arguments2Dict.GetOrNullableNull(name);

                yield return CustomAttributeNamedArgumentDiff.Calculate(argument1, argument2);
            }
        }

        protected CustomAttributeNamedArgumentsDiff(Different different)
            : base(different) {
        }

        protected CustomAttributeNamedArgumentsDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public CustomAttributeNamedArgumentType ArgumentsType { get; protected set; }
    }
}
