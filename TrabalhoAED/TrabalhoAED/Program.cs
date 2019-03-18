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

        void MergeSort(int[] A,int inicio, int fim)
        {

            if (inicio < fim)
            {
                int meio = (inicio + fim) / 2;
                MergeSort(A, inicio, meio);
                MergeSort(A, meio + 1, fim);
                Merge(A, inicio, meio, fim);
            }

        }

        void Merge(int[] A, int inicio, int meio, int fim)
        {

            int n1, n2, i, j, k;
            n1 = meio - inicio + 1;
            n2 = fim - meio;

            int[] A1 = new int[n1 + 1]; 
            int[] A2 = new int[n2 + 1];

            for (i = 0; i < n1; i++)
                A1[i] = A[inicio + i];
            for (j = 0; j < n2; j++)
                A2[j] = A[meio + j + 1];

            A1[i] = int.MaxValue;
            A2[j] = int.MaxValue;

            i = 0;
            j = 0;

            for (k = inicio; k <= fim; k++)
            {
                if (A1[i] <= A2[j])
                    A[k] = A1[i++];
                else
                    A[k] = A2[j++];
            }
        }

        static void Main(string[] args)
        {
        }
    }
}
