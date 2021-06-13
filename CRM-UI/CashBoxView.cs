using CRM_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM_UI
{
    class CashBoxView
    {
        private CashDesk CashDesk;
        public Label CashDeskName { get; set; }
        public NumericUpDown Price { get; set; }
        public ProgressBar QueuLength { get; set; }
        public Label ExitCustomersCount { get; set; }

        public CashBoxView(CashDesk cashDesk, int number, int x, int y)
        {
            
            CashDeskName = new Label();
            Price = new NumericUpDown();
            QueuLength = new ProgressBar();
            ExitCustomersCount = new Label();

            CashDesk = cashDesk;

            CashDeskName.AutoSize = true;
            CashDeskName.Location = new System.Drawing.Point(x, y);
            CashDeskName.Name = $"label {number}";
            CashDeskName.Size = new System.Drawing.Size(35, 13);
            CashDeskName.TabIndex = number;
            CashDeskName.Text = CashDesk.ToString();

            Price.Location = new System.Drawing.Point(x + 100, y);
            Price.Name = $"numericUpDown {number}";
            Price.Size = new System.Drawing.Size(120, 20);
            Price.TabIndex = number;
            Price.Maximum = 100000000;

            QueuLength.Location = new System.Drawing.Point(x + 250, y);
            QueuLength.Maximum = cashDesk.MaxQueueLenght;
            QueuLength.Name = $"progressBar {number}";
            QueuLength.Size = new System.Drawing.Size(100, 23);
            QueuLength.TabIndex = number;
            QueuLength.Value = 0;

            ExitCustomersCount.AutoSize = true;
            ExitCustomersCount.Location = new System.Drawing.Point(x + 400, y);
            ExitCustomersCount.Name = $"label2 {number}";
            ExitCustomersCount.Size = new System.Drawing.Size(35, 13);
            ExitCustomersCount.TabIndex = number;
            ExitCustomersCount.Text = "";

            cashDesk.CheckClosed += CashDesk_CheckClosed;
        }

        private void CashDesk_CheckClosed(object sender, Check e)
        {
            Price.Invoke((Action)delegate 
            {
                Price.Value += e.Price;
                QueuLength.Value = CashDesk.Count;
                ExitCustomersCount.Text = CashDesk.ExitCustomer.ToString();
            });
        }
    }
}
