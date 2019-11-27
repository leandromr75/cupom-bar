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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form1 frm1 = new Form1();
            frm1.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Você deseja sair do aplicativo?";
            string caption = "Recanto Sertanejo";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                // Closes the parent form.
                Application.Exit();


            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.cadastro cad = new cadastro();
            cad.Show();
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.CodNoite cad = new CodNoite();
            cad.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.movimento mov = new movimento();
            mov.Show();
            this.Visible = false;
        }
    }
}
 