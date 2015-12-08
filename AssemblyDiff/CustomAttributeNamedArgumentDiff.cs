using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class CustomAttributeNamedArgumentDiff : DiffBase {
        public static CustomAttributeNamedArgumentDiff Calculate(CustomAttributeNamedArgument? argument1, CustomAttributeNamedArgument? argument2) {
            if (argument1 == null && argument2 == null) {
                return new CustomAttributeNamedArgumentDiff(Different.Same);
            }

            String name;

            if (argument1 != null && argument2 != null) {
                var name1 = argument1.Value.Name;
                var name2 = argument2.Value.Name;
                if (!String.Equals(name1, name2, StringComparison.Ordinal)) {
                    throw new ArgumentOutOfRangeException(nameof(argument2), "Name of both attributes should be equal");
                }
                name = name1;
            } else {
                name = argument1?.Name ?? argument2?.Name;
            }

            var children = new IDiff[] {
                CustomAttributeArgumentDiff.Calculate(argument1?.Argument, argument2?.Argument)
            };
            var result = new CustomAttributeNamedArgumentDiff(children);
            result.Name = name;
            return result;
        }

        protected CustomAttributeNamedArgumentDiff(Different different)
            : base(different) {
        }

        protected CustomAttributeNamedArgumentDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public String Name { get; private set; }
    }
}