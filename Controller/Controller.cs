using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volatily_GUI.Model;
using Volatily_GUI.Service;

namespace Volatily_GUI.Controller
{
    class Controller
    {
        public string executeCommand(string command, evidence evidence, process process)
        {
            constructArguments construct = new constructArguments();
            Service.Service getResponse = new Service.Service();
            string argument = construct.Argument(command, evidence, process);
            string result = getResponse.getVolatilityResponse(argument);
            return result;
        }
    }
}
