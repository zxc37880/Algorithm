// <copyright file="TwoNumber_Algorithm.cs" company="">
// All rights reserved. Allware Technology source code is an unpublished work and the
// use of a copyright notice does not imply otherwise. This source code contains
// confidential, trade secret material of Allware. Any attempt or participation
// in deciphering, decoding, reverse engineering or in any way altering the source
// code is strictly prohibited, unless the prior written consent of Company Name
// is obtained.
// </copyright>
// <date>       Created : 2019/12/20   </date>
// <brief>      Description :
// </brief>
// <author>     BoRen
// </author>

namespace Algorithm
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class TwoNumber_Algorithm
    {
        /// <summary>
        /// 兩數相關演算法
        /// </summary>
        public static void TwoNumber()
        {
            Console.Write("1=>輾轉相除法, 2=>Karatsuba法, 3=>模擬手算算法");
            Console.WriteLine();
            Console.Write("請輸入演算法：");
            string algorithm = Console.ReadLine();
            try
            {
                if (algorithm == "1")
                {
                    Console.WriteLine("進入輾轉相除法 ");
                    Console.Write("請輸入第一個數：");
                    string A = Console.ReadLine();
                    Console.Write("請輸入第二個數：");
                    string B = Console.ReadLine();

                    ulong result = TwoNumber_Algorithm.Euclidean(Convert.ToUInt64(A), Convert.ToUInt64(B));
                    Console.WriteLine($"{A} {B}的最大公因數：{result}");
                    Console.ReadKey();
                }
                else if (algorithm == "2")
                {
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

                    Console.WriteLine("進入Karatsuba法 ");
                    Console.Write("請輸入第一個數：");
                    ulong A = Convert.ToUInt64(Console.ReadLine());
                    Console.Write("請輸入第二個數：");
                    ulong B = Convert.ToUInt64(Console.ReadLine());

                    sw.Reset();
                    sw.Start();
                    Console.WriteLine("原始兩數相乘算法: ");
                    ulong x1 = A * B;
                    sw.Stop();
                    Console.WriteLine($"結果：{x1}");
                    Console.WriteLine($"原始兩數相乘 時間：{ sw.Elapsed.TotalMilliseconds.ToString()}");

                    sw.Reset();
                    sw.Start();
                    Console.WriteLine("普通Karatsuba法算法: ");
                    ulong a = TwoNumber_Algorithm.Karatsuba(A, B);
                    sw.Stop(); Console.WriteLine($"結果：{a}");
                    Console.WriteLine($"Karatsuba 時間：{sw.Elapsed.TotalMilliseconds.ToString()}");


                    int[] tmp1 = new int[A.ToString().Length];
                    int[] tmp2 = new int[B.ToString().Length];
                    for (int i = 0; i < A.ToString().Length; i++)
                    {
                        tmp1[i] = Convert.ToInt16(A.ToString().Substring(i, 1));
                    }

                    for (int i = 0; i < B.ToString().Length; i++)
                    {
                        tmp2[i] = Convert.ToInt16(B.ToString().Substring(i, 1));
                    }
                    sw.Reset();
                    sw.Start();
                    Console.WriteLine("模擬手算算法: ");
                    int[] b = TwoNumber_Algorithm.BigNumberMultiply2(tmp1, tmp2);
                    string yy = "";
                    foreach (int xa in b)
                    {
                        yy += xa.ToString();
                    }

                    sw.Stop(); Console.WriteLine($"結果：{yy}");
                    Console.WriteLine($"模擬手算 時間：{sw.Elapsed.TotalMilliseconds.ToString()}");


                    Console.Write("輸入任何鍵結束...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error：{ex.Message}");
                Console.Write("輸入任何鍵結束...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Karatsuba算法
        /// </summary>
        /// <param name="A">A數</param>
        /// <param name="B">B數</param>
        /// <returns>AB兩數相乘結果</returns>
        public static ulong Karatsuba(ulong A, ulong B)
        {
            if (A < 10 || B < 10)
            {
                return A * B;
            }

            int sizeA = A.ToString().Length;
            int sizeB = B.ToString().Length;
            int halfN = Math.Max(sizeA, sizeB) / 2;

            ulong a = Convert.ToUInt64(A.ToString().Substring(0, sizeA - halfN));
            ulong b = Convert.ToUInt64(A.ToString().Substring(sizeA - halfN));
            ulong c = Convert.ToUInt64(B.ToString().Substring(0, sizeB - halfN));
            ulong d = Convert.ToUInt64(B.ToString().Substring(sizeB - halfN));

            /*
            1. a*c
            2. b*d 
            3. (a+b)*(c+d)
            4. (3) - (2) - (1) 
             */

            ulong ac = Karatsuba(a, c);
            ulong bd = Karatsuba(b, d);
            ulong ad_plus_bc = Karatsuba((a + b), (c + d)) - ac - bd;

            return (ulong)(ac * Math.Pow(10, (2 * halfN)) + ad_plus_bc * Math.Pow(10, halfN) + bd);
        }

        /// <summary>
        /// Euclidean algorithm
        /// A = B*q + R
        /// (A, B) = (B, R)
        /// </summary>
        /// <param name="A">A</param>
        /// <param name="B">B</param>
        /// <returns>求AB兩數最大公因數</returns>
        public static ulong Euclidean(ulong A, ulong B)
        {
            /*
            int temp = A % B;
            if (temp == 0)
            {
                return A;
            }

            return Run1(B, temp);
            初期想法 */

            if (B == 0)
                return A;

            return Euclidean(B, A % B);
        }

        /// <summary>
        /// 兩數相乘
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int[] BigNumberMultiply2(int[] num1, int[] num2)
        {
            // 分配一個空間，用來存儲運算的結果，num1長的數 * num2長的數，結果不會超過num1+num2長
            int[] result = new int[num1.Length + num2.Length];

            // 先不考慮進位問題，根據豎式的乘法運算，num1的第i位與num2的第j位相乘，結果應該存放在結果的第i+j位上
            for (int i = 0; i < num1.Length; i++)
            {
                for (int j = 0; j < num2.Length; j++)
                {
                    result[i + j + 1] += num1[i] * num2[j];  // (因為進位的問題，最終放置到第i+j+1位) 
                }
            }

            //單獨處理進位
            for (int k = result.Length - 1; k > 0; k--)
            {
                if (result[k] > 10)
                {
                    result[k - 1] += result[k] / 10;
                    result[k] %= 10;
                }
            }
            return result;
        }
    }
}
