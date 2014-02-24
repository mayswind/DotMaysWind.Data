namespace DotMaysWind.Data.Test
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenDefaultDatabase = new System.Windows.Forms.Button();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnOpenDatabaseFromConfig = new System.Windows.Forms.Button();
            this.btnOpenDatabaseFromString = new System.Windows.Forms.Button();
            this.txtConnectionSettingName = new System.Windows.Forms.TextBox();
            this.txtProviderName = new System.Windows.Forms.TextBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.pnlSplit = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.ssMain.SuspendLayout();
            this.grpConnection.SuspendLayout();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenDefaultDatabase
            // 
            this.btnOpenDefaultDatabase.Location = new System.Drawing.Point(12, 24);
            this.btnOpenDefaultDatabase.Name = "btnOpenDefaultDatabase";
            this.btnOpenDefaultDatabase.Size = new System.Drawing.Size(256, 23);
            this.btnOpenDefaultDatabase.TabIndex = 0;
            this.btnOpenDefaultDatabase.Text = "Open Default Database";
            this.btnOpenDefaultDatabase.UseVisualStyleBackColor = true;
            this.btnOpenDefaultDatabase.Click += new System.EventHandler(this.btnOpenDefaultDatabase_Click);
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.ssMain.Location = new System.Drawing.Point(0, 429);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(794, 22);
            this.ssMain.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 17);
            this.lblStatus.Text = "Ready";
            // 
            // btnOpenDatabaseFromConfig
            // 
            this.btnOpenDatabaseFromConfig.Location = new System.Drawing.Point(12, 53);
            this.btnOpenDatabaseFromConfig.Name = "btnOpenDatabaseFromConfig";
            this.btnOpenDatabaseFromConfig.Size = new System.Drawing.Size(256, 23);
            this.btnOpenDatabaseFromConfig.TabIndex = 1;
            this.btnOpenDatabaseFromConfig.Text = "Open Database From Configuration";
            this.btnOpenDatabaseFromConfig.UseVisualStyleBackColor = true;
            this.btnOpenDatabaseFromConfig.Click += new System.EventHandler(this.btnOpenDatabaseFromConfig_Click);
            // 
            // btnOpenDatabaseFromString
            // 
            this.btnOpenDatabaseFromString.Location = new System.Drawing.Point(12, 109);
            this.btnOpenDatabaseFromString.Name = "btnOpenDatabaseFromString";
            this.btnOpenDatabaseFromString.Size = new System.Drawing.Size(256, 23);
            this.btnOpenDatabaseFromString.TabIndex = 3;
            this.btnOpenDatabaseFromString.Text = "Open Database From String";
            this.btnOpenDatabaseFromString.UseVisualStyleBackColor = true;
            this.btnOpenDatabaseFromString.Click += new System.EventHandler(this.btnOpenDatabaseFromString_Click);
            // 
            // txtConnectionSettingName
            // 
            this.txtConnectionSettingName.Location = new System.Drawing.Point(12, 84);
            this.txtConnectionSettingName.Name = "txtConnectionSettingName";
            this.txtConnectionSettingName.Size = new System.Drawing.Size(256, 21);
            this.txtConnectionSettingName.TabIndex = 2;
            this.txtConnectionSettingName.Text = "OLDDBCONN";
            // 
            // txtProviderName
            // 
            this.txtProviderName.Location = new System.Drawing.Point(12, 138);
            this.txtProviderName.Name = "txtProviderName";
            this.txtProviderName.Size = new System.Drawing.Size(256, 21);
            this.txtProviderName.TabIndex = 4;
            this.txtProviderName.Text = "System.Data.OleDb";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(12, 165);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(256, 21);
            this.txtConnectionString.TabIndex = 5;
            this.txtConnectionString.Text = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Z:\\ACCESSDB.mdb";
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.btnOpenDefaultDatabase);
            this.grpConnection.Controls.Add(this.txtConnectionString);
            this.grpConnection.Controls.Add(this.btnOpenDatabaseFromConfig);
            this.grpConnection.Controls.Add(this.txtProviderName);
            this.grpConnection.Controls.Add(this.btnOpenDatabaseFromString);
            this.grpConnection.Controls.Add(this.txtConnectionSettingName);
            this.grpConnection.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpConnection.Location = new System.Drawing.Point(10, 10);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(286, 195);
            this.grpConnection.TabIndex = 6;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.groupBox1);
            this.scMain.Panel1.Controls.Add(this.pnlSplit);
            this.scMain.Panel1.Controls.Add(this.grpConnection);
            this.scMain.Panel1.Padding = new System.Windows.Forms.Padding(10, 10, 5, 10);
            this.scMain.Panel1MinSize = 300;
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.dgvPreview);
            this.scMain.Panel2.Padding = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.scMain.Size = new System.Drawing.Size(794, 429);
            this.scMain.SplitterDistance = 301;
            this.scMain.SplitterWidth = 2;
            this.scMain.TabIndex = 7;
            // 
            // dgvPreview
            // 
            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.AllowUserToDeleteRows = false;
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPreview.Location = new System.Drawing.Point(5, 10);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.RowTemplate.Height = 23;
            this.dgvPreview.Size = new System.Drawing.Size(476, 409);
            this.dgvPreview.TabIndex = 0;
            this.dgvPreview.TabStop = false;
            // 
            // pnlSplit
            // 
            this.pnlSplit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSplit.Location = new System.Drawing.Point(10, 205);
            this.pnlSplit.Name = "pnlSplit";
            this.pnlSplit.Size = new System.Drawing.Size(286, 10);
            this.pnlSplit.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(10, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 204);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Method";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 20);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(111, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "TestMethod";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 451);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.ssMain);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DotMaysWind.Data.Test";
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDefaultDatabase;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Button btnOpenDatabaseFromConfig;
        private System.Windows.Forms.Button btnOpenDatabaseFromString;
        private System.Windows.Forms.TextBox txtConnectionSettingName;
        private System.Windows.Forms.TextBox txtProviderName;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Panel pnlSplit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTest;
    }
}

