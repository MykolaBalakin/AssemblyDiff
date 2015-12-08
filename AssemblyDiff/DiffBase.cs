using System;
using System.Collections.Generic;
using System.Linq;

namespace Balakin.AssemblyDiff {
    public abstract class DiffBase : IDiff {
        protected DiffBase(Different different) {
            Different = different;
            Children = new IDiff[0];
        }

        protected DiffBase(IEnumerable<IDiff> children) {
            Children = children.ToList().AsReadOnly();
            var allDifferents = Children.Select(c => c.Different).Distinct().ToList();
            if (allDifferents.Count == 0) {
                Different = Different.Same;
            } else if (allDifferents.Count == 1) {
                Different = allDifferents.Single();
            } else {
                Different = Different.Different;
            }
        }

        public Different Different { get; }

        public IList<IDiff> Children { get; }
        public abstract DiffType DiffType { get; }
    }
}