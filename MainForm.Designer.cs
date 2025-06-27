namespace IISLogAnalyzer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.ComboBox filterFieldComboBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridView = new DataGridView();
            filterTextBox = new TextBox();
            browseButton = new Button();
            exportButton = new Button();
            filterFieldComboBox = new ComboBox();
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(14, 47);
            dataGridView.Margin = new Padding(4, 3, 4, 3);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(1162, 629);
            dataGridView.TabIndex = 0;
            // 
            // filterTextBox
            // 
            filterTextBox.Location = new Point(124, 13);
            filterTextBox.Margin = new Padding(4, 3, 4, 3);
            filterTextBox.Name = "filterTextBox";
            filterTextBox.PlaceholderText = "Type filter here";
            filterTextBox.Size = new Size(349, 23);
            filterTextBox.TabIndex = 1;
            filterTextBox.TextChanged += filterTextBox_TextChanged;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(14, 10);
            browseButton.Margin = new Padding(4, 3, 4, 3);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(88, 27);
            browseButton.TabIndex = 2;
            browseButton.Text = "Open Log";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // exportButton
            // 
            exportButton.Location = new Point(1071, 10);
            exportButton.Margin = new Padding(4, 3, 4, 3);
            exportButton.Name = "exportButton";
            exportButton.Size = new Size(105, 27);
            exportButton.TabIndex = 3;
            exportButton.Text = "Export to CSV";
            exportButton.UseVisualStyleBackColor = true;
            exportButton.Click += exportButton_Click;
            // 
            // filterFieldComboBox
            // 
            filterFieldComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterFieldComboBox.FormattingEnabled = true;
            filterFieldComboBox.Location = new Point(541, 16);
            filterFieldComboBox.Margin = new Padding(4, 3, 4, 3);
            filterFieldComboBox.Name = "filterFieldComboBox";
            filterFieldComboBox.Size = new Size(139, 23);
            filterFieldComboBox.TabIndex = 4;
            // 
            // chart
            // 
            chart.BackColor = Color.Transparent;
            chart.Location = new Point(14, 682);
            chart.Margin = new Padding(4, 3, 4, 3);
            chart.Name = "chart";
            chart.Size = new Size(1162, 10);
            chart.TabIndex = 5;
            chart.Text = "chart";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(482, 19);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 6;
            label1.Text = "Filter By:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1189, 705);
            Controls.Add(label1);
            Controls.Add(chart);
            Controls.Add(filterFieldComboBox);
            Controls.Add(exportButton);
            Controls.Add(browseButton);
            Controls.Add(filterTextBox);
            Controls.Add(dataGridView);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "IIS Log Analyzer - by Sean Sherman";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
    }
}
