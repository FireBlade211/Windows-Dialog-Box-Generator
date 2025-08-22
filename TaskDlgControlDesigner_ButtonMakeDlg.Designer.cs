namespace Windows_Dialog_Box_Generator
{
    partial class TaskDlgControlDesigner_ButtonMakeDlg
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
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            comboBox1 = new ComboBox();
            groupBox1 = new GroupBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            checkBox3 = new CheckBox();
            textBox1 = new TextBox();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(14, 12);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(200, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Use a built-in button (localized)...";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(14, 130);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(157, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "...or use a custom button";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Abort", "Cancel", "Close", "Continue", "Help", "Ignore", "No", "OK", "Retry", "Try again", "Yes" });
            comboBox1.Location = new Point(13, 37);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(144, 23);
            comboBox1.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Enabled = false;
            groupBox1.Location = new Point(14, 160);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(320, 234);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Custom button";
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Location = new Point(16, 198);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Note Text...";
            textBox3.Size = new Size(145, 23);
            textBox3.TabIndex = 5;
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(16, 169);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Main Text...";
            textBox2.Size = new Size(145, 23);
            textBox2.TabIndex = 4;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new Point(221, 400);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(113, 19);
            checkBox3.TabIndex = 5;
            checkBox3.Text = "Can close dialog";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 58);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Text...";
            textBox1.Size = new Size(145, 23);
            textBox1.TabIndex = 2;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(16, 144);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(104, 19);
            radioButton4.TabIndex = 1;
            radioButton4.Text = "Command link";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Checked = true;
            radioButton3.Location = new Point(16, 31);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(61, 19);
            radioButton3.TabIndex = 0;
            radioButton3.TabStop = true;
            radioButton3.Text = "Button";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(88, 400);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(116, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Show UAC shield";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(14, 400);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(68, 19);
            checkBox2.TabIndex = 4;
            checkBox2.Text = "Enabled";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(257, 438);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(176, 438);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // TaskDlgControlDesigner_ButtonMakeDlg
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(346, 471);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(groupBox1);
            Controls.Add(checkBox1);
            Controls.Add(comboBox1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TaskDlgControlDesigner_ButtonMakeDlg";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Add button";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox1;
        private Button button1;
        private Button button2;
        public RadioButton radioButton1;
        public RadioButton radioButton2;
        public ComboBox comboBox1;
        public TextBox textBox3;
        public TextBox textBox2;
        public CheckBox checkBox1;
        public TextBox textBox1;
        public RadioButton radioButton4;
        public RadioButton radioButton3;
        public CheckBox checkBox2;
        public CheckBox checkBox3;
    }
}