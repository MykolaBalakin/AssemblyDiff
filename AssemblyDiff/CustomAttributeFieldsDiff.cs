using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class CustomAttributeFieldsDiff : CustomAttributeNamedArgumentsDiff {
        public static CustomAttributeFieldsDiff Calculate(ICollection<CustomAttributeNamedArgument> arguments1, ICollection<CustomAttributeNamedArgument> arguments2) {
            var children = CustomAttributeNamedArgumentsDiff.CalculateChildrenDiffs(arguments1, arguments2);
            var result = new CustomAttributeFieldsDiff(children);
            result.ArgumentsType = CustomAttributeNamedArgumentType.Property;
            return result;
        }

        protected CustomAttributeFieldsDiff(Different different) 
            : base(different) {
        }

        protected CustomAttributeFieldsDiff(IEnumerable<IDiff> children) 
            : base(children) {
        }

        public override DiffType DiffType => DiffType.Field | DiffType.Group;
    }
}