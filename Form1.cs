using FireBlade.WinInteropUtils;
using FireBlade.WinInteropUtils.Dialogs;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;

namespace Windows_Dialog_Box_Generator
{
    public partial class Form1 : Form
    {
        private string taskDlg_footnoteIconPath = string.Empty;
        private int? taskDlg_footnoteIconIndex = null;

        private string taskDlg_iconPath;
        private int taskDlg_iconIndex = 0;

        private List<TaskDialogControl> TaskDlgControls = [];

        public Form1()
        {
            InitializeComponent();

            //comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            taskDlg_iconPath = GetImageresPath();

            numericUpDown4.Minimum = int.MinValue;
            numericUpDown4.Maximum = int.MaxValue;

            numericUpDown5.Minimum = int.MinValue;
            numericUpDown5.Maximum = int.MaxValue;

            numericUpDown6.Minimum = int.MinValue;
            numericUpDown6.Maximum = int.MaxValue;

            numericUpDown7.Maximum = int.MaxValue;
            numericUpDown7.Minimum = int.MinValue;
            numericUpDown8.Maximum = int.MaxValue;
            numericUpDown8.Minimum = int.MinValue;

            numericUpDown13.Maximum = IconHelper.CountIcons(textBox14.Text);

            ApplyIconsToRadioButtons();

            comboBox4.Items.AddRange([.. Enum.GetValues<Environment.SpecialFolder>().Select(f => (SpecialFolderComboBoxItem)f)]);

            IconDlgUpdateDefaultIconPreview();

            mbCultureCombo.Items.AddRange(CultureInfo.GetCultures(CultureTypes.AllCultures).Select(c => new CultureComboBoxItem
            {
                Culture = c
            }).ToArray());
        }

