
namespace Volatily_GUI
{
    partial class Volatility
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Add evidence");
            this.contextMenuEvidence = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addEvidence = new System.Windows.Forms.ToolStripMenuItem();
            this.evidence = new System.Windows.Forms.TreeView();
            this.Process = new System.Windows.Forms.TreeView();
            this.network = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.contextMenuMemDump = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuAnalizeProfile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripProcess = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuEvidence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.network)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuEvidence
            // 
            this.contextMenuEvidence.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuEvidence.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEvidence});
            this.contextMenuEvidence.Name = "contextMenuEvidence";
            this.contextMenuEvidence.Size = new System.Drawing.Size(193, 36);
            this.contextMenuEvidence.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuEvidence_ItemClicked);
            // 
            // addEvidence
            // 
            this.addEvidence.Name = "addEvidence";
            this.addEvidence.Size = new System.Drawing.Size(192, 32);
            this.addEvidence.Text = "Add evidence";
            // 
            // evidence
            // 
            this.evidence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.evidence.Location = new System.Drawing.Point(12, 12);
            this.evidence.Name = "evidence";
            treeNode1.ContextMenuStrip = this.contextMenuEvidence;
            treeNode1.Name = "Add";
            treeNode1.Text = "Add evidence";
            this.evidence.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.evidence.Size = new System.Drawing.Size(695, 437);
            this.evidence.TabIndex = 0;
            this.evidence.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.evidence_NodeMouseClick);
            // 
            // Process
            // 
            this.Process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Process.Location = new System.Drawing.Point(12, 455);
            this.Process.Name = "Process";
            this.Process.Size = new System.Drawing.Size(695, 1234);
            this.Process.TabIndex = 1;
            this.Process.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Process_NodeMouseClick);
            // 
            // network
            // 
            this.network.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.network.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.network.Location = new System.Drawing.Point(1626, 455);
            this.network.Name = "network";
            this.network.RowHeadersWidth = 62;
            this.network.RowTemplate.Height = 33;
            this.network.Size = new System.Drawing.Size(669, 1234);
            this.network.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(714, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1581, 437);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(714, 455);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(906, 1234);
            this.textBox2.TabIndex = 6;
            // 
            // contextMenuMemDump
            // 
            this.contextMenuMemDump.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuMemDump.Name = "contextMenuMemDump";
            this.contextMenuMemDump.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuAnalizeProfile
            // 
            this.contextMenuAnalizeProfile.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuAnalizeProfile.Name = "contextMenuAnalizeProfile";
            this.contextMenuAnalizeProfile.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStripProcess
            // 
            this.contextMenuStripProcess.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripProcess.Name = "contextMenuStripProcess";
            this.contextMenuStripProcess.Size = new System.Drawing.Size(61, 4);
            // 
            // Volatility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2307, 1701);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.network);
            this.Controls.Add(this.Process);
            this.Controls.Add(this.evidence);
            this.Name = "Volatility";
            this.Text = "Volatility";
            this.contextMenuEvidence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.network)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView evidence;
        private System.Windows.Forms.TreeView Process;
        private System.Windows.Forms.DataGridView network;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuEvidence;
        private System.Windows.Forms.ToolStripMenuItem addEvidence;
        private System.Windows.Forms.ContextMenuStrip contextMenuMemDump;
        private System.Windows.Forms.ContextMenuStrip contextMenuAnalizeProfile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripProcess;
    }
}

