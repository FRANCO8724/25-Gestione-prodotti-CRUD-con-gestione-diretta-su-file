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

        }

        //Funzioni di servizio

        public void Aggiunta(string nome, string prezzo)
        {
            StreamWriter sw = new StreamWriter(path + @"/lista.txt",true);
            sw.WriteLine("Prodotto: " + nome + " , Prezzo: " + prezzo + " $");
            sw.Close();
        }

        public void Elimina(string nome)    

        {
            //File di lettura
            using (StreamReader sw = File.OpenText("lista.txt"))
            {
                string s;

                //File di scrittura
                using (StreamWriter ws2 = new StreamWriter("temp.csv"))
                {

                    while ((s = sw.ReadLine()) != null)
                    {
                        //se il nome appartiene alla stringa 
                        if (s != nome)
                        {
                            ws2.WriteLine(s);
                        }

                    }
                }
            }

                //cancello il file principale
                File.Delete("lista.txt");

                //e rinomino il file momentaneo, rendendolo il nuovo principale
                File.Move("temp.csv", "lista.txt");         
        }

    }
}
