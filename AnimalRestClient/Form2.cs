using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalRestClient
{
    public partial class Form2 : Form
    {
        private Form1.SendMessage Sender;
        public Form2(Form1.SendMessage sender)
        {
            InitializeComponent();
            this.Sender = sender;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Sender(true);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Sender(false);
            Close();
        }
    }
}
