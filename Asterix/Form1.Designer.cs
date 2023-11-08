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
            SuspendLayout();
            // 
            // buttonAbrirArchivo
            // 
            buttonAbrirArchivo.Location = new Point(55, 62);
            buttonAbrirArchivo.Name = "buttonAbrirArchivo";
            buttonAbrirArchivo.Size = new Size(94, 29);
            buttonAbrirArchivo.TabIndex = 0;
            buttonAbrirArchivo.Text = "Abrir";
            buttonAbrirArchivo.UseVisualStyleBackColor = true;
            buttonAbrirArchivo.Click += buttonAbrirArchivo_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(198, 66);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(556, 27);
            textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(buttonAbrirArchivo);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAbrirArchivo;
        private TextBox textBox1;
    }
}