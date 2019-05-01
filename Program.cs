using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace projekt1
{
    class Program
    {
        static Random _random = new Random();


        // ponizej kod skopiowany z Internetu, zależało mi na tym, żeby tworzyć tablice bez powtórzeń, co wykonuje poniższy kod
        static int[] Randomizeints(int[] arr)
        {
            List<KeyValuePair<int, int>> list =
                new List<KeyValuePair<int, int>>();
            // Add all ints from array.
            // ... Add new random int each time.
            foreach (int s in arr)
            {
                list.Add(new KeyValuePair<int, int>(_random.Next(), s));
            }
            // Sort the list by the random number.
            var sorted = from item in list
                         orderby item.Key
                         select item;
            // Allocate new int array.
            int[] result = new int[arr.Length];
            // Copy values to array.
            int index = 0;
            foreach (KeyValuePair<int, int> pair in sorted)
            {
                result[index] = pair.Value;
                index++;
            }
            // Return copied array.
            return result;
        }

        static int Wyszukiwanieliniowe(int dt, int[] s, int sl) //dt = dlugosc tablicy; a = tablica; sl = szukana liczba
        {
            int i = 0;
            int j = dt;
            for (i = 0; i < dt; i++)
            {
                if (s[i] == sl)
                {
                    j = i;
                }
            }
            return j + 1;
        }

        static TimeSpan Wyszukiwanieliniowe_czas(int dt, int[] s, int sl) //dt = dlugosc tablicy; a = tablica; sl = szukana liczba
        {
            int i = 0;
            int j = dt;

            DateTime start = DateTime.Now;
            for (i = 0; i < dt; i++)
            {
                if (s[i] == sl)
                {
                    j = i;
                }
            }
            TimeSpan czas = DateTime.Now - start;


            return czas;
        }

        static int Wyszukiwaniebinarne(int dt, int[] a, int sl) //dt = dlugosc tablicy; a = tablica; sl = szukana liczba
        {
            
            bool znaleziona = false;
            int lewo = 0;
            int prawo = dt - 1;
            int srodek = 0;
            int szukana = sl;

            while ((lewo <= prawo) && (znaleziona != true) )
            {
                srodek = (lewo + prawo) / 2;
                if (a[srodek] == szukana)
                {
                    znaleziona = true;
                }
                else
                    if (a[srodek] < sl)
                    {
                    lewo = srodek + 1;
                    }
                else
                    prawo = srodek - 1;
                }
            if (znaleziona == true)
            {
                return srodek + 1;
            }
            else
            return dt + 1; 
        }

        static TimeSpan Wyszukiwaniebinarne_czas(int dt, int[] a, int sl) //dt = dlugosc tablicy; a = tablica; sl = szukana liczba
        {
            int j = 0;
            bool znaleziona = false;
            int lewo = 0;
            int prawo = dt - 1;
            int srodek = 0;

            DateTime start = DateTime.Now;
            while ((lewo <= prawo) && (znaleziona != true))
            {
                srodek = (lewo + prawo) / 2;
                if (a[srodek] == sl)
                {
                    znaleziona = true;
                }
                else
                    if (a[srodek] < sl)
                {
                    lewo = srodek + 1;
                }
                else
                    prawo = srodek - 1;
            }
            TimeSpan czas = DateTime.Now - start;


            if (znaleziona == true)
            {
                return czas;
            }
            else

            return czas;
        }

        static int Wyszukiwaniebinarne_zlozonosc(int dt, int[] a, int sl) //dt = dlugosc tablicy; a = tablica; sl = szukana liczba
        {
            int j = 0;
            bool znaleziona = false;
            int lewo = 0;
            int prawo = dt - 1;
            int srodek = 0;
            int operacje = 0;

            while ((lewo <= prawo) && (znaleziona != true))
            {
                operacje++;
                srodek = (lewo + prawo) / 2;
                if (a[srodek] == sl)
                {
                    znaleziona = true;
                }
                else
                    if (a[srodek] < sl)
                {
                    lewo = srodek + 1;
                }
                else
                    prawo = srodek - 1;
            }
            if (znaleziona == true)
            {
                return operacje;
            }
            else
                return operacje + 1;
        }

        static void Liniowe()
        {
            Console.WriteLine("Podaj dlugosc tablicy: ");
            int dlugosctablicy = int.Parse(Console.ReadLine());
            Console.WriteLine("Podaj ilosc prob");
            int iloscprob = int.Parse(Console.ReadLine());

            using (StreamWriter writer = new StreamWriter("liniowe.txt"))
            {
                writer.WriteLine("n ; szukana ; pozycja ; czas ; czas_pes ; tablica");

                for (int k = 1; k < iloscprob + 1; k++)
                {

                    int[] arr = new int[dlugosctablicy];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i] = i + 1;
                    }

                    int[] shuffle = Randomizeints(arr);
                    Console.WriteLine("tablica " + k + " wygenerowana");
                    /*
                    Console.WriteLine("Tak wyglada tablica");
                    foreach (int s in shuffle)
                    {
                        Console.WriteLine(s);
                    }
                    */

                    //Console.ReadLine();
                    Console.WriteLine("A teraz bedzie wyszukiwanie numer: " + k);

                    int szukanaliczba;
                    Random r = new Random();
                    szukanaliczba = r.Next(dlugosctablicy);
                    int pozycja = Wyszukiwanieliniowe(dlugosctablicy, shuffle, szukanaliczba);
                    TimeSpan time = Wyszukiwanieliniowe_czas(dlugosctablicy, shuffle, szukanaliczba);

                    //przypadek pesymistyczny:                
                    TimeSpan time_pes = Wyszukiwanieliniowe_czas(dlugosctablicy, shuffle, dlugosctablicy + 1);



                    if (pozycja <= dlugosctablicy)
                    {
                        Console.WriteLine("Szukana liczba to " + szukanaliczba + " i znajduje sie na pozycji: " + pozycja + " co zajęło: " + time.TotalMilliseconds);
                        Console.WriteLine("Czas dla przypadku pesymistycznego: " + time_pes.TotalMilliseconds);
                    }
                    else
                        Console.WriteLine("Szukana liczba to " + szukanaliczba + " i znajduje sie poza zakresem!" + " co zajęło: " + time.TotalMilliseconds);

                    writer.WriteLine(k + " ; " + szukanaliczba + " ; " + pozycja + " ; " + time.TotalMilliseconds + " ; " + time_pes.TotalMilliseconds + " ; " + dlugosctablicy);

                }
                Console.WriteLine("Koniec!");
                Console.ReadLine();

        }
        }

        static void Binarne()
        {
            Console.WriteLine("Podaj dlugosc tablicy: ");
            int dlugosctablicy = int.Parse(Console.ReadLine());
            Console.WriteLine("Podaj ilosc prob");
            int iloscprob = int.Parse(Console.ReadLine());
            //Console.WriteLine("Podaj szukana liczbe");
            //int szukanaliczba = int.Parse(Console.ReadLine());

            using (StreamWriter writer = new StreamWriter("binarne.txt"))
            {
                writer.WriteLine("n ; szukana ; pozycja ; czas ; operacje; czas_pes ; operacje_pes ; tablica");

                for (int k = 1; k < iloscprob + 1; k++)
                {

                    int[] arr = new int[dlugosctablicy];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i] = i + 1;
                    }

                    //int[] shuffle = Randomizeints(arr);

                    /*
                    Console.WriteLine("tablica " + k + " wygenerowana");

                    Console.WriteLine("Tak wyglada tablica");
                    foreach (int s in arr)
                    {
                        Console.WriteLine(s);
                    }
                    */


                    //Console.ReadLine();
                    Console.WriteLine("A teraz bedzie wyszukiwanie numer: " + k);

                    int szukanaliczba;
                    Random r = new Random();
                    szukanaliczba = r.Next(dlugosctablicy);
                    Console.WriteLine(szukanaliczba);
                    int pozycja = Wyszukiwaniebinarne(dlugosctablicy, arr, szukanaliczba);
                    TimeSpan time = Wyszukiwaniebinarne_czas(dlugosctablicy, arr, szukanaliczba);
                    int operacje = Wyszukiwaniebinarne_zlozonosc(dlugosctablicy, arr, szukanaliczba);

                    // dla przypadku pesymistycznego
                    TimeSpan time_pes = Wyszukiwaniebinarne_czas(dlugosctablicy, arr, 268435456);
                    int operacje_pes = Wyszukiwaniebinarne_zlozonosc(dlugosctablicy, arr, 268435456);

                    if (pozycja <= dlugosctablicy)
                    {
                        Console.WriteLine("Szukana liczba to " + szukanaliczba + " i znajduje sie na pozycji: " + pozycja + " co zajęło: " + time.TotalMilliseconds + " ms i wymagalo " + operacje + " operacji");
                        Console.WriteLine("Czas dla przypadku pesymistycznego: " + time_pes.TotalMilliseconds + " ms i ilość operacji: " + operacje_pes);
                    }
                    else
                        Console.WriteLine("Szukana liczba to " + szukanaliczba + " i  znajduje sie poza zakresem!" + " co zajęło: " + time.TotalMilliseconds + " ms i wymagalo " + operacje + " operacji");

                    writer.WriteLine(k + " ; " + szukanaliczba + " ; " + pozycja + " ; " + time.TotalMilliseconds + " ; " + operacje + " ; " + time_pes.TotalMilliseconds + " ; " + operacje_pes + " ; " + dlugosctablicy);

                }
            }
            Console.WriteLine("Koniec!");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz operacje:");
            Console.WriteLine("1. wyszukiwanie liniowe (do 10 000 000)");
            Console.WriteLine("2. wyszukiwanie binarne");
            int sterowanie = int.Parse(Console.ReadLine());
            

                switch (sterowanie)
            {
                case 1:
                    Liniowe();

                        break;
                case 2:
                    Binarne();
                    break;
                default:
                    Console.WriteLine("cos nie tak!");
                    break;
            }                                
        }        
    }
}