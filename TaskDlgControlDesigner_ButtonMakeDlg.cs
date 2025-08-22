namespace Windows_Dialog_Box_Generator
{
    public partial class TaskDlgControlDesigner_ButtonMakeDlg : Form
    {
        public TaskDlgControlDesigner_ButtonMakeDlg()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = radioButton2.Checked;
            comboBox1.Enabled = radioButton1.Checked;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = radioButton3.Checked;
            textBox2.Enabled = radioButton4.Checked;
            textBox3.Enabled = radioButton4.Checked;
        }
    }
}
