using AnimalRestClient.BUS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalRestClient
{
    public partial class Form1 : Form
    {
        public delegate void SendMessage(Boolean check);
        public SendMessage Sender;
        public Form1()
        {
            InitializeComponent();
            Sender = new SendMessage(GetMessage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new AnimalBUS().GetAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text.Trim().Length>0)
            {
                dataGridView1.DataSource = new AnimalBUS().GetSelectByName(txtSearch.Text.Trim());
            }
            else
            {
                dataGridView1.DataSource = new AnimalBUS().GetAll();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (new AnimalBUS().Insert(BuildItem()))
            {
                dataGridView1.DataSource = new AnimalBUS().GetAll().ToList();
                ClearEditText();
                MessageBox.Show("Insert thành công");
            }
            else MessageBox.Show("Insert thất bại");
        }

        private void GetMessage(Boolean check)
        {
            if (check)
            {
                if (new AnimalBUS().Delete(BuildItem()))
                {
                    dataGridView1.DataSource = new AnimalBUS().GetAll();
                    ClearEditText();
                    MessageBox.Show("Delete thành công");
                }
                else
                    MessageBox.Show("Delete thất bại");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            if (selectedrowindex != -1)
            {
                Animal animal = new AnimalBUS().GetDetail(int.Parse(dataGridView1.Rows[selectedrowindex].Cells[0].Value.ToString()));
                txt_ID.Text = animal.ID.ToString();
                txt_KIND.Text = animal.KIND;
                txt_WEIGHT.Text = animal.WEIGHT.ToString();
                txt_HEIGHT.Text = animal.HEIGHT.ToString();
                txt_AGE.Text = animal.AGE.ToString();
                if (animal.SEX.CompareTo("Male") == 0)
                {
                    radioButtonMale.Checked = true;
                }
                else radioButtonFemale.Checked = true;
            }
        }

        private Animal BuildItem()
        {
            String sex = "";
            if (radioButtonFemale.Checked)
            {
                sex = "Female";
            }
            else sex = "Male";
            Animal itemAnimals = new Animal()
            {
                ID = int.Parse(txt_ID.Text.ToString()),
                KIND = txt_KIND.Text.ToString(),
                SEX = sex,
                AGE = int.Parse(txt_AGE.Text.ToString()),
                HEIGHT = Math.Round(float.Parse(txt_HEIGHT.Text.ToString()), 2),
                WEIGHT = Math.Round(float.Parse(txt_WEIGHT.Text.ToString()), 2)
            };
            return itemAnimals;
        }

        private void ClearEditText()
        {
            txt_ID.Text = "";
            txt_KIND.Text = "";
            txt_WEIGHT.Text = "";
            txt_HEIGHT.Text = "";
            txt_AGE.Text = "";
            radioButtonFemale.Checked = false;
            radioButtonMale.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (new AnimalBUS().Update(BuildItem()))
            {
                dataGridView1.DataSource = new AnimalBUS().GetAll().ToList();
                ClearEditText();
                MessageBox.Show("Update thành công");
            }
            else MessageBox.Show("Update thất bại");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this.Sender);

            f.Owner = this;
            f.StartPosition = FormStartPosition.CenterParent;

            f.ShowDialog();
        }

        private void txt_HEIGHT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_WEIGHT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_AGE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
