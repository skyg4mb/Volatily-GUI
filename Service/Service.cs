using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volatily_GUI.Service
{
    public class Service
    {
        public string getVolatilityResponse(string argument)
        {
            string response = "";

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "volatility.exe",
                    Arguments = argument,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            };

            proc.Start();
            response = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            return response;
        }
    }
}
