using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class ValueDiff : DiffBase {
        public static ValueDiff Calculate(Object value1, Boolean value1Present, Object value2, Boolean value2Present) {
            if (!value1Present && !value2Present) {
                return new ValueDiff(Different.Same);
            }

            ValueDiff result;
            if (!value1Present) {
                result = new ValueDiff(Different.Only2);
            } else if (!value2Present) {
                result = new ValueDiff(Different.Only1);
            } else {
                if (value1 is TypeReference && value2 is TypeReference) {
                    result = new ValueDiff(new[] { TypeReferenceDiff.Calculate((TypeReference) value1, (TypeReference) value2) });
                } else {
                    var equals = ValueEquals(value1, value2);
                    result = new ValueDiff(equals ? Different.Same : Different.Different);
                }
            }

            result.Value1 = value1;
            result.Value2 = value2;
            return result;
        }

        private static Boolean ValueEquals(Object value1, Object value2) {
            return Object.Equals(value1, value2);
        }

        protected ValueDiff(Different different)
            : base(different) {
        }

        protected ValueDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public Object Value1 { get; private set; }
        public Object Value2 { get; private set; }
        public override DiffType DiffType => DiffType.Value;
    }
}