namespace WinFormDemo
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textMessage = new System.Windows.Forms.TextBox();
            this.btnTrace = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnWarning = new System.Windows.Forms.Button();
            this.btnError = new System.Windows.Forms.Button();
            this.btnCritical = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textMessage
            // 
            this.textMessage.Location = new System.Drawing.Point(12, 12);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(480, 23);
            this.textMessage.TabIndex = 0;
            this.textMessage.Text = "这是一条日志信息";
            // 
            // btnTrace
            // 
            this.btnTrace.Location = new System.Drawing.Point(12, 41);
            this.btnTrace.Name = "btnTrace";
            this.btnTrace.Size = new System.Drawing.Size(75, 23);
            this.btnTrace.TabIndex = 1;
            this.btnTrace.Text = "Trace";
            this.btnTrace.UseVisualStyleBackColor = true;
            this.btnTrace.Click += new System.EventHandler(this.BtnTrace_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(93, 41);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 2;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.BtnDebug_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(174, 41);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(75, 23);
            this.btnInfo.TabIndex = 3;
            this.btnInfo.Text = "Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.BtnInfo_Click);
            // 
            // btnWarning
            // 
            this.btnWarning.Location = new System.Drawing.Point(255, 41);
            this.btnWarning.Name = "btnWarning";
            this.btnWarning.Size = new System.Drawing.Size(75, 23);
            this.btnWarning.TabIndex = 4;
            this.btnWarning.Text = "Warning";
            this.btnWarning.UseVisualStyleBackColor = true;
            this.btnWarning.Click += new System.EventHandler(this.BtnWarning_Click);
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(336, 41);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(75, 23);
            this.btnError.TabIndex = 5;
            this.btnError.Text = "Error";
            this.btnError.UseVisualStyleBackColor = true;
            this.btnError.Click += new System.EventHandler(this.BtnError_Click);
            // 
            // btnCritical
            // 
            this.btnCritical.Location = new System.Drawing.Point(417, 41);
            this.btnCritical.Name = "btnCritical";
            this.btnCritical.Size = new System.Drawing.Size(75, 23);
            this.btnCritical.TabIndex = 6;
            this.btnCritical.Text = "Critical";
            this.btnCritical.UseVisualStyleBackColor = true;
            this.btnCritical.Click += new System.EventHandler(this.BtnCritical_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 76);
            this.Controls.Add(this.btnCritical);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.btnWarning);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnTrace);
            this.Controls.Add(this.textMessage);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textMessage;
        private Button btnTrace;
        private Button btnDebug;
        private Button btnInfo;
        private Button btnWarning;
        private Button btnError;
        private Button btnCritical;
    }
}