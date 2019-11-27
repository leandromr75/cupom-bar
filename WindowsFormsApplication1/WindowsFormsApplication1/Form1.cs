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
    public partial class Form1 : Form
    {
        //declaração variáveis da classe relatório
        private string _Produto;

        private int _Quantidade;

        private string _senha_noite;

        private string _Data;



        
        private int contaLinhas; //variável BindDataGridView

        LinqDadosDataContext db = new LinqDadosDataContext(); //instancia conexão  LINQ com BD
        
        decimal Total; //variável que armazena valor total da venda
        
        int iRetorno = 0; //Variável para retorno das chamadas para MP2032
        string senha;    
        string[] itens;
        int[] quantidade;
        int i;
        int j;
        int contador;
        decimal valorparcial;
        decimal valorUnitario;
        

        public Form1()
        {
            InitializeComponent();
           
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            itens = new string[50];
            quantidade = new int[50];
            i = 0;
            j = 0;
            contador = 0;
            

            //Configurando a impressora e sua porta
            iRetorno = WindowsFormsApplication1.MP2032.ConfiguraModeloImpressora(7);
            iRetorno = WindowsFormsApplication1.MP2032.IniciaPorta("USB");
            iRetorno = WindowsFormsApplication1.MP2032.AjustaLarguraPapel(80);
            
            //recupera senha da noite
            CodigoBind();
            
           /* if (iRetorno <= 0) //testa se a conexão da porta foi bem sucedido
            {
                MessageBox.Show("Não foi possível conectar com a impressora!!!");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Impressora conectada!!!");
            }*/ 

        }

        private void dataGridView1_CellClick (object sender, DataGridViewCellEventArgs e)
        {
            
        }

        
        
        private void BindDataGridView()
        {
            var getData = from Produtos in db.Produtos
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }
        
        //Busca código da noite no banco
        private void CodigoBind()
        {
            var getData = (from Cod in db.Cods
                          where Cod.Id == 1
                          select Cod.senha_noite).Single();
            senha = getData;
            return;            

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Você deseja verificar o movimento?";
            string caption = "Recanto Sertanejo";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                iRetorno = WindowsFormsApplication1.MP2032.FechaPorta();
                WindowsFormsApplication1.movimento mov = new movimento();
                mov.Show();
                this.Visible = false;
                // Closes the parent form.
                //Application.Exit();
            }
            else
            {
                Application.Exit();
            }
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Gelo"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            BindDataGridView();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            iRetorno = WindowsFormsApplication1.MP2032.Le_Status();


            if (iRetorno == 0)
            {
                MessageBox.Show("Erro de comunicação com impressora");
                //Application.Exit();
            }

            if (iRetorno == 5)
            {
                MessageBox.Show("Impressora com pouco papel");
                //Application.Exit();
            }

            if (iRetorno == 9)
            {
                MessageBox.Show("Tampa da impressora aberta");
                //Application.Exit();
            }

            if (iRetorno == 32)
            {
                MessageBox.Show("Impressora sem papel");
                //Application.Exit();
            } 
            
            string promptValue = Prompt.ShowDialog("Quantidade", "Recanto Sertanejo");
            Int32 qtde;
            if (int.TryParse(promptValue.Trim(), out qtde) == false)
            {
                MessageBox.Show("Quantidade incorreta");
                return; 
            }
           
            valorUnitario = Convert.ToDecimal (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].FormattedValue.ToString());
            valorparcial = valorUnitario * qtde;
            textBox4.Text += qtde + "\r\n";

            textBox1.Text += dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].FormattedValue.ToString() + "\r\n";

            itens[i] = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].FormattedValue.ToString();
            quantidade[j] = qtde;
            i = i + 1;
            j = j + 1;
            contador = contador + 1;
            textBox3.Text += "R$ ";
            textBox3.Text += valorparcial + "\r\n";
            
            Total = Total + valorparcial;
            textBox2.Text = "R$ " + Convert.ToString (Total);

            
            
            
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string message = "Você deseja Imprimir Comprovantes??";
            string caption = "Recanto Sertanejo";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Mostra a MessageBox.

            result = MessageBox.Show(this, message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                int k = 0;

                while (contador > 0)
                {

                   
                    for (int l = 0; l < quantidade[k]; l++)
                    {

                        string sFileName = "c:\\Recanto\\Logo.bmp";
                        iRetorno = WindowsFormsApplication1.MP2032.ImprimeBitmap(sFileName, 0);
                        iRetorno = WindowsFormsApplication1.MP2032.FormataTX("\n", 3, 0, 1, 1, 1);
                        iRetorno = WindowsFormsApplication1.MP2032.FormataTX("      " + itens[k], 3, 0, 1, 1, 1);
                        iRetorno = WindowsFormsApplication1.MP2032.FormataTX("\n", 3, 0, 1, 1, 1);
                        iRetorno = WindowsFormsApplication1.MP2032.FormataTX("\n", 3, 0, 1, 1, 1);
                        iRetorno = WindowsFormsApplication1.MP2032.FormataTX(senha, 3, 0, 1, 1, 1);
                        iRetorno = WindowsFormsApplication1.MP2032.FormataTX("\n", 3, 0, 1, 1, 1);

                        DateTime Data = DateTime.Now;
                        string DataFormato = Data.ToString("F");
                        iRetorno = WindowsFormsApplication1.MP2032.FormataTX(DataFormato, 1, 0, 1, 0, 1);
                        iRetorno = WindowsFormsApplication1.MP2032.AcionaGuilhotina(0);

                        if (l == 0)
                        {
                            //Inserir no relatório movimento
                            DateTime Data2 = DateTime.Now;
                            string DataFormato2 = Data2.ToString("D");
                            _Produto = itens[k];
                            _Quantidade = quantidade[k];
                            _senha_noite = senha;
                            _Data = DataFormato2;
                            Relatorio relat = new Relatorio();
                            relat.Produto = _Produto;
                            relat.Quantidade = _Quantidade;
                            relat.senha_noite = _senha_noite;
                            relat.Data = _Data;

                            db.Relatorios.InsertOnSubmit(relat);
                            try
                            {
                                db.SubmitChanges();
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }
                            

                        }
                         
                        

                        
                    }
                    
                        
                    
                    
                      
                    contador = contador - 1;
                    k = k + 1;
                }

               
                



                valorUnitario = 0;
                valorparcial = 0;
                Total = 0;
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
                textBox4.Text = null;
                contador = 0;
                i = 0;
                j = 0;

            }
            
            
            
            
            

        }

        private void button16_Click(object sender, EventArgs e)
        {
            
            valorUnitario = 0;
            valorparcial = 0;
            Total = 0;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            contador = 0;
            i = 0;
            j = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Litros Bebidas"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Cervejas"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Batidas"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Refrigerante_Agua"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Ice"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Torres"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Cerveja Preta"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Porções"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Energeticos"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var getData = from Produtos in db.Produtos
                          where Produtos.Familia == "Doses"
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }
    }
}
