namespace Windows_Dialog_Box_Generator
{
    public partial class TaskDlgControlDesigner : Form
    {
        public List<TaskDialogControl> TaskDlgControls = [];

        public TaskDlgControlDesigner()
        {
            InitializeComponent();
        }

        public TaskDlgControlDesigner(IEnumerable<TaskDialogControl> controls)
        {
            InitializeComponent();
            TaskDlgControls = [.. controls];

            RefreshList();
        }

        private void RefreshList()
        {
            listView1.Items.Clear();
            listView1.BeginUpdate();
            imgList.Images.Clear();

            imgList.Images.Add(Properties.Resources.icon_button);
            imgList.Images.Add(Properties.Resources.icon_button_uacshield);
            imgList.Images.Add(Properties.Resources.icon_cmdlink);
            imgList.Images.Add(Properties.Resources.icon_check);
            imgList.Images.Add(Properties.Resources.icon_radio);

            foreach (TaskDialogControl control in TaskDlgControls)
            {
                listView1.Items.Add(new ListViewItem
                {
                    Text = control switch // TaskDialogControl doesn't have a Text property, we have to check types instead
                    {
                        TaskDialogButton b => b.Text,
                        TaskDialogRadioButton r => r.Text,
                        TaskDialogVerificationCheckBox v => v.Text,
                        _ => string.Empty
                    },
                    ImageIndex = control switch
                    {
                        TaskDialogCommandLinkButton c => 2,
                        TaskDialogButton b => b.ShowShieldIcon ? 1 : 0,
                        TaskDialogRadioButton r => 4,
                        TaskDialogVerificationCheckBox v => 3,
                        _ => -1 // -1 = no icon, this is the default
                    },
                    Tag = control,
                    SubItems =
                    {
                        new ListViewItem.ListViewSubItem
                        {
                            Text = control switch
                            {
                                TaskDialogButton b => b.Enabled.ToString(),
                                TaskDialogRadioButton r => r.Enabled.ToString(),
                                // A TaskDialogVerificationCheckBox doesn't have Enabled
                                _ => string.Empty
                            }
                        },
                        new ListViewItem.ListViewSubItem
                        {
                            Text = control switch
                            {
                                TaskDialogButton b => string.Empty,
                                TaskDialogRadioButton r => r.Checked.ToString(),
                                TaskDialogVerificationCheckBox v => v.Checked.ToString(),
                                _ => string.Empty
                            }
                        }
                    }
                });
            }

            listView1.EndUpdate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dlg = new TaskDlgControlDesigner_ButtonMakeDlg();
            var result = dlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                TaskDialogButton button;

                if (dlg.radioButton1.Checked)
                {
                    button = dlg.comboBox1.SelectedIndex switch
                    {
                        0 => TaskDialogButton.Abort,
                        1 => TaskDialogButton.Cancel,
                        2 => TaskDialogButton.Close,
                        3 => TaskDialogButton.Continue,
                        4 => TaskDialogButton.Help,
                        5 => TaskDialogButton.Ignore,
                        6 => TaskDialogButton.No,
                        7 => TaskDialogButton.OK,
                        8 => TaskDialogButton.Retry,
                        9 => TaskDialogButton.TryAgain,
                        10 => TaskDialogButton.Yes,
                        _ => new TaskDialogButton("(Error)")
                    };

                    button.AllowCloseDialog = dlg.checkBox3.Checked;
                    button.Enabled = dlg.checkBox2.Checked;
                    button.ShowShieldIcon = dlg.checkBox1.Checked;
                }
                else
                {
                    if (dlg.radioButton3.Checked)
                    {
                        button = new TaskDialogButton(dlg.textBox1.Text, dlg.checkBox2.Checked, dlg.checkBox3.Checked)
                        {
                            ShowShieldIcon = dlg.checkBox1.Checked
                        };
                    }
                    else
                    {
                        button = new TaskDialogCommandLinkButton(dlg.textBox2.Text, dlg.textBox3.Text, dlg.checkBox2.Checked, dlg.checkBox3.Checked)
                        {
                            ShowShieldIcon = dlg.checkBox1.Checked
                        };
                    }
                }

                if (button != null)
                {
                    TaskDlgControls.Add(button);

                    RefreshList();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                if (lvi.Tag is TaskDialogControl c)
                {
                    TaskDlgControls.Remove(c);
                }
            }

            RefreshList();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Enabled = listView1.SelectedItems.Count > 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dlg = new TaskDlgControlDesigner_RadioMakeDlg();

            // Smart checking: if a radio button already exists and is checked, uncheck this one
            // otherwise, check it
            foreach (TaskDialogRadioButton rb in TaskDlgControls.OfType<TaskDialogRadioButton>())
            {
                if (rb.Checked)
                {
                    dlg.checkBox1.Checked = false;
                    break;
                }
            }

            var result = dlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                TaskDlgControls.Add(new TaskDialogRadioButton
                {
                    Text = dlg.textBox1.Text,
                    Checked = dlg.checkBox1.Checked,
                    Enabled = dlg.checkBox2.Checked
                });
                RefreshList();
            }
        }
    }
}
