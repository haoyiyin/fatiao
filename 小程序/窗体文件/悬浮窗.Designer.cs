namespace fatiao
{
    partial class 悬浮窗
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(悬浮窗));
            contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(components);
            运行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            运行ToolStripMenuItem,
            toolStripMenuItem2,
            toolStripMenuItem4,
            toolStripSeparator1,
            toolStripMenuItem1,
            toolStripMenuItem8,
            toolStripMenuItem3});
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new System.Drawing.Size(181, 164);
            // 
            // 运行ToolStripMenuItem
            // 
            运行ToolStripMenuItem.Name = "运行ToolStripMenuItem";
            运行ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            运行ToolStripMenuItem.Text = "开始运行";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItem2.Text = "暂停运行";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItem4.Text = "缓停止";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItem1.Text = "隐藏悬浮窗";
            toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItem3.Text = "退出发条";
            toolStripMenuItem3.Click += new System.EventHandler(toolStripMenuItem3_Click);
            // 
            // contextMenuStrip4
            // 
            contextMenuStrip4.Name = "contextMenuStrip1";
            contextMenuStrip4.Size = new System.Drawing.Size(61, 4);
            contextMenuStrip4.Click += new System.EventHandler(contextMenuStrip4_Click);
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItem8.Text = "重启发条";
            // 
            // 悬浮窗
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.IndianRed;
            BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$BackgroundImage")));
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ClientSize = new System.Drawing.Size(53, 53);
            ContextMenuStrip = contextMenuStrip2;
            ControlBox = false;
            Cursor = System.Windows.Forms.Cursors.Default;
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "悬浮窗";
            Opacity = 0.8D;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            TopMost = true;
            DoubleClick += new System.EventHandler(悬浮窗_DoubleClick);
            MouseMove += new System.Windows.Forms.MouseEventHandler(悬浮窗_MouseMove);
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem 运行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
    }
}