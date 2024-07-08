using System;
using System.IO;

namespace SZTF1_feleveshazi
{
    class Program
    {
        static void Main(string[] args)
        {
            //beolvassuk az inputot
            string[] sorok = File.ReadAllLines("rendszer.be");
            //1-50ig
            Munkanap[] munkanapok = new Munkanap[Convert.ToInt32(sorok[0]) + 1];

            //kulonboztessuk meg a ket rendszergazdat. Egyik legyen Janos, masik Terez
            //János és Teréz alapvetően dolgozik
            for (int i = 0; i < munkanapok.Length; i++)
            {
                munkanapok[i] = new Munkanap(true, true);
            }


            //János szabadságainak időintervalluma
            int janosSzabadsagKeresei = Convert.ToInt32(sorok[1]);

            //3.sortol-5 sorig
            for (int i = 2; i < janosSzabadsagKeresei + 2; i++)
            {
                string[] temp = sorok[i].Split(" ");
                int kezdonap = Convert.ToInt32(temp[0]);
                int vegnap = Convert.ToInt32(temp[1]);

                //Megadjuk, hogy János mettől-meddig NEM lesz bent
                for (int j = kezdonap; j <= vegnap; j++)
                {
                    //janos false, terez az default
                    munkanapok[j] = new Munkanap(false, munkanapok[j].terez);
                }
            }


            //Teréz szabadságainak időintervalluma
            
            //6.index
            for (int i = janosSzabadsagKeresei + 3; i < sorok.Length; i++)
            {
                string[] temp = sorok[i].Split(" ");
                int kezdonap = Convert.ToInt32(temp[0]);
                int vegnap = Convert.ToInt32(temp[1]);

                //Megadjuk, hogy Teréz mettől-meddig NEM lesz bent
                for (int j = kezdonap; j <= vegnap; j++)
                {
                    //janos default, terez modified
                    munkanapok[j] = new Munkanap(munkanapok[j].janos, false);
                }
            }

            StreamWriter kiFile = new StreamWriter("rendszer.ki");

            //Biztonságos időszakok meghatározása és fájlba való kiírása
            Idoszak[] biztonsagosIdoszakok = new Idoszak[munkanapok.Length];
            int biztonsagosNapok = 0;


            //igy lehet null ereteket 
            int? kezdoBiztonsagosNap = null;

            //1-50 napig
            for (int i = 1; i < munkanapok.Length; i++)
            {
                //ha aktualis biztonsagos
                if (munkanapok[i].Biztonsagos)
                {
                    //ha meg nem volt biztonsagos, akkor most ez lesz az elso
                    if (kezdoBiztonsagosNap == null)
                    {
                        kezdoBiztonsagosNap = i;
                    }

                    //ha a legelso nem, es a kovetkezo sem biztonsagos --> ez lesz a biztonsagos
                    if (kezdoBiztonsagosNap != null && (i == munkanapok.Length - 1 || !munkanapok[i + 1].Biztonsagos))
                    {
                        //uj biztonsagost hozza adjuk (uj idoszak)
                        biztonsagosIdoszakok[biztonsagosNapok] = new Idoszak((int)kezdoBiztonsagosNap, i);
                        //kovetkezonel ujra ezt, ezert nullozzunk ki
                        kezdoBiztonsagosNap = null;
                        //indexeles tomb miatt
                        biztonsagosNapok++;
                    }
                }
            }
            //kiiratas 
            kiFile.WriteLine(biztonsagosNapok);

            for (int i = 0; i < biztonsagosNapok; i++)
            {
                kiFile.WriteLine(biztonsagosIdoszakok[i].ToString());
            }


            //Veszélyes időszakok meghatározása és fájlba való kiírása
            Idoszak[] veszelyesIdoszakok = new Idoszak[munkanapok.Length];
            int veszelyesNapok = 0;

            int? kezdoVeszelyesNap = null;

            for (int i = 1; i < munkanapok.Length; i++)
            {
                //ha aktualis veszelyes
                if (munkanapok[i].Veszelyes)
                {
                    //ha meg nem volt veszelyes, akkor most ez lesz az elso
                    if (kezdoVeszelyesNap == null)
                    {
                        kezdoVeszelyesNap = i;
                    }

                    //ha a legelso nem, es a kovetkezo sem veszelyes --> ez lesz a veszelyes
                    if (kezdoVeszelyesNap != null && (i == munkanapok.Length - 1 || !munkanapok[i + 1].Veszelyes))
                    {
                        //uj veszelyest hozza adjuk (uj idoszak)
                        veszelyesIdoszakok[veszelyesNapok] = new Idoszak((int)kezdoVeszelyesNap, i);
                        //kovetkezonel ujra ezt, ezert nullozzunk ki
                        kezdoVeszelyesNap = null;
                        //indexeles tomb miatt
                        veszelyesNapok++;
                    }
                }
            }
            //kiiratas
            kiFile.WriteLine(veszelyesNapok);

            for (int i = 0; i < veszelyesNapok; i++)
            {
                kiFile.WriteLine(veszelyesIdoszakok[i].ToString());
            }

            kiFile.Close();
        }
    }
}