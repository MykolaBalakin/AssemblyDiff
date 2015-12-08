using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class TypeDiff : DiffBase {
        public static TypeDiff Calculate(TypeDefinition type1, TypeDefinition type2) {
            if (type1 == null && type2 == null) {
                return new TypeDiff(Different.Same);
            }

            var children = new List<IDiff>();
            children.Add(CustomAttributesDiff.Calculate(type1?.CustomAttributes, type2?.CustomAttributes));
            if (type1?.HasNestedTypes == true || type2?.HasNestedTypes == true) {
                children.Add(TypesDiff.Calculate(type1?.NestedTypes, type2?.NestedTypes));
            }
            // TODO: Base type
            // TODO: Interfaces

            children.Add(MethodsDiff.Calculate(type1?.Methods, type2?.Methods));
            // TODO: Properties
            // TODO: Events
            // TODO: Fields

            var result = new TypeDiff(children);
            if (type1 == null) {
                result.TypeName = type2.Name;
            } else if (type2 == null) {
                result.TypeName = type1.Name;
            } else {
                var type1Name = type1.Name;
                var type2Name = type2.Name;
                if (String.Equals(type1Name, type2Name)) {
                    result.TypeName = type1Name;
                }
            }
            return result;
        }

        protected TypeDiff(Different different)
            : base(different) {
        }

        protected TypeDiff(IEnumerable<IDiff> children)
            : base(children) {
        }

        public String TypeName { get; private set; }
        public override DiffType DiffType => DiffType.Type;
    }
}
