using System;
using System.IO;
using System.Windows.Forms;

namespace _25_Gestione_prodotti_CRUD_con_gestione_diretta_su_file
{
    public partial class Form1 : Form
    {
        public string path;

        public Form1()
        {
            InitializeComponent();
            path = Path.GetFullPath("."); //Percorso del file

            path = Path.GetDirectoryName(path); //Torna indietro di una cartella
            path = Path.GetDirectoryName(path); //Torna indietro di una cartella
            path = Path.GetDirectoryName(path); //Torna indietro di una cartella

            path += @"/lista";

            Directory.CreateDirectory(path);
            StreamWriter sw = new StreamWriter(path + @"/lista.txt");
            sw.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Aggiunta(Nome.Text, Prezzo.Text);

            Nome.Text = "";
            Nome.Focus();
            Prezzo.Text = "";
            Prezzo.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Elimina(newelement.Text);
            newelement.Text = "";
            newelement.Focus();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Modifica(Nome.Text,textBox2.Text, textBox1.Text);
            Nome.Text = "";
            Nome.Focus();
            Prezzo.Text = "";
            Prezzo.Focus();
            textBox2.Text = "";
            textBox2.Focus();
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Aggiorna(sender,e);
        }

        //Funzioni di servizio

        public void Aggiunta(string nome, string prezzo)
        {
            StreamWriter sw = new StreamWriter(path + @"/lista.txt", true);
            sw.WriteLine("Prodotto: " + nome + " , Prezzo: " + prezzo + " $");
            sw.Close();
        }

        public void Elimina(string nome)

        {
            //File di lettura
            using (StreamReader sw = File.OpenText(path + @"/lista.txt"))
            {
                string s;

                //File di scrittura
                using (StreamWriter ws2 = new StreamWriter(path + @"/temp.csv",false))
                {

                    while ((s = sw.ReadLine()) != null)
                    {
                        //se il nome appartiene alla stringa 
                        if (s.Contains(nome))
                        {
                            ws2.WriteLine();
                        }
                        else
                        {
                            ws2.WriteLine(s);
                        }

                    }
                }
            }

            //cancello il file principale
            File.Delete(path + @"/lista.txt");

            //e rinomino il file momentaneo, rendendolo il nuovo principale
            File.Move(path + @"/temp.csv",path +  @"/lista.txt");
        }

        public void Modifica(string oldnome,string prezzo,string newnome)

        {
            //File di lettura
            using (StreamReader sw = File.OpenText(path + @"/lista.txt"))
            {
                string s;

                //File di scrittura
                using (StreamWriter ws2 = new StreamWriter(path + @"/temp.csv", false))
                {

                    while ((s = sw.ReadLine()) != null)
                    {
                        //se il nome appartiene alla stringa 
                        if (s.Contains(oldnome))
                        {
                            ws2.WriteLine("Prodotto: " + newnome + " , Prezzo: " + prezzo + " $");
                        }
                        else
                        {
                            ws2.WriteLine(s);
                        }

                    }
                }
            }

            //cancello il file principale
            File.Delete(path + @"/lista.txt");

            //e rinomino il file momentaneo, rendendolo il nuovo principale
            File.Move(path + @"/temp.csv", path + @"/lista.txt");
        }

        public void Aggiorna(object sender, EventArgs e)
        {
            using (StreamReader sw = File.OpenText(path + @"/lista.txt"))
            {
                string s;

                listView1.Items.Clear();

                while ((s = sw.ReadLine()) != null)
                {
                    listView1.Items.Add(s);
                }
            }
        }

    }
}
      



