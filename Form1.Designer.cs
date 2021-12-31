
using System.Drawing;

namespace ASEProgrammingLanguageEnvironment
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OutputWindow = new System.Windows.Forms.PictureBox();
            this.ProgramWindow = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.btn_command = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.commandLine = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.OutputWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // OutputWindow
            // 
            this.OutputWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputWindow.Location = new System.Drawing.Point(378, 29);
            this.OutputWindow.Margin = new System.Windows.Forms.Padding(2);
            this.OutputWindow.Name = "OutputWindow";
            this.OutputWindow.Size = new System.Drawing.Size(388, 368);
            this.OutputWindow.TabIndex = 0;
            this.OutputWindow.TabStop = false;
            this.OutputWindow.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // ProgramWindow
            // 
            this.ProgramWindow.Location = new System.Drawing.Point(22, 29);
            this.ProgramWindow.Margin = new System.Windows.Forms.Padding(2);
            this.ProgramWindow.Name = "ProgramWindow";
            this.ProgramWindow.Size = new System.Drawing.Size(283, 369);
            this.ProgramWindow.TabIndex = 2;
            this.ProgramWindow.Text = "";
            this.ProgramWindow.Font = new Font("Segoe UI",10);
            //this.ProgramWindow.TextChanged += new System.EventHandler(this.Program_Text_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Program";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Command Line";
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(310, 374);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(62, 23);
            this.btn_Execute.TabIndex = 5;
            this.btn_Execute.Text = "Run";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // btn_command
            // 
            this.btn_command.Location = new System.Drawing.Point(311, 436);
            this.btn_command.Name = "btn_command";
            this.btn_command.Size = new System.Drawing.Size(62, 23);
            this.btn_command.TabIndex = 6;
            this.btn_command.Text = "Execute";
            this.btn_command.UseVisualStyleBackColor = true;
            this.btn_command.Click += new System.EventHandler(this.btn_command_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Display";
            // 
            // commandLine
            // 
            this.commandLine.Location = new System.Drawing.Point(22, 438);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(283, 20);
            this.commandLine.TabIndex = 8;
            this.commandLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandLine_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 488);
            this.Controls.Add(this.commandLine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_command);
            this.Controls.Add(this.btn_Execute);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProgramWindow);
            this.Controls.Add(this.OutputWindow);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ASEP";
            ((System.ComponentModel.ISupportInitialize)(this.OutputWindow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox OutputWindow;
        private System.Windows.Forms.RichTextBox ProgramWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Execute;
        private System.Windows.Forms.Button btn_command;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox commandLine;
    }
}

