using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Program
    {
        static void Bubble(Dados[] v)
        {
            Dados aux;
            for(int i = 0; i < v.Length - 1; i++)
                for (int j = 0; j < v.Length - i - 1; j++)
                    if (v[j].room_id > v[j + 1].room_id)
                    {
                        aux = v[j];
                        v[j] = v[j + 1];
                        v[j + 1] = aux;
                    }
        }

        static void Selection(Dados[] v)
        {
            for(int i = 0; i < v.Length-1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < v.Length; j++)
                    if (v[j].room_id < v[menor].room_id)
                        menor = j;
                Dados aux = v[menor];
                v[menor] = v[i];
                v[i] = aux;
            }
        }

        static void Insertion(Dados[] v)
        {
            Dados x;
            int j = 0;
            for(int i = 1; i < v.Length; i++)
            {
                x = v[i];
                j = i - 1;
                while(x.room_id < v[j].room_id)
                {
                    v[j + 1] = v[j];
                    j--;
                    if (j < 0)
                        break;
                }
                v[j + 1] = x;
            }
        }

      static void MergeSort(Dados[] v,int inicio, int fim)

        {

            if (inicio < fim)
            {
                int meio = (inicio + fim) / 2;
                MergeSort(v, inicio, meio);
                MergeSort(v, meio + 1, fim);
                Merge(v, inicio, meio, fim);
            }

        }

        static void Merge(Dados[] v, int inicio, int meio, int fim)
        {

            int n1, n2, i, j, k;
            n1 = meio - inicio + 1;
            n2 = fim - meio;

            Dados[] A1 = new Dados[n1 + 1];
            Dados[] A2 = new Dados[n2 + 1];

            for (i = 0; i < n1; i++)
                A1[i] = v[inicio + i];
            for (j = 0; j < n2; j++)
                A2[j] = v[meio + j + 1];

            A1[i] = new Dados();
            A1[i].room_id = int.MaxValue;

            A2[j] = new Dados();
            A2[j].room_id = int.MaxValue;

            i = 0;
            j = 0;

            for (k = inicio; k <= fim; k++)
            {
                if (A1[i].room_id <= A2[j].room_id)
                    v[k] = A1[i++];
                else
                    v[k] = A2[j++];
            }
        }

        static void QuickSort(Dados[] A, int esquerda, int direita)
        {

            Dados temp, pivo;
            int i, j;
            i = esquerda;
            j = direita;
            pivo = A[(esquerda + direita) / 2];

            while (i <= j)
            {

                while (A[i].room_id < pivo.room_id && i < direita) i++;
                while (A[j].room_id > pivo.room_id && j > esquerda) j--;

                if (i <= j)
                {
                    temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                    i++;
                    j--;
                }
            }

            if (j > esquerda)
                QuickSort(A, esquerda, j);

            if (i < direita)
                QuickSort(A, i, direita);
        }


        static void Main(string[] args)
        {
            Dados x = new Dados();
            x.room_id = 3;
            Dados y = new Dados();
            y.room_id = 4;
            Dados z = new Dados();
            z.room_id = 1;

            Dados[] v = { x, y, z };


            for (int i =  0; i < v.Length; i++)
            {
                Console.Write("{0} | ", v[i].room_id);
            }

            QuickSort(v, 0, v.Length - 1);

            Console.WriteLine();

            for (int i = 0; i < v.Length; i++)
            {
                Console.Write("{0} | ", v[i].room_id);
            }

            Console.ReadKey();
        }
    }
}
