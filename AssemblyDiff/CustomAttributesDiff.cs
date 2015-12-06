using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Balakin.AssemblyDiff {
    public class CustomAttributesDiff : DiffBase {
        public static CustomAttributesDiff Calculate(ICollection<CustomAttribute> attributes1, ICollection<CustomAttribute> attributes2) {
            throw new NotImplementedException();
        }
    }
}