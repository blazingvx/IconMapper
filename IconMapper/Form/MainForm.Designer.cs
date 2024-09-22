namespace IconMapper
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.applyIconButton = new System.Windows.Forms.Button();
            this.DirectoryFinder = new System.Windows.Forms.GroupBox();
            this.folderTreeView = new System.Windows.Forms.TreeView();
            this.IconBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.iconPreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.iconListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectoryFinder.SuspendLayout();
            this.IconBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPreviewPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // applyIconButton
            // 
            this.applyIconButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.applyIconButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.applyIconButton.Location = new System.Drawing.Point(0, 321);
            this.applyIconButton.Name = "applyIconButton";
            this.applyIconButton.Size = new System.Drawing.Size(834, 40);
            this.applyIconButton.TabIndex = 0;
            this.applyIconButton.Text = "Apply Icon";
            this.applyIconButton.UseVisualStyleBackColor = true;
            this.applyIconButton.Click += new System.EventHandler(this.ApplyIconButton_Click);
            // 
            // DirectoryFinder
            // 
            this.DirectoryFinder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryFinder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DirectoryFinder.Controls.Add(this.folderTreeView);
            this.DirectoryFinder.Location = new System.Drawing.Point(0, 27);
            this.DirectoryFinder.Name = "DirectoryFinder";
            this.DirectoryFinder.Size = new System.Drawing.Size(406, 294);
            this.DirectoryFinder.TabIndex = 1;
            this.DirectoryFinder.TabStop = false;
            this.DirectoryFinder.Text = "Directory Finder";
            // 
            // folderTreeView
            // 
            this.folderTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTreeView.Location = new System.Drawing.Point(3, 16);
            this.folderTreeView.Name = "folderTreeView";
            this.folderTreeView.Size = new System.Drawing.Size(400, 275);
            this.folderTreeView.TabIndex = 0;
            this.folderTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.FolderTreeView_BeforeExpand);
            this.folderTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FolderTreeView_AfterSelect);
            // 
            // IconBox
            // 
            this.IconBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.IconBox.Controls.Add(this.splitContainer1);
            this.IconBox.Location = new System.Drawing.Point(416, 27);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(418, 294);
            this.IconBox.TabIndex = 2;
            this.IconBox.TabStop = false;
            this.IconBox.Text = "IconBox";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.iconPreviewPictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.iconListBox);
            this.splitContainer1.Size = new System.Drawing.Size(412, 275);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.TabIndex = 2;
            // 
            // iconPreviewPictureBox
            // 
            this.iconPreviewPictureBox.Location = new System.Drawing.Point(168, 3);
            this.iconPreviewPictureBox.Name = "iconPreviewPictureBox";
            this.iconPreviewPictureBox.Size = new System.Drawing.Size(64, 64);
            this.iconPreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconPreviewPictureBox.TabIndex = 1;
            this.iconPreviewPictureBox.TabStop = false;
            // 
            // iconListBox
            // 
            this.iconListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconListBox.FormattingEnabled = true;
            this.iconListBox.Location = new System.Drawing.Point(0, 0);
            this.iconListBox.Name = "iconListBox";
            this.iconListBox.Size = new System.Drawing.Size(412, 120);
            this.iconListBox.TabIndex = 0;
            this.iconListBox.SelectedIndexChanged += new System.EventHandler(this.IconListBox_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(834, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.importIconsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Image = global::IconMapper.Properties.Resources.settings_24dp;
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.SettingsMenuItem_Click);
            // 
            // importIconsToolStripMenuItem
            // 
            this.importIconsToolStripMenuItem.Image = global::IconMapper.Properties.Resources.upload_24dp;
            this.importIconsToolStripMenuItem.Name = "importIconsToolStripMenuItem";
            this.importIconsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importIconsToolStripMenuItem.Text = "Import Icons";
            this.importIconsToolStripMenuItem.Click += new System.EventHandler(this.ImportMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(834, 361);
            this.Controls.Add(this.IconBox);
            this.Controls.Add(this.DirectoryFinder);
            this.Controls.Add(this.applyIconButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Icon Mapper";
            this.DirectoryFinder.ResumeLayout(false);
            this.IconBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPreviewPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applyIconButton;
        private System.Windows.Forms.GroupBox DirectoryFinder;
        private System.Windows.Forms.TreeView folderTreeView;
        private System.Windows.Forms.GroupBox IconBox;
        private System.Windows.Forms.ListBox iconListBox;
        private System.Windows.Forms.PictureBox iconPreviewPictureBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        


    }
}

