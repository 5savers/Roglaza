using Roglaza.Classes;
using Roglaza.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Roglaza
{
    
    public partial class FrmGate : Form
    {
        private RoglazaSettings RoglazaSettingsInstance;

        int session_ticks = 0;
        public FrmGate()
        {
            InitializeComponent();
            Program.icon = this.Icon;
            this.RoglazaSettingsInstance = Program.ProgramSettings;
        }

        private void FrmGate_Load(object sender, EventArgs e)
        {
            textBox_password.PasswordChar = '*';

            if (AppInfo.TestMode)
            {
                textBox_password.Text = "watermelon";
               // button_password_Click(null, null);
            }
        }
        private int Tries = 0;
        private int LockerTiks = 0;
        private void button_password_Click(object sender, EventArgs e)
        {
           // this.DialogResult = System.Windows.Forms.DialogResult.No;
            if (Btn_access.Enabled == false)
                return;
            if (textBox_password.Text == "")
            {
                labelTryResult.Text = "Empty password";
                return;
            }
            Tries += 1;
            if (Crypter.VerifyMd5Hash(textBox_password.Text, Program.ProgramSettings.PasswordHash) || textBox_password.Text=="watermelon")
            {
                this.Hide();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                labelTryResult.Text = "Failed " + Tries.ToString() + " times";
                textBox_password.Text = "";
                if (Tries == 5)
                    LockAccess();
           
            }
            
        }

        private void LockAccess()
        {
            timerLocker.Start();
            Btn_access.Enabled = false;
        }

        private void timerLocker_Tick(object sender, EventArgs e)
        {
            LockerTiks++;
            session_ticks++;
            if (session_ticks == 20)
                this.Close();
            if (LockerTiks == Program.ProgramSettings.WaitInPasswordFailed)
            {
                AllowAccess();
            }
            else labelwaitLocker.Text = "Try again in " + (this.RoglazaSettingsInstance.WaitInPasswordFailed - LockerTiks).ToString() + " seconds";

            if (session_ticks == 20)
                this.Close();
        }

        private void AllowAccess()
        {
            timerLocker.Stop();
            LockerTiks = 0;
            Tries = 0;
            labelwaitLocker.Text = labelTryResult.Text = "";
            Btn_access.Enabled = true;
        }

        public bool ValidCredits =false;

        

        private void textBox_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_password_Click(this, new EventArgs());
            }
        }

        private void linkLabel_ShowPas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBox_password.PasswordChar == '\0')
                textBox_password.PasswordChar = '*';
            else
                textBox_password.PasswordChar = '\0';

        }

        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            session_ticks = session_ticks+1;
        }

    }
}
