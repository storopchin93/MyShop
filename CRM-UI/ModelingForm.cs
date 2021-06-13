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
    public partial class ModelingForm : Form
    {
        ShopComputerModel model = new ShopComputerModel();


        public ModelingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var cashBoxes = new List<CashBoxView>();

            for (int i = 0; i < model.CashDesks.Count; i++)
            {
                var box = new CashBoxView(model.CashDesks[i], i, 10, i * 26);
                cashBoxes.Add(box);
                Controls.Add(box.CashDeskName);
                Controls.Add(box.Price);
                Controls.Add(box.QueuLength);
                Controls.Add(box.ExitCustomersCount);
            }

            model.Start();
        }

        private void ModelingForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = model.CustomerSpeed;
            numericUpDown2.Value = model.CashDeskSpeed;
        }

        private void ModelingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.Stop();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            model.CustomerSpeed = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            model.CashDeskSpeed = (int)numericUpDown2.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            model.Stop();
        }
    }
}
