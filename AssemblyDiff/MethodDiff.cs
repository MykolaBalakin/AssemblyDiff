using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class MethodDiff : DiffBase {
        public static MethodDiff Calculate(MethodDefinition method1, MethodDefinition method2) {
            if (method1 == null && method2 == null) {
                return new MethodDiff(Different.Same);
            }

            String methodName;
            if (method1 == null) {
                methodName = method2.Name;
            } else if (method2 == null) {
                methodName = method1.Name;
            } else {
                var methodName1 = method1.Name;
                var methodName2 = method2.Name;
                if (!String.Equals(methodName1, methodName2)) {
                    throw new ArgumentOutOfRangeException(nameof(method2), "Method names should be equal");
                }
                methodName = methodName1;
            }

            var children = new List<IDiff>();
            children.Add(CustomAttributesDiff.Calculate(method1?.CustomAttributes, method2?.CustomAttributes));
            children.Add(MethodAttributesDiff.Calculate(method1?.Attributes, method2?.Attributes));

            return new MethodDiff(children) {
                MethodName = methodName
            };
        }

        protected MethodDiff(Different different)
            : base(different) {
        }

        protected MethodDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public override DiffType DiffType => DiffType.Method;

        public String MethodName { get; private set; }
    }
}