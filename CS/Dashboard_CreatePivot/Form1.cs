using System;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess;

namespace Dashboard_CreatePivot {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private PivotDashboardItem CreatePivot(DataSource dataSource) {

            // Creates a pivot dashboard item and specifies its data source.
            PivotDashboardItem pivot = new PivotDashboardItem();
            pivot.DataSource = dataSource;

            // Specifies dimensions that provide pivot column and row headers.
            pivot.Columns.AddRange(new Dimension("Country"), new Dimension("Sales Person"));
            pivot.Rows.AddRange(new Dimension("CategoryName"), new Dimension("ProductName"));

            // Specifies measures whose data is used to calculate pivot cell values.
            pivot.Values.AddRange(new Measure("Extended Price"), new Measure("Quantity"));

            // Specifies the default expanded state of pivot column field values.
            pivot.AutoExpandColumnGroups = true;

            return pivot;
        }
        private void Form1_Load(object sender, EventArgs e) {

            // Creates a dashboard and sets it as the currently opened dashboad in the dashboard viewer.
            dashboardViewer1.Dashboard = new Dashboard();

            // Creates a data source and adds it to the dashboard data source collection.
            DataSource dataSource = new DataSource("Sales Person");
            dashboardViewer1.Dashboard.DataSources.Add(dataSource);

            // Creates a pivot dashboard item with the specified data source 
            // and adds it to the Items collection, to display it within the dashboard.
            PivotDashboardItem pivot = CreatePivot(dataSource);
            dashboardViewer1.Dashboard.Items.Add(pivot);

            // Reloads data in the data sources.
            dashboardViewer1.ReloadData();
        }
        private void dashboardViewer1_DataLoading(object sender, DataLoadingEventArgs e) {

            // Specifies data for the current data source.
            e.Data = (new nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData();
        }
    }
}
