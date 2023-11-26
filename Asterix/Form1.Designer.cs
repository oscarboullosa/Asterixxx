namespace Asterix
{
    partial class Form1
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
            buttonAbrirArchivo = new Button();
            textBox1 = new TextBox();
            dataRecordTable = new DataGridView();
            dataRecordTableOneClick = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataRecordTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataRecordTableOneClick).BeginInit();
            SuspendLayout();
            // 
            // buttonAbrirArchivo
            // 
            buttonAbrirArchivo.Location = new Point(12, 12);
            buttonAbrirArchivo.Name = "buttonAbrirArchivo";
            buttonAbrirArchivo.Size = new Size(65, 27);
            buttonAbrirArchivo.TabIndex = 0;
            buttonAbrirArchivo.Text = "Abrir";
            buttonAbrirArchivo.UseVisualStyleBackColor = true;
            buttonAbrirArchivo.Click += buttonAbrirArchivo_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(83, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(556, 27);
            textBox1.TabIndex = 1;
            // 
            // dataRecordTable
            // 
            dataRecordTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataRecordTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataRecordTable.Location = new Point(12, 56);
            dataRecordTable.Name = "dataRecordTable";
            dataRecordTable.RowHeadersWidth = 51;
            dataRecordTable.RowTemplate.Height = 29;
            dataRecordTable.Size = new Size(350, 196);
            dataRecordTable.TabIndex = 2;
            dataRecordTable.CellClick += dataRecordTable_CellClick;
            dataRecordTable.ColumnAdded += dataRecordTable_ColumnsAdded;
            dataRecordTable.RowsAdded += dataRecordTable_RowsAdded;
            // 
            // dataRecordTableOneClick
            // 
            dataRecordTableOneClick.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataRecordTableOneClick.Location = new Point(12, 289);
            dataRecordTableOneClick.Name = "dataRecordTableOneClick";
            dataRecordTableOneClick.RowHeadersWidth = 51;
            dataRecordTableOneClick.RowTemplate.Height = 29;
            dataRecordTableOneClick.Size = new Size(776, 114);
            dataRecordTableOneClick.TabIndex = 3;
            dataRecordTableOneClick.CellDoubleClick += dataRecordTableOneClick_CellDoubleClick;
            // 
            // button1
            // 
            button1.Location = new Point(379, 105);
            button1.Name = "button1";
            button1.Size = new Size(183, 85);
            button1.TabIndex = 4;
            button1.Text = "Mapa";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(590, 105);
            button2.Name = "button2";
            button2.Size = new Size(169, 85);
            button2.TabIndex = 5;
            button2.Text = "CSV";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataRecordTableOneClick);
            Controls.Add(dataRecordTable);
            Controls.Add(textBox1);
            Controls.Add(buttonAbrirArchivo);
            Name = "Form1";
            Text = "Form1";
            DoubleClick += Form1_DoubleClick;
            ((System.ComponentModel.ISupportInitialize)dataRecordTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataRecordTableOneClick).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAbrirArchivo;
        private TextBox textBox1;
        private DataGridView dataRecordTable;
        private DataGridView dataRecordTableOneClick;
        private Button button1;
        private Button button2;
    }
}