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
            this.iconListBox = new System.Windows.Forms.ListBox();
            this.DirectoryFinder.SuspendLayout();
            this.IconBox.SuspendLayout();
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
            this.DirectoryFinder.Location = new System.Drawing.Point(0, 0);
            this.DirectoryFinder.Name = "DirectoryFinder";
            this.DirectoryFinder.Size = new System.Drawing.Size(406, 321);
            this.DirectoryFinder.TabIndex = 1;
            this.DirectoryFinder.TabStop = false;
            this.DirectoryFinder.Text = "Directory Finder";
            // 
            // folderTreeView
            // 
            this.folderTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTreeView.Location = new System.Drawing.Point(3, 16);
            this.folderTreeView.Name = "folderTreeView";
            this.folderTreeView.Size = new System.Drawing.Size(400, 302);
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
            this.IconBox.Controls.Add(this.iconListBox);
            this.IconBox.Location = new System.Drawing.Point(416, 0);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(418, 321);
            this.IconBox.TabIndex = 2;
            this.IconBox.TabStop = false;
            this.IconBox.Text = "IconBox";
            // 
            // iconListBox
            // 
            this.iconListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconListBox.FormattingEnabled = true;
            this.iconListBox.Location = new System.Drawing.Point(3, 16);
            this.iconListBox.Name = "iconListBox";
            this.iconListBox.Size = new System.Drawing.Size(412, 302);
            this.iconListBox.TabIndex = 0;
            this.iconListBox.SelectedIndexChanged += new System.EventHandler(this.IconListBox_SelectedIndexChanged);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Icon Mapper";
            this.DirectoryFinder.ResumeLayout(false);
            this.IconBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button applyIconButton;
        private System.Windows.Forms.GroupBox DirectoryFinder;
        private System.Windows.Forms.TreeView folderTreeView;
        private System.Windows.Forms.GroupBox IconBox;
        private System.Windows.Forms.ListBox iconListBox;
    }
}

