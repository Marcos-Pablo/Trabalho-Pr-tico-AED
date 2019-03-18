using System;
using System.IO;
using System.Collections;
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

        static void LerArquivo(Dados[] v)
        {
            FileStream arq = new FileStream("dados_airbnb.csv", FileMode.Open);
            StreamReader read = new StreamReader(arq);
            read.ReadLine();
            string linha;
            string[] linhasplit;

            for(int i = 0; i < v.Length; i++)
            {
                linha = read.ReadLine();
                if (linha != null){
                    linhasplit = linha.Split(';');
                    v[i] = PreencheArq(linhasplit);
                }
            }
            arq.Close();
        }

        static Dados PreencheArq(string[] linhasplit)
        {
            Dados x = new Dados();
            x.room_id = int.Parse(linhasplit[0]);
            x.host_id = int.Parse(linhasplit[1]);
            x.room_type = linhasplit[2];
            x.country = linhasplit[3];
            x.city = linhasplit[4];
            x.neighborhood = linhasplit[5];
            x.reviews = int.Parse(linhasplit[6]);
            x.overall_satisfaction = double.Parse(linhasplit[7]);
            x.accommodates = int.Parse(linhasplit[8]);
            x.bedrooms = double.Parse(linhasplit[9]);
            x.price = double.Parse(linhasplit[10]);
            x.property_type = linhasplit[11];
            return x;
        }

        static void CalculaBubble()
        {

            FileStream arq = new FileStream("RelatorioBubble.txt", FileMode.OpenOrCreate);
            StreamWriter write = new StreamWriter(arq);
            Dados[] v;
            List<double> time;

            for (int i = 2000; i <= 16000; i *= 2)
            {
                time = new List<double>();
                Console.WriteLine("\nValor de I: {0}",i);
                for (int j = 0; j < 5; j++)
                {
                    v = new Dados[i];
                    LerArquivo(v);

                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Bubble(v);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);

                    

                }
                double med = MediaTempo(time);
                write.WriteLine("Valor de i: {0} - media tempo: {1}s", i, med);
            }


            write.Close();
            arq.Close();

        }

        static double MediaTempo(List<double> time)
        {
            time.Sort();
            return (time[1] + time[2] + time[3]) / 3;
        }

        static void Main(string[] args)
        {
            CalculaBubble();
        }
    }
}
