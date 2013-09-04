using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Uebung8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Unterprogramme zur Matrizenbearbeitung
    public void Addition(ref double[,] c,double[,] a,double[,] b,int n,int m,double faktor)
    {
    // Addition c = a + factor * b
    // n .. Zeilenlänge von c, a und b
    // m .. Spaltenlänge von c, a und b
      int i, j;
      for(i = 0; i < n; i++)
      {
        for (j = 0; j < m; j++)
        {
          c[i, j] = a[i, j] + faktor * b[i, j];
        }
      }
    }
    public void Multiplikation(ref double[,] c, double[,] a, double[,] b, int n, int m, int k, int trans)
    {
    // Multiplikation c = a * b
    // n .. Zeilenlänge von c und a bzw. Spaltenlänge von a
    // k .. Spaltenlänge von c und b
    // m .. Zeilenlänge von b und Spalten- bzw. Zeilenlänge von a
    // trans=0  .. Produkt c = a * b
    // trans<>0 .. Produkt c = a(t) * b
      int i, j, l;
      double wert;
      for(i=0;i<n;i++)
      {
        for(j=0;j<k;j++)
        {
          c[i, j] = 0.0;
          for(l = 0;l< m;l++)
          {
            if( trans == 0) wert = a[i, l];
            else wert = a[l, i];
            c[i, j] = c[i, j] + wert * b[l, j];
          }
        }
      }
    }
    public void Multi_Diagonal(ref double[,] c, double[,] a, double[] b, int n, int m, int trans)
    {
    // Multiplikation c = a * b
    // b ist Diagonalmatrix gegeben als Vektor mit den Elementen der Diagonalen
    // n .. Zeilenlänge von c und Zeile- bzw. Spaltenlänge von a
    // m .. Spaltenlänge von c und Spalten- bzw. Zeilenlänge von a
    // trans=0 .. Produkt c = a * b
    // trans<>0 .. Produkt c = a(t) * b
      int i, j;
      for (i = 0; i < n; i++)
      {
        for (j = 0; j < m; j++)
        {
          if (trans == 0) c[i, j] = a[i, j] * b[j];
          else c[i, j] = a[j, i] * b[j];
        }
      }
    }
    public void Quadrat_Form(ref double[,] c, double[,] a, double[] b, int n, int m)
    {
    // Multiplikation c = a(t) * b * a
    // b ist Diagonalmatrix gegeben als Vektor mit den Elementen der Diagonalen
    // m .. Zeilenlänge von c und a
    // n .. Spaltenlänge von c und a
      int i, j, l;
      for (i = 0; i < n; i++)
      {
        for (j = 0; j < n; j++)
        {
          c[i, j] = 0.0;
          for (l = 0; l < m; l++)c[i, j] = c[i, j] + a[l, i] * a[l, j] * b[l];
        }
      }
    }
    public int Inverse(ref double[,] a, int n, double S, out double det, out double cond)
    {
    // Aufruf mit a .. Ausgangs- und Ergebnismatrix
    //            S .. Schrankenwert für den Abbruch
    //            det .. Determiante von a
    //            cond .. Konditionszahl von a
    //            return = 0 .. fehlerfrei
    //            return = i .. Unbekannte i nicht bestimmbar
      double ai, amax;
      int i, j, k;

      det = 0.0;
      cond = 0.0;
      // 1. Berechnung von G transponiert
      if (a[0, 0] < S) return (1);
      else
      {
        amax = a[0, 0];
        a[0, 0] = Math.Sqrt(a[0, 0]);
        det = a[0, 0];
        for (i = 0; i < n-1; i++)
        {
          if (a[i + 1, i + 1] > amax) amax = a[i + 1, i + 1];
          for (j = i + 1; j < n; j++)
          {
            ai = 0.0;
            for (k = 0; k <= i-1; k++) ai = ai + a[k, i] * a[k, j];
            a[i, j] = (a[i, j] - ai) / a[i, i];
          }
          ai = 0.0;
          for (j = 0; j <= i; j++) ai = ai + a[j, i + 1] * a[j, i + 1];
          ai = a[i + 1, i + 1] - ai;
          if (ai < S) return (i + 2);
          else
          {
            a[i + 1, i + 1] = Math.Sqrt(ai);
            det = det * a[i + 1, i + 1];
          }
        }
        det = det * det;
      }
      //. Berechnung von G**-1
      cond = amax * n;
      a[0, 0] = 1.0 / a[0, 0];
      for (i = 0; i < n-1; i++)
      {
        for (j = i + 1; j < n; j++)
        {
          ai = 0.0;
          for (k = i; k <= j - 1; k++) ai = ai - a[i, k] * a[k, j];
          a[i, j] = ai / a[j, j];
        }
        a[i + 1, i + 1] = 1.0 / a[i + 1, i + 1];
      }
      // 3. Berechnung der Inversen im oberen Dreieck
      amax = 0.0;
      for (i = 0; i < n; i++)
      {
        for (j = i; j < n; j++)
        {
          ai = 0.0;
          for (k = j; k < n; k++) ai = ai + a[i, k] * a[j, k];
          a[i, j] = ai;
        }
        if (a[i, i] > amax) amax = a[i, i];
      }
      cond = cond * amax;
      //  4. Umspeichern der Inversen in das untere Dreieck
      for (i = 0; i < n - 1; i++)
      {
        for (j = i + 1; j < n; j++) a[j, i] = a[i, j];
      }
      return (0);
    }
  




        private void button2_Click(object sender, EventArgs e)
        {
            {
                string zeile;
                int i = 0, j = 0, merk = 0, k, m, n;
                double hz, v;
                double[,] A = new double[5, 5];
                double[,] AP = new double[5, 5];
                double[] L = new double[4];
                double[] P = new double[4]; //Matrix definiert die 100 Zeilen und 100 Spalten hat. 
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(textBox1.Text); // Überprüfung des Arbeitsverzeichnis
                if (!dir.Exists) // exisitert wirklich? dann...
                {
                    listBox1.Items.Add("Arbeitsverzeichnis existiert nicht");
                }
                else
                {

                    listBox1.Items.Add("Working Directory konnte geladen werden, Berechnung erfolgt.");
                   System.IO.FileInfo fil = new System.IO.FileInfo(textBox1.Text + "\\" + textBox2.Text); // Überprüfung der Eingabedatei mit absolutem Pfad
                    if (!fil.Exists) // existiert wirklich? dann..  ! verneint die if abfrage 
                    {
                        listBox1.Items.Add("Ausgabedatei konnte nicht angelegt werden.");

                    }
                    else
                    {
                        if (textBox3.Text.Length == 0) textBox3.Text = "prot.txt";
                        System.IO.FileInfo fil1 = new System.IO.FileInfo(textBox1.Text + "\\" + textBox3.Text); // Überprüfung der Eingabedatei mit absolutem Pfad
                        if (fil1.Exists) // existiert wirklich? dann..
                        {
                            //hier stand voher nur ne mitteilung die Datei gibt es.. allerdings habt ihr nicchts damit gemacht.. hab die also einfach mal gelöscht und den else Fall also default gsetzt.
                            fil1.Delete();
                            listBox1.Items.Add("Alte prot.txt Datei wurde gelöscht.");

                        }
                                       
                

                            
                            System.IO.StreamReader ein = new System.IO.StreamReader(textBox1.Text + "\\" + textBox2.Text);
                            System.IO.StreamWriter aus = new System.IO.StreamWriter(textBox1.Text + "\\" + textBox3.Text);
                            char[] Tr = { ' ' };
                            listBox1.Items.Add("Die prot.txt wurde angelegt.");
                           
                            // Einlesen der Matrix A, des Vektros L, der Diagonalmatrix P

                            while (!ein.EndOfStream)
                            {
                                zeile = ein.ReadLine();
                                i++;
                                //zeile.Substring(0, 2);
                                if (String.Compare(zeile.Substring(0, 2), "M=") == 0)
                                {
                                    if (!int.TryParse(zeile.Substring(2, zeile.Length - 2), out m)) m = 0;
                                }
                                if (String.Compare(zeile.Substring(0, 2), "N=") == 0)
                                {
                                    if (!int.TryParse(zeile.Substring(2, zeile.Length - 2), out n)) n = 0;
                                }
                                if (String.Compare(zeile.Substring(0, 2), "A=") == 0) merk = 1;
                                if (String.Compare(zeile.Substring(0, 2), "L=") == 0) merk = 2;
                                if (String.Compare(zeile.Substring(0, 2), "P=") == 0) merk = 3;

                                if (merk == 1 && i > 3)
                                {
                                    string[] words = zeile.Split(Tr);
                                    j = -1;
                                    foreach (string s in words) //foreach läuft den Vektor so lange ab bis nichts mehr drin steht. 
                                    {
                                        j++;
                                        if (!double.TryParse(s, out A[i - 4, j])) A[i - 4, j] = 0.0;
                                    }
                                }
                                
                                if (merk == 2)
                                {
                                    string[] words = zeile.Split(Tr);
                                    j = -1;
                                    foreach (string s in words) //foreach läuft den Vektor so lange ab bis nichts mehr drin steht. 
                                    {
                                        j++;
                                        if (!double.TryParse(s, out L[j])) L[j] = 0.0;
                                    }
                                }

                                if (merk == 3) 
                                {
                                    string[] words = zeile.Split(Tr);
                                    j = -1;
                                    foreach (string s in words) //foreach läuft den Vektor so lange ab bis nichts mehr drin steht. 
                                    {
                                        j++;
                                        if (!double.TryParse(s, out P[j])) P[j] = 0.0;
                                    }
                                }
                            }

                            aus.WriteLine("Ausgleichung nach vermittelnden Beobachtungen");
                            aus.WriteLine("---------------------------------------------");
                            aus.WriteLine("");
                            aus.WriteLine("Arbeitsverzeichnis: " + textBox1.Text);
                            aus.WriteLine("Eingabedatei: " + textBox2.Text);
                            aus.WriteLine("Protokolldatei: " + textBox3.Text);
                            aus.WriteLine("Bearbeitungszeitpunkt: " + DateTime.Now);
                            aus.WriteLine("");
                            aus.WriteLine("Anzahl der Beobachtungen: ");
                            aus.WriteLine("Anzahl der Unbekannten: ");
                            aus.WriteLine("");
                            aus.WriteLine("Koeffizientenmatrix:");

                            // Konditionszahl und Determinante


                            // Inverse des Normalgleichungssystems (A'P A)^-1
                            Quadrat_Form(ref AP, A, P, 4, 4);

                            for (i = 0; i < 3; i++) 
                            {
                                aus.WriteLine(AP[i, 0] + " " + AP[i, 1] + " " + AP[i, 2]);
                                //for (j = 0; j < 3; j++)   
                                    //aus.Write(AP[i, j]);
                            }
                            // Konditionszahl und Determinante
                            // Standardabweichung der Gewichtseinheit und Freiheitsgrad
                            // Unbekannte X mit zugehörigen Standardabweichungen
                            // Verbesserungen v 
                            // Redundanzen r(i)
                            // Kontrolle A'Pv = 0



                            ein.Close();
                            aus.Close();
                              // Einlesen von L und P (erledigt) 
                              // Matrizenoperationen (siehe Skript)
                              // Protokollierung in die Ausgabedatei 
                            listBox1.Items.Add("Berechnung abgeschlossen.");
                        }
                    }
                

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {

                string datei;
                char[] tr = { '\\' };
                int i = 0;
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    datei = openFileDialog1.FileName;

                    string[] words = datei.Split(tr);
                    textBox1.Text = "";
                    foreach (string s in words)
                    {
                        i++;
                        if (i > 1 && i < words.Length) textBox1.Text += "\\";
                        if (i < words.Length) textBox1.Text += s;
                        textBox2.Text = s;
                    }
                }
                listBox1.Items.Add("Working Directory wurde gesetzt.");
               
            }
         
            

            }
        //einfach wo der Fehler auftritt diese methode aufrufen ( error_message(); )
        private void error_message()
        {
            listBox1.Items.Add("Protokoll wurde fehlerhaft erstellt.");
        }
    }
}








