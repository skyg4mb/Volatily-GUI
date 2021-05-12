
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volatily_GUI.Model
{
    public class process
    {
        private string name;
        private string offset;
        private string pid;
        private string ppid;

        public process(string name, string offset, string pid, string ppid)
        {
            this.name = name;
            this.offset = offset;
            this.pid = pid;
            this.ppid = ppid;
        }

        public string Name { get => name; set => name = value; }
        public string Offset { get => offset; set => offset = value; }
        public string Pid { get => pid; set => pid = value; }
        public string Ppid { get => ppid; set => ppid = value; }
    }
}
