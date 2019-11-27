using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.ActiveControl = textBox1;
        }
        


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "recanto8")
            {
                //MessageBox.Show("senha correta");
                WindowsFormsApplication1.Form3 frm = new Form3();
                frm.Show();
                this.Visible = false;
            }
            else
                MessageBox.Show("senha incorreta");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
