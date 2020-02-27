using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    class Backtracking_TSP
    {
        /// <summary>
        /// 設定成無窮大的值
        /// </summary>
        const double INF = 1e7;

        const int N = 100;

        int[,] g = new int[N, N];

        /// <summary>
        /// 紀錄目前路徑
        /// </summary>
        int[] x = new int[N];

        /// <summary>
        /// 紀錄目前最優路徑
        /// </summary>
        int[] bestx = new int[N];

        /// <summary>
        /// 記錄路徑長度
        /// </summary>
        int cl;

        /// <summary>
        /// 目前最短路徑長度
        /// </summary>
        int bestl;

        /// <summary>
        /// 城市個數n，邊數m
        /// </summary>
        int n, m;

        public void Main()
        {
            int u, v, w;
            n = 5;
            Init();

            int m = 9;
            g[1, 2] = g[2, 1] = 3;
            g[1, 4] = g[4, 1] = 8;
            g[1, 5] = g[5, 1] = 9;
            g[2, 3] = g[3, 2] = 3;
            g[2, 4] = g[4, 2] = 10;
            g[2, 5] = g[5, 2] = 5;
            g[3, 4] = g[4, 3] = 4;
            g[3, 5] = g[5, 3] = 3;
            g[4, 5] = g[5, 4] = 20;
            Traveling(2);
            Print();

        }

        private void Traveling(int t)
        {
            if (t > n)
            {
                //到達子節點
                //推銷貨物的最後一個城市與住地城市有邊相連並且路徑長度比目前最優值小
                //說明找到了一條更好的路徑，紀錄相關資訊
                //g[最後一個點,1]!=∞ && cl+g[最後一個點,2]<bestl=∞ 
                if (g[x[n], 1] != INF && (cl + g[x[n], 1] < bestl))
                {
                    for (int j = 1; j <= n; j++)
                        bestx[j] = x[j];

                    bestl = cl + g[x[n], 1];
                }
            }
            else
            {
                for (int j = t; j <= n; j++)
                {
                    //搜尋擴展節點的所有分支
                    //如果第t-1個城市與第j個城市有邊相連並且有可能得到更短的路線
                    //舉例g[1,2] =>城市1到城市2的距離
                    //g[1,2]!=∞ && cl+g[1,2]<bestl=∞
                    if (g[x[t - 1], x[j]] != INF && (cl + g[x[t - 1], x[j]] < bestl))
                    {
                        //保存第t個要去的城市編號到x[t]中，進入到第t+1層
                        Swap(ref x[t], ref x[j]);
                        cl = cl + g[x[t - 1], x[t]];
                        Traveling(t + 1);//從第t+1層的擴展節點繼續搜尋
                        //第t+1層搜尋完畢，回朔到第t層
                        cl = cl - g[x[t - 1], x[t]];
                        Swap(ref x[t], ref x[j]);
                    }
                }
            }
        }

        /// <summary>
        /// 交換swap
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            bestl = (int)INF;
            cl = 0;

            for (int i = 1; i <= n; i++)
            {
                for (int j = i; j <= n; j++)
                {
                    //表示路徑不可走
                    g[i, j] = g[j, i] = (int)INF;
                }
            }

            for (int i = 0; i <= n; i++)
            {
                x[i] = i;
                bestx[i] = 0;
            }
        }

        /// <summary>
        /// 列印路徑
        /// </summary>
        public void Print()
        {
            Console.Write("最短路徑: ");
            for (int i = 1; i <= n; i++)
            {
                Console.Write($"{bestx[i]}----->");
            }

            Console.WriteLine($"1");
            Console.WriteLine($"最短路徑長度: {bestl}");
            Console.ReadKey();
        }
    }
}
