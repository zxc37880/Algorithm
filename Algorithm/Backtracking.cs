// <copyright file="Backtracking.cs" company="">
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

    internal class Backtracking
    {
        private int N;              ///n表示n個物品
        private int W;              ///w表示購物車的容量
        private double[] wArray;    /// w[i]表示第i個物品的重量
        private double[] vArray;    /// v[i]表示第i個物品的價值
        private double cw;          /// 目前重量
        private double cp;          /// 目前價值

        private bool[] x;           /// 目前x[i]表示第i個物品是否放入購物車
        private double bestp = 0;   /// 目前最優價值
        private bool[] bestx;       /// 目前最優解
        private double sumw = 0.0;
        private double sumv = 0.0;

        public void Main()
        {
            Console.Write("請輸入物品的個數");
            Console.WriteLine();
            string n_string = Console.ReadLine();
            N = Convert.ToInt32(n_string);
            wArray = new double[N + 1];
            vArray = new double[N + 1];

            Console.Write("請輸入購物車的容量W");
            Console.WriteLine();
            string weight_string = Console.ReadLine();
            W = Convert.ToInt32(weight_string);

            Console.Write("依次輸入重量w和價值v, ex.2 6 5 3 = 第一項W=2 V=6, 第二項W=5, V=3 依此類推..");
            Console.WriteLine();
            string s = Console.ReadLine();
            string[] arr = s.Split(' ');

            int b = 0;
            int c = 1;
            for (int a = 0; a < N; a++)
            {
                wArray[a] = Convert.ToDouble(arr[b]);
                vArray[a] = Convert.ToDouble(arr[c]);
                b = b + 2;
                c = c + 2;
            }

            Knapsack(W, N);
        }

        /// <summary>
        /// 背包問題
        /// </summary>
        /// <param name="W"></param>
        /// <param name="n"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        public void Knapsack(double W, int n)
        {
            x = new bool[n + 1];
            bestx = new bool[n + 1];

            for (int i = 0; i < n; i++)
            {
                sumv += vArray[i];
                sumw += wArray[i];
            }

            if (sumw <= W)
            {
                bestp = sumv;
                Console.WriteLine($"放入購物車物品最大價值:{bestp}");
                return;
            }
            Backtrack(0);

            Console.WriteLine($"放入購物車物品最大價值:{bestp}");
            Console.Write($"放入購物車物品序號為:");
            for (int i = 0; i < N; i++)
            {
                if (bestx[i] == true)
                {
                    Console.Write($"第{i+1}個 ");
                }
            }
            Console.WriteLine();
            Console.Write("輸入任何鍵結束...");
            Console.ReadKey();
        }

        /// <summary>
        /// Backtrack 演算法
        /// </summary>
        /// <param name="t">目前擴展結點層數</param>
        private void Backtrack(int t)
        {
            if (t > N)
            {
                for (int j = 0; j < N; j++)
                {
                    bestx[j] = x[j];
                }
                bestp = cp;
                return;
            }

            //如果滿足限制條件則搜尋左子樹
            if (cw + wArray[t] <= W)
            {
                x[t] = true;
                cw += wArray[t];
                cp += vArray[t];
                Backtrack(t + 1);
                cw -= wArray[t];
                cp -= vArray[t];
            }

            //如果滿足限制條件則搜尋右子樹
            if (Bound(t + 1) > bestp)
            {
                x[t] = false;
                Backtrack(t + 1);
            }
        }

        /// <summary>
        /// 限界函數，以裝入物品價值(i)+剩餘物品總價值
        /// </summary>
        /// <param name="i">目前節點</param>
        /// <returns>物品價值(i)+剩餘物品總價值</returns>
        public double Bound(int i)
        {
            double rp = 0;
            while (i <= N)
            {
                rp += vArray[i];
                i++;
            }
            return cp + rp;
        }
    }
}