using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class CustomAttributeArgumentDiff : DiffBase {
        public static CustomAttributeArgumentDiff Calculate(CustomAttributeArgument? argument1, CustomAttributeArgument? argument2) {
            if (argument1 == null && argument2 == null) {
                return new CustomAttributeArgumentDiff(Different.Same);
            }

            var children = new IDiff[] {
                TypeReferenceDiff.Calculate(argument1?.Type, argument2?.Type),
                ValueDiff.Calculate(argument1?.Value, argument1 != null, argument2?.Value, argument2 != null)
            };
            return new CustomAttributeArgumentDiff(children);
        }

        protected CustomAttributeArgumentDiff(Different different)
            : base(different) {
        }

        protected CustomAttributeArgumentDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public override DiffType DiffType => DiffType.Argument;
    }
}