using System;
using System.IO;
using System.Collections;

class Zadanie5
{
    int n, m;
    int[,] weight;
    int[] dist, pred;

    public void PrintWeight()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                Console.Write(weight[j, i] + " ");
            Console.Write("\n");
        }
    }

    public void Print_tables()
    {
        Console.Write("Dist[ ");
        for (int i = 0; i < n; i++)
        {
            Console.Write(dist[i] + " ");
        }
        Console.Write("]\nPred[ ");
        for (int i = 0; i < n; i++)
        {
            Console.Write(pred[i] + " ");
        }
        Console.Write("]");
    }

    public void PrintResult()
    {
        //Element z Dist/Pred =0 oznacza ujście
        using (StreamWriter sw = new StreamWriter("Out0305.txt"))
        {
            sw.Write("Dist[ ");
            for (int i = 0; i < n; i++)
            {
                sw.Write(dist[i] + " ");
            }
            sw.Write("]\nPred[ ");
            for (int i = 0; i < n; i++)
            {
                sw.Write(pred[i] + " ");
            }
            sw.Write("]");
        }
    }

    public void Transpose_weight()
    {
        int temp;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                temp = weight[i, j];
                weight[i, j] = weight[j, i];
                weight[j, i] = temp;
            }
        }
    }

    public void Dijkstra()
    {
        int current;
        int next = -1;
        int min = 999;
        bool[] v = new bool[n];

        //zerowanie tablicy bool
        for (int i = 0; i < n; i++)
        {
            v[i] = false;
        }
        v[m - 1] = true; //ustawienie bool na true, dla wierzchołka będącego ujściem

        for (int j = 0; j < n; j++) //znalezienie najmniejszej wagi w tablicy dist
        {
            if (v[j] == false && dist[j] < min)
            {
                min = dist[j];
                next = j;
            }
        }
        pred[next] = m;
        dist[m - 1] = 0;

        for (int i = 0; i < n - 2; i++)
        {
            current = next;
            v[current] = true;

            for (int j = 0; j < n; j++)
            {
                if (v[j] == false && weight[j, current] + min < dist[j]) //aktualizacja wagi ścieżki, jeśli dany wierzchołek jeszcze nie był rozpatrywany jako current
                {
                    dist[j] = weight[j, current] + min;
                    pred[j] = current + 1;
                }
            }

            min = 999;
            next = -1;

            for (int j = 0; j < n; j++) //znalezienie najmniejszej wagi w tablicy dist
            {
                if (v[j] == false && dist[j] < min)
                {
                    min = dist[j];
                    next = j;
                }
            }
        }
    }

    public Zadanie5()
    {
        using (StreamReader sr = new StreamReader("In0305.txt"))
        {
            string[] split;
            string read;
            read = sr.ReadLine();
            split = read.Split();

            n = Int32.Parse(split[0]);
            m = Int32.Parse(split[1]);
            weight = new int[6, 6];
            dist = new int[n];
            pred = new int[n];

            for (int i = 0; i < n; i++)
            {
                read = sr.ReadLine();
                split = read.Split();
                for (int j = 0; j < n; j++)
                {
                    weight[j, i] = Int32.Parse(split[j]);
                }
            }

            for (int i = 0; i < n; i++) //przepisanie wartości do tablicy dist przed transpozycja - przepisanie z kolumny odpowiadającej nr ujścia
            {
                dist[i] = weight[m - 1, i];
                //pred[i] = m - 1;

                if (weight[m - 1, i] < 999) //ustawienie wartości początkowych w tablicy pred
                    pred[i] = m;
            }
        }
    }
}

namespace AlgorytmDijkstry
{
    class Program
    {
        static void Main(string[] args)
        {
            //Zadanie 5 - algorytm Dijkstry
            Zadanie5 z5 = new Zadanie5();
            z5.Transpose_weight();
            z5.PrintResult();
            z5.Dijkstra();
            z5.PrintResult();
        }
    }
}
