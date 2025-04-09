using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Calculadora : Form
    {
        private double memoria = 0;

        public Calculadora()
        {
            InitializeComponent();
        }
        private void btn9_Click(object sender, EventArgs e) { txtOpe.Text += "9"; }
        private void btn8_Click(object sender, EventArgs e) { txtOpe.Text += "8"; }
        private void btn7_Click(object sender, EventArgs e) { txtOpe.Text += "7"; }
        private void btn6_Click(object sender, EventArgs e) { txtOpe.Text += "6"; }
        private void btn5_Click(object sender, EventArgs e) { txtOpe.Text += "5"; }
        private void btn4_Click(object sender, EventArgs e) { txtOpe.Text += "4"; }
        private void btn3_Click(object sender, EventArgs e) { txtOpe.Text += "3"; }
        private void btn2_Click(object sender, EventArgs e) { txtOpe.Text += "2"; }
        private void btn1_Click(object sender, EventArgs e) { txtOpe.Text += "1"; }
        private void btn0_Click(object sender, EventArgs e) { txtOpe.Text += "0"; }
        private void btnPunto_Click(object sender, EventArgs e) { txtOpe.Text += "."; }
        private void btnPi_Click(object sender, EventArgs e) { txtOpe.Text += "3.14159"; }
        private void btnLParen_Click(object sender, EventArgs e) { txtOpe.Text += "("; }
        private void btnRParen_Click(object sender, EventArgs e) { txtOpe.Text += ")"; }
        private void btnC_Click(object sender, EventArgs e) { txtOpe.Text = ""; txtRespu.Text = ""; }
        private void btnCE_Click(object sender, EventArgs e) { if (txtOpe.Text.Length > 0) txtOpe.Text = txtOpe.Text.Substring(0, txtOpe.Text.Length - 1); }
        private void btnSIN_Click(object sender, EventArgs e) { txtOpe.Text += "sin"; }
        private void btnRaiz_Click(object sender, EventArgs e) { txtOpe.Text += "raiz"; }
        private void btnSuma_Click(object sender, EventArgs e) { txtOpe.Text += "+"; }
        private void btnResta_Click(object sender, EventArgs e) { txtOpe.Text += "-"; }
        private void btnMultiplica_Click(object sender, EventArgs e) { txtOpe.Text += "*"; }
        private void btnDividir_Click(object sender, EventArgs e) { txtOpe.Text += "/"; }
        private void btnM_Click(object sender, EventArgs e) { if (double.TryParse(txtRespu.Text, out double valorMemoria)) { memoria = valorMemoria; } }
        private void btnMR_Click(object sender, EventArgs e) { txtOpe.Text += memoria.ToString(); }
    }
}
