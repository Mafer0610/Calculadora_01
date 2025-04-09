using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.calitha.goldparser;

namespace Calculadora
{
    public partial class Calculadora : Form
    {
        private double memoria = 0;
        private AnalizadorGoldParser parser;

        public Calculadora()
        {
            InitializeComponent();
            parser = new AnalizadorGoldParser("gramatica.cgt");
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

        private void btnIgual_Click(object sender, EventArgs e)
        {
            string expresion = txtOpe.Text;
            double resultado;

            if (parser.EvaluarExpresion(expresion, out resultado))
            {
                txtRespu.Text = "Resultado: " + resultado.ToString();
            }
            else
            {
                txtRespu.Text = "Error en la expresión.";
            }
        }
    }

    public class AnalizadorGoldParser
    {
        private LALRParser parser;
        private double resultadoFinal;

        public AnalizadorGoldParser(string archivoGramatica)
        {
            if (!File.Exists(archivoGramatica))
            {
                MessageBox.Show("No se encontró el archivo de gramática.");
                return;
            }

            try
            {
                FileStream stream = new FileStream(archivoGramatica, FileMode.Open, FileAccess.Read, FileShare.Read);
                CGTReader reader = new CGTReader(stream);
                parser = new LALRParser(reader.GetSymbolCollection(), reader.GetRuleCollection(), reader.GetLALRStateCollection(), reader.GetInitialLALRState());
                parser.OnReduce += new LALRParser.ReduceHandler(AceptarEvento);
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la gramática: " + ex.Message);
            }
        }

        public bool EvaluarExpresion(string expresion, out double resultado)
        {
            resultadoFinal = 0;
            bool success = true;

            try
            {
                IStringTokenizer tokenizer = new StringTokenizer(expresion);
                parser.Parse(tokenizer);
                resultado = resultadoFinal;
                return success;
            }
            catch (Exception)
            {
                resultado = 0;
                return false;
            }
        }

        private void AceptarEvento(LALRParser parser, ReduceEventArgs args)
        {
            Rule rule = args.ReducedRule;
            int ruleIndex = rule.Index;

            try
            {
                Token[] tokens = new Token[args.TokenCount];
                for (int i = 0; i < args.TokenCount; i++)
                {
                    tokens[i] = args.GetToken(i);
                }

                switch (ruleIndex)
                {
                    case 1: // Suma
                        double val1 = GetDoubleValue(tokens[0]);
                        double val2 = GetDoubleValue(tokens[2]);
                        resultadoFinal = val1 + val2;
                        break;
                    case 2: // Resta
                        resultadoFinal = GetDoubleValue(tokens[0]) - GetDoubleValue(tokens[2]);
                        break;
                    case 3: // Multiplicación
                        resultadoFinal = GetDoubleValue(tokens[0]) * GetDoubleValue(tokens[2]);
                        break;
                    case 4: // División
                        resultadoFinal = GetDoubleValue(tokens[0]) / GetDoubleValue(tokens[2]);
                        break;
                    case 5: // Paréntesis
                        resultadoFinal = GetDoubleValue(tokens[1]);
                        break;
                    case 6: // Número
                        resultadoFinal = double.Parse(tokens[0].Text);
                        break;
                    default:
                        resultadoFinal = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al evaluar la expresión: " + ex.Message);
            }
        }

        private double GetDoubleValue(Token token)
        {
            // Aquí debes extraer el valor numérico del token según la estructura de tu Token
            if (token == null || string.IsNullOrEmpty(token.Text))
                return 0;

            try
            {
                return double.Parse(token.Text);
            }
            catch
            {
                return 0;
            }
        }
    }
}
