using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volatily_GUI.Parser
{
    public class parser
    {
        public DataTable fixedColumnToTable(string value)
        {
            DataTable table = new DataTable();
            string[] datos;
            string[] encabezados;
            string[] size = null;
            datos = value.Split("\r\n");
            int count = 0;


            foreach (var line in datos) {
                
                if (count == 0){
                    encabezados = line.Split(" ");
                    foreach (var line2 in encabezados)
                    {
                        if (line2 != "")
                        {
                            table.Columns.Add(line2.Trim());
                        }
                    }
                   
                }
                if (count == 1)
                {
                    size = line.Split(" ");
                }

                if (count > 1)
                {
                    if (line != "")
                    {
                        List<string> fila = new List<string>();
                        List<string> values = new List<string>();

                        int n = 0;
                        for (int i=0; i < size.Count(); i++)
                        {
                            values.Add(line.Substring(n, size[i].Length).Trim());
                            n = (n+1) + size[i].Length;
                        }

                        var workrow = table.NewRow();
                        int cont = 0;
                        foreach (var column in table.Columns)
                        {
                            workrow[cont] = values[cont];
                            cont = cont + 1;
                        }

                        table.Rows.Add(workrow);
                    }
                }
                count = count + 1;
            }

            return table;
        }
    }
}
