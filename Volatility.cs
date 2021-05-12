using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Volatily_GUI.Model;
using Volatily_GUI.Parser;

namespace Volatily_GUI
{
    public partial class Volatility : Form
    {
        evidence memoryDump;
        DataTable processTable;

        public Volatility()
        {
            InitializeComponent();

            contextMenuMemDump.Items.Add("Analize profile", null, analizaProfileClick);
            contextMenuAnalizeProfile.Items.Add("Network scan", null, networkDumpClick);
            contextMenuAnalizeProfile.Items.Add("Proccess list", null, proccessListClick);
            contextMenuAnalizeProfile.Items.Add("Commands", null, commandsClick);
            contextMenuAnalizeProfile.Items.Add("VerInfo", null, verInfoClick);
            contextMenuStripProcess.Items.Add("List dll", null, ListDllClick);
            Process.ContextMenuStrip = contextMenuStripProcess;
            
        }

        private void contextMenuEvidence_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:";
                openFileDialog.Filter = "dmp files (*.dmp)|*.dmp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    evidence.Nodes[0].Nodes.Add(filePath);
                    memoryDump = new evidence(filePath,openFileDialog.FileName);
                }
            }
        }

        private void evidence_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                evidence.SelectedNode = e.Node;
            }
            if (e.Node.Level == 0)
            {
                e.Node.ContextMenuStrip = contextMenuEvidence;
            }
            else if (e.Node.Level == 1)
            {
                e.Node.ContextMenuStrip = contextMenuMemDump;
            }
            else if (e.Node.Level == 2)
            {
                e.Node.ContextMenuStrip = contextMenuAnalizeProfile;
            }
        }

        private void analizaProfileClick(Object sender, System.EventArgs e)
        {
            MessageBox.Show("Analizando evidencia: " + evidence.SelectedNode.Text);
            evidence.SelectedNode.Nodes.Add("Win10x64_10586");
            evidence.SelectedNode.Nodes.Add("Win10x64_14393");
            evidence.SelectedNode.Nodes.Add("Win10x64");
            evidence.SelectedNode.Nodes.Add("Win2016x64_14393");
        }


        private void ListDllClick(object sender, System.EventArgs e)
        {
            foreach (DataRow row in processTable.Rows)
            {
                if (row["PID"].ToString().Trim() == Process.SelectedNode.Name.ToString().Trim())
                {
                    process process = new process(row[1].ToString(), row[0].ToString(), row[2].ToString(), row[3].ToString());
                    var thread = new Thread(getProcessDlls);
                    thread.Start(process);
                    break;
                }
            }
        }

        private string obtainOffset(string process)
        {
            foreach (DataRow row in processTable.Rows)
            {
                if (row["PID"].ToString().Trim() == process)
                {
                    return (row[0].ToString());
                }
            }
            return "No se logro obtener offset";
        }

        private void verInfoClick(Object sender, System.EventArgs e)
        {
            memoryDump.Profile = evidence.SelectedNode.Text;
            var thread = new Thread(getverInfo);
            thread.Start();
        }
        private void networkDumpClick(Object sender, System.EventArgs e)
        {
            memoryDump.Profile = evidence.SelectedNode.Text;
            var thread = new Thread(getNetScan);
            thread.Start();
        }

        private void commandsClick(Object sender, System.EventArgs e)
        {
            memoryDump.Profile = evidence.SelectedNode.Text;
            var thread = new Thread(getCommands);
            thread.Start();
        }

        private void proccessListClick(Object sender, System.EventArgs e)
        {
            memoryDump.Profile = evidence.SelectedNode.Text;
            var thread = new Thread(getProccess);
            thread.Start();
        }

        private void populateTreeView(DataTable myDataTable)
        {
            DataRow root = myDataTable.Rows[0];
            Process.Nodes.Add(root["PID"].ToString(), root["Name"].ToString());

            for (int i = 1; i < myDataTable.Rows.Count; i++)
            {
                DataRow parentRow = myDataTable.Rows.Find(myDataTable.Rows[i]["PPID"]);
                try
                {
                    TreeNode[] parent = Process.Nodes.Find(parentRow["PID"].ToString(), true);
                    parent[0].Nodes.Add(myDataTable.Rows[i]["PID"].ToString(), myDataTable.Rows[i]["Name"].ToString());
                }
                catch
                {
                    Process.Nodes.Add(myDataTable.Rows[i]["PID"].ToString(), myDataTable.Rows[i]["Name"].ToString());
                }
            }
        }

        /// <summary>
        /// Hilo que obtiene los comandos despues del analisis 
        /// </summary>
        /// 

        private delegate void DisplayCommandsDelegate(string commands);
        private void getCommands()
        {
            Controller.Controller executeGetCommands = new Controller.Controller();
            textBox1.Invoke(new DisplayCommandsDelegate(DisplayCommands), "Buscando comandos en volcado de memoria");
            string commands = executeGetCommands.executeCommand("getCommands", memoryDump, null);
            textBox1.Invoke(new DisplayCommandsDelegate(DisplayCommands), commands);
        }

        private void DisplayCommands(string commands)
        {
            if (commands == "")
            {
                textBox1.Text = "Busqueda de comandos finalizada sin resultados";
            }
            else
            {
                textBox1.Text = commands;
            }
        }

        ///Fin de hilo de comandsos
        ///





        /// <summary>
        /// Obtiene lista de procesos mediante hilo
        /// </summary>
        /// 

        private delegate void DisplayProcessDelegate(DataTable table);
        private void getProccess()
        {
            Controller.Controller executeNetworkDump = new Controller.Controller();
            string procesos = executeNetworkDump.executeCommand("getListProcess", memoryDump, null);
            Volatily_GUI.Parser.parser tableProcesos = new Volatily_GUI.Parser.parser();
            DataTable table = tableProcesos.fixedColumnToTable(procesos);
            DataColumn[] keyColumn = new DataColumn[1];
            keyColumn[0] = table.Columns["PID"];
            table.PrimaryKey = keyColumn;
            Process.Invoke(new DisplayProcessDelegate(DisplayProccess), table);
        }

        private void DisplayProccess(DataTable table)
        {
            processTable = table;
            populateTreeView(table);
        }

        ///Fin de hilo de Procesos
        ///


        /// <summary>
        /// Retorna informacion del sistema a partir de la memoria volatil
        /// </summary>
        /// 
        private delegate void DisplayInfoDelegate(string info);
        private void getverInfo()
        {
            Controller.Controller executeGetCommands = new Controller.Controller();
            textBox1.Invoke(new DisplayCommandsDelegate(DisplayInfo), "Buscando informacion del sistema");
            string info = executeGetCommands.executeCommand("verInfo", memoryDump, null);
            textBox1.Invoke(new DisplayCommandsDelegate(DisplayInfo), info);
        }

        private void DisplayInfo(string info)
        {
            if (info == "")
            {
                textBox1.Text = "Busqueda de comandos finalizada sin resultados";
            }
            else
            {
                textBox1.Text = info;
            }
        }

        private void Process_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        ///Fin de hilo de informacion del sistema
        ///


        /// <summary>
        /// Retorna informacion de dlls asociadas a un proceso
        /// </summary>
        /// 
        private delegate void DisplaydllDelegate(string info);
        private void getProcessDlls(object process)
        {
            Controller.Controller executeGetCommands = new Controller.Controller();
            textBox2.Invoke(new DisplaydllDelegate(DisplayDlls), "Buscando informacion de proceso");
            string info = executeGetCommands.executeCommand("processDlls", memoryDump, (process)process);
            textBox2.Invoke(new DisplaydllDelegate(DisplayDlls), info);
        }

        private void DisplayDlls(string info)
        {
            if (info == "")
            {
                textBox2.Text = "Busqueda de comandos finalizada sin resultados";
            }
            else
            {
                textBox2.Text = info;
            }
        }

        ///Fin de hilo de informacion del sistema
        ///



        /// <summary>
        /// Hilo que obtiene las conexiones de red 
        /// </summary>
        /// 

        private delegate void DisplayNetScanDelegate(string commands);
        private void getNetScan()
        {
            Controller.Controller executeNetworkDump = new Controller.Controller();
            textBox1.Invoke(new DisplayCommandsDelegate(DisplayNetScan), "Buscando conexiones en volcado de memoria");
            string commands = executeNetworkDump.executeCommand("getNetScan", memoryDump, null);
            textBox1.Invoke(new DisplayCommandsDelegate(DisplayNetScan), commands);
        }

        private void DisplayNetScan(string commands)
        {
            if (commands == "")
            {
                textBox1.Text = "Busqueda de red finalizada sin resultados";
            }
            else
            {
                textBox1.Text = commands;
            }
        }

        ///Fin de hilo de comandsos
        ///

    }
}
