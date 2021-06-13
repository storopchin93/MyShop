using CRM_BL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_UI
{
    public partial class Autorization : Form
    {
        public Customer Customer { get; set; }
        public Autorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer = new Customer()
            {
                Name = textBox1.Text
            };
            DialogResult = DialogResult.OK;
        }
    }
}
