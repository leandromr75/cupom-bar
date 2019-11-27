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


    public partial class CodNoite : Form
    {
        private int _Id;
        private string _codigo_noite;
        private int RowId = 0;
        LinqDadosDataContext db = new LinqDadosDataContext();

        public CodNoite()
        {
            InitializeComponent();
        }

        private void CodNoite_Load(object sender, EventArgs e)
        {
            BindDataGridView();
        }

        private void BindDataGridView()
        {
            var getData = from Cod in db.Cods
                          where Cod.Id == 1
                          select Cod;
            dataGridView1.DataSource = getData;
            dataGridView1.Columns[0].ReadOnly = true;


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RowId = dataGridView1.CurrentRow.Index;
            _Id = Convert.ToInt32(dataGridView1[0, RowId].Value);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            _codigo_noite = dataGridView1.Rows[RowId].Cells[1].Value.ToString();
            Cod senha = new Cod();
            senha.senha_noite = _codigo_noite;
            //db.Cods.InsertOnSubmit(senha);
            db.SubmitChanges();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
