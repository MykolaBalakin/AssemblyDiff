using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class TypeDiff : DiffBase {
        public static TypeDiff Calculate(TypeReference type1, TypeReference type2) {
            if (type1 == null && type2 == null) {
                return new TypeDiff(Different.Same);
            }

            TypeDiff result;
            if (type1 == null) {
                result = new TypeDiff(Different.Only2);
            } else if (type2 == null) {
                result = new TypeDiff(Different.Only2);
            } else {
                var type1Name = type1.FullName;
                var type2Name = type2.FullName;

                var equals = String.Equals(type1Name, type2Name, StringComparison.Ordinal);
                result = new TypeDiff(equals ? Different.Same : Different.Different);
            }

            result.Type1 = type1?.Name;
            result.Type2 = type2?.Name;

            return result;
        }

        protected TypeDiff(Different different)
            : base(different) {
        }

        protected TypeDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public String Type1 { get; private set; }
        public String Type2 { get; private set; }
    }
}