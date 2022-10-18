using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tabliczka_Mnożenia_x100
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly EngineCalculator _Calculator = new();
        public MainWindow()
        {
            InitializeComponent();

            ContentRendered += (sender, args) =>
            {
                TextInput.CaretIndex = TextInput.Text.Length;
                TextInput.ScrollToEnd(); // not necessary for single line texts
                TextInput.Focus();
            };
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (_Calculator.SprawdzOtrzymanyWynik(TextInput.Text)) 
                {
                    Tablica.Content = _Calculator.GetNoweDzialanie();
                    TextInput.Text = "";
                }

                // UpdateColorRectangle
                KwadratSygnalowy.Fill = _ColorKwadratSygnalowy;
                KwadratSeryjnosci.Fill = _ColorKwadratSeryjnosci;
                KwadratPoziomu.Fill = _ColorKwadratPoziomu;
            }
        }

        private static SolidColorBrush _ColorKwadratSygnalowy;
        public static SolidColorBrush ColorKwadratSygnalowy
        {
            set => _ColorKwadratSygnalowy = value;
        }

        private static SolidColorBrush _ColorKwadratSeryjnosci;
        public static SolidColorBrush ColorKwadratSeryjnosci
        {
            set => _ColorKwadratSeryjnosci = value;
        }

        private static SolidColorBrush _ColorKwadratPoziomu;
        public static SolidColorBrush ColorKwadratPoziomu
        {
            set => _ColorKwadratPoziomu = value;
        }
    }
}
