using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lista_de_Compras
{
    public partial class vater_app : Form
    {
        public vater_app()
        {
            InitializeComponent();
        }
        void Limpar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            num_prod.Text = (richTextBox1.Lines.Length -1).ToString();

            //grid

            int n = richTextBox1.Lines.Length - 1;
            dataGridView1.RowCount = n - 1;

            int linha = 0;
            for (int i = 1; i < n; i++)
            {
                string[] dados = richTextBox1.Lines[i].Split('#');
                if (dados[5] == "A")
                {
                    dataGridView1.Rows[linha].Cells[0].Value = i;
                    dataGridView1.Rows[linha].Cells[1].Value = dados[0];
                    dataGridView1.Rows[linha].Cells[2].Value = dados[1];
                    dataGridView1.Rows[linha].Cells[3].Value = dados[2];
                    dataGridView1.Rows[linha].Cells[4].Value = dados[3];
                    dataGridView1.Rows[linha].Cells[5].Value = dados[4];
                    linha++;
                } 
            }
            dataGridView1.RowCount = linha;
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile("Dados.txt", RichTextBoxStreamType.PlainText);
            MessageBox.Show ("Arquivo salvo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void carregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.LoadFile("Dados.txt", RichTextBoxStreamType.PlainText);
                MessageBox.Show("Arquivo carregado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                richTextBox1.Text = "\n";
                richTextBox1.SaveFile("Dados.txt", RichTextBoxStreamType.PlainText);
                Limpar();
                MessageBox.Show("Arquivo apagado ou corrompido", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Criando banco novamente", "Aviso", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void vater_app_Load(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.LoadFile("Dados.txt", RichTextBoxStreamType.PlainText);
                Limpar();
            }
            catch
            {
                richTextBox1.Text = "\n";
                richTextBox1.SaveFile("Dados.txt", RichTextBoxStreamType.PlainText);
                Limpar();
            }
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                int n = int.Parse(textBox6.Text);
                if (n < richTextBox1.Lines.Length - 1)
                {
                    string linha = richTextBox1.Lines[n];
                    string[] campo = linha.Split('#');
                    if (campo[5] == "A")
                    {
                        textBox1.Text = campo[0];
                        textBox2.Text = campo[1];
                        textBox3.Text = campo[2];
                        textBox4.Text = campo[3];
                        textBox5.Text = campo[4];
                        num_prod.Text = n.ToString();
                    }
                    else
                    {
                        MessageBox.Show("O produto solicitado foi excluído", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("O produto solicitado não foi encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("O produto solicitado não foi encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void adicinarToolStripMenuItem_Click(object sender, EventArgs e)
        {   if (textBox1.Text != "")
            {
                String dados_novos = (textBox1.Text + "#" + textBox2.Text + "#" + textBox3.Text + "#" +
                                        textBox4.Text + "#" + textBox5.Text + "#A");
                if (int.Parse(num_prod.Text) == (richTextBox1.Lines.Length - 1))
                {
                    richTextBox1.Text += (dados_novos + "\n");
                    MessageBox.Show("O produto foi adicionado ao banco", "Aviso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string dados_antigos = richTextBox1.Lines[int.Parse(num_prod.Text)];
                    richTextBox1.Text = richTextBox1.Text.Replace(dados_antigos, dados_novos);
                    MessageBox.Show("O produto atualizado", "Aviso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Limpar();
                richTextBox1.SaveFile("Dados.txt", RichTextBoxStreamType.PlainText);
            }
            else
            {
                MessageBox.Show("Impossível adicionar produto sem nome", "Aviso",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = int.Parse(num_prod.Text);
            if (n < richTextBox1.Lines.Length - 1)
            { 
                if (DialogResult.Yes == MessageBox.Show("Tem certeza que dejasa excluir o produto", "Aviso",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    String dados_novos = (textBox1.Text + "#" + textBox2.Text + "#" + textBox3.Text + "#" +
                                        textBox4.Text + "#" + textBox5.Text + "#E");
                    String dados_antigos = richTextBox1.Lines[int.Parse(num_prod.Text)];
                    richTextBox1.Text = richTextBox1.Text.Replace(dados_antigos, dados_novos);
                    richTextBox1.SaveFile("Dados.txt", RichTextBoxStreamType.PlainText);
                    MessageBox.Show("Produto excluído com susceso", "Aviso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpar();
                }
                else
                {
                    MessageBox.Show("Produto não foi excluído", "Aviso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Não é possível excluir um produto inexistente", "Aviso",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limparToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void reniciarBancoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Ao continuar todo o banco de dados será reniciado",
                                 "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
               if (DialogResult.Yes == MessageBox.Show("Deseja mesmo continuar?",
                                 "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    richTextBox1.SelectAll();
                    richTextBox1.SelectedText = ("\n");
                    Limpar();
                    richTextBox1.SaveFile("Dados.txt", RichTextBoxStreamType.PlainText);
                }
            }
        }
    }
}
