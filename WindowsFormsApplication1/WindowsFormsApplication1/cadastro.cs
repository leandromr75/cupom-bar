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
    public partial class cadastro : Form
    {
        private int contaLinhas;
        private int RowId = 0;
        private int _Id = 0;
        private string _Produto;
        private string _Familia;
        private string _Valor;
        private string _Quantidade;
        LinqDadosDataContext db = new LinqDadosDataContext();
        
        public cadastro()
        {
            InitializeComponent();
        }

        private void cadastro_Load(object sender, EventArgs e)
        {
            BindDataGridView();
            label1.Visible = false;

        }
        private void BindDataGridView()
        {
            var getData = from Produtos in db.Produtos
                          select Produtos;

            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;
            contaLinhas = dataGridView1.RowCount - 1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RowId = dataGridView1.CurrentRow.Index;
            _Id = Convert.ToInt32(dataGridView1[0, RowId].Value);

            contaLinhas = dataGridView1.RowCount - 1;

            if (RowId == contaLinhas)
            {
                button1.Visible = true;
            }

            button2.Visible = true;
            button3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[RowId].Cells[1].FormattedValue.ToString() != "")
            {
                _Produto = dataGridView1.Rows[RowId].Cells[1].Value.ToString();
            }
            else { _Produto = null; }

            if (dataGridView1.Rows[RowId].Cells[2].FormattedValue.ToString() != "")
            {
                _Familia = dataGridView1.Rows[RowId].Cells[2].Value.ToString();
            }
            else { _Familia = null; }

            if (dataGridView1.Rows[RowId].Cells[3].FormattedValue.ToString() != "")
            {
                _Valor = dataGridView1.Rows[RowId].Cells[3].Value.ToString();
            }
            else { _Valor = null; }

            if (dataGridView1.Rows[RowId].Cells[4].FormattedValue.ToString() != "")
            {
                _Quantidade = dataGridView1.Rows[RowId].Cells[4].Value.ToString();
            }
            else { _Quantidade = null; }

            try
            {
                Produtos prod = new Produtos();
                if (_Produto != null && _Familia != null && _Valor != null)
                {
                    prod.Produto = _Produto;
                    prod.Familia = _Familia;
                    prod.Valor = Convert.ToDecimal (_Valor);
                    prod.Quantidade = Convert.ToInt32 (_Quantidade);
                    //db.Produtos.InsertOnSubmit(prod);
                    db.SubmitChanges();
                }
                else
                {
                    MessageBox.Show("Informe os valores para inclusão...");
                }
                BindDataGridView();
                label1.Text = "Registro incluído com sucesso !!";
                label1.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[RowId].Cells[1].Value != null)
            {
                _Produto = dataGridView1.Rows[RowId].Cells[1].Value.ToString();
            }
            if (dataGridView1.Rows[RowId].Cells[2].Value != null)
            {
                _Familia = dataGridView1.Rows[RowId].Cells[2].Value.ToString();
            }
            if (dataGridView1.Rows[RowId].Cells[3].Value != null)
            {
                _Valor = dataGridView1.Rows[RowId].Cells[3].Value.ToString();
            }
            if (dataGridView1.Rows[RowId].Cells[4].Value != null)
            {
                _Quantidade = dataGridView1.Rows[RowId].Cells[4].Value.ToString();
            }
            if (_Id != 0)
            {
                var getData = (from Produtos in db.Produtos
                               where Produtos.Id == _Id
                               select Produtos).Single();

                getData.Produto = _Produto;
                getData.Familia = _Familia;
                getData.Valor = Convert.ToDecimal (_Valor);
                getData.Quantidade = Convert.ToInt32 (_Quantidade);

                db.SubmitChanges();

                BindDataGridView();
                label1.Text = "Registros atualizados com sucesso !!";
                label1.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o registro selecionado?",
               "Aviso", MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false)
               == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    RowId = dataGridView1.SelectedRows[i].Index;
                    _Id = Convert.ToInt32(dataGridView1[0, RowId].Value);

                    if (_Id != 0)
                    {
                        var getData = (from Produtos in db.Produtos
                                       where Produtos.Id == _Id
                                       select Produtos).Single();

                        db.Produtos.DeleteOnSubmit(getData);
                        db.SubmitChanges();
                    }
                }
            }
            label1.Text = "Registro deletado com sucesso !!";
            label1.Visible = true;
            BindDataGridView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja encerrar a aplicação ?",
               "Aviso", MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0, false)
               == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        
       
        

        
    }
}
