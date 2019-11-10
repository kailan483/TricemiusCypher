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
            for (int i = alphabet.Count - 1; i >= 1; i--)
            {   
                int j = random.Next(i + 1);              
                var temp = alphabet[j];
                alphabet[j] = alphabet[i];
                alphabet[i] = temp;
            }
        }
        List<char> alphabet = new List<char>();
        public void CreateAlphabet()
        {
            for (char i = 'А'; i <= 'Я'; i++)
            {
                alphabet.Add(i);
            }
            alphabet.Add(' ');
            ShuffleAlphabet();            
        }
        public void Encrypt()
        {
            string text = textBox1.Text;
            string cypher = "";            
            if (!String.IsNullOrEmpty(text))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    int posInAlphabet = alphabet.FindIndex(character => character.Equals(text[i]));
                    if (posInAlphabet != -1)
                    {
                        int encryptedCharPos = (posInAlphabet + i) % alphabet.Count;
                        cypher += alphabet[encryptedCharPos];
                    }
                    else
                    {
                        MessageBox.Show("Введен недопустимый символ!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                } 
            }
            textBox2.Text = cypher;
        }
        public void Decrypt()
        {
            string encryptedText = textBox2.Text;
            string text = "";

            for (int i = 0; i < encryptedText.Length; i++)
            {
                int posInAlphabet = alphabet.FindIndex(character => character.Equals(encryptedText[i]));
                if (posInAlphabet != -1)
                {
                    int decryptedCharPos;
                    if (posInAlphabet - i > 0)
                    {
                        decryptedCharPos = (posInAlphabet - i) % alphabet.Count;
                        text += alphabet[decryptedCharPos];
                    }                        
                    else
                    {
                        int dimension = posInAlphabet - i;
                        while (dimension < 0)                        
                            dimension += alphabet.Count;
                        decryptedCharPos = dimension % alphabet.Count;
                        text += alphabet[decryptedCharPos];
                    }
                        
                    
                }
                else
                    {
                        MessageBox.Show("Введен недопустимый символ!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
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

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                opf.Filter = "txt files (*.txt)|*.txt";
                opf.InitialDirectory = "C:\\";

                if(opf.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(opf.FileName, Encoding.Default))
                    {
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            textBox1.Text += line;
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                opf.Filter = "txt files (*.txt)|*.txt";
                opf.InitialDirectory = "C:\\";
                opf.Title = "Открыть файл с ключом";

                if (opf.ShowDialog() == DialogResult.OK)
                {
                    alphabet.Clear();
                    using (StreamReader sr = new StreamReader(opf.FileName, Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            foreach (var item in line)
                            {
                                alphabet.Add(item);
                            }
                        }
                    }
                }
            }
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                opf.Filter = "txt files (*.txt)|*.txt";
                opf.InitialDirectory = "C:\\";
                opf.Title = "Открыть файл с шифром";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(opf.FileName, Encoding.Default))
                    {
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            textBox2.Text += line;
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button5_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt";
                sfd.InitialDirectory = "C:\\";
                sfd.Title = "Сохранить ключ в файл";              
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.Default))
                    {
                        var key = String.Join("",alphabet);
                        if (!String.IsNullOrWhiteSpace(key))
                            await sw.WriteLineAsync(key);                        
                    }
                }
            }
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt";
                sfd.InitialDirectory = "C:\\";
                sfd.Title = "Сохранить шифр в файл";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.Default))
                    {
                        if (!String.IsNullOrWhiteSpace(textBox2.Text))
                            await sw.WriteLineAsync(textBox2.Text);
                        else MessageBox.Show("Поле с шифром пустое!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
        }
    }
}
