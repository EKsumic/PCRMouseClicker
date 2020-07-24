using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseClicker
{
    public partial class MainForm : Form
    {
        HotKeys h = new HotKeys();
        public MainForm()
        {
            InitializeComponent();
            h.Regist(this.Handle, 0, Keys.Z, CallBack);
            //MessageBox.Show("热键注册成功!");
        }
        protected override void WndProc(ref Message m)
        {
            h.ProcessHotKey(m);
            base.WndProc(ref m);
        }
        public int Flag = 0;
        public void CallBack()
        {
            if (Flag == 0)
            {
                ClickTimer.Start();
                Flag = 1;
            }
            else
            {
                ClickTimer.Stop();
                Flag = 0;
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            h.UnRegist(this.Handle, CallBack);
            //MessageBox.Show("热键卸载成功!");
        }

        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            MouseHelper.mouse_event(MouseHelper.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            MouseHelper.mouse_event(MouseHelper.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }
}
