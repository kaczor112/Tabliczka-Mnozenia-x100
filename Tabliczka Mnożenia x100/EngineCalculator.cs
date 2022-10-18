using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tabliczka_Mnożenia_x100
{
    public class EngineCalculator
    {
        private int IloscDobrychOdpowiedzi;
        private int IloscZlychOdpowiedzi;
        private int CzynnikA = 100;
        private int CzynnikB = 100;
        private static LogowanieWynikow _LogowanieWynikow = new();

        public bool SprawdzOtrzymanyWynik(string wynik)
        {
            try
            {
                if(int.TryParse(wynik, out int myWynik))
                {
                    _LogowanieWynikow.DodajWpis(CzynnikA, CzynnikB, myWynik);

                    if ((CzynnikA * CzynnikB) == myWynik)
                    {
                        IloscDobrychOdpowiedzi++;
                        MainWindow.ColorKwadratSygnalowy = Brushes.Green;
                    }
                    else
                    {
                        IloscZlychOdpowiedzi++;
                        MainWindow.ColorKwadratSygnalowy = Brushes.Red;
                    }

                    int[] noweDzialanie = NewMathOperation.GetOperation
                            (IloscDobrychOdpowiedzi, IloscZlychOdpowiedzi);

                    if (noweDzialanie != null)
                    {
                        CzynnikA = noweDzialanie[0];
                        CzynnikB = noweDzialanie[1];

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message, "Exception in EngineCalculator/SprawdzOtrzymanyWynik");
            }

            return false;
        }

        public string GetNoweDzialanie()
            => ((CzynnikA.ToString().Length == 1)?"     ": "    ") + CzynnikA + " x " + CzynnikB;
    }
}
