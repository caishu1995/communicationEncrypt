namespace connectSocket
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.showRich = new System.Windows.Forms.RichTextBox();
            this.ipText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.TextBox();
            this.listenButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.messageRich = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // showRich
            // 
            this.showRich.Location = new System.Drawing.Point(47, 86);
            this.showRich.Name = "showRich";
            this.showRich.Size = new System.Drawing.Size(393, 117);
            this.showRich.TabIndex = 17;
            this.showRich.Text = "";
            // 
            // ipText
            // 
            this.ipText.Location = new System.Drawing.Point(152, 19);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(100, 21);
            this.ipText.TabIndex = 16;
            this.ipText.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "端口号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "ip:";
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(152, 46);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(100, 21);
            this.portText.TabIndex = 13;
            this.portText.Text = "2000";
            // 
            // listenButton
            // 
            this.listenButton.Location = new System.Drawing.Point(314, 38);
            this.listenButton.Name = "listenButton";
            this.listenButton.Size = new System.Drawing.Size(75, 23);
            this.listenButton.TabIndex = 12;
            this.listenButton.Text = "开始连接";
            this.listenButton.UseVisualStyleBackColor = true;
            this.listenButton.Click += new System.EventHandler(this.listenButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(314, 364);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 19;
            this.sendButton.Text = "发送消息";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // messageRich
            // 
            this.messageRich.Location = new System.Drawing.Point(47, 230);
            this.messageRich.Name = "messageRich";
            this.messageRich.Size = new System.Drawing.Size(393, 117);
            this.messageRich.TabIndex = 18;
            this.messageRich.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 411);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageRich);
            this.Controls.Add(this.showRich);
            this.Controls.Add(this.ipText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.listenButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox showRich;
        private System.Windows.Forms.TextBox ipText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.Button listenButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.RichTextBox messageRich;
    }
}

