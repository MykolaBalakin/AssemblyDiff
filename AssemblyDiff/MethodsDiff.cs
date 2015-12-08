using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class MethodsDiff : DiffBase {
        public static MethodsDiff Calculate(ICollection<MethodDefinition> methods1, ICollection<MethodDefinition> methods2) {
            if (methods1.NullOrEmpty() && methods2.NullOrEmpty()) {
                return new MethodsDiff(Different.Same);
            }

            var children = new List<IDiff>();

            var names1 = methods1?.Select(m => m.Name);
            var names2 = methods2?.Select(m => m.Name);
            var allNames = names1.UnionIfNotNull(names2).Distinct().OrderBy(n => n).ToList();
            var allMethods = allNames.ToDictionary(
                n => n,
                n => Tuple.Create(
                    methods1?.Where(m => m.Name == n)?.ToList() ?? new List<MethodDefinition>(),
                    methods2?.Where(m => m.Name == n)?.ToList() ?? new List<MethodDefinition>()
                    )
                );

            foreach (var name in allNames) {
                var m1 = allMethods[name].Item1;
                var m2 = allMethods[name].Item2;

                if (m1.Count <= 1 && m2.Count <= 1) {
                    children.Add(MethodDiff.Calculate(m1.SingleOrDefault(), m2.SingleOrDefault()));
                } else {
                    Func<MethodDefinition, MethodDefinition, Boolean> comparer = (mm1, mm2) => {
                        var p1 = mm1.Parameters;
                        var p2 = mm2.Parameters;
                        if (p1.Count != p2.Count) {
                            return false;
                        }
                        for (var i = 0; i < p1.Count; i++) {
                            if (p1[i].ParameterType.FullName != p2[i].ParameterType.FullName) {
                                return false;
                            }
                        }
                        return true;
                    };
                    foreach (var method1 in m1) {
                        var method2 = m2.SingleOrDefault(m => comparer(method1, m));
                        children.Add(MethodDiff.Calculate(method1, method2));
                        m2.Remove(method2);
                    }
                    foreach (var method2 in m2) {
                        children.Add(MethodDiff.Calculate(null, method2));
                    }
                }
            }

            return new MethodsDiff(children);
        }

        protected MethodsDiff(Different different) 
            : base(different) {
        }

        protected MethodsDiff(IEnumerable<IDiff> children) 
            : base(children) {
        }

        public override DiffType DiffType => DiffType.Method | DiffType.Group;
    }
}