using System;
using System.Collections.Generic;

namespace Balakin.AssemblyDiff {
    public abstract class DiffBase : IDiff {
        public Different Different {
            get {
                throw new NotImplementedException();
            }
        }

        public IList<IDiff> Children {
            get {
                throw new NotImplementedException();
            }
        }
    }
}