// <copyright file="Program.cs" company="">
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

    class Program
    {
        static void Main(string[] args)
        {
            //TwoNumber_Algorithm.TwoNumber();

            Backtracking_TSP btk_TSP = new Backtracking_TSP();
            btk_TSP.Main();

            /*
            Backtracking btk = new Backtracking();
            btk.Main();
            */
        }

    }
}
