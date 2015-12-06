using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.AssemblyDiff {
    internal abstract class DiffBase : IDiff {
        protected DiffBase(params IDiff[] children)
            : this((IEnumerable<IDiff>)children) {
        }

        protected DiffBase(IEnumerable<IDiff> children) {
            Children = children.ToList().AsReadOnly();
            var types = Children.Select(c => c.Type).Distinct().ToList();
            Type = types.Count == 0 ? types.Single() : DiffType.Different;
        }

        public DiffType Type { get; }
        public IList<IDiff> Children { get; }
    }
}