        private string GetImageresPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "imageres.dll");
        }

        private void ApplyIconsToRadioButtons()
        {
            //var sysRoot = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            //var imageres = sysRoot + @"\System32\imageres.dll";
            //var iconInfo = Icon.ExtractIcon(imageres, 81, true); // Set third param to true for small icon
            //var iconWarning = Icon.ExtractIcon(imageres, 84, true);
            //var iconError = Icon.ExtractIcon(imageres, 98, true);
            //var iconQuestion = Icon.ExtractIcon(imageres, 94, 16);

            //if (iconInfo != null && iconWarning != null && iconError != null && iconQuestion != null)
            //{
            //    radioButton15.Image = iconInfo.ToBitmap(); // needs ToBitmap because requires Image not Icon
            //    radioButton16.Image = iconWarning.ToBitmap();
            //    radioButton17.Image = iconError.ToBitmap();
            //    radioButton18.Image = iconQuestion.ToBitmap();
            //}

            radioButton15.Image = StockIconHelper.GetIcon(StockIcon.Info, StockIconOptions.SmallIcon).ToBitmap();
            radioButton16.Image = StockIconHelper.GetIcon(StockIcon.Warning, StockIconOptions.SmallIcon).ToBitmap();
            radioButton17.Image = StockIconHelper.GetIcon(StockIcon.Error, StockIconOptions.SmallIcon).ToBitmap();

            radioButton18.Image = StockIconHelper.GetIcon(StockIcon.Help, StockIconOptions.SmallIcon).ToBitmap();

            radioButton25.Image = StockIconHelper.GetIcon(StockIcon.Info, StockIconOptions.SmallIcon).ToBitmap();
            radioButton26.Image = StockIconHelper.GetIcon(StockIcon.Warning, StockIconOptions.SmallIcon).ToBitmap();
            radioButton27.Image = StockIconHelper.GetIcon(StockIcon.Error, StockIconOptions.SmallIcon).ToBitmap();
            radioButton28.Image = StockIconHelper.GetIcon(StockIcon.Shield, StockIconOptions.SmallIcon).ToBitmap();
            radioButton29.Image = StockIconHelper.GetIcon(StockIcon.Shield, StockIconOptions.SmallIcon).ToBitmap();
            radioButton30.Image = StockIconHelper.GetIcon(StockIcon.Shield, StockIconOptions.SmallIcon).ToBitmap();
            radioButton31.Image = Icon.ExtractIcon(GetImageresPath(), -106, 16)?.ToBitmap();
            radioButton32.Image = Icon.ExtractIcon(GetImageresPath(), -107, 16)?.ToBitmap();
            radioButton33.Image = Icon.ExtractIcon(GetImageresPath(), -105, 16)?.ToBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TaskDialog.ShowDialog(this, new TaskDialogPage
            {
                Caption = "Why is the Question icon not recommended for use?",
                Heading = "Why is the Question icon not recommended for use?",
                Icon = TaskDialogIcon.Information,
                Text = "The question mark message icon is no longer recommended because it does not clearly represent a specific type of message and because" +
                " the phrasing of a message as a question could apply to any message type.\n\nIn addition, users can confuse the question mark symbol" +
                " with a help information symbol. Therefore, do not use this question mark symbol in your message boxes." +
                " The system continues to support its inclusion only for backward compatibility.",
                Buttons = [TaskDialogButton.OK],
                AllowCancel = true
            });
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TaskDialog.ShowDialog(this, new TaskDialogPage
            {
                Caption = "Why shouldn't I use the Service Notification option?",
                Heading = "Why shouldn't I use the Service Notification option?",
                Icon = TaskDialogIcon.Information,
                Text = "The Service Notification message box option tells Windows to start the message box on Session 0.\n\nBack in the Windows XP and earlier days, Session 0 was" +
                " assigned to the first user that logged in on the system. However, this was a big security risk. Ever since Windows Vista, Session 0 has always been reserved" +
                " for the system. But, some old apps still asked for user input on Session 0.\n\nMicrosoft realized this, so they made a feature called Interactive Services Detection." +
                " The feature detected this and automatically offered the user to temporarily switch to session 0 to view the message.\n\nHowever, since Windows 10, the feature has been" +
                " removed, and as a result, there is no way to access windows on Session 0. Therefore, do not use this option.",
                Buttons = [TaskDialogButton.OK],
                AllowCancel = true
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (helpFileDialog.ShowDialog() == DialogResult.OK)
            {
                //msgBoxHelpFileTextBox.Text = helpFileDialog.FileName;
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Checked changed!");
            //label10.Text = UIHelper.GetCheckedRadioButton(panel1)?.Tag?.ToString() ?? string.Empty;

            //numericUpDown2.Maximum = UIHelper.GetCheckedRadioButton(panel1)?.Name switch
            //{
            //    "radioButton11" => int.MaxValue,
            //    "radioButton12" => uint.MaxValue,
            //    "radioButton13" => nint.MaxValue,
            //    "radioButton22" => ulong.MaxValue,
            //    "radioButton14" => short.MaxValue,
            //    "radioButton20" => ushort.MaxValue,
            //    "radioButton21" => long.MaxValue,
            //    // This should technically be first, but it needs to be put last because of the _
            //    "radioButton10" or "radioButton23" or _ => 10000000000000000000,
            //};

            //numericUpDown2.Minimum = UIHelper.GetCheckedRadioButton(panel1)?.Name switch
            //{
            //    "radioButton11" => int.MinValue,
            //    "radioButton12" => uint.MinValue,
            //    "radioButton13" => nint.MinValue,
            //    "radioButton22" => ulong.MinValue,
            //    "radioButton14" => short.MinValue,
            //    "radioButton20" => ushort.MinValue,
            //    "radioButton21" => long.MinValue,
            //    "radioButton10" or "radioButton23" or _ => 0,
            //};
        }

        private WinMessageBoxButtons MsgBoxGetButtons()
        {
            return UIHelper.GetCheckedRadioButton(groupBox1)?.Tag?.ToString() switch
            {
                "o" => WinMessageBoxButtons.Ok,
                "oc" => WinMessageBoxButtons.OkCancel,
                "rc" => WinMessageBoxButtons.RetryCancel,
                "ctc" => WinMessageBoxButtons.CancelRetryContinue,
                "ari" => WinMessageBoxButtons.AbortRetryIgnore,
                "yn" => WinMessageBoxButtons.YesNo,
                "ync" => WinMessageBoxButtons.YesNoCancel,
                _ => WinMessageBoxButtons.Ok // Could've been merged with "o" ("o" or _ => MessageBoxButtons.OK), but this keeps it ordered
            };
        }

        private WinMessageBoxIcon MsgBoxGetIcon()
        {
#pragma warning disable CS0618
            return UIHelper.GetCheckedRadioButton(groupBox2)?.Tag?.ToString() switch
            {
                "n" => WinMessageBoxIcon.None,
                "i" => WinMessageBoxIcon.Information,
                "w" => WinMessageBoxIcon.Warning,
                "e" => WinMessageBoxIcon.Error,
                "q" => WinMessageBoxIcon.Question,
                _ => WinMessageBoxIcon.None
            };
#pragma warning restore
        }

        //private WinMessageBoxOptions MsgBoxGetOptions()
        //{
        //    MessageBoxOptions options = 0;

        //    if (checkBox1.Checked)
        //    {
        //        options |= MessageBoxOptions.RtlReading;
        //    }

        //    if (checkBox2.Checked)
        //    {
        //        options |= MessageBoxOptions.ServiceNotification;
        //    }

        //    if (checkBox3.Checked)
        //    {
        //        options |= MessageBoxOptions.RightAlign;
        //    }

        //    if (checkBox4.Checked)
        //    {
        //        options |= MessageBoxOptions.DefaultDesktopOnly;
        //    }

        //    return options;
        //}

        //private HelpNavigator MsgBoxGetHelpNavigator()
        //{
        //    // Topic
        //    // Find
        //    // Table of contents
        //    // Topic ID
        //    // Index
        //    // Associate index
        //    // Keyword index

        //    return comboBox1.SelectedIndex switch
        //    {
        //        0 => HelpNavigator.Topic,
        //        1 => HelpNavigator.Find,
        //        2 => HelpNavigator.TableOfContents,
        //        3 => HelpNavigator.TopicId,
        //        4 => HelpNavigator.Index,
        //        5 => HelpNavigator.AssociateIndex,
        //        6 => HelpNavigator.KeywordIndex,
        //        _ => HelpNavigator.Topic
        //    };
        //}

        private WinMessageBoxModality MsgBoxGetModality()
        {
            return UIHelper.GetCheckedRadioButton(groupBox3)?.Name switch
            {
                "radioButton8" => WinMessageBoxModality.ApplicationModal,
                "radioButton9" => WinMessageBoxModality.TaskModal,
                "radioButton10" => WinMessageBoxModality.SystemModal,
                _ => WinMessageBoxModality.ApplicationModal
            };
        }

        private Icon? TaskDlgGetCustomIcon()
        {
            var mapper = new IconResourceMapper(taskDlg_iconPath);

            var icon = Icon.ExtractIcon(taskDlg_iconPath, mapper.GetIconIndex((ushort)taskDlg_iconIndex));

            return icon;
        }

        private TaskDialogIcon? TaskDlgGetIcon()
        {
            var isDefault = radioButton35.Checked;

            if (radioButton34.Checked)
            {
                isDefault = radioButton35.Checked && !taskDlg_iconPath.Equals(GetImageresPath(), StringComparison.OrdinalIgnoreCase);
            }

            if (isDefault)
            {
                return UIHelper.GetCheckedRadioButton(groupBox6)?.Name switch
                {
                    "radioButton25" => TaskDialogIcon.Information,
                    "radioButton26" => TaskDialogIcon.Warning,
                    "radioButton27" => TaskDialogIcon.Error,
                    "radioButton28" => TaskDialogIcon.Shield,
                    "radioButton29" => TaskDialogIcon.ShieldBlueBar,
                    "radioButton30" => TaskDialogIcon.ShieldGrayBar,
                    "radioButton31" => TaskDialogIcon.ShieldSuccessGreenBar,
                    "radioButton32" => TaskDialogIcon.ShieldWarningYellowBar,
                    "radioButton33" => TaskDialogIcon.ShieldErrorRedBar,
                    "radioButton34" => new TaskDialogIcon(TaskDlgGetCustomIcon()!),
                    "radioButton24" or _ => null
                };
            }
            else
            {
                return UIHelper.GetCheckedRadioButton(groupBox7)?.Name switch
                {
                    "radioButton36" => TaskDialogIcon.ShieldGrayBar,
                    "radioButton40" => TaskDialogIcon.ShieldWarningYellowBar,
                    "radioButton41" => TaskDialogIcon.ShieldBlueBar,
                    "radioButton37" => TaskDialogIcon.ShieldErrorRedBar,
                    "radioButton39" => TaskDialogIcon.ShieldSuccessGreenBar,
                    "radioButton38" or _ => null
                };
            }
        }

        private async void showDlgButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabControl1.SelectedTab?.Tag)
                {
                    case "msgbox":
                        {
                            //DialogResult result;

                            //if (checkBox5.Checked)
                            //{
                            //    if (!File.Exists(msgBoxHelpFileTextBox.Text))
                            //    {
                            //        DialogResult r = MessageBox.Show($"Warning: The help file '{msgBoxHelpFileTextBox.Text}' doesn't exist. Continuing can lead to weird," +
                            //            $" undefined behavior and crashes. Are you sure you want to" +
                            //            $" continue?", "File not found warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                            //        if (r != DialogResult.OK)
                            //        {
                            //            return;
                            //        }
                            //    }

                            //    if (radioButton8.Checked)
                            //    {
                            //        result = MessageBox.Show(msgBoxTextBox.Text, !string.IsNullOrEmpty(msgBoxCaptionBox.Text) ? msgBoxCaptionBox.Text : null, MsgBoxGetButtons(),
                            //            MsgBoxGetIcon(), MsgBoxGetDefaultButton(), MsgBoxGetOptions(), msgBoxHelpFileTextBox.Text, MsgBoxGetHelpNavigator());
                            //    }
                            //    else
                            //    {
                            //        result = MessageBox.Show(msgBoxTextBox.Text, !string.IsNullOrEmpty(msgBoxCaptionBox.Text) ? msgBoxCaptionBox.Text : null, MsgBoxGetButtons(),
                            //            MsgBoxGetIcon(), MsgBoxGetDefaultButton(), MsgBoxGetOptions(), msgBoxHelpFileTextBox.Text, textBox1.Text);
                            //    }
                            //}
                            //else
                            //{
                            //    result = MessageBox.Show(msgBoxTextBox.Text, !string.IsNullOrEmpty(msgBoxCaptionBox.Text) ? msgBoxCaptionBox.Text : null, MsgBoxGetButtons(),
                            //        MsgBoxGetIcon(), MsgBoxGetDefaultButton(), MsgBoxGetOptions());
                            //}

                            var mb = new WinMessageBox
                            {
                                Text = msgBoxTextBox.Text,
                                Caption = string.IsNullOrEmpty(msgBoxCaptionBox.Text) ? null : msgBoxCaptionBox.Text,
                                Culture = (mbCultureCombo.SelectedItem as CultureComboBoxItem)?.Culture,
                                DefaultButton = (int)numericUpDown1.Value,
                                SetForeground = checkBox53.Checked,
                                RightAlign = checkBox3.Checked,
                                Buttons = MsgBoxGetButtons(),
                                DefaultDesktopOnly = checkBox4.Checked,
                                Icon = MsgBoxGetIcon(),
                                Modality = MsgBoxGetModality(),
                                RightToLeft = checkBox1.Checked,
                                ServiceNotification = checkBox2.Checked,
                                ShowHelp = checkBox5.Checked,
                                TopMost = checkBox54.Checked,
                                ContextHelpId = checkBox55.Checked ? (nuint)numericUpDown2.Value : null
                            };

                            mb.OnHelp += (s, e) =>
                            {
                                OutputLogsBox.Text += $"Help requested at mouse position {e.MousePos} with help context ID: {e.ContextId}\r\n";
                            };

                            if (Window.FromHandle(Handle) is Window wnd)
                            {
                                var result = mb.Show(wnd);
                                OutputLogsBox.Text += $"Message box result: {result}\r\n";
                            }

                            break;
                        }

                    case "taskdlg":
                        {
                            #region Assemble the Task Dialog Page
                            var page = new TaskDialogPage
                            {
                                Caption = textBox3.Text,
                                Heading = textBox4.Text,
                                Text = textBox5.Text,
                                Icon = TaskDlgGetIcon(),
                                SizeToContent = checkBox10.Checked,
                                AllowCancel = checkBox11.Checked,
                                AllowMinimize = checkBox12.Checked,
                                EnableLinks = checkBox13.Checked
                            };

                            var isDefault = radioButton35.Checked;

                            if (radioButton34.Checked)
                            {
                                isDefault = radioButton35.Checked && !taskDlg_iconPath.Equals(GetImageresPath(), StringComparison.OrdinalIgnoreCase);
                            }

                            if (!isDefault) // This means this is a banner override icon
                            {
                                page.Created += (s, e) =>
                                {
                                    TaskDialog? dialog = page.BoundDialog;
                                    if (dialog != null)
                                    {
                                        IntPtr hwnd = dialog.Handle;
                                        try
                                        {
                                            Debug.WriteLine("Path: " + taskDlg_iconPath);

                                            // We can update the icon using a SendMessage call. But we must specify the icon via ID, not an object or hIcon handle
                                            // We do NOT use the negative of the ID, since the API is doing other stuff with the ID and handles it automatically
                                            Window.FromHandle(hwnd)?.SendMessage((uint)TDM.UPDATE_ICON, nuint.Zero,
                                                new nint(UIHelper.GetCheckedRadioButton(groupBox6)?.Name switch
                                                {
                                                    "radioButton25" => 81,
                                                    "radioButton26" => 84,
                                                    "radioButton27" => 98,
                                                    "radioButton28" or "radioButton29" or "radioButton30" => 78,
                                                    "radioButton31" => 106,
                                                    "radioButton32" => 107,
                                                    "radioButton33" => 105,
                                                    "radioButton34" => new IconIndexMapper(taskDlg_iconPath).GetResourceIdFromPickIconIndex(taskDlg_iconIndex) - 2, // Since we already know the icon is from imageres, we can do this
                                                    _ => 89
                                                }));
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error updating icon: " + ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                };
                            }

                            if (checkBox8.Checked)
                            {
                                page.Footnote = new TaskDialogFootnote(textBox6.Text);

                                if (!string.IsNullOrEmpty(taskDlg_footnoteIconPath) && taskDlg_footnoteIconIndex != null)
                                {
                                    page.Footnote.Icon = new TaskDialogIcon(Icon.ExtractIcon(taskDlg_footnoteIconPath, (int)taskDlg_footnoteIconIndex, 16)!);
                                }
                            }

                            if (checkBox6.Checked)
                            {
                                page.Expander = new TaskDialogExpander
                                {
                                    Text = textBox7.Text,
                                    CollapsedButtonText = !string.IsNullOrEmpty(textBox8.Text) ? textBox8.Text : null,
                                    ExpandedButtonText = !string.IsNullOrEmpty(textBox9.Text) ? textBox9.Text : null,
                                    Expanded = checkBox7.Checked,
                                    Position = comboBox2.SelectedIndex == 0 ? TaskDialogExpanderPosition.AfterText : TaskDialogExpanderPosition.AfterFootnote
                                };
                            }

                            if (comboBox3.SelectedIndex != 0)
                            {
                                page.ProgressBar = new TaskDialogProgressBar
                                {
                                    Minimum = (int)numericUpDown5.Value,
                                    Maximum = (int)numericUpDown4.Value,
                                    Value = (int)numericUpDown3.Value,
                                    MarqueeSpeed = (int)numericUpDown6.Value,
                                    State = comboBox3.SelectedIndex switch
                                    {
                                        1 => TaskDialogProgressBarState.Normal,
                                        2 => TaskDialogProgressBarState.Paused,
                                        3 => TaskDialogProgressBarState.Error,
                                        4 => TaskDialogProgressBarState.Marquee,
                                        5 => TaskDialogProgressBarState.MarqueePaused,
                                        _ => TaskDialogProgressBarState.Normal
                                    }
                                };
                            }

                            int verifCount = 0;
                            foreach (TaskDialogControl control in TaskDlgControls)
                            {
                                var clone = control.Clone();

                                if (clone != null)
                                {
                                    switch (clone)
                                    {
                                        case TaskDialogButton b:
                                            page.Buttons.Add(b);
                                            break;
                                        case TaskDialogRadioButton b:
                                            page.RadioButtons.Add(b);
                                            break;
                                        case TaskDialogVerificationCheckBox v:
                                            verifCount++;
                                            if (verifCount > 1)
                                            {
                                                throw new ArgumentException("Only one verification check box can be applied on a task dialog.");
                                            }
                                            page.Verification = v;
                                            break;
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(textBox10.Text))
                            {
                                page.Verification = new TaskDialogVerificationCheckBox
                                {
                                    Text = textBox10.Text,
                                    Checked = checkBox14.Checked
                                };
                            }

                            var navigToLinks = checkBox15.Checked;

                            page.LinkClicked += (s, e) =>
                            {
                                OutputLogsBox.Text += $"Task dialog: Link clicked with href: {e.LinkHref}\r\n";

                                if (navigToLinks && Uri.TryCreate(e.LinkHref, new UriCreationOptions(), out Uri? uri))
                                {
                                    Process.Start(new ProcessStartInfo
                                    {
                                        FileName = e.LinkHref,
                                        UseShellExecute = true
                                    });
                                }
                            };

                            #endregion

                            TaskDialogButton result = null!;

                            if (checkBox9.Checked)
                            {
                                result = TaskDialog.ShowDialog(page);
                            }
                            else
                            {
                                result = TaskDialog.ShowDialog(this, page);
                            }

                            OutputLogsBox.Text += $"Task dialog result: {result.Text}\r\n";
                            break;
                        }

                    case "col":
                        {
                            var color = colorDialog1.Color;
                            var result = colorDialog1.ShowDialog();
                            OutputLogsBox.Text += $"Color dialog result: {result}\r\n";

                            if (result == DialogResult.OK)
                            {
                                OutputLogsBox.Text += $"Chosen color: {colorDialog1.Color}\r\n";
                            }

                            colorDialog1.Color = color; // Restore previous color
                            break;
                        }

                    case "font":
                        {
                            var color = fontDialog1.Color;
                            var font = fontDialog1.Font;

                            var result = fontDialog1.ShowDialog();

                            OutputLogsBox.Text += $"Font dialog result: {result}\r\n";

                            if (result == DialogResult.OK)
                            {
                                OutputLogsBox.Text += $"Chosen font: {fontDialog1.Font}\r\n";
                                OutputLogsBox.Text += $"(Font Dialog) Chosen color: {fontDialog1.Color}\r\n";

                                // Update the demo box
                                richTextBox1.SelectionFont = fontDialog1.Font;
                                richTextBox1.SelectionColor = fontDialog1.Color;
                            }

                            fontDialog1.Font = font;
                            fontDialog1.Color = color;

                            break;
                        }
                    case "dirpick":
                        {
                            var oldPath = folderBrowserDialog1.SelectedPath;
                            var result = folderBrowserDialog1.ShowDialog();

                            OutputLogsBox.Text += $"Folder dialog result: {result}\r\n";

                            if (result == DialogResult.OK)
                            {
                                OutputLogsBox.Text += $"Chosen path: {folderBrowserDialog1.SelectedPath}\r\n";
                            }

                            folderBrowserDialog1.SelectedPath = oldPath;

                            break;
                        }
                    case "pageset":
                        {
                            var result = pageSetupDialog1.ShowDialog();

                            OutputLogsBox.Text += $"Page setup dialog result: {result}\r\n";
                            if (result == DialogResult.OK)
                            {
                                var orientStr = printDocument1.DefaultPageSettings.Landscape ? "Landscape" : "Portrait";
                                OutputLogsBox.Text += $"Orientation: {orientStr}\r\n";
                                OutputLogsBox.Text += $"Paper source: {printDocument1.DefaultPageSettings.PaperSource.SourceName}\r\n";
                                OutputLogsBox.Text += $"Paper name: {printDocument1.DefaultPageSettings.PaperSize.PaperName}\r\n";
                                OutputLogsBox.Text += $"Paper size: {printDocument1.DefaultPageSettings.PaperSize.Width} x {printDocument1.DefaultPageSettings.PaperSize.Height}\r\n";
                            }

                            break;
                        }
                    case "prn":
                        {
                            var result = printDialog1.ShowDialog();

                            OutputLogsBox.Text += $"Print dialog result: {result}\r\n";
                            if (result == DialogResult.OK && checkBox52.Checked)
                            {
                                printDocument1.Print();
                            }

                            break;
                        }
                    case "ico":
                        {
                            if (Shell32.ShowPickIconDialog(Handle, textBox14.Text, (int)numericUpDown13.Value, out string path, out int idx))
                            {
                                OutputLogsBox.Text += $"Icon dialog result: OK\r\n";
                                var icon = Icon.ExtractIcon(path, idx);

                                pictureBox3.Image = icon?.ToBitmap();

                                OutputLogsBox.Text += $"Selected icon: {path},{idx}\r\n";
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                OutputLogsBox.Text += $"{ex.GetType().FullName ?? ex.GetType().Name}: {ex.Message} ({ex.HResult:X})\r\n";
                OutputLogsBox.Focus();


                for (int i = 0; i < 2; i++) // number of pulses
                {
                    OutputLogsBox.BackColor = Color.Red;
                    await Task.Delay(500);  // non-blocking delay
                    OutputLogsBox.BackColor = SystemColors.Control;
                    await Task.Delay(500);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OutputLogsBox.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Shell32.ShowPickIconDialog(Handle, !string.IsNullOrEmpty(taskDlg_footnoteIconPath) ? taskDlg_footnoteIconPath : GetImageresPath(),
                taskDlg_footnoteIconIndex != null ? (int)taskDlg_footnoteIconIndex : 0, out string path, out int idx))
            {
                taskDlg_footnoteIconPath = path;
                taskDlg_footnoteIconIndex = idx;
                button6.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            taskDlg_footnoteIconPath = string.Empty;
            taskDlg_footnoteIconIndex = null;
            button6.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Shell32.ShowPickIconDialog(Handle, !string.IsNullOrEmpty(taskDlg_iconPath) ? taskDlg_iconPath : GetImageresPath(),
                taskDlg_iconIndex, out string path, out int idx))
            {
                taskDlg_iconPath = path;
                taskDlg_iconIndex = idx;
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Maximum = numericUpDown4.Value;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Minimum = numericUpDown5.Value;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var designer = new TaskDlgControlDesigner(TaskDlgControls);

            var result = designer.ShowDialog();

            if (result == DialogResult.OK)
            {
                TaskDlgControls = designer.TaskDlgControls;
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TaskDialog.ShowDialog(this, new TaskDialogPage
            {
                Caption = "How do I use links?",
                Heading = "How do I use links?",
                Icon = TaskDialogIcon.Information,
                Text = "Task Dialogs allow you to embed links within them. To use them, first, enable the Enable Links check box." +
                " If you put URLs instead of regular strings of text in your links, you may also want to enable Auto-navigate to URLs. Now, to insert a URL into the task" +
                " dialog message, footnote, or expander, put the string to turn into a link between the <a> XML tag, for example: <a href=\"https://microsoft.com\">" +
                "This is some text</a>. The href attribute specifies the string or URL to invoke.",
                Buttons = [TaskDialogButton.OK],
                AllowCancel = true
            });
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var colDlg = new ColorDialog
            {
                Color = colorDialog1.Color
            };

            if (colDlg.ShowDialog() == DialogResult.OK)
            {
                colorDialog1.Color = colDlg.Color;
                panel2.BackColor = colDlg.Color;
            }
        }

        private void checkBox16_CheckStateChanged(object sender, EventArgs e)
        {
            checkBox16.Text = checkBox16.CheckState switch
            {
                CheckState.Unchecked => "Not allowed",
                CheckState.Checked => "Open automatically",
                CheckState.Indeterminate => "Allowed",
                _ => "Unknown"
            };

            switch (checkBox16.CheckState)
            {
                case CheckState.Unchecked:
                    colorDialog1.AllowFullOpen = false;
                    colorDialog1.FullOpen = false;
                    break;
                case CheckState.Checked:
                    colorDialog1.AllowFullOpen = true;
                    colorDialog1.FullOpen = true;
                    break;
                case CheckState.Indeterminate:
                    colorDialog1.AllowFullOpen = true;
                    colorDialog1.FullOpen = false;
                    break;
            }
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            colorDialog1.AnyColor = checkBox17.Checked;
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            colorDialog1.SolidColorOnly = checkBox19.Checked;
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            colorDialog1.ShowHelp = checkBox18.Checked;
        }

        private void colorDialog1_HelpRequest(object sender, EventArgs e)
        {
            OutputLogsBox.Text += "Color dialog: User requested help!\r\n";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var colDlg = new ColorDialog
            {
                Color = fontDialog1.Color
            };

            if (colDlg.ShowDialog() == DialogResult.OK)
            {
                fontDialog1.Color = colDlg.Color;
                panel3.BackColor = colDlg.Color;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var fontDlg = new FontDialog
            {
                Font = fontDialog1.Font
            };

            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                fontDialog1.Font = fontDlg.Font;
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            fontDialog1.MinSize = (int)numericUpDown7.Value;
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            fontDialog1.MaxSize = (int)numericUpDown8.Value;
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.FixedPitchOnly = checkBox27.Checked;
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.FontMustExist = checkBox28.Checked;
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.AllowScriptChange = checkBox20.Checked;
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.AllowSimulations = checkBox21.Checked;
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.AllowVectorFonts = checkBox22.Checked;
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.AllowVerticalFonts = checkBox23.Checked;
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = checkBox24.Checked;
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.ShowApply = checkBox25.Checked;
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.ShowEffects = checkBox26.Checked;
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            fontDialog1.ShowHelp = checkBox29.Checked;
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            OutputLogsBox.Text += "Font dialog: Apply button clicked!\r\n";
        }

        private void fontDialog1_HelpRequest(object sender, EventArgs e)
        {
            OutputLogsBox.Text += "Font dialog: User requested help!\r\n";
        }

        private string? PickDirectory()
        {
            var dlg = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                Description = "Pick a Directory",
                UseDescriptionForTitle = true
            };

            var result = dlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                return dlg.SelectedPath;
            }

            return null;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var dir = PickDirectory();

            if (dir != null)
            {
                textBox12.Text = dir;
                folderBrowserDialog1.InitialDirectory = dir;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var dir = PickDirectory();

            if (dir != null)
            {
                textBox13.Text = dir;
                folderBrowserDialog1.SelectedPath = dir;
            }
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.AutoUpgradeEnabled = !checkBox30.Checked;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = textBox11.Text;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.InitialDirectory = textBox12.Text;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem is SpecialFolderComboBoxItem item)
            {
                folderBrowserDialog1.RootFolder = item.Folder;
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox13.Text;
        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = checkBox31.Checked;
        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.UseDescriptionForTitle = checkBox32.Checked;
        }

        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.AddToRecent = checkBox33.Checked;
        }

        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.OkRequiresInteraction = checkBox34.Checked;
        }

        private void checkBox35_CheckedChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowHiddenFiles = checkBox35.Checked;
        }

        private void checkBox36_CheckedChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowPinnedPlaces = checkBox36.Checked;
        }

        private int printPageIdx = 0;

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            printPageIdx++;
            using (Font font = new("Segoe UI", 96))
            {
                var size = e.Graphics?.MeasureString(printPageIdx.ToString(), font);
                if (size != null)
                {
                    using Brush brush = new SolidBrush(Color.Black);

                    e.Graphics?.DrawString(printPageIdx.ToString(), font, brush,
                        new PointF((e.PageBounds.Width - size.Value.Width) / 2, (e.PageBounds.Height - size.Value.Height) / 2));
                }
            }

            e.HasMorePages = printPageIdx < 5;
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            printPageIdx = 0;
        }

        public enum MarginProperty
        {
            Left,
            Right,
            Top,
            Bottom
        }

        private Margins SetMargins(Margins? original, MarginProperty prop, int value) =>
            prop switch
            {
                MarginProperty.Left => new Margins(value, original?.Right ?? 0, original?.Top ?? 0, original?.Bottom ?? 0),
                MarginProperty.Right => new Margins(original?.Left ?? 0, value, original?.Top ?? 0, original?.Bottom ?? 0),
                MarginProperty.Top => new Margins(original?.Left ?? 0, original?.Right ?? 0, value, original?.Bottom ?? 0),
                MarginProperty.Bottom => new Margins(original?.Left ?? 0, original?.Right ?? 0, original?.Top ?? 0, value),
                _ => throw new ArgumentOutOfRangeException(nameof(prop))
            };

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.MinMargins = SetMargins(pageSetupDialog1.MinMargins, MarginProperty.Bottom, (int)numericUpDown9.Value);
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.MinMargins = SetMargins(pageSetupDialog1.MinMargins, MarginProperty.Right, (int)numericUpDown11.Value);
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.MinMargins = SetMargins(pageSetupDialog1.MinMargins, MarginProperty.Top, (int)numericUpDown10.Value);
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.MinMargins = SetMargins(pageSetupDialog1.MinMargins, MarginProperty.Left, (int)numericUpDown12.Value);
        }

        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.EnableMetric = checkBox37.Checked;
        }

        private void checkBox42_CheckedChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowHelp = checkBox42.Checked;
        }

        private void checkBox43_CheckedChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowNetwork = checkBox43.Checked;
        }

        private void checkBox38_CheckedChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.AllowMargins = checkBox38.Checked;
        }

        private void checkBox39_CheckedChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.AllowOrientation = checkBox39.Checked;
        }

        private void checkBox40_CheckedChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.AllowPaper = checkBox40.Checked;
        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
        {
            pageSetupDialog1.AllowPrinter = checkBox41.Checked;
        }

        private void pageSetupDialog1_HelpRequest(object sender, EventArgs e)
        {
            OutputLogsBox.Text += "Page setup dialog: User requested help!\r\n";
        }

        private void checkBox44_CheckedChanged(object sender, EventArgs e)
        {
            printDialog1.UseEXDialog = checkBox44.Checked;
        }

        private void checkBox49_CheckedChanged(object sender, EventArgs e)
        {
            printDialog1.PrintToFile = checkBox49.Checked;
        }

        private void checkBox50_CheckedChanged(object sender, EventArgs e)
        {
            printDialog1.ShowHelp = checkBox50.Checked;
        }

        private void checkBox51_CheckedChanged(object sender, EventArgs e)
        {
            printDialog1.ShowNetwork = checkBox51.Checked;
        }

        private void checkBox45_CheckedChanged(object sender, EventArgs e)
        {
            printDialog1.AllowCurrentPage = checkBox45.Checked;
        }

        private void checkBox46_CheckedChanged(object sender, EventArgs e)
        {
            printDialog1.AllowPrintToFile = checkBox46.Checked;
        }

        private void checkBox47_CheckedChanged(object sender, EventArgs e)
        {
            printDialog1.AllowSelection = checkBox47.Checked;
        }

        private void checkBox48_CheckedChanged(object sender, EventArgs e)
        {
            printDialog1.AllowSomePages = checkBox48.Checked;
        }

        private void printDialog1_HelpRequest(object sender, EventArgs e)
        {
            OutputLogsBox.Text += "Print dialog: User requested help!\r\n";
        }

        private void IconDlgUpdateDefaultIconPreview()
        {
            try
            {
                var icon = Icon.ExtractIcon(textBox14.Text, (int)numericUpDown13.Value);

                if (icon != null)
                {
                    pictureBox2.Image = icon.ToBitmap();
                }
            }
            catch
            {

            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox14.Text))
            {
                IconDlgUpdateDefaultIconPreview();

                numericUpDown13.Maximum = IconHelper.CountIcons(textBox14.Text);
            }
        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e)
        {
            IconDlgUpdateDefaultIconPreview();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (exportDlg.ShowDialog() == DialogResult.OK)
            {
                bool wasSerializerFound = true;

                using (FileStream stream = new FileStream(exportDlg.FileName, FileMode.Create, FileAccess.Write))
                {
                    // Serialize dialogs to XML
                    using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings
                    {
                        Indent = true
                    }))
                    {
                        switch (tabControl1.SelectedTab?.Tag)
                        {
                            case "msgbox":
                                {
                                    writer.WriteStartElement("MessageBox");
                                    writer.WriteAttributeString("Version", "2");

                                    writer.WriteElementString("Text", msgBoxTextBox.Text);
                                    writer.WriteElementString("Caption", msgBoxCaptionBox.Text);
                                    writer.WriteElementString("DefaultButton", numericUpDown1.Value.ToString());

                                    writer.WriteStartElement("MessageBoxOptions");

                                    writer.WriteElementString("Rtl", checkBox1.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("ServiceNotification", checkBox2.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("RightAlign", checkBox3.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("DefaultDesktopOnly", checkBox4.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("TopMost", checkBox54.Checked.ToString().ToLowerInvariant());

                                    writer.WriteEndElement();

                                    writer.WriteElementString("Buttons", MsgBoxGetButtons().ToString()); // e.g, AbortRetryIgnore
                                    writer.WriteElementString("Icon", MsgBoxGetIcon().ToString()); // e.g, Warning, Error, ...

                                    writer.WriteStartElement("Modality");

                                    writer.WriteElementString("Modality", MsgBoxGetModality().ToString());
                                    writer.WriteElementString("SetForeground", checkBox53.Checked.ToString().ToLowerInvariant());

                                    writer.WriteEndElement();

                                    writer.WriteStartElement("Help");

                                    writer.WriteElementString("ShowHelp", checkBox5.Checked.ToString().ToLowerInvariant());
                                    //writer.WriteElementString("HelpPath", msgBoxHelpFileTextBox.Text);

                                    //if (radioButton8.Checked)
                                    //{
                                    //    writer.WriteElementString("HelpNavigator", MsgBoxGetHelpNavigator().ToString());
                                    //}
                                    //else
                                    //{
                                    //    writer.WriteElementString("Keyword", textBox1.Text);
                                    //}

                                    if (checkBox55.Checked)
                                        writer.WriteElementString("ContextHelpId", numericUpDown2.Value.ToString());

                                    writer.WriteEndElement();

                                    writer.WriteEndElement();
                                    break;
                                }
                            case "taskdlg":
                                {
                                    writer.WriteStartElement("TaskDialog");

                                    writer.WriteElementString("Caption", textBox3.Text);
                                    writer.WriteElementString("Heading", textBox4.Text);
                                    writer.WriteElementString("Message", textBox5.Text);

                                    writer.WriteStartElement("Footnote");

                                    writer.WriteElementString("Text", textBox6.Text);

                                    if (!string.IsNullOrEmpty(taskDlg_footnoteIconPath) && taskDlg_footnoteIconIndex != null)
                                    {
                                        writer.WriteElementString("Icon", $"{taskDlg_footnoteIconPath},{taskDlg_footnoteIconIndex}");
                                    }

                                    writer.WriteEndElement();

                                    if (checkBox6.Checked)
                                    {
                                        writer.WriteStartElement("Expander");

                                        writer.WriteElementString("Text", textBox7.Text);
                                        writer.WriteElementString("CollapsedText", textBox8.Text);
                                        writer.WriteElementString("ExpandedText", textBox9.Text);
                                        writer.WriteElementString("Expanded", checkBox7.Checked.ToString().ToLowerInvariant());
                                        writer.WriteElementString("Position", comboBox2.SelectedIndex == 0 ? "AfterText" : "AfterFootnote");

                                        writer.WriteEndElement();
                                    }

                                    writer.WriteElementString("Icon", UIHelper.GetCheckedRadioButton(groupBox6)?.Name switch
                                    {
                                        "radioButton24" => "None",
                                        "radioButton25" => "Information",
                                        "radioButton26" => "Warning",
                                        "radioButton27" => "Error",
                                        "radioButton28" => "ShieldNoBanner",
                                        "radioButton29" => "ShieldBlueBanner",
                                        "radioButton30" => "ShieldGrayBanner",
                                        "radioButton31" => "ShieldSuccessGreenBanner",
                                        "radioButton32" => "ShieldWarningYellowBanner",
                                        "radioButton33" => "ShieldErrorRedBanner",
                                        "radioButton34" => $"{taskDlg_iconPath},{taskDlg_iconIndex}",
                                        _ => "None"
                                    });

                                    writer.WriteElementString("BannerOverride", UIHelper.GetCheckedRadioButton(groupBox7)?.Name switch
                                    {
                                        "radioButton35" => "Default",
                                        "radioButton40" => "Yellow",
                                        "radioButton36" => "Gray",
                                        "radioButton38" => "None",
                                        "radioButton41" => "Blue",
                                        "radioButton37" => "Red",
                                        "radioButton39" => "Green",
                                        _ => "Default"
                                    });

                                    if (comboBox3.SelectedIndex != 0)
                                    {
                                        writer.WriteStartElement("ProgressBar");

                                        writer.WriteElementString("State", comboBox3.SelectedIndex switch
                                        {
                                            1 => "Normal",
                                            2 => "Paused",
                                            3 => "Error",
                                            4 => "Marquee",
                                            5 => "MarqueePaused",
                                            _ => "Normal"
                                        });
                                        writer.WriteElementString("Value", numericUpDown3.Value.ToString());
                                        writer.WriteElementString("Max", numericUpDown4.Value.ToString());
                                        writer.WriteElementString("Min", numericUpDown5.Value.ToString());

                                        if (comboBox3.SelectedIndex > 3) // Marquee/MarqueePaused
                                        {
                                            writer.WriteElementString("MarqueeSpeed", numericUpDown6.Value.ToString());
                                        }


                                        writer.WriteEndElement();
                                    }

                                    writer.WriteStartElement("TaskDialogControls");

                                    foreach (var control in TaskDlgControls)
                                    {
                                        switch (control)
                                        {
                                            case TaskDialogButton button:
                                                writer.WriteStartElement(button is TaskDialogCommandLinkButton ? "CommandLinkButton" : "Button");
                                                writer.WriteAttributeString("Text", button.Text);
                                                if (button is TaskDialogCommandLinkButton cmdLink)
                                                {
                                                    writer.WriteAttributeString("NoteText", cmdLink.DescriptionText);
                                                }
                                                writer.WriteAttributeString("CanCloseDialog", button.AllowCloseDialog.ToString().ToLowerInvariant());
                                                writer.WriteAttributeString("ShowUacShield", button.ShowShieldIcon.ToString().ToLowerInvariant());
                                                writer.WriteAttributeString("Enabled", button.Enabled.ToString().ToLowerInvariant());
                                                writer.WriteEndElement();
                                                break;
                                            case TaskDialogRadioButton radio:
                                                writer.WriteStartElement("RadioButton");
                                                writer.WriteAttributeString("Text", radio.Text);
                                                writer.WriteAttributeString("Enabled", radio.Enabled.ToString().ToLowerInvariant());
                                                writer.WriteAttributeString("Checked", radio.Checked.ToString().ToLowerInvariant());

                                                writer.WriteEndElement();
                                                break;
                                        }
                                    }

                                    writer.WriteEndElement();

                                    if (!string.IsNullOrEmpty(textBox10.Text))
                                    {
                                        writer.WriteStartElement("VerificationCheckBox");

                                        writer.WriteAttributeString("Text", textBox10.Text);
                                        writer.WriteAttributeString("Checked", checkBox14.Checked.ToString().ToLowerInvariant());

                                        writer.WriteEndElement();
                                    }

                                    writer.WriteStartElement("TaskDialogOptions");

                                    writer.WriteElementString("Modeless", checkBox9.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("SizeToContent", checkBox10.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("AllowCancel", checkBox11.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("AllowMinimize", checkBox12.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("EnableLinks", checkBox13.Checked.ToString().ToLowerInvariant());
                                    writer.WriteElementString("UrlAutoNavigate", checkBox15.Checked.ToString().ToLowerInvariant());

                                    writer.WriteEndElement();

                                    writer.WriteEndElement();
                                    break;
                                }
                            default:
                                wasSerializerFound = false;
                                MessageBox.Show("A serializer wasn't found for this dialog. This means that Windows Dialog Box Generator can't load or save a dialog of this type.",
                                    null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                }

                if (wasSerializerFound)
                    MessageBox.Show("Your dialog has been successfully exported.", "Export complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    File.Delete(exportDlg.FileName);
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (importDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream stream = new FileStream(importDlg.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings
                        {
                            IgnoreWhitespace = true
                        }))
                        {
                            reader.MoveToContent();
                            switch (reader.Name)
                            {
                                case "MessageBox":
                                    {
                                        tabControl1.SelectTab(0);

                                        string? versionAttr = reader.GetAttribute("Version");
                                        int version = 1; // default for legacy files

                                        if (!string.IsNullOrEmpty(versionAttr))
                                        {
                                            _ = int.TryParse(versionAttr, out version);
                                        }

                                        reader.ReadStartElement("MessageBox");

                                        msgBoxTextBox.Text = reader.ReadElementContentAsString("Text", string.Empty);
                                        msgBoxCaptionBox.Text = reader.ReadElementContentAsString("Caption", string.Empty);
                                        numericUpDown1.Value = reader.ReadElementContentAsInt("DefaultButton", string.Empty);

                                        reader.ReadStartElement("MessageBoxOptions");

                                        if (version == 2)
                                            checkBox1.Checked = reader.ReadElementContentAsBoolean("Rtl", string.Empty);
                                        else
                                            checkBox1.Checked = reader.ReadElementContentAsBoolean("RtlReading", string.Empty);

                                        checkBox2.Checked = reader.ReadElementContentAsBoolean("ServiceNotification", string.Empty);
                                        checkBox3.Checked = reader.ReadElementContentAsBoolean("RightAlign", string.Empty);
                                        checkBox4.Checked = reader.ReadElementContentAsBoolean("DefaultDesktopOnly", string.Empty);

                                        if (version == 2)
                                            checkBox54.Checked = reader.ReadElementContentAsBoolean("TopMost", string.Empty);

                                        reader.ReadEndElement();

                                        UIHelper.SetCheckedRadioButton(reader.ReadElementContentAsString("Buttons", string.Empty).ToLowerInvariant() switch
                                        {
                                            "ok" => radioButton1,
                                            "okcancel" => radioButton2,
                                            "retrycancel" => radioButton3,
                                            "canceltrycontinue" => radioButton4,
                                            "abortretryignore" => radioButton5,
                                            "yesno" => radioButton6,
                                            "yesnocancel" => radioButton7,
                                            _ => radioButton1
                                        });

                                        UIHelper.SetCheckedRadioButton(reader.ReadElementContentAsString("Icon", string.Empty).ToLowerInvariant() switch
                                        {
                                            "none" => radioButton19,
                                            "information" => radioButton15,
                                            "warning" => radioButton16,
                                            "error" => radioButton17,
                                            "question" => radioButton18,
                                            _ => radioButton19
                                        });

                                        if (version == 2)
                                        {
                                            reader.ReadStartElement("Modality");

                                            UIHelper.SetCheckedRadioButton(reader.ReadElementContentAsString("Modality", string.Empty).ToLowerInvariant() switch
                                            {
                                                "applicationmodal" => radioButton8,
                                                "taskmodal" => radioButton9,
                                                "systemmodal" => radioButton10,
                                                _ => radioButton8
                                            });

                                            checkBox53.Checked = reader.ReadElementContentAsBoolean("SetForeground", string.Empty);

                                            reader.ReadEndElement();
                                        }

                                        reader.ReadStartElement("Help");

                                        checkBox5.Checked = reader.ReadElementContentAsBoolean("ShowHelp", string.Empty);

                                        if (version == 1)
                                        {
                                            reader.ReadElementContentAsString("HelpPath", string.Empty);

                                            reader.ReadElementContentAsString();
                                        }
                                        else
                                        {
                                            if (reader.IsStartElement("ContextHelpId"))
                                            {
                                                numericUpDown2.Value = reader.ReadElementContentAsInt();
                                                checkBox55.Checked = true;
                                            }
                                        }

                                        reader.ReadEndElement();

                                        break;
                                    }
                                case "TaskDialog":
                                    tabControl1.SelectTab(1);
                                    reader.ReadStartElement("TaskDialog");

                                    textBox3.Text = reader.ReadElementContentAsString("Caption", string.Empty);
                                    textBox4.Text = reader.ReadElementContentAsString("Heading", string.Empty);
                                    textBox5.Text = reader.ReadElementContentAsString("Message", string.Empty);

                                    if (reader.IsStartElement("Footnote"))
                                    {
                                        checkBox8.Checked = true;

                                        reader.ReadStartElement("Footnote");
                                        textBox6.Text = reader.ReadElementContentAsString("Text", string.Empty);
                                        var iconStr = reader.ReadElementContentAsString("Icon", string.Empty);
                                        var split = iconStr.Split(',');
                                        taskDlg_footnoteIconPath = split[0];
                                        taskDlg_footnoteIconIndex = int.Parse(split[1]);

                                        reader.ReadEndElement();
                                    }
                                    else
                                    {
                                        checkBox8.Checked = false;
                                    }

                                    if (reader.IsStartElement("Expander"))
                                    {
                                        checkBox6.Checked = true;
                                        reader.ReadStartElement("Expander");

                                        textBox7.Text = reader.ReadElementContentAsString("Text", string.Empty);
                                        textBox8.Text = reader.ReadElementContentAsString("CollapsedText", string.Empty);
                                        textBox9.Text = reader.ReadElementContentAsString("ExpandedText", string.Empty);
                                        checkBox7.Checked = reader.ReadElementContentAsBoolean("Expanded", string.Empty);
                                        comboBox2.SelectedIndex = reader.ReadElementContentAsString("Position", string.Empty) == "AfterText" ? 0 : 1;

                                        reader.ReadEndElement();
                                    }
                                    else
                                    {
                                        checkBox6.Checked = false;
                                    }

                                    var icon = reader.ReadElementContentAsString("Icon", string.Empty);
                                    var rb = icon switch
                                    {
                                        "None" => radioButton24,
                                        "Information" => radioButton25,
                                        "Warning" => radioButton26,
                                        "Error" => radioButton27,
                                        "ShieldNoBanner" => radioButton28,
                                        "ShieldBlueBanner" => radioButton29,
                                        "ShieldGrayBanner" => radioButton30,
                                        "ShieldSuccessGreenBanner" => radioButton31,
                                        "ShieldWarningYellowBanner" => radioButton32,
                                        "ShieldErrorRedBanner" => radioButton33,
                                        _ => null
                                    };


                                    if (rb != null)
                                    {
                                        UIHelper.SetCheckedRadioButton(rb);
                                    }
                                    else
                                    {
                                        var split = icon.Split(',');
                                        taskDlg_iconPath = split[0];
                                        taskDlg_iconIndex = int.Parse(split[1]);

                                        UIHelper.SetCheckedRadioButton(radioButton34);
                                    }

                                    UIHelper.SetCheckedRadioButton(reader.ReadElementContentAsString("BannerOverride", string.Empty) switch
                                    {
                                        "Default" => radioButton35,
                                        "Yellow" => radioButton40,
                                        "Gray" => radioButton36,
                                        "None" => radioButton38,
                                        "Blue" => radioButton41,
                                        "Red" => radioButton37,
                                        "Green" => radioButton39,
                                        _ => radioButton35
                                    });

                                    if (reader.IsStartElement("ProgressBar"))
                                    {
                                        reader.ReadStartElement("ProgressBar");

                                        comboBox3.SelectedIndex = reader.ReadElementContentAsString("State", string.Empty) switch
                                        {
                                            "Normal" => 1,
                                            "Paused" => 2,
                                            "Error" => 3,
                                            "Marquee" => 4,
                                            "MarqueePaused" => 5,
                                            _ => 0
                                        };

                                        numericUpDown3.Value = reader.ReadElementContentAsInt("Value", string.Empty);
                                        numericUpDown4.Value = reader.ReadElementContentAsInt("Max", string.Empty);
                                        numericUpDown5.Value = reader.ReadElementContentAsInt("Min", string.Empty);
                                        if (comboBox3.SelectedIndex > 3)
                                        {
                                            numericUpDown6.Value = reader.ReadElementContentAsInt("MarqueeSpeed", string.Empty);
                                        }

                                        reader.ReadEndElement();
                                    }
                                    else
                                    {
                                        comboBox2.SelectedIndex = 0;
                                    }

                                    reader.ReadStartElement("TaskDialogControls");

                                    var controls = new List<TaskDialogControl>();

                                    while (reader.NodeType == XmlNodeType.Element)
                                    {
                                        switch (reader.Name)
                                        {
                                            case "Button":
                                                controls.Add(new TaskDialogButton
                                                {
                                                    Text = reader.GetAttribute("Text"),
                                                    AllowCloseDialog = bool.Parse(reader.GetAttribute("CanCloseDialog") ?? "false"),
                                                    ShowShieldIcon = bool.Parse(reader.GetAttribute("ShowUacShield") ?? "false"),
                                                    Enabled = bool.Parse(reader.GetAttribute("Enabled") ?? "false")
                                                });
                                                reader.Read(); // move past this self-closing <Button/>
                                                break;

                                            case "RadioButton":
                                                controls.Add(new TaskDialogRadioButton
                                                {
                                                    Text = reader.GetAttribute("Text"),
                                                    Enabled = bool.Parse(reader.GetAttribute("Enabled") ?? "false"),
                                                    Checked = bool.Parse(reader.GetAttribute("Checked") ?? "false")
                                                });
                                                reader.Read();
                                                break;

                                            case "CommandLinkButton":
                                                controls.Add(new TaskDialogCommandLinkButton
                                                {
                                                    Text = reader.GetAttribute("Text"),
                                                    AllowCloseDialog = bool.Parse(reader.GetAttribute("CanCloseDialog") ?? "false"),
                                                    ShowShieldIcon = bool.Parse(reader.GetAttribute("ShowUacShield") ?? "false"),
                                                    Enabled = bool.Parse(reader.GetAttribute("Enabled") ?? "false")
                                                });
                                                reader.Read();
                                                break;

                                            default:
                                                reader.Read(); // unknown element
                                                break;
                                        }
                                    }

                                    reader.ReadEndElement();

                                    TaskDlgControls = [.. controls];

                                    if (reader.IsStartElement("VerificationCheckBox"))
                                    {
                                        reader.ReadStartElement("VerificationCheckBox");

                                        textBox10.Text = reader.GetAttribute("Text") ?? string.Empty;
                                        checkBox14.Checked = reader.GetAttribute("Checked") == "true";
                                    }

                                    reader.ReadStartElement("TaskDialogOptions");

                                    checkBox9.Checked = reader.ReadElementContentAsBoolean("Modeless", string.Empty);
                                    checkBox10.Checked = reader.ReadElementContentAsBoolean("SizeToContent", string.Empty);
                                    checkBox11.Checked = reader.ReadElementContentAsBoolean("AllowCancel", string.Empty);
                                    checkBox12.Checked = reader.ReadElementContentAsBoolean("AllowMinimize", string.Empty);
                                    checkBox13.Checked = reader.ReadElementContentAsBoolean("EnableLinks", string.Empty);
                                    checkBox15.Checked = reader.ReadElementContentAsBoolean("UrlAutoNavigate", string.Empty);

                                    reader.ReadEndElement();

                                    break;
                            }

                            reader.ReadEndElement();
                        }
                    }

                    MessageBox.Show("Your dialog has been successfully imported.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine("Error: " + ex.GetType().FullName ?? ex.GetType().Name);
                    throw;
#else
                    MessageBox.Show($"An error occured while importing the file.\n0x{ex.HResult:X}: \"{ex.Message}\"");
#endif
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Shell32.ShellAbout(Handle, "Windows Dialog Box Generator", "You are running Windows Dialog Box Generator v1.1.", Icon);
        }
    }

    public class SpecialFolderComboBoxItem(Environment.SpecialFolder folder)
    {
        public Environment.SpecialFolder Folder { get; init; } = folder;
        public string DirPath => Environment.GetFolderPath(Folder);
        public string Name
        {
            get
            {
                if (Directory.Exists(DirPath))
                {
                    using WindowsFile? info = Shell32.GetFileInfo(DirPath);

                    if (info != null)
                        return info.DisplayName;
                }

                return Folder.ToString();
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public static explicit operator SpecialFolderComboBoxItem(Environment.SpecialFolder f)
        {
            return new SpecialFolderComboBoxItem(f);
        }
    }

    public class CultureComboBoxItem
    {
        public CultureInfo Culture { get; set; } = CultureInfo.CurrentCulture;

        public override string ToString() => Culture.TextInfo.ToTitleCase(Culture.NativeName);
    }
}
