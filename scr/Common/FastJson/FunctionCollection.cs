using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iuiu.VirtualMachine
{
    public class FunctionCollection : Function
    {
        public List<Function> Functions { get; private set; }

        public FunctionCollection()
            : base(null, string.Empty, string.Empty, 0, new string[0], new Instruction[0])
        {
            Functions = new List<Function>();
        }

        public FunctionCollection(IEnumerable<Function> funcs)
            : base(null, string.Empty, string.Empty, 0, new string[0], new Instruction[0])
        {
            Functions = new List<Function>(funcs);
        }
    }
}
