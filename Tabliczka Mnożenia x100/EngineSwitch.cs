using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tabliczka_Mnożenia_x100
{
    public partial class NewMathOperation
    {
        private static Random rnd = new();
        private static int PoprzedniPoziomGracza;
        private static int MaxWartosciA = 10;
        private static int MaxWartosciB = 50;
        private static int ZmianaWartosciB;
        //private static int PoprzedniPoziomGracza;

        private static int[] EngineSwitch()
        {
            try
            {
                Random rndA = new(rnd.Next());
                Random rndB = new(rnd.Next());

                int MinimumWartosci = 1;
                switch (PoziomGracza / 10)
                {
                    case 0: break;
                    case 1: MinimumWartosci = 10; break;
                    case 2: MinimumWartosci = 20; break;
                    case 3: MinimumWartosci = 30; break;
                    case 4: MinimumWartosci = 40; break;
                    default: MinimumWartosci = 50; break;
                }


                // Regres
                if (PoziomGracza < PoprzedniPoziomGracza)
                {
                    if (ZmianaWartosciB++ > 2)
                    {
                        MaxWartosciA -= 5;
                        ZmianaWartosciB = 0;
                    }
                    else
                    {
                        MaxWartosciB -= 10;
                    }
                }

                // Progres
                if (PoziomGracza > PoprzedniPoziomGracza)
                {
                    if (ZmianaWartosciB++ > 2)
                    {
                        MaxWartosciA += 5;
                        ZmianaWartosciB = 0;
                    }
                    else
                    {
                        MaxWartosciB += 10;
                    }
                }

                if (MaxWartosciA < 1) MaxWartosciA = 1;
                if (MaxWartosciA > 100) MaxWartosciA = 100;
                if (MaxWartosciB < 50) MaxWartosciB = 50;
                if (MaxWartosciB > 100) MaxWartosciB = 100;

                PoprzedniPoziomGracza = PoziomGracza;

                // 25% szans na odwrócenie wartości :)
                if(rndB.Next(1, 4) == 4)
                    return new int[] { 
                        rndB.Next(MinimumWartosci, MaxWartosciB), 
                        rndA.Next(1, MaxWartosciA) };

                return new int[] { 
                    rndA.Next(1, MaxWartosciA), 
                    rndB.Next(MinimumWartosci, MaxWartosciB) };
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message, "Exception in NewMathOperation/EngineSwitch");
            }

            return null;
        }
    }
}
