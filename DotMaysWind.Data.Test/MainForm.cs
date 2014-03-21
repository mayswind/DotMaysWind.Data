using System;
using System.Data;
using System.Windows.Forms;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Command.Function;

namespace DotMaysWind.Data.Test
{
    public partial class MainForm : Form
    {
        #region 字段
        private IDatabase _database;
        #endregion

        #region 构造方法
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Connection
        private void btnOpenDefaultDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                this._database = DatabaseFactory.CreateDatabase();
                this.ShowDatabaseInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenDatabaseFromConfig_Click(object sender, EventArgs e)
        {
            try
            {
                this._database = DatabaseFactory.CreateDatabase(this.txtConnectionSettingName.Text);
                this.ShowDatabaseInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenDatabaseFromString_Click(object sender, EventArgs e)
        {
            try
            {
                this._database = DatabaseFactory.CreateDatabase(this.txtConnectionString.Text, this.txtProviderName.Text);
                this.ShowDatabaseInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowDatabaseInfo()
        {
            this.Text = String.Format("{0} - DotMaysWind.Data.Test", this._database.ProviderName);
            this.lblStatus.Text = String.Format("Database opened, ProviderName = {0}", this._database.ProviderName);
        }
        #endregion

        #region Method
        private void btnTest_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}