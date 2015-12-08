using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class CustomAttributeArgumentsDiff : DiffBase {
        public static CustomAttributeArgumentsDiff Calculate(IList<CustomAttributeArgument> arguments1, IList<CustomAttributeArgument> arguments2) {
            if (arguments1.NullOrEmpty() && arguments2.NullOrEmpty()) {
                return new CustomAttributeArgumentsDiff(Different.Same);
            }

            var children = new List<IDiff>();

            var count = 0;
            if (arguments1 != null) {
                count = Math.Max(count, arguments1.Count);
            }
            if (arguments2 != null) {
                count = Math.Max(count, arguments2.Count);
            }


            for (var i = 0; i < count; i++) {
                var arg1 = arguments1.GetOrNullableNull(i);
                var arg2 = arguments2.GetOrNullableNull(i);

                children.Add(CustomAttributeArgumentDiff.Calculate(arg1, arg2));
            }
            return new CustomAttributeArgumentsDiff(children);
        }
        protected CustomAttributeArgumentsDiff(Different different)
             : base(different) {
        }

        protected CustomAttributeArgumentsDiff(IEnumerable<IDiff> children)
             : base(children) {
        }

        public override DiffType DiffType => DiffType.Argument | DiffType.Group;
    }
}
