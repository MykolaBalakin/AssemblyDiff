using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class CustomAttributeDiff : DiffBase {
        public static CustomAttributeDiff Calculate(CustomAttribute attribute1, CustomAttribute attribute2) {
            if (attribute1 == null && attribute2 == null) {
                return new CustomAttributeDiff(Different.Same);
            }

            String typeName = null;
            if (attribute1 != null && attribute2 != null) {
                var name1 = attribute1.AttributeType.FullName;
                var name2 = attribute2.AttributeType.FullName;
                if (!String.Equals(name1, name2, StringComparison.Ordinal)) {
                    throw new ArgumentOutOfRangeException(nameof(attribute2), "Type of both attributes should be equal");
                }
                typeName = attribute1.AttributeType.Name;
            }

            var children = new List<IDiff>();

            var constructorArguments1 = attribute1?.ConstructorArguments;
            var constructorArguments2 = attribute2?.ConstructorArguments;
            if (constructorArguments1 != null || constructorArguments2 != null) {
                children.Add(CustomAttributeArgumentsDiff.Calculate(constructorArguments1, constructorArguments2));
            }

            var properties1 = attribute1?.Properties;
            var properties2 = attribute2?.Properties;
            if (properties1 != null || properties2 != null) {
                children.Add(CustomAttributePropertiesDiff.Calculate(properties1, properties2));
            }

            var fields1 = attribute1?.Fields;
            var fields2 = attribute2?.Fields;
            if (fields1 != null || fields2 != null) {
                children.Add(CustomAttributeFieldsDiff.Calculate(fields1, fields2));
            }

            var result = new CustomAttributeDiff(children);
            result.CustomAttributeType = typeName;
            return result;
        }


        protected CustomAttributeDiff(Different different)
            : base(different) {
        }

        protected CustomAttributeDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public String CustomAttributeType { get; private set; }
    }
}