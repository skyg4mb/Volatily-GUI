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
        string commands;

        public Volatility()
        {
            InitializeComponent();

            contextMenuMemDump.Items.Add("Analize profile", null, analizaProfileClick);
            contextMenuAnalizeProfile.Items.Add("Network scan", null, networkDumpClick);
            contextMenuAnalizeProfile.Items.Add("Proccess list", null, proccessListClick);
            contextMenuAnalizeProfile.Items.Add("Commands", null, commandsClick);

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

        private void networkDumpClick(Object sender, System.EventArgs e)
        {
            Controller.Controller executeNetworkDump = new Controller.Controller();
            memoryDump.Profile = evidence.SelectedNode.Text;
            executeNetworkDump.executeCommand("getNetScan", memoryDump);
        }

        private void commandsClick(Object sender, System.EventArgs e)
        {
            var thread = new Thread(getCommands);

        }

        private void proccessListClick(Object sender, System.EventArgs e)
        {
            Controller.Controller executeNetworkDump = new Controller.Controller();
            memoryDump.Profile = evidence.SelectedNode.Text;
            string procesos = executeNetworkDump.executeCommand("getListProcess", memoryDump);
            Volatily_GUI.Parser.parser tableProcesos = new Volatily_GUI.Parser.parser();
            
            DataTable table = tableProcesos.fixedColumnToTable(procesos);
            
            DataColumn[] keyColumn = new DataColumn[1];
            keyColumn[0] = table.Columns["PID"];
            table.PrimaryKey = keyColumn;
            populateTreeView(table);
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
            memoryDump.Profile = evidence.SelectedNode.Text;
            textBox1.Invoke(new DisplayCommandsDelegate(DisplayCommands), "Buscando comandos en volcado de memoria");
            string commands = executeGetCommands.executeCommand("getCommands", memoryDump);
            textBox1.Invoke(new DisplayCommandsDelegate(DisplayCommands), commands);
        }

        private void DisplayCommands(string commands)
        {
            textBox1.Text = commands + "Busqueda finalizada";
        }

        ///Fin de hilo de comandsos


    }
}
