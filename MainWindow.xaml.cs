using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Diagnostics;
using TestManagedCUDA.Model;

namespace TestManagedCUDA
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CUDARunner cr = new CUDARunner();
            textBox.Text = cr.GetSummary();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string s = "";

            int ni = 12;
            int nj = 23;
            int nk = 31;
            int[,,] a = new int[ni, nj, nk];
            for (int i = 0; i < ni; i++)
            {
                for (int j = 0; j < nj; j++)
                {
                    for (int k = 0; k < nk; k++)
                    {
                        // вычисление смещения по индексам трехмерного массива
                        int p = i * nj * nk + j * nk + k;
                        //int offset = i * ni * nj + j * nj + k;

                        // вычисление индексов трехмерного массива по смещению

                        // прямой метод
                        //int _k = p % nk;
                        //int _krest = p / nk;
                        //int _j = _krest % nj;
                        //int _i = _krest / nj;

                        // оптимизированно с учетом ASM DIV
                        int _krest = Math.DivRem(p, nk, out int _k);
                        int _i = Math.DivRem(_krest, nj, out int _j);

                        if ((i == _i) && (j == _j) && (k == _k))
                        {
                            s += $"[{i}, {j}, {k}] = [{p}] = [{_i}, {_j}, {_k}] - OK\n";
                        }
                        else
                        {
                            s += $"[{i}, {j}, {k}] = [{p}] = [{_i}, {_j}, {_k}] - !!!!!!!!!!! ERROR !!!!!!!!!!\n";
                        }


                    }
                }
            }

            textBox.Text = s;

        }
    }
}
