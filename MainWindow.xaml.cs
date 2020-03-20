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

            int ni = 3;
            int nj = 4;
            int nk = 5;
            int[,,] a = new int[ni, nj, nk];
            for (int i = 0; i < ni; i++)
            {
                for (int j = 0; j < nj; j++)
                {
                    for (int k = 0; k < nk; k++)
                    {
                        int offset = i * nj * nk + j * nk + k;
                        //int offset = i * ni * nj + j * nj + k;
                        s+=$"[{i}, {j}, {k}] = [{offset}] \n";
                    }
                }
            }

            textBox.Text = s;

        }
    }
}
