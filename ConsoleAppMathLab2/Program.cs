using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberMethodLab2
{
    internal class Program
    {
        public static void matrixWrite(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            double E = 0.00001;

            double[] result = new double[4];

            double[,] matrix = {
                {2, 0, -1, -2, -8},
                {0, 1, 2, -1, -1},
                {1, -1, 0, -1, -6 },
                {-1, 3, -2, 0, 7}
            };

            calcGausse(matrix);

            matrix = getMatrixZeidel();

            calcZeidel(matrix, E);

            Console.ReadKey();
        }



        static double func1(double x, double y)
        {
            return (x - Math.Log((y + 1.1), Math.E));
        }

        static double func2(double x, double y)
        {

            return (y - Math.Cos((x + 0.1)));
        }

        static double[,] getMatrixZeidel()
        {
            double[,] matrix = {
                { 0.08, -0.03, 0, -0.04 },
                { 0, 0.51, 0.27, -0.08 },
                { 0.33, -0.37, 0, 0.21 },
                { 0.11, 0, 0.03, 0.58 }
            };

            return matrix;
        }

        static void calcGausse(double[,] matrix)
        {
            double[] iter0 = new double[4];

            for (var i = 1; i < 5; i++)
            {
                iter0[i - 1] = matrix[0, i] / matrix[0, 0];
            }

            double[,] matrixIter0 = new double[3, 4];

            for (var i = 1; i < 4; i++)
            {
                for (var j = 1; j < 5; j++)
                {
                    matrixIter0[i - 1, j - 1] = matrix[i, j] - matrix[i, 0] * iter0[j - 1];
                }
            }

            double[] iter1 = new double[3];

            for (var i = 1; i < 4; i++)
            {
                iter1[i - 1] = matrixIter0[0, i] / matrixIter0[0, 0];
            }

            double[,] matrixIter1 = new double[2, 3];

            for (var i = 1; i < 3; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    matrixIter1[i - 1, j - 1] = matrixIter0[i, j] - matrixIter0[i, 0] * iter1[j - 1];
                }
            }

            double[] iter2 = new double[2];

            for (var i = 1; i < 3; i++)
            {
                iter2[i - 1] = matrixIter1[0, i] / matrixIter1[0, 0];
            }

            double[,] matrixIter2 = new double[1, 2];

            for (var i = 1; i < 2; i++)
            {
                for (var j = 1; j < 3; j++)
                {
                    matrixIter2[i - 1, j - 1] = matrixIter1[i, j] - matrixIter1[i, 0] * iter2[j - 1];
                }
            }

            double x4 = Math.Round((Math.Round(matrixIter2[0, 1], 3) / Math.Round(matrixIter2[0, 0], 2)), 3);

            double x3 = Math.Round(iter2[1] - Math.Round((iter2[0] * x4), 3), 3);

            double x2 = iter1[2] - iter1[0] * x3 - iter1[1] * x4;

            double x1 = Math.Round((iter0[3] - Math.Round((iter0[0] * x2), 3) - Math.Round((iter0[1] * x3), 3) - Math.Round((iter0[2] * x4), 3)), 3);

            Console.WriteLine("Метод Гаусса");

            matrixWrite(matrix);

            Console.WriteLine("------------");

            matrixWrite(matrixIter0);

            Console.WriteLine("------------");

            matrixWrite(matrixIter1);

            Console.WriteLine("------------");

            matrixWrite(matrixIter2);

            Console.WriteLine("------------");

            Console.WriteLine($" x1 = {x1}  x2 = {x2}  x3 = {x3}  x4 = {x4}");
        }

        static void calcZeidel(double[,] matrix, double E)
        {
            double[] result = new double[4];

            double x1h = 0;
            double x2h = 0;
            double x3h = 0;
            double x4h = 0;
            double x1 = -1.2;
            double x2 = 0.81;
            double x3 = -0.92;
            double x4 = 0.17;

            double count0 = 0;
            double count1 = 0;
            double count2 = 0;
            double count3 = 0;

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (i == 0)
                    {
                        count0 += matrix[i, j];
                    }
                    if (i == 1)
                    {
                        count1 += matrix[i, j];
                    }
                    if (i == 2)
                    {
                        count2 += matrix[i, j];
                    }
                    if (i == 3)
                    {
                        count3 += matrix[i, j];
                    }
                }
            }

            double[] help = new double[4] { Math.Abs(count0), Math.Abs(count1), Math.Abs(count2), Math.Abs(count3) };

            double d = help.Max();

            if (d > 1)
            {
                throw new Exception();
            }

            int inc = 0;

            double delta1 = 0;
            double delta2 = 0;
            double delta3 = 0;
            double delta4 = 0;
            double maxDelta = 1;

            Console.WriteLine();
            Console.WriteLine("Метод Зейделя");

            while (maxDelta > E)
            {
                x1 = x1h;
                x2 = x2h;
                x3 = x3h;
                x4 = x4h;

                x1h = 0.08 * x1 - 0.03 * x2 - 0.04 * x4 - 1.2;
                x2h = 0.51 * x2 + 0.27 * x3 - 0.08 * x4 + 0.81;
                x3h = 0.33 * x1h - 0.37 * x2h + 0.21 * x4 - 0.92;
                x4h = 0.11 * x1h + 0.03 * x3h + 0.58 * x4 + 0.17;

                delta1 = Math.Abs(Math.Abs(x1) - Math.Abs(x1h));
                delta2 = Math.Abs(Math.Abs(x2) - Math.Abs(x2h));
                delta3 = Math.Abs(Math.Abs(x3) - Math.Abs(x3h));
                delta4 = Math.Abs(Math.Abs(x4) - Math.Abs(x4h));

                maxDelta = Math.Max(delta1, Math.Max(delta2, Math.Max(delta3, delta4)));

                if (inc < 10 && inc != 0)
                {
                    Console.WriteLine(inc + "  x1=" + Math.Round(x1h, 6) + "\tx2=" + Math.Round(x2h, 6) + "\tx3="
                    + Math.Round(x3h, 6) + "\tx4=" + Math.Round(x4h, 6) + "\tmaxDelta=" + Math.Round(maxDelta, 6));
                }
                else if (inc == 0)
                {
                    Console.WriteLine(inc + "  x1=" + Math.Round(x1h, 6) + "\tx2=" + Math.Round(x2h, 6) + "\t\tx3="
                    + Math.Round(x3h, 6) + "\tx4=" + Math.Round(x4h, 6) + "\tmaxDelta=" + Math.Round(maxDelta, 6));
                }
                else
                {
                    Console.WriteLine(inc + " x1=" + Math.Round(x1h, 6) + "\tx2=" + Math.Round(x2h, 6) + "\tx3="
                    + Math.Round(x3h, 6) + "\tx4=" + Math.Round(x4h, 6) + "\tmaxDelta=" + Math.Round(maxDelta, 6));
                }


                inc++;
            }
        }
    }
}
