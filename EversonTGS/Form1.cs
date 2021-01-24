using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EversonTGS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ExibirDados();
        }

        private void ExibirDados()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = DataBase.ExibirVoos();
                dataGridView1.DataSource = dataTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }

        }

        private void btn_incluir_Click(object sender, EventArgs e)
        {
            Foguete objFoguete = new Foguete();
            try
            {
                objFoguete.IdVoo = null;
                objFoguete.DataVoo = Convert.ToDateTime(txtData.Text);
                objFoguete.Custo = Convert.ToDouble(txtCusto.Text);
                objFoguete.Distancia = Convert.ToInt32(txtDistancia.Text);
                if (rbSim.Checked)
                {
                    objFoguete.Captura = "S";
                    objFoguete.NivelDor = null;
                }
                if (rbNao.Checked)
                {
                    objFoguete.Captura = "N";
                    if (Convert.ToInt32(txtDor.Text) > 10 || Convert.ToInt32(txtDor.Text) < 0 || txtDor.Text.Equals(""))
                    {
                        MessageBox.Show("Insira um nível de dor de 0 a 10");
                    }
                    else
                    {
                        objFoguete.NivelDor = Convert.ToInt32(txtDor.Text);
                    }
                }

                DataBase.RegistrarVoo(objFoguete);
                MessageBox.Show("Dados incluidos com sucesso!");
                txtDor.Clear();
                txtDistancia.Clear();
                txtData.Clear();
                txtCusto.Clear();
                rbNao.Checked = false;
                rbSim.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cadeia de caracteres não foi reconhecida como DateTime válido"))
                {
                    MessageBox.Show("Insira uma data válida!");
                }
                if (ex.Message.Contains("A cadeia de caracteres de entrada não estava em um formato correto."))
                {
                    MessageBox.Show("Preencha todos os campos!");
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro inesperado, por favor contate o suporte.");
                    throw;
                }
            }


            ExibirDados();
        }

        private void rbSim_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void rbNao_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int linha = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_VOO"].Value);
                DataBase.ExcluirVoo(linha);
                MessageBox.Show("Dados excluidos com sucesso!");
            }
            else
            {
                MessageBox.Show("Selecione uma linha para excluir!");
            }

            ExibirDados();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int linha = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_VOO"].Value);

                Foguete objFoguete = new Foguete();
                try
                {
                    objFoguete.IdVoo = linha;
                    if (txtData.Text != "")
                    {
                        objFoguete.DataVoo = Convert.ToDateTime(txtData.Text);
                    }
                    else
                    {
                        objFoguete.DataVoo = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["DATA"].Value);
                    }

                    //var x = condition ? 12 : (int?)null;

                    objFoguete.Custo = txtCusto.Text != "" ? Convert.ToDouble(txtCusto.Text) : Convert.ToDouble(dataGridView1.CurrentRow.Cells["CUSTO"].Value);
                    objFoguete.Distancia = txtDistancia.Text != "" ? Convert.ToInt32(txtDistancia.Text) : Convert.ToInt32(dataGridView1.CurrentRow.Cells["DISTANCIA"].Value);
                    if (rbSim.Checked)
                    {
                        objFoguete.Captura = "S";
                        objFoguete.NivelDor = null;
                    }
                    if (rbNao.Checked)
                    {
                        objFoguete.Captura = "N";
                        if (Convert.ToInt32(txtDor.Text) > 10 || Convert.ToInt32(txtDor.Text) < 0 || txtDor.Text.Equals(""))
                        {
                            MessageBox.Show("Insira um nível de dor de 0 a 10");
                        }
                        else
                        {
                            objFoguete.NivelDor = Convert.ToInt32(txtDor.Text);
                        }
                    }
                    else
                    {
                        objFoguete.Captura = Convert.ToString(dataGridView1.CurrentRow.Cells["CAPTURA"].Value);
                        objFoguete.NivelDor = Convert.ToInt32(dataGridView1.CurrentRow.Cells["NIVEL_DOR"].Value);
                    }

                    DataBase.EditarVoo(objFoguete);
                    MessageBox.Show("Dados alterados com sucesso!");
                    txtDor.Clear();
                    txtDistancia.Clear();
                    txtData.Clear();
                    txtCusto.Clear();
                    rbNao.Checked = false;
                    rbSim.Checked = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro inesperado, por favor contate o suporte.", ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha para excluir!");
            }

            ExibirDados();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_VOO"].Value);
            
            DataTable dataTable =  DataBase.GetVoo(linha);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                txtData.Text = dataRow["DATA_VOO"].ToString();
                txtCusto.Text = dataRow["CUSTO"].ToString();
                txtDistancia.Text = dataRow["DISTANCIA"].ToString();
                txtDor.Text = dataRow["NIVEL_DOR"].ToString();
                if (dataRow["CAPTURA"].Equals("N"))
                {
                    rbNao.Checked = true;
                }
                else
                {
                    rbSim.Checked = true;
                }
            }

        }
    }
}

