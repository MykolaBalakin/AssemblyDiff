using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class MethodAttributesDiff : DiffBase {
        public static MethodAttributesDiff Calculate(MethodAttributes? attributes1, MethodAttributes? attributes2) {
            var different = Different.Different;
            if (attributes1 == attributes2) {
                different=Different.Same;
            }else if (attributes1 == null) {
                different=Different.Only2;
            }else if (attributes2 == null) {
                different=Different.Only1;
            }

            return new MethodAttributesDiff(different) {
                Attributes1 = attributes1,
                Attributes2 = attributes2
            };
        }

        protected MethodAttributesDiff(Different different)
            : base(different) {
        }

        protected MethodAttributesDiff(IEnumerable<IDiff> children) 
            : base(children) {
        }

        public override DiffType DiffType=>DiffType.Unknown;

        public MethodAttributes? Attributes1 { get;private set; }
        public MethodAttributes? Attributes2 { get;private set; }
    }
}