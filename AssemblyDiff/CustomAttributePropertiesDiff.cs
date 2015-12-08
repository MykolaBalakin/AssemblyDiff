using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class CustomAttributePropertiesDiff : CustomAttributeNamedArgumentsDiff {
        public static CustomAttributePropertiesDiff Calculate(ICollection<CustomAttributeNamedArgument> arguments1, ICollection<CustomAttributeNamedArgument> arguments2) {
            var children = CustomAttributeNamedArgumentsDiff.CalculateChildrenDiffs(arguments1, arguments2);
            var result = new CustomAttributePropertiesDiff(children);
            result.ArgumentsType = CustomAttributeNamedArgumentType.Property;
            return result;
        }

        protected CustomAttributePropertiesDiff(Different different) : base(different) {
        }

        protected CustomAttributePropertiesDiff(IEnumerable<IDiff> children) : base(children) {
        }

        public override DiffType DiffType => DiffType.Property | DiffType.Group;
    }
}