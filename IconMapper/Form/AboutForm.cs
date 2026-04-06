using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;


namespace IconMapper
{
    public partial class AboutForm : Form
    {
        private Label lblAppName;
        private Label lblVersion;
        private Label lblDescription;
        private Button btnViewChangelog;
        private Button btnClose;

        public AboutForm()
        {
            Text = "About Icon Mapper";
            Width = 400;
            Height = 250;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            lblAppName = new Label
            {
                Text = "Icon Mapper",
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Top = 15,
                Left = 15
            };

            lblVersion = new Label
            {
                Text = "Version 1.1.1",
                AutoSize = true,
                Top = 50,
                Left = 20
            };

            lblDescription = new Label
            {
                Text = "This application allows you to apply custom icons to folders. \nYou can select icon files, preview them, and manage icon settings.\nDeveloped by Vedant Sood",
                Width = 340,
                Top = 80,
                Left = 20,
                AutoSize = true

            };

            btnViewChangelog = new Button
            {
                Text = "View Changelog",
                Width = 120,
                Top = 150,
                Left = 20
            };
            btnViewChangelog.Click += BtnViewChangelog_Click;

            btnClose = new Button
            {
                Text = "Close",
                Width = 80,
                Top = 150,
                Left = 260
            };
            btnClose.Click += (s, e) => Close();

            Controls.Add(lblAppName);
            Controls.Add(lblVersion);
            Controls.Add(lblDescription);
            Controls.Add(btnViewChangelog);
            Controls.Add(btnClose);
        }

        private void BtnViewChangelog_Click(object sender, EventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["ChangeLogPath"]; // Ensure this file exists in output folder

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Changelog file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string content = File.ReadAllText(filePath);

            // Show in separate form with multiline textbox
            Form changelogForm = new Form
            {
                Text = "Changelog",
                Width = 600,
                Height = 500,
                StartPosition = FormStartPosition.CenterParent
            };

            TextBox textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Consolas", 10),
                Text = content
            };

            changelogForm.Controls.Add(textBox);
            changelogForm.ShowDialog();
        }
    }
}
