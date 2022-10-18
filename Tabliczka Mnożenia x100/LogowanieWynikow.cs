using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tabliczka_Mnożenia_x100
{
    public class LogowanieWynikow
    {
        private static readonly object synchSave = new();
        private DateTime DataDoNazwyPliku = DateTime.Now;
        private string MyPath;

        public LogowanieWynikow()
        {
            try
            {
                string MyDirectory = Directory.GetCurrentDirectory() + @"\MojeWyniki-" + DataDoNazwyPliku.ToString("yyyy-MM-dd");

                lock (synchSave)
                    if (!Directory.Exists(MyDirectory))
                        Directory.CreateDirectory(MyDirectory);

                MyPath = MyDirectory + @"\MojWynik-" + DataDoNazwyPliku.ToString("yyyy-MM-dd_'H'HH_'m'mm") + ".txt";
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message, "Exception in LogowanieWynikow/LogowanieWynikow INIT");
            }
        }

        public void DodajWpis(int czynnikA, int czynnikB, int mojaOdpowiedz)
        {
            try
            {
                string messege = DateTime.Now + " : " + (((czynnikA * czynnikB) == mojaOdpowiedz) ?
                    "Dobrze " : ("ŹLE [powinno być: " + czynnikA * czynnikB + "] ")) +
                    czynnikA + " x " + czynnikB + " = " + mojaOdpowiedz;

                lock (synchSave)
                {
                    if (File.Exists(MyPath))
                    {
                        using StreamWriter sw = File.AppendText(MyPath);
                        sw.WriteLine(messege);
                    }
                    else
                    {
                        using StreamWriter sw = File.CreateText(MyPath);
                        sw.WriteLine(messege);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message, "Exception in LogowanieWynikow/DodajWpis");
            }
        }
    }
}
