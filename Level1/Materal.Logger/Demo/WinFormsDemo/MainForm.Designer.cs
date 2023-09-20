namespace WinFormsDemo
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
            components = new System.ComponentModel.Container();
            btnTrace = new Button();
            btnDebug = new Button();
            btnInformation = new Button();
            btnWarning = new Button();
            btnError = new Button();
            btnCritical = new Button();
            richTextBoxLogInfo = new RichTextBox();
            loadLogTimer = new System.Windows.Forms.Timer(components);
            btnRemove = new Button();
            SuspendLayout();
            // 
            // btnTrace
            // 
            btnTrace.Location = new Point(789, 12);
            btnTrace.Name = "btnTrace";
            btnTrace.Size = new Size(96, 29);
            btnTrace.TabIndex = 0;
            btnTrace.Text = "Trace";
            btnTrace.UseVisualStyleBackColor = true;
            btnTrace.Click += BtnTrace_Click;
            // 
            // btnDebug
            // 
            btnDebug.Location = new Point(789, 47);
            btnDebug.Name = "btnDebug";
            btnDebug.Size = new Size(96, 29);
            btnDebug.TabIndex = 2;
            btnDebug.Text = "Debug";
            btnDebug.UseVisualStyleBackColor = true;
            btnDebug.Click += BtnDebug_Click;
            // 
            // btnInformation
            // 
            btnInformation.Location = new Point(789, 82);
            btnInformation.Name = "btnInformation";
            btnInformation.Size = new Size(96, 29);
            btnInformation.TabIndex = 3;
            btnInformation.Text = "Information";
            btnInformation.UseVisualStyleBackColor = true;
            btnInformation.Click += BtnInformation_Click;
            // 
            // btnWarning
            // 
            btnWarning.Location = new Point(789, 117);
            btnWarning.Name = "btnWarning";
            btnWarning.Size = new Size(96, 29);
            btnWarning.TabIndex = 4;
            btnWarning.Text = "Warning";
            btnWarning.UseVisualStyleBackColor = true;
            btnWarning.Click += BtnWarning_Click;
            // 
            // btnError
            // 
            btnError.Location = new Point(789, 152);
            btnError.Name = "btnError";
            btnError.Size = new Size(96, 29);
            btnError.TabIndex = 5;
            btnError.Text = "Error";
            btnError.UseVisualStyleBackColor = true;
            btnError.Click += BtnError_Click;
            // 
            // btnCritical
            // 
            btnCritical.Location = new Point(789, 187);
            btnCritical.Name = "btnCritical";
            btnCritical.Size = new Size(96, 29);
            btnCritical.TabIndex = 6;
            btnCritical.Text = "Critical";
            btnCritical.UseVisualStyleBackColor = true;
            btnCritical.Click += BtnCritical_Click;
            // 
            // richTextBoxLogInfo
            // 
            richTextBoxLogInfo.Dock = DockStyle.Left;
            richTextBoxLogInfo.Location = new Point(0, 0);
            richTextBoxLogInfo.Name = "richTextBoxLogInfo";
            richTextBoxLogInfo.ReadOnly = true;
            richTextBoxLogInfo.Size = new Size(783, 469);
            richTextBoxLogInfo.TabIndex = 7;
            richTextBoxLogInfo.Text = "";
            // 
            // loadLogTimer
            // 
            loadLogTimer.Interval = 500;
            loadLogTimer.Tick += LoadLogTimer_Tick;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(789, 428);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(96, 29);
            btnRemove.TabIndex = 8;
            btnRemove.Text = "Delete";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += BtnRemove_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(897, 469);
            Controls.Add(btnRemove);
            Controls.Add(richTextBoxLogInfo);
            Controls.Add(btnCritical);
            Controls.Add(btnError);
            Controls.Add(btnWarning);
            Controls.Add(btnInformation);
            Controls.Add(btnDebug);
            Controls.Add(btnTrace);
            Name = "MainForm";
            Text = "日志Demo";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnTrace;
        private Button btnDebug;
        private Button btnInformation;
        private Button btnWarning;
        private Button btnError;
        private Button btnCritical;
        private RichTextBox richTextBoxLogInfo;
        private System.Windows.Forms.Timer loadLogTimer;
        private Button btnRemove;
    }
}