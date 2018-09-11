using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTableSample
{
    public partial class Form1 : Form
    {
        public DataTable DataTable { get; set; } = new DataTable();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = DataTable;
            LoadDb();
            HideColumns();
            GetColumnNames();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
        }

        public void LoadDb()
        {
            using (var connection = GetConnection())
            {
                var query = "select * from features";
                var adapter = new SQLiteDataAdapter(query, connection);
                adapter.Fill(DataTable);
            }
        }

        private void HideColumns()
        {
            dataGridView1.Columns["id"].Visible = false;
        }


        private void UpdateDatabase()
        {
            using (var connection = GetConnection())
            {
                var query = "select * from features";
                var adapter = new SQLiteDataAdapter(query, connection);
                var builder = new SQLiteCommandBuilder(adapter);

                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.Update(DataTable);
            }
        }

        private static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(@"Data Source=features.db;");
        }

        private void GetColumnNames()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SQLiteCommand("select * from features", connection);
                var dataReader = command.ExecuteReader();
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    Debug.WriteLine(dataReader.GetName(i));
                }
            }
        }
    }

}
