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
        static void Bubble(Dados[] A)
        {
            Dados aux;
            for(int i = 0; i < A.Length - 1; i++)
                for (int j = 0; j < A.Length - i - 1; j++)
                    if (A[j].room_id > A[j + 1].room_id)
                    {
                        aux = A[j];
                        A[j] = A[j + 1];
                        A[j + 1] = aux;
                    }
        }

        static void Selection(Dados[] A)
        {
            for(int i = 0; i < A.Length-1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < A.Length; j++)
                    if (A[j].room_id < A[menor].room_id)
                        menor = j;
                Dados aux = A[menor];
                A[menor] = A[i];
                A[i] = aux;
            }
        }

        static void Insertion(Dados[] A)
        {
            Dados x;
            int j = 0;
            for(int i = 1; i < A.Length; i++)
            {
                x = A[i];
                j = i - 1;
                while(x.room_id < A[j].room_id)
                {
                    A[j + 1] = A[j];
                    j--;
                    if (j < 0)
                        break;
                }
                A[j + 1] = x;
            }
        }

        static void MergeSort(Dados[] A,int inicio, int fim)

        {

            if (inicio < fim)
            {
                int meio = (inicio + fim) / 2;
                MergeSort(A, inicio, meio);
                MergeSort(A, meio + 1, fim);
                Merge(A, inicio, meio, fim);
            }

        }

        static void Merge(Dados[] A, int inicio, int meio, int fim)
        {

            int n1, n2, i, j, k;
            n1 = meio - inicio + 1;
            n2 = fim - meio;

            Dados[] A1 = new Dados[n1 + 1];
            Dados[] A2 = new Dados[n2 + 1];

            for (i = 0; i < n1; i++)
                A1[i] = A[inicio + i];
            for (j = 0; j < n2; j++)
                A2[j] = A[meio + j + 1];

            A1[i] = new Dados();
            A1[i].room_id = int.MaxValue;

            A2[j] = new Dados();
            A2[j].room_id = int.MaxValue;

            i = 0;
            j = 0;

            for (k = inicio; k <= fim; k++)
            {
                if (A1[i].room_id <= A2[j].room_id)
                    A[k] = A1[i++];
                else
                    A[k] = A2[j++];
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

        static void LerArquivo()
        {
            FileStream arq = new FileStream("dados_airbnb.csv", FileMode.Open);
            StreamReader read = new StreamReader(arq);
            read.ReadLine();
            string linha;
            string[] linhasplit;

            for(int i = 0; i < Medio.Length; i++)
            {
                linha = read.ReadLine();
                if (linha != null){
                    linhasplit = linha.Split(';');
                    Medio[i] = PreencheArq(linhasplit);
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

            Dados[] A;
           
            List<double> time;
            write.WriteLine("Valor de I;Caso Médio;Melhor Caso;Pior Caso");

            for (int i = 2000; i <= 16000; i *= 2)
            {
                time = new List<double>();
                
                write.Write("{0};", i);

                A = new Dados[i];
                CasoMed(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Bubble(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);

                   
                }
                double med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));                

                time = new List<double>();

                A = new Dados[i];
                MelhorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Bubble(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                PiorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Bubble(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.WriteLine("{0}", Math.Round(med, 3));
            }


            write.Close();
            arq.Close();

        }

        static void CalculaSelection()
        {

            FileStream arq = new FileStream("RelatorioSelection.txt", FileMode.OpenOrCreate);
            StreamWriter write = new StreamWriter(arq);

            Dados[] A;

            List<double> time;
            write.WriteLine("Valor de I;Caso Médio;Melhor Caso;Pior Caso");

            for (int i = 2000; i <= 16000; i *= 2)
            {
                time = new List<double>();

                write.Write("{0};", i);

                A = new Dados[i];
                CasoMed(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Selection(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                double med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                MelhorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Selection(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                PiorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Selection(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.WriteLine("{0}", Math.Round(med, 3));
            }


            write.Close();
            arq.Close();

        }

        static void CalculaInsertion()
        {

            FileStream arq = new FileStream("RelatorioInsertion.txt", FileMode.OpenOrCreate);
            StreamWriter write = new StreamWriter(arq);

            Dados[] A;

            List<double> time;
            write.WriteLine("Valor de I;Caso Médio;Melhor Caso;Pior Caso");

            for (int i = 2000; i <= 16000; i *= 2)
            {
                time = new List<double>();

                write.Write("{0};", i);

                A = new Dados[i];
                CasoMed(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Insertion(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                double med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                MelhorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Insertion(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                PiorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    Insertion(A);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.WriteLine("{0}", Math.Round(med, 3));
            }


            write.Close();
            arq.Close();

        }

        static void CalculaMerge()
        {

            FileStream arq = new FileStream("RelatorioMerge.txt", FileMode.OpenOrCreate);
            StreamWriter write = new StreamWriter(arq);

            Dados[] A;

            List<double> time;
            write.WriteLine("Valor de I;Caso Médio;Melhor Caso;Pior Caso");

            for (int i = 2000; i <= 16000; i *= 2)
            {
                time = new List<double>();

                write.Write("{0};", i);

                A = new Dados[i];
                CasoMed(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    MergeSort(A, 0, A.Length - 1);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                double med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                MelhorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    MergeSort(A, 0, A.Length - 1);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                PiorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    MergeSort(A, 0, A.Length - 1);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.WriteLine("{0}", Math.Round(med, 3));
            }


            write.Close();
            arq.Close();

        }

        static void CalculaQuick()
        {

            FileStream arq = new FileStream("RelatorioQuick.txt", FileMode.OpenOrCreate);
            StreamWriter write = new StreamWriter(arq);

            Dados[] A;

            List<double> time;
            write.WriteLine("Valor de I;Caso Médio;Melhor Caso;Pior Caso");

            for (int i = 2000; i <= 16000; i *= 2)
            {
                time = new List<double>();

                write.Write("{0};", i);

                A = new Dados[i];
                CasoMed(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    QuickSort(A, 0, A.Length - 1);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                double med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                MelhorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    QuickSort(A, 0, A.Length - 1);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.Write("{0};", Math.Round(med, 3));

                time = new List<double>();

                A = new Dados[i];
                PiorCaso(A);

                for (int j = 0; j < 5; j++)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    QuickSort(A, 0, A.Length - 1);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds / 1000.0;
                    time.Add(elapsedMs);


                }
                med = MediaTempo(time);
                write.WriteLine("{0}", Math.Round(med, 3));
            }


            write.Close();
            arq.Close();

        }

        static void CasoMed(Dados[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = Medio[i];
            }
        }

        static void MelhorCaso(Dados[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = Cresc[i];
            }
        }

        static void PiorCaso(Dados[] A)
        {
            for(int i  =0; i < A.Length; i++)
            {
                A[i] = Dec[i];
            }
        }

        static void Inverte()
        {
            int j = 0;
            for (int i = Cresc.Length - 1; i >= 0; i--, j++)
            {
                Dec[j] = Cresc[i];
            }
        }

        static double MediaTempo(List<double> time)
        {
            time.Sort();
            return (time[1] + time[2] + time[3]) / 3;
        }

        static Dados[] Medio = new Dados[128000];
        static Dados[] Cresc = new Dados[128000];
        static Dados[] Dec = new Dados[128000];

        static void Main(string[] args)
        {
            //coloca os dados do arquivo no vetor Medio
            LerArquivo();
            //vetor Cresc recebe caso medio que foi populado com as informações dos arquivos
            Cresc = Medio;
            //Ordena vetor Cresc em Crescente
            QuickSort(Cresc, 0, Cresc.Length - 1);
            //Inverte inverter os números crescentes e colocara no vetor Dec
            Inverte();
            //executa as ordenações
            //tempo total gasto e memoria gasta
            FileStream arq = new FileStream("RelatorioTotal.txt", FileMode.OpenOrCreate);
            StreamWriter write = new StreamWriter(arq);

            var watch = System.Diagnostics.Stopwatch.StartNew();

            CalculaBubble();
            CalculaSelection();
            CalculaInsertion();
            CalculaMerge();
            CalculaQuick();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds / 1000.0;

            write.WriteLine("Tempo total gasto => "+ elapsedMs);

            write.Close();
            arq.Close();

        }
    }
}
