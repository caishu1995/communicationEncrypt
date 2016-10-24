namespace listenSocket
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
            this.ipLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.TextBox();
            this.listenButton = new System.Windows.Forms.Button();
            this.showRich = new System.Windows.Forms.RichTextBox();
            this.clientListCombo = new System.Windows.Forms.ComboBox();
            this.shockSendButton = new System.Windows.Forms.Button();
            this.msgSendButton = new System.Windows.Forms.Button();
            this.fileSendButton = new System.Windows.Forms.Button();
            this.fileChooseButton = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(113, 18);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(59, 12);
            this.ipLabel.TabIndex = 9;
            this.ipLabel.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "端口号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "ip:";
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(115, 33);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(100, 21);
            this.portText.TabIndex = 6;
            this.portText.Text = "2000";
            // 
            // listenButton
            // 
            this.listenButton.Location = new System.Drawing.Point(251, 25);
            this.listenButton.Name = "listenButton";
            this.listenButton.Size = new System.Drawing.Size(75, 23);
            this.listenButton.TabIndex = 5;
            this.listenButton.Text = "开始监听";
            this.listenButton.UseVisualStyleBackColor = true;
            this.listenButton.Click += new System.EventHandler(this.listenButton_Click);
            // 
            // showRich
            // 
            this.showRich.Location = new System.Drawing.Point(58, 66);
            this.showRich.Name = "showRich";
            this.showRich.Size = new System.Drawing.Size(435, 117);
            this.showRich.TabIndex = 10;
            this.showRich.Text = "";
            // 
            // clientListCombo
            // 
            this.clientListCombo.FormattingEnabled = true;
            this.clientListCombo.Location = new System.Drawing.Point(353, 27);
            this.clientListCombo.Name = "clientListCombo";
            this.clientListCombo.Size = new System.Drawing.Size(136, 20);
            this.clientListCombo.TabIndex = 11;
            // 
            // shockSendButton
            // 
            this.shockSendButton.Location = new System.Drawing.Point(414, 333);
            this.shockSendButton.Name = "shockSendButton";
            this.shockSendButton.Size = new System.Drawing.Size(75, 23);
            this.shockSendButton.TabIndex = 18;
            this.shockSendButton.Text = "发送震动";
            this.shockSendButton.UseVisualStyleBackColor = true;
            this.shockSendButton.Click += new System.EventHandler(this.shockSendButton_Click);
            // 
            // msgSendButton
            // 
            this.msgSendButton.Location = new System.Drawing.Point(303, 333);
            this.msgSendButton.Name = "msgSendButton";
            this.msgSendButton.Size = new System.Drawing.Size(75, 23);
            this.msgSendButton.TabIndex = 17;
            this.msgSendButton.Text = "发送信息";
            this.msgSendButton.UseVisualStyleBackColor = true;
            this.msgSendButton.Click += new System.EventHandler(this.msgSendButton_Click);
            // 
            // fileSendButton
            // 
            this.fileSendButton.Location = new System.Drawing.Point(154, 333);
            this.fileSendButton.Name = "fileSendButton";
            this.fileSendButton.Size = new System.Drawing.Size(75, 23);
            this.fileSendButton.TabIndex = 16;
            this.fileSendButton.Text = "发送文件";
            this.fileSendButton.UseVisualStyleBackColor = true;
            this.fileSendButton.Click += new System.EventHandler(this.fileSendButton_Click);
            // 
            // fileChooseButton
            // 
            this.fileChooseButton.Location = new System.Drawing.Point(73, 333);
            this.fileChooseButton.Name = "fileChooseButton";
            this.fileChooseButton.Size = new System.Drawing.Size(75, 23);
            this.fileChooseButton.TabIndex = 13;
            this.fileChooseButton.Text = "选择文件";
            this.fileChooseButton.UseVisualStyleBackColor = true;
            this.fileChooseButton.Click += new System.EventHandler(this.fileChooseButton_Click);
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(58, 199);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(435, 117);
            this.txtText.TabIndex = 19;
            this.txtText.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 382);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.shockSendButton);
            this.Controls.Add(this.msgSendButton);
            this.Controls.Add(this.fileSendButton);
            this.Controls.Add(this.fileChooseButton);
            this.Controls.Add(this.clientListCombo);
            this.Controls.Add(this.showRich);
            this.Controls.Add(this.ipLabel);
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

        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.Button listenButton;
        private System.Windows.Forms.RichTextBox showRich;
        private System.Windows.Forms.ComboBox clientListCombo;
        private System.Windows.Forms.Button shockSendButton;
        private System.Windows.Forms.Button msgSendButton;
        private System.Windows.Forms.Button fileSendButton;
        private System.Windows.Forms.Button fileChooseButton;
        private System.Windows.Forms.RichTextBox txtText;
    }
}

