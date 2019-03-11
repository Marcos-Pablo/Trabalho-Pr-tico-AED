using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Program
    {
        static void Bubble(int[] v)
        {
            int aux;
            for(int i = 0; i < v.Length - 1; i++)
                for (int j = 0; j < v.Length - i - 1; j++)
                    if (v[j] > v[j + 1])
                    {
                        aux = v[j];
                        v[j] = v[j + 1];
                        v[j + 1] = aux;
                    }
        }

        static void Selection(int[] v)
        {
            for(int i = 0; i < v.Length-1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < v.Length; j++)
                    if (v[j] < v[menor])
                        menor = j;
                int aux = v[menor];
                v[menor] = v[i];
                v[i] = aux;
            }
        }

        static void Insertion(int[] v)
        {
            int i, j, x;
            for (i = 2; i <= v.Length; i++)
            {
                x = v[i];
                j = i - 1;
                v[0] = x;
                while (x < v[j])
                {
                    v[j + 1] = v[j];
                    j--;
                }
                v[j + 1] = x;
            }
        }

    

        static void Main(string[] args)
        {
        }
    }
}
