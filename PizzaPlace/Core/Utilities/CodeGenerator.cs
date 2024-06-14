using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaPlace.Core.Utilities
{
    public class CodeGenerator
    {
        static int seed = Environment.TickCount;

        static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static string Generate(int count)
        {
            string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder();
            Random r = new Random();

            for (int i = 1; i <= count; i++)
            {
                int idx = random.Value.Next(0, s.Length);
                sb.Append(s.Substring(idx, 1));
            }

            return sb.ToString();
        }

        public static string GenerateWithSymbol(int count)
        {
            string s = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            StringBuilder sb = new StringBuilder();
            Random r = new Random();

            for (int i = 1; i <= count; i++)
            {
                int idx = random.Value.Next(0, s.Length);
                sb.Append(s.Substring(idx, 1));
            }

            return sb.ToString();
        }

        public static string GenerateNumber(int count)
        {
            string s = "0123456789";
            StringBuilder sb = new StringBuilder();
            Random r = new Random();

            for (int i = 1; i <= count; i++)
            {
                int idx = random.Value.Next(0, s.Length);
                sb.Append(s.Substring(idx, 1));
            }

            return sb.ToString();
        }
    }
}
