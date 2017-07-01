namespace Roglaza
{
    partial class FrmGate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGate));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.Btn_access = new System.Windows.Forms.Button();
            this.timerLocker = new System.Windows.Forms.Timer(this.components);
            this.labelwaitLocker = new System.Windows.Forms.Label();
            this.labelTryResult = new System.Windows.Forms.Label();
            this.linkLabel_ShowPas = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Password Required";
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_password.Location = new System.Drawing.Point(19, 52);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(206, 28);
            this.textBox_password.TabIndex = 1;
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_password_TextChanged);
            this.textBox_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_password_KeyDown);
            // 
            // Btn_access
            // 
            this.Btn_access.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_access.Location = new System.Drawing.Point(77, 92);
            this.Btn_access.Name = "Btn_access";
            this.Btn_access.Size = new System.Drawing.Size(98, 34);
            this.Btn_access.TabIndex = 2;
            this.Btn_access.Text = "Access";
            this.Btn_access.UseVisualStyleBackColor = true;
            this.Btn_access.Click += new System.EventHandler(this.button_password_Click);
            // 
            // timerLocker
            // 
            this.timerLocker.Enabled = true;
            this.timerLocker.Interval = 1000;
            this.timerLocker.Tick += new System.EventHandler(this.timerLocker_Tick);
            // 
            // labelwaitLocker
            // 
            this.labelwaitLocker.AutoSize = true;
            this.labelwaitLocker.Location = new System.Drawing.Point(26, 122);
            this.labelwaitLocker.Name = "labelwaitLocker";
            this.labelwaitLocker.Size = new System.Drawing.Size(0, 13);
            this.labelwaitLocker.TabIndex = 3;
            // 
            // labelTryResult
            // 
            this.labelTryResult.AutoSize = true;
            this.labelTryResult.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTryResult.Location = new System.Drawing.Point(112, 129);
            this.labelTryResult.Name = "labelTryResult";
            this.labelTryResult.Size = new System.Drawing.Size(30, 20);
            this.labelTryResult.TabIndex = 4;
            this.labelTryResult.Text = ".......";
            // 
            // linkLabel_ShowPas
            // 
            this.linkLabel_ShowPas.AutoSize = true;
            this.linkLabel_ShowPas.Location = new System.Drawing.Point(231, 60);
            this.linkLabel_ShowPas.Name = "linkLabel_ShowPas";
            this.linkLabel_ShowPas.Size = new System.Drawing.Size(14, 13);
            this.linkLabel_ShowPas.TabIndex = 5;
            this.linkLabel_ShowPas.TabStop = true;
            this.linkLabel_ShowPas.Text = "S";
            this.linkLabel_ShowPas.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_ShowPas_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.linkLabel_ShowPas);
            this.groupBox1.Controls.Add(this.textBox_password);
            this.groupBox1.Controls.Add(this.labelTryResult);
            this.groupBox1.Controls.Add(this.Btn_access);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 180);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Access Panel";
            // 
            // FrmGate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(302, 204);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelwaitLocker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmGate";
            this.Text = "Roglaza";
            this.Load += new System.EventHandler(this.FrmGate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button Btn_access;
        private System.Windows.Forms.Timer timerLocker;
        private System.Windows.Forms.Label labelwaitLocker;
        private System.Windows.Forms.Label labelTryResult;
        private System.Windows.Forms.LinkLabel linkLabel_ShowPas;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

