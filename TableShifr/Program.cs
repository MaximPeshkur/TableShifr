using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            char[,] arr = new char[str.Length / key.Length + (((int)str.Length / key.Length == (double)str.Length / key.Length) ? 1 : 2), key.Length];
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
                        SwitchCollums(ref arr, i, j);
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
        static void SwitchCollums(ref char[,] arr, int collum1, int collum2)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                var temp = arr[i, collum1];
                arr[i, collum1] = arr[i, collum2];
                arr[i, collum2] = temp;
            }
        }
        /// <summary>
        /// Меняет в массиве arr строки line1 и line2 местами
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        static void SwitchLines(ref char[,] arr, int line1, int line2)
        {
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                var temp = arr[line1, i];
                arr[line1, i] = arr[line2, i];
                arr[line2, i] = temp;
            }
        }



        static void Decoding()
        {
            Console.Write("Введите строку, которую надо расшифровать: ");
            string str = Console.ReadLine();
            Console.Write("Введите ключ: ");
            string key = Console.ReadLine();
            str = str.ToUpper();
            key = key.ToUpper();
            double temp = (double)str.Length / (double)key.Length;
            if ((double)temp != (int)temp)
            {
                Console.WriteLine("Строку невозможно записать в таблицу.\n\n\n");
                return;
            }
            int k = 0;
            do
            {
                Console.WriteLine("Ключ расположен:\n1. Горизонтально\n2. Вертикально");
                k = Convert.ToInt32(Console.ReadLine());
            } while (k<1 || k>2);
            switch (k)
            {
                case 1:
                    KeywordHorizontal(str, key); break;
                case 2:
                    KeywordVertical(str, key); break;
                default:
                    break;
            }
        }
        static void KeywordHorizontal(string str, string key)
        {
            #region Keyword horizontal
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
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
            char[] tempp = new char[arr.GetLength(1)];
            for (int i = 0; i < tempp.Length; i++)
            {
                tempp[i] = arr[0, i];
            }
            for (int i = 0; i < key.Length; i++)
            {
                int n = IndexOf(tempp, key[i], i);
                SwitchCollums(ref arr, i, n);
                char t = tempp[i];
                tempp[i] = tempp[n];
                tempp[n] = t;
            }
            Console.WriteLine("Вторая таблица:");
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
            for (int i = 1; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    res[l++] = arr[i, j];
                }
            }
            Console.Write("Открытый текст: ");
            for (int i = 0; i < res.Length; i++)
            {
                Console.Write(res[i]);
            }
            Console.WriteLine();
            var tmp = key.GroupBy(w => w).Where(w => w.Count() > 1).Select(w => w.Key).ToList();
            tmp = tmp.OrderBy(w => new Regex($@"{w}").Matches(key).Count).ToList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key.IndexOf(tmp[i], j) == -1)
                        continue;
                    if (key[j] != tmp[i])
                        continue;

                    
                    SwitchCollums(ref arr, key.IndexOf(tmp[i]), key.IndexOf(tmp[i], j));
                    l = 0;
                    for (int m = 1; m < arr.GetLength(0); m++)
                    {
                        for (int n = 0; n < arr.GetLength(1); n++)
                        {
                            res[l++] = arr[m, n];
                        }
                    }
                    Console.Write("Открытый текст: ");
                    for (int n = 0; n < res.Length; n++)
                    {
                        Console.Write(res[n]);
                    }
                    Console.WriteLine();
                }
            }
           
            #endregion

        }

        static void KeywordVertical(string str, string key)
        {
            #region Keyword Vertical
            char[,] arr1 = new char[key.Length, str.Length / key.Length];
            int l = 0;
            for (int i = 0; i < arr1.GetLength(1); i++)
            {
                for (int j = 0; j < arr1.GetLength(0); j++)
                {
                    arr1[j,i] = str[l++];
                }
            }
            Console.WriteLine("Первая таблица:");
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                for (int j = 0; j < arr1.GetLength(1); j++)
                {
                    Console.Write(arr1[i, j] + " ");
                }
                Console.WriteLine();
            }
            char[] tempp1 = new char[arr1.GetLength(0)];
            for (int i = 0; i < tempp1.Length; i++)
            {
                tempp1[i] = arr1[i, 0];
            }
            for (int i = 0; i < key.Length; i++)
            {
                int n = IndexOf(tempp1, key[i], i);
                SwitchLines(ref arr1, i, n);
                char t = tempp1[i];
                tempp1[i] = tempp1[n];
                tempp1[n] = t;
            }
            Console.WriteLine("Вторая таблица:");
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                for (int j = 0; j < arr1.GetLength(1); j++)
                {
                    Console.Write(arr1[i, j] + " ");
                }
                Console.WriteLine();
            }
            char[] res1 = new char[arr1.Length];
            l = 0;
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                for (int j = 1; j < arr1.GetLength(1); j++)
                {
                    res1[l++] = arr1[i, j];
                }
            }
            Console.Write("Открытый текст: ");
            for (int i = 0; i < res1.Length; i++)
            {
                Console.Write(res1[i]);
            }
            Console.WriteLine();
            var tmp = key.GroupBy(w => w).Where(w => w.Count() > 1).Select(w => w.Key).ToList();
            tmp = tmp.OrderBy(w => new Regex($@"{w}").Matches(key).Count).ToList();
            for (int i = 0; i < tmp.Count; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (key.IndexOf(tmp[i], j) == -1)
                        continue;
                    if (key[j] != tmp[i])
                        continue;
                    SwitchLines(ref arr1, key.IndexOf(tmp[i]), key.IndexOf(tmp[i], j));
                    l = 0;
                    for (int m =  0; m < arr1.GetLength(0); m++)
                    {
                        for (int n = 1; n < arr1.GetLength(1); n++)
                        {
                            res1[l++] = arr1[m, n];
                        }
                    }
                    Console.Write("Открытый текст: ");
                    for (int n = 0; n < res1.Length; n++)
                    {
                        Console.Write(res1[n]);
                    }
                    Console.WriteLine();
                }
            }
            
            #endregion

        }

        static int IndexOf(char[] arr, char a, int j)
        {
            for (int i = j; i < arr.Length; i++)
            {
                if (arr[i] == a)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
