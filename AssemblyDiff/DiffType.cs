using System;

namespace Balakin.AssemblyDiff {
    [Flags]
    public enum DiffType {
        Unknown,
        Assembly,
        Module,
        Type,
        CustomAttribute,
        Argument,
        Return,
        Value,
        Field,
        Property,
        TypeReference,
        Method,

        Group = 0x100000
    }
}