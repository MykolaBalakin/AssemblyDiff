using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class CustomAttributesDiff : DiffBase {
        public static CustomAttributesDiff Calculate(ICollection<CustomAttribute> attributes1, ICollection<CustomAttribute> attributes2) {
            if (attributes1.NullOrEmpty() && attributes2.NullOrEmpty()) {
                return new CustomAttributesDiff(Different.Same);
            }

            var children = new List<IDiff>();

            var attributeTypes1 = attributes1?.Select(a => a.AttributeType.Name);
            var attributeTypes2 = attributes2?.Select(a => a.AttributeType.Name);
            var allAttributeTypes = attributeTypes1.UnionIfNotNull(attributeTypes2).Distinct().OrderBy(t => t).ToList();
            var allAttributes = allAttributeTypes.ToDictionary(
                t => t,
                t => Tuple.Create(
                    attributes1?.Where(a => a.AttributeType.Name == t).ToList() ?? new List<CustomAttribute>(),
                    attributes2?.Where(a => a.AttributeType.Name == t).ToList() ?? new List<CustomAttribute>()
                    )
                );
            foreach (var type in allAttributeTypes) {
                var a1 = allAttributes[type].Item1;
                var a2 = allAttributes[type].Item2;
                if (a1.Count == 0) {
                    children.AddRange(a2.Select(customAttribute => CustomAttributeDiff.Calculate(null, customAttribute)));
                } else if (a2.Count == 0) {
                    children.AddRange(a1.Select(customAttribute => CustomAttributeDiff.Calculate(customAttribute, null)));
                } else if (a1.Count == 1 && a2.Count == 1) {
                    children.Add(CustomAttributeDiff.Calculate(a1.Single(), a2.Single()));
                } else {
                    var assignedA1 = new List<CustomAttribute>();
                    foreach (var attribute1 in a1) {
                        CustomAttributeDiff diff = null;
                        CustomAttribute resultedA2 = null;
                        foreach (var attribute2 in a2) {
                            diff = CustomAttributeDiff.Calculate(attribute1, attribute2);
                            if (diff.Different == Different.Same) {
                                resultedA2 = attribute2;
                                break;
                            }
                        }
                        if (resultedA2 != null && diff != null) {
                            assignedA1.Add(attribute1);
                            a2.Remove(resultedA2);
                            children.Add(diff);
                        }
                    }
                    foreach (var attribute in assignedA1) {
                        a1.Remove(attribute);
                    }

                    if (a1.Any() && a2.Count == 0) {
                        foreach (var attribute in a1) {
                            children.Add(CustomAttributeDiff.Calculate(attribute, null));
                        }
                    } else if (a1.Count == 0) {
                        foreach (var attribute in a2) {
                            children.Add(CustomAttributeDiff.Calculate(null, attribute));
                        }
                    } else {
                        throw new NotImplementedException("Multiple attributes of type " + type);
                    }
                }
            }

            return new CustomAttributesDiff(children);
        }

        protected CustomAttributesDiff(Different different)
            : base(different) {
        }

        protected CustomAttributesDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public override DiffType DiffType => DiffType.CustomAttribute | DiffType.Group;
    }
}