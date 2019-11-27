using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class movimento : Form
    {
        public movimento()
        {
            InitializeComponent();
        }
        
                
        private void button3_Click(object sender, EventArgs e)
        {

            //define string de conexÆo - Provedor + fonte de dados (caminho do banco de dados e seu nome)
            string strConnection = "Data Source=.\\SQLEXPRESS;Initial Catalog=Recanto_Sertanejo;User ID=sa;Password=#lecoteco1975 ;Provider=SQLOLEDB";

            //define a instru‡Æo SQL para atualizar os dados da tabela Clientes - UPDATE tabela SET campos
            //string strSQL = "DELETE FROM [Recanto_Sertanejo].[dbo].[Relatorio] WHERE Id >=1";

            //Deleta e recria bd
            string strSQL = "DROP TABLE [dbo].[Relatorio] CREATE TABLE [dbo].[Relatorio]([Id] [int] IDENTITY(1,1) NOT NULL,	[Produto] [nvarchar](50) NOT NULL,[Quantidade] [int] NOT NULL,	[senha_noite] [nvarchar](50) NOT NULL,[Data] [nvarchar](50) NOT NULL,CONSTRAINT [PK_Relatorio] PRIMARY KEY CLUSTERED ([Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";

            //cria a conexÆo com o banco de dados
            OleDbConnection dbConnection = new OleDbConnection(strConnection);

            //cria a conexão com o banco de dados
            OleDbConnection con = new OleDbConnection(strConnection);
            //cria o objeto command para executar a instruçao sql
            OleDbCommand cmd = new OleDbCommand(strSQL, con);

            //abre a conexao
            con.Open();

            //define o tipo do comando 
            cmd.CommandType = CommandType.Text;
            //cria um dataadapter
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            
            //cria um objeto datatable
            DataTable clientes = new DataTable();
            
            //preenche o datatable via dataadapter
            da.Fill(clientes);

            //atribui o datatable ao datagridview para exibir o resultado
            //dataGridView1.DataSource = clientes;
            con.Close();
            this.RelatorioTableAdapter.Fill(this.Recanto_SertanejoDataSet.Relatorio);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();


        }

        private void button2_Click(object sender, EventArgs e)
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

        private void movimento_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Recanto_SertanejoDataSet.Relatorio' table. You can move, or remove it, as needed.
            this.RelatorioTableAdapter.Fill(this.Recanto_SertanejoDataSet.Relatorio);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

       

      
    }
}
