using System;
using System.ComponentModel;
using System.Globalization;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static int[] input1(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{i + 1} ");
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            return arr;
        }
        static int[,] input2(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{i + 1}.{j + 1} ");
                    arr[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            return arr;
        }
        static int[,] replay(int[,] arr)
        {
            int[,] Array = new int[arr.GetLength(0), arr.GetLength(1)];

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] = arr[i, j];
                }
            }
            return Array;
        }
        static void output1(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }
        static void output2(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] >= 0)
                    {
                        Console.Write(array[i, j] + " ");
                    }
                    else
                    {
                        Console.Write(" " + " ");
                    }
                }
                Console.WriteLine();
            }
        }
        static int check(int[] arr)
        {
            int result = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                result = result + arr[i];
            }
            return result;
        }
        static int[,] metod(int[] M, int[] N, int[,] C)
        {
            int[,] A = new int[M.Length, N.Length];
            while (checkOn(N) && checkOn(M))
            {
                int k;
                int l;
                //нахождение минимального элемента
                minimum(C, out k, out l);
                C[k, l] = 0;
                //заполнение пустующих ячеек
                full(k, l, ref N, ref M, ref A);
            }

            return A;
        }
        static void minimum(int[,] array, out int k, out int l)
        {
            k = 0;
            l = 0;
            int min = -1;
            int flag = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (flag == 0 && array[i, j] > 0)
                    {
                        min = array[i, j];
                        flag = 1;
                    }
                    if (flag == 1 && min > array[i, j] && array[i, j] > 0)
                    {
                        min = array[i, j];
                        k = i;
                        l = j;
                    }
                }
            }
        }
        static void full(int k, int l, ref int[] N, ref int[] M, ref int[,] A)
        {
            if (N[l] >= M[k] && M[k] > 0)
            {
                A[k, l] = M[k];
                N[l] -= M[k];
                M[k] = 0;

                if (M[k] == 0)
                {
                    for (int i = 0; i < A.GetLength(1); i++)
                    {
                        if (i != l && !(A[k, i] > 0))
                        {
                            A[k, i] = 0;
                        }
                    }
                }
            }
            if (M[k] >= N[l] && N[l] > 0)
            {
                A[k, l] = N[l];
                M[k] -= N[l];
                N[l] = 0;

                if (N[l] == 0)
                {
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        if (i != k && !(A[i, l] > 0))
                        {
                            A[i, l] = 0;
                        }
                    }
                }
            }
        }
        static bool checkOn(int[] arr)
        {
            bool flag = false;
            foreach (int i in arr)
            {
                if (i != 0)
                {
                    flag = true;
                }
            }
            return flag;
        }
        static void Main(string[] args)
        {
            //ввод размерности
            Console.WriteLine("Введите размерность вектора мощности поставщика:");
            int[] M = new int[Convert.ToInt32(Console.ReadLine())];
            Console.WriteLine("Введите размерность вектора спросов потребителей:");
            int[] N = new int[Convert.ToInt32(Console.ReadLine())];
            int[,] C = new int[M.Length, N.Length];
            int[,] A = new int[M.Length, N.Length];
            //ввод векторов 
            Console.WriteLine("Введите вектор мощности поставщиков:");
            M = input1(M);
            Console.WriteLine("Введите вектор спросов потребителей");
            N = input1(N);
            Console.Clear();
            //проверка на применимость
            if (check(N) == check(M))
            {
                //ввод матрицы
                Console.WriteLine("Введите матрицу затрат на перевозку единицы продукции");
                C = input2(C);
                int[,] cCopy = replay(C);
                Console.Clear();
                //вывод результатов
                Console.WriteLine("Вектор мощности поставщиков:");
                output1(M);
                Console.WriteLine("Вектор спросов потребителей");
                output1(N);
                Console.WriteLine("Матрица затрат на перевозку единицы продукции");
                output2(C);
                Console.WriteLine("Итоговое распределение:");
                A = metod(M, N, C);
                output2(A);
                Console.WriteLine("Сумма денежных затрат:");
                int sum = 0;
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    for (int j = 0; j < A.GetLength(1); j++)
                    {
                        sum += A[i, j] * cCopy[i, j];
                    }
                }
                Console.WriteLine(sum);
            }
            else
            {
                Console.WriteLine("Вектор мощности поставщиков и вектор спросов потребителей не равны");
            }
        }
    }
}