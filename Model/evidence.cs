using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volatily_GUI.Model
{
    public class evidence
    {
        private string route;
        private string name;
        private string profile;

        public evidence(string route, string name)
        {
            this.route = route;
            this.name = name;
        }

        public string Route { get => route; set => route = value; }
        public string Name { get => name; set => name = value; }
        public string Profile { get => profile; set => profile = value; }
    }
}
