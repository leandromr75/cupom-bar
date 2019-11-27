using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public static class Prompt

    {

        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.ControlBox = false;
            prompt.Width = 250;
            prompt.Height = 200;
            prompt.StartPosition = FormStartPosition.CenterScreen;
          
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 100 };
            Button confirmation = new Button() { Text = "Ok", Left = 50, Width = 100, Top = 100 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            
            prompt.ShowDialog();
            return textBox.Text;


        }
        
       
       
    }
}
