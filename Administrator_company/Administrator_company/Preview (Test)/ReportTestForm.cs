using System;
using Administrator_company.LogicProgram;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using Microsoft.Reporting.WinForms;



namespace Administrator_company.Preview__Test_
{
    public partial class ReportTestForm : Form
    {
        Connection connect = new Connection();
        public ReportTestForm()
        {
            InitializeComponent();
        }

        private void ReportTestForm_Load(object sender, EventArgs e)
        {
            #region Try To use ReportViewer
            /*
            try
            {
                
                 //string query = "SELECT * FROM supermarket.stock;";
                 //connect.command = new MySqlCommand(query, connect.connection);
                 //connect.adapter = new MySqlDataAdapter(connect.command) {SelectCommand = connect.command};
                 //DataSet dataSet  = new DataSet();
                 //connect.adapter.Fill(dataSet);
                 //DataTable dataTable = dataSet.Tables[0];
                 //dataTable.TableName = "myDataTable";
                 //reportViewer1.LocalReport.DataSources.Clear();
                 //ReportDataSource source = new ReportDataSource("myDataTable", dataTable);
                 //reportViewer1.LocalReport.DataSources.Add(source);
                 ////reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet",dataSet.Tables[0]));
                 //this.reportViewer1.RefreshReport();

                connect.OpenConnection();
                string query = "SELECT * FROM supermarket.stock;";
                connect.command = new MySqlCommand(query, connect.connection);
                connect.adapter = new MySqlDataAdapter(connect.command) {SelectCommand = connect.command};
                DataTable dataTable = new DataTable();
                dataTable.TableName = "myDataTable";
                dataTable.Clear();
                //connect.adapter.Fill(dataTable);
                //DataSet dataSet = new DataSet();
                //connect.adapter.Fill(dataSet);
                //DataTable dataTable = dataSet.Tables[0];
                connect.CloseConnection();

                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportEmbeddedResource = "Administrator_company.Preview__Test_.MyReport.rdlc";
                
                ReportDataSource dataset = new ReportDataSource(dataTable.TableName, dataTable); // set the datasource
                dataset.Value = dataTable;
                reportViewer1.LocalReport.DataSources.Clear(); //clear report
                reportViewer1.LocalReport.DataSources.Add(dataset);

                reportViewer1.LocalReport.Refresh();
                reportViewer1.RefreshReport(); // refresh report

                

                //portDataSource1.Name = "DataSet1_DataTable1";
                //reportDataSource1.Value = this.DataTable1BindingSource;
                //this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                //this.reportViewer1.LocalReport.ReportEmbeddedResource = "DynamicDataSet.Report1.rdlc";
                //this.DataTable1BindingSource.DataMember = "DataTable1";
                //this.DataTable1BindingSource.DataSource = this.DataSet1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            #endregion
        }
    }
}
