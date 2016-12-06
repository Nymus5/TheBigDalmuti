using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace BigDalmuti.Extensions
{
    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> cardList)
        {
            int n = cardList.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = cardList[k];
                cardList[k] = cardList[n];
                cardList[n] = value;
            }
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}
