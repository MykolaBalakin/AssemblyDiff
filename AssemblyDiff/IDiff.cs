using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.AssemblyDiff {
    public interface IDiff {
        DiffType Type { get; }
        IList<IDiff> Children { get; }
    }
}
