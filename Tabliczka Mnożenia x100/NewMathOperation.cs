using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tabliczka_Mnożenia_x100
{
    public static partial class NewMathOperation
    {
        private static int PoziomGracza;
        private static int SeriaDobrychOdpowiedzi;
        private static int SeriaZlychOdpowiedzi;
        private static int LastUpdateSeries;
        private static int PoprzednieDobreOdpowiedzi;
        private static int PoprzednieZleOdpowiedzi;
        //private static int ObecnyCzynnykA;
        //private static int ObecnyCzynnykB;
        private static DateTime CzasOdpowiedzi = DateTime.Now;

        public static int[] GetOperation(int dobreOdpowiedzi, int zleOdpowiedzi)
        {
            try
            {
                // Klasa odpowiadajaca za nowe działania:
                // return int[0] == CzynnykA
                //        int[1] == CzynnykB

                if((dobreOdpowiedzi > PoprzednieDobreOdpowiedzi)
                    && (zleOdpowiedzi == PoprzednieZleOdpowiedzi))
                {
                    // Progres
                    if (SeriaDobrychOdpowiedzi == 0) LastUpdateSeries = 0;

                    SeriaDobrychOdpowiedzi++;
                    SeriaZlychOdpowiedzi = 0;

                    bool ActivSeryjnosci = ((SeriaDobrychOdpowiedzi - LastUpdateSeries) >= 3);
                    if (ActivSeryjnosci)
                        MainWindow.ColorKwadratSeryjnosci = Brushes.Green;

                    // Zmieniam poziom jeżeli czas był <= 10sec
                    if (ActivSeryjnosci && (CzasOdpowiedzi.AddSeconds(20) <= DateTime.Now))
                    {
                        LastUpdateSeries = SeriaDobrychOdpowiedzi;
                        PoziomGracza++;
                        MainWindow.ColorKwadratPoziomu = Brushes.Green;
                    }
                    else
                    {
                        if (CzasOdpowiedzi.AddSeconds(20) <= DateTime.Now)
                            MainWindow.ColorKwadratPoziomu = Brushes.Yellow;
                        if (ActivSeryjnosci)
                            MainWindow.ColorKwadratPoziomu = Brushes.Blue;
                    }
                }

                if((zleOdpowiedzi > PoprzednieZleOdpowiedzi)
                    && (dobreOdpowiedzi == PoprzednieDobreOdpowiedzi))
                {
                    // Regres
                    if (SeriaZlychOdpowiedzi == 0) LastUpdateSeries = 0;

                    SeriaZlychOdpowiedzi++;
                    SeriaDobrychOdpowiedzi = 0;

                    bool ActiveSeryjnosci = ((SeriaZlychOdpowiedzi - LastUpdateSeries) >= 3);
                    if(ActiveSeryjnosci) 
                        MainWindow.ColorKwadratSeryjnosci = Brushes.Red;

                    if (ActiveSeryjnosci)
                    {
                        LastUpdateSeries = SeriaDobrychOdpowiedzi;
                        PoziomGracza--;

                        if (PoziomGracza < 0)
                            MainWindow.ColorKwadratPoziomu = Brushes.Black;
                        else
                            MainWindow.ColorKwadratPoziomu = Brushes.Red;
                    }
                }

                PoprzednieDobreOdpowiedzi = dobreOdpowiedzi;
                PoprzednieZleOdpowiedzi = zleOdpowiedzi;
                CzasOdpowiedzi = DateTime.Now;

                return EngineSwitch();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message, "Exception in NewMathOperation/GetOperation");
            }

            return null;
        }
    }
}
