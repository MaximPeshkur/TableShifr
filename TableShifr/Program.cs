using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableShifr
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = -1;
            while (k!=0)
            {
                do
                {
                    Console.WriteLine("Выберите:\n1. Зашифровать\n2. Расшифровать\n0. Выход");
                    k = Convert.ToInt32(Console.ReadLine());
                } while (k<0 || k>2);
                switch (k)
                {
                    case 1:
                        Encryption(); break;
                    case 2:
                        Decoding(); break;
                    default:
                        break;
                }
            }
        }
        static void Encryption()
        {
            #region EncryptionWithKey
            Console.Write("Введите строку, которую надо зашифровать: ");
            string str = Console.ReadLine();
            Console.Write("Введите ключ: ");
            string key = Console.ReadLine();
            char[,] arr = new char[str.Length / key.Length + 2, key.Length];
            int l = 0, L=0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (i==0)
                    {
                        arr[i, j] = key[L++];
                        continue;
                    }
                    if (l<str.Length)
                    {
                        arr[i, j] = str[l++];
                    }
                    else
                    {
                        arr[i, j] = ' ';
                    }
                }
            }
            Console.WriteLine("Первая таблица:");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i,j] + " ");
                }
                Console.WriteLine();
            }
            char[] collum = key.ToCharArray();
            for (int i = 0; i < collum.Length; i++)
            {
                for (int j = i + 1; j < collum.Length; j++)
                {
                    if (collum[i] > collum[j])
                    {
                        var temp = collum[i];
                        collum[i] = collum[j];
                        collum[j] = temp;
                        Switch(ref arr, i, j);
                    }
                }
            }

            Console.WriteLine("Вторая таблица: ");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
            char[] res = new char[arr.Length];
            l = 0;
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    res[l++] = arr[j, i];
                }
            }
            Console.Write("Зашифрованный текст: ");
            for (int i = 0; i < res.Length; i++)
            {
                Console.Write(res[i]);
            }
            Console.WriteLine("\n\n\n\n");
            #endregion

        }
        /// <summary>
        /// Меняет в массиве arr стольбцы collum1 и collum2
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="collum1"></param>
        /// <param name="collum2"></param>
        /// <param name="n"></param>
        static void Switch(ref char[,] arr, int collum1, int collum2)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                var temp = arr[i, collum1];
                arr[i, collum1] = arr[i, collum2];
                arr[i, collum2] = temp;
            }
        }




        static void Decoding()
        {
            Console.Write("Введите строку, которую надо расшифровать: ");
            string str = Console.ReadLine();
            Console.Write("Введите ключ: ");
            string key = Console.ReadLine();
            double temp = (double)str.Length / (double)key.Length;
            if ((double)temp != (int)temp)
            {
                Console.WriteLine("Строку невозможно записать в таблицу.\n\n\n");
                return;
            }
            char[,] arr = new char[str.Length / key.Length, key.Length];
            int l = 0;
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    arr[j, i] = str[l++];
                }
            }
            Console.WriteLine("Первая таблица:");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i,j]+" ");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {

                }

            }

        }
    }
}
