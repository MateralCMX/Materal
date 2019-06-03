namespace CodeCreate.UI
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textConfig = new System.Windows.Forms.TextBox();
            this.btnBrowseConfig = new System.Windows.Forms.Button();
            this.btnBrowseTarget = new System.Windows.Forms.Button();
            this.textTarget = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.checkClear = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置文件：";
            // 
            // textConfig
            // 
            this.textConfig.Location = new System.Drawing.Point(83, 12);
            this.textConfig.Name = "textConfig";
            this.textConfig.Size = new System.Drawing.Size(291, 21);
            this.textConfig.TabIndex = 1;
            // 
            // btnBrowseConfig
            // 
            this.btnBrowseConfig.Location = new System.Drawing.Point(380, 12);
            this.btnBrowseConfig.Name = "btnBrowseConfig";
            this.btnBrowseConfig.Size = new System.Drawing.Size(31, 21);
            this.btnBrowseConfig.TabIndex = 2;
            this.btnBrowseConfig.Text = "...";
            this.btnBrowseConfig.UseVisualStyleBackColor = true;
            this.btnBrowseConfig.Click += new System.EventHandler(this.BtnBrowseConfig_Click);
            // 
            // btnBrowseTarget
            // 
            this.btnBrowseTarget.Location = new System.Drawing.Point(380, 39);
            this.btnBrowseTarget.Name = "btnBrowseTarget";
            this.btnBrowseTarget.Size = new System.Drawing.Size(31, 21);
            this.btnBrowseTarget.TabIndex = 5;
            this.btnBrowseTarget.Text = "...";
            this.btnBrowseTarget.UseVisualStyleBackColor = true;
            this.btnBrowseTarget.Click += new System.EventHandler(this.BtnBrowseTarget_Click);
            // 
            // textTarget
            // 
            this.textTarget.Location = new System.Drawing.Point(83, 39);
            this.textTarget.Name = "textTarget";
            this.textTarget.Size = new System.Drawing.Size(291, 21);
            this.textTarget.TabIndex = 4;
            this.textTarget.Text = "D:\\CodeCreate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "生成路径：";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(83, 66);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "生成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // checkClear
            // 
            this.checkClear.AutoSize = true;
            this.checkClear.Checked = true;
            this.checkClear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkClear.Location = new System.Drawing.Point(164, 70);
            this.checkClear.Name = "checkClear";
            this.checkClear.Size = new System.Drawing.Size(84, 16);
            this.checkClear.TabIndex = 7;
            this.checkClear.Text = "生成前清空";
            this.checkClear.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 101);
            this.Controls.Add(this.checkClear);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnBrowseTarget);
            this.Controls.Add(this.textTarget);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseConfig);
            this.Controls.Add(this.textConfig);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TTA代码生成器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textConfig;
        private System.Windows.Forms.Button btnBrowseConfig;
        private System.Windows.Forms.Button btnBrowseTarget;
        private System.Windows.Forms.TextBox textTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.CheckBox checkClear;
    }
}

