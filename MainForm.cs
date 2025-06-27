using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IISLogAnalyzer
{
    public partial class MainForm : Form
    {
        private DataTable logTable = new DataTable();
        private List<string> dynamicColumns = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += MainForm_DragEnter;
            DragDrop += MainForm_DragDrop;
        }

        private void LoadLogFile(string filePath)
        {
            logTable = new DataTable();
            dynamicColumns.Clear();
            filterFieldComboBox.Items.Clear();

            var lines = File.ReadAllLines(filePath);
            string[] fields = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("#Fields:"))
                {
                    fields = line.Substring(8).Trim().Split(' ');
                    break;
                }
            }

            if (fields == null)
            {
                MessageBox.Show("Could not find #Fields line in the log file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var field in fields)
            {
                string colName = NormalizeFieldName(field);
                logTable.Columns.Add(colName, colName.Equals("TimeTaken", StringComparison.OrdinalIgnoreCase) ? typeof(int) : typeof(string));
                dynamicColumns.Add(colName);
                filterFieldComboBox.Items.Add(colName);
            }

            filterFieldComboBox.SelectedIndex = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith("#")) continue;
                var parts = Regex.Split(line.Trim(), @"\s+");
                if (parts.Length != fields.Length) continue;

                var row = logTable.NewRow();
                for (int i = 0; i < fields.Length; i++)
                {
                    string colName = NormalizeFieldName(fields[i]);
                    if (colName == "TimeTaken" && int.TryParse(parts[i], out int time))
                        row[colName] = time;
                    else
                        row[colName] = parts[i];
                }
                logTable.Rows.Add(row);
            }

            dataGridView.DataSource = logTable;

            if (logTable.Columns.Contains("TimeTaken"))
            {
                dataGridView.Sort(dataGridView.Columns["TimeTaken"], System.ComponentModel.ListSortDirection.Descending);
            }

            UpdateChart();
        }

        private string NormalizeFieldName(string field)
        {
            return field.Replace("(", "-").Replace(")", "").Replace("_", "-");
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
                LoadLogFile(files[0]);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                LoadLogFile(ofd.FileName);
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            using SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV|*.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var sb = new StringBuilder();
                var columns = logTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
                sb.AppendLine(string.Join(",", columns));
                foreach (DataRow row in logTable.Rows)
                {
                    var fields = row.ItemArray.Select(f => f.ToString().Replace(",", " "));
                    sb.AppendLine(string.Join(",", fields));
                }
                File.WriteAllText(sfd.FileName, sb.ToString());
            }
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            string selectedField = filterFieldComboBox.SelectedItem?.ToString();
            string filterText = filterTextBox.Text;

            if (string.IsNullOrWhiteSpace(filterText) || string.IsNullOrWhiteSpace(selectedField))
            {
                logTable.DefaultView.RowFilter = "";
            }
            else
            {
                logTable.DefaultView.RowFilter = $"[{selectedField}] LIKE '%{filterText.Replace("'", "''")}%'";
            }
        }

        private void UpdateChart()
        {
            if (!logTable.Columns.Contains("cs-uri-stem") || !logTable.Columns.Contains("TimeTaken"))
                return;

            var grouped = logTable.AsEnumerable()
                .Where(r => !r.IsNull("cs-uri-stem") && !r.IsNull("TimeTaken"))
                .GroupBy(row => row.Field<string>("cs-uri-stem"))
                .Select(g => new
                {
                    Endpoint = g.Key,
                    AvgTime = g.Average(r => r.Field<int>("TimeTaken"))
                })
                .OrderByDescending(x => x.AvgTime)
                .Take(10)
                .ToList();

            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(new ChartArea("Main"));

            var series = new Series("Avg Time")
            {
                ChartType = SeriesChartType.Bar
            };

            foreach (var item in grouped)
            {
                series.Points.AddXY(item.Endpoint, item.AvgTime);
            }

            chart.Series.Add(series);
        }
    }
}
