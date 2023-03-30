using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigL6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            double[] x0 = new double[20] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2 };
            double[] y0 = new double[20] { 2.18, 2.43, 2.40, 2.43, 2.65, 2.75, 2.67, 2.66, 2.63, 2.75, 2.41, 2.24, 2.12, 1.74, 1.57, 1.17, 0.96, 0.63, 0.25, 0.01 };

            for (int i = 0; i < 20; i++)
                chart1.Series[2].Points.AddXY(x0[i], y0[i]);

            // Шаг интерполяции
            double h = 0.1;
            int n = x0.Length;

            // Количество шагов вдоль оси x
            double steps = (x0[n - 1] - x0[0]) / h;

            //NewTone
            // Массив значений интерполируемых аргументов
            double[] x = new double[20];
            for (int i = 0; i < steps + 1; i++)
            {
                x[i] = x0[1] + ((i - 1) * h);
            }

            // Вычисляем разделённые разности
            double[] DIFF = new double[20];
            for (int i = 0; i < 20; i++)
            {
                DIFF[i] = y0[i];
            }

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n - k - 1; i++)
                {
                    DIFF[i] = (DIFF[i + 1] - DIFF[i]) / (x0[i + k + 1] - x0[i]);
                }
            }

            // Интерполяция Ньютона
            double[] y = new double[20];
            for (int i = 0; i < 20; i++)
            {
                y[i] = DIFF[0];
            }

            for (int k = 1; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    y[i] = DIFF[k] + (x[i] - x0[k]) * y[i];

                }

            }

            for (int i = 0; i < 20; i++)
                textBox1.Text += "y[" + i + "] = " + String.Format("{0:0.00}", y[i]) + " ";

            for (int i = 0; i < steps + 1; i++)
                chart1.Series[0].Points.AddXY(x[i], y[i]);

            //Лакринж
            double steps1 = (x0[n - 1] - x0[0]) / h;

            double[] x1 = new double[19];
            // Массив значений интерполируемых аргументов
            x1[0] = x0[0];
            for (int i = 1; i < steps; i++)
            {
                x1[i] = x0[0] + ((i + 1) * h);
            }
            int f = 1;
            double[] y1 = new double[19];
            // Интерполяция Лагранжа
            for (int k = 0; k < steps; k++)
            {
                double summ = 0;
                for (int i = 0; i < n; i++)
                {
                    double l = 1;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            l = l * ((x1[k] - x0[j]) / (x0[i] - x0[j]));
                        }
                    }
                    summ = summ + (y0[i] * l);
                }
                y1[k] = summ;
                textBox2.Text += "y[" + k + "] = " + String.Format("{0:0.00}", y1[k]) + " ";
                if (f % 3 == 0)
                {
                    textBox2.Text += Environment.NewLine;
                }
                f++;
            }
            for (int i = 0; i < steps; i++)
                chart1.Series[1].Points.AddXY(x1[i], y1[i]);
        }

        private void BigL6_Load_1(object sender, EventArgs e)
        {

        }

        private void MyChart_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}

