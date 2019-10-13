using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
namespace TrisemusCipher
{
    public partial class Form1 : Form
    {
        void ShuffleAlphabet()
        {
            Random random = new Random();
            for (int i = 32; i >= 1; i--)
{
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = TricemusTable[0,j];
                TricemusTable[0,j] = TricemusTable[0,i];
                TricemusTable[0,i] = temp;
            }
        }
        char[,] TricemusTable = new char[33, 33];        
        public void CreateAlphabet()
        {
            int id = 0;
            for (char i = 'А'; i <= 'Я'; i++)
            {               
                TricemusTable[0, id++] = i;
                
            }
            TricemusTable[0, id] = ' ';
            ShuffleAlphabet();
            for (int i = 1; i < 33; i++)
            {
                for (int j = 0; j < 33; j++)
                {
                    TricemusTable[i, j] = TricemusTable[i - 1, j < 32 ? j + 1 : 0];
                }
            }
        }
        public void Encrypt()
        {
            string text = textBox1.Text;
            string cypher = "";
            List<string> textParts = new List<string>();
            if (!String.IsNullOrEmpty(text))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    for (int j = 0; j < 33; j++)
                    {
                        if (TricemusTable[0,j] == text[i])
                        {
                            cypher += TricemusTable[i % 33, j];
                        }
                    }
                }
                
            }
            textBox2.Text = cypher;
        }
        public void Decrypt()
        {
            string encriptedText = textBox2.Text;
            string text = "";

            for (int i = 0; i < encriptedText.Length; i++)
            {
                for (int j = 0; j < 33; j++)
                {
                    if (encriptedText[i] == TricemusTable[i % 33, j])
                        text += TricemusTable[0, j];
                }
            }
            textBox1.Text = text;
        }
        public Form1()
        {            
            InitializeComponent();
            CreateAlphabet();
        }
        
        /// <summary>
        /// Кнопка "Зашифровать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            Encrypt();
        }

        /// <summary>
        /// Кнопка расшифровать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            Decrypt();
        }
    }
}
