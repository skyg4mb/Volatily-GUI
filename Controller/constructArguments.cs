using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volatily_GUI.Model;

namespace Volatily_GUI.Controller
{
    public class constructArguments
    {
        public string Argument(string hability, evidence evidence, process process)
        {
            switch (hability)
            {
                case "getImageInfo":
                    return argGetImageInfo(evidence);
                case "getListProcess":
                    return argGetListProcess(evidence);
                case "getNetScan":
                    return argGetNetScan(evidence);
                case "getCommands":
                    return argGetCommands(evidence);
                case "verInfo":
                    return argVerInfo(evidence);
                case "processDlls":
                    return argListDlls(evidence, process);
                default:
                    // Default stuff
                    return "noInfo";
            }
        }

        private string argListDlls(evidence evidence, process process)
        {
            string response;
            response = @"-f " + evidence.Route + " --profile=" + evidence.Profile + " dlllist -p " + process.Pid;
            return response;
        }

        private string argVerInfo(evidence evidence)
        {
            string response;
            response = @"-f " + evidence.Route + " --profile=" + evidence.Profile + " verinfo";
            return response;
        }
        private string argGetCommands(evidence evidence)
        {
            string response;
            response = @"-f " + evidence.Route + " --profile=" + evidence.Profile + " cmdscan";
            return response;
        }
        private string argGetImageInfo(evidence evidence)
        {
            string response;
            response = @" -f " + evidence.Route + " imageinfo";
            return response;
        }

        private string argGetListProcess(evidence evidence)
        {
            string response;
            response = @" -f " + evidence.Route + " --profile=" + evidence.Profile + " pslist";
            return response;
        }

        private string argGetNetScan(evidence evidence)
        {
            string response;
            response = @" -f " + evidence.Route + " --profile=" + evidence.Profile + " netscan";
            return response;
        }

    }
}
