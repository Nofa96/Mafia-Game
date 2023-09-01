using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efecte : MonoBehaviour
{   
    public int Runda;
    public int[] Categorie = new int[25];
    public int[] Ordinea = new int[25];
    public int NrBoti;
    public int NrBotiVii;
    public int NrBotiMorti;
    public int NrLupi;
    public int NrTowni;

    

    public int[] EfectMort = new int[25];
    public int[] EfectMortRundaAsta = new int[25]; // reset 0

    public int[] CeFragAi = new int[3];
    public int[] CePutereRandomAi = new int[10]; // doar pt jucator. 
    public int[] CeItemAi = new int[10]; // doar pt jucator 
    // basic adica abilitatile.
    public int[,] CePutereBasicAi = new int[25, 25]; // [botul,index] = indexulputerii          botul 0 - 0(prima putere)  are  puterea 4 // poti sa ai si 3 4 5 puteri basice
    public int[,] CePutereBasicAiRundaAsta = new int[25, 25];
    public int[,] TinteleBotilor1 = new int[25, 25]; // botul si indexu puterii(orice putere)
    public int[,] TinteleBotilor2 = new int[25, 25]; // tintele(cred ca indexii voiam sa zic) sunt in corelatie cu ceputerebasic ai de ex: ceputerebasicai[i,j] tinta acestei puteri trebuie sa se afle la indexul tintelebotilor1/2[i,j] acelasi i si j gen

    public int[,] TinteleBotilorRandom1 = new int[25, 25];
    public int[,] TinteleBotilorRandom2 = new int[25, 25];

    public int[,] TinteleBotilorIteme1 = new int[25, 25];
    public int[,] TinteleBotilorIteme2 = new int[25, 25];



    public int[] EfectDeflect;
    public int[] EfectReflect; // botul care a primit reflect EfectReflect[tinta1] = 1
    public int[] PeCineReflect; // pe cine a primit reflect PeCineReflect[tinta1] = tinta2

    public int GenjutsuIndex = 46;
    public int ProtectIndex = 10;
    public int KillIndex = 55;
    public int DRidKillIndex = 16;


    public int[] EfectGuardian = new int[25]; // [botul] = 0/1
    public int[,] CineAdatGuardianEfect = new int[25, 25]; // tinta si index = botul cu guard// reset pe -1;

    public int[] EfectProtect = new int[25]; // 0 / 1
    public int[] CateVietiAi = new int[25]; // [botul] 0/1...n
    public int[] RolulBotilor = new int[25]; // [botul] = index rol

    public int[] DRidKillRatat = new int[25]; // nu se reseteaza


    public int[] TinteleHidRid = new int[25]; // tintele se pastreaza, doar se mai adauga.  // se reseteaza doar la inceput pe -1 si atat;
    public int[] RolurileTintelor = new int[25]; // mereu le dai din nou daca e nevoie // se reseteaza pe -1;

    public int[] EfectPoison = new int[25]; // [botul] = runda in care a primit;

    public int[,] FollowSpyE = new int[25, 25]; // [tinta, index liber] = botul pe care a vizitat;
    public int[,] TargetSpyE = new int[25, 25]; // [tinta, index liber] = botul care te a vizitat;


    public int[,] CePutereFuri = new int[25, 25]; // [botul,indexliber] = indexulputerii furate;
    

    public int[,] CePutereCopiezi = new int[25, 25]; // [botul,indexliber] = indexulputerii copiate;


    public int[,] CePutereDuplicate = new int[25, 25]; // [botul, indexliber] = indexputerea duplicata // la reset ii dai puterea la ceputbasicrunda[] si pui CePutereDuplicate[b,index] = -1;


    public int GhostlyWhisperC;


    public int[,] PutereSensorialCopiata = new int[25, 25]; // [botul, indexliber] = puterea copiata;




    // Efecte pe 0 si 1 || legati pe x y botii legati.
    public int[] EfectBlocat = new int[25];
    public int[] EfectRedirect = new int[25];
    public int[] Legati = new int[2];
    // Efecte pe 0 si 1
    public int[] AntiBlock = new int[25];
    public int[] AntiRedirect = new int[25];
    public int[] AntiBAR = new int[25];
    // Nr
    public int NrBotiCuBlock;
    public int NrBotiCuRedirect;
    public int NrBotiCuPidon;
    // Tintele
    public int[] TinteleBlock = new int[25];
    public int[] TinteleRedirect = new int[25];
    public int[] PeCineRedirect = new int[25];
    public int[] ePeCineRedirect = new int[25];
    public int[] TinteleCupidon = new int[2];
    // Botii
    public int[] BotiiCuBlock = new int[25];
    public int[] BotiiCuRedirect = new int[25];
    public int BotulCuPidon;
    


    public void ResetCumArVeni() // DUPA CE SE TERMINA RUNDA
    {
        for(int i = 0;i<EfectPoison.Length;i++)
        {
            if(Runda - EfectPoison[i] >= 2 && EfectPoison[i] != 0)
            {
                EfectMortRundaAsta[i] = 1;
                EfectPoison[i] = 0;
            }
        }    
        // Botii
        NrTowni = 0;
        NrLupi = 0;
        for (int i = 0; i < NrBoti; i++)
        {
            if (EfectMortRundaAsta[i] == 1)
            {
                EfectMort[i] = 1;
                NrBotiMorti++;
            } else
            {
                if (Categorie[i] == 0)
                {
                    NrTowni++;
                }
                else NrLupi++;
            }
        }
        NrBotiVii = NrBoti - NrBotiMorti;

        //  TINTELE SI PUTERILE
        for (int i = 0; i< TinteleBotilor1.GetLength(0); i++)
        {
            for (int j = 0; j < TinteleBotilor1.GetLength(1); j++)
            {
                TinteleBotilor1[i, j] = -1;
                TinteleBotilor2[i, j] = -1;
                CePutereBasicAiRundaAsta[i, j] = CePutereBasicAi[i, j];
            }
        }

        // --------------------- PUTERILE --------------------- //

        // BLOCK REDIRECT CUPIDON
        for(int i = 0;i<EfectBlocat.Length;i++)
        {
            EfectBlocat[i] = 0;
            EfectRedirect[i] = 0;
            AntiBlock[i] = 0;
            AntiRedirect[i] = 0;
            AntiBAR[i] = 0;
            TinteleBlock[i] = -1;
            TinteleRedirect[i] = -1;
            PeCineRedirect[i] = -1;
            BotiiCuBlock[i] = -1;
            BotiiCuRedirect[i] = -1;
            ePeCineRedirect[i] = -1;
        }
        { 
            Legati[0] = -1;
            Legati[1] = -1;
            NrBotiCuBlock = 0;
            NrBotiCuPidon = 0;
            NrBotiCuRedirect = 0;
            TinteleCupidon[0] = -1;
            TinteleCupidon[1] = -1;
        }

        // DEFLECT REFLECT 
        for (int i = 0;i<EfectDeflect.Length;i++)
        {
            EfectDeflect[i] = 0;
            EfectReflect[i] = 0;
            PeCineReflect[i] = -1;
        }

        // GUARDIAN
        for(int i = 0;i<EfectGuardian.Length;i++)
        {
            EfectGuardian[i] = 0;
            for(int j = 0;j<EfectGuardian.Length;j++)
            {
                CineAdatGuardianEfect[i, j] = -1;
            }
        }

        // PROTECT
        for(int i = 0;i<EfectProtect.Length;i++)
        {
            EfectProtect[i] = 0;
        }
        
        // HIDRID
        for(int i = 0;i< RolurileTintelor.Length;i++)
        {
            RolurileTintelor[i] = -1;
        }

        // FOLLOW TARGET SPY
        for(int i = 0;i<FollowSpyE.GetLength(0);i++)
        {
            for(int j = 0; j<FollowSpyE.GetLength(1); j++)
            {
                FollowSpyE[i, j] = -1;
                TargetSpyE[i, j] = -1;
            }
        }
        
        // ROGUE || COPY || DUPLICATE || SENSORIAL
        for(int i =0;i< CePutereFuri.GetLength(0); i++)
        {
            for(int j = 0;j< CePutereFuri.GetLength(1); j++)
            {
                if (CePutereFuri[i, j] != -1)
                {
                    for (int l = 2; l < CePutereBasicAiRundaAsta.GetLength(1); l++)
                    {
                        if (CePutereBasicAiRundaAsta[i, l] == -1)
                        {
                            CePutereBasicAiRundaAsta[i, l] = CePutereFuri[i, j];
                            break;
                        }
                    }
                }
                if(CePutereCopiezi[i,j] != -1)
                {
                    for (int l = 2; l < Mathf.Sqrt(CePutereBasicAiRundaAsta.Length); l++)
                    {
                        if (CePutereBasicAiRundaAsta[i, l] == -1)
                        {
                            CePutereBasicAiRundaAsta[i, l] = CePutereCopiezi[i, j];
                            break;
                        }
                    }
                }
                if(CePutereDuplicate[i,j] != -1)
                {
                    for (int l = 2; l < Mathf.Sqrt(CePutereBasicAiRundaAsta.Length); l++)
                    {
                        if (CePutereBasicAiRundaAsta[i, l] == -1)
                        {   
                            CePutereBasicAiRundaAsta[i, l] = CePutereDuplicate[i, j];
                            break;
                        }
                    }
                }
                if(PutereSensorialCopiata[i,j] != -1)
                {
                    for (int l = 2; l < Mathf.Sqrt(CePutereBasicAiRundaAsta.Length); l++)
                    {
                        if (CePutereBasicAiRundaAsta[i, l] == -1)
                        {
                            CePutereBasicAiRundaAsta[i, l] = PutereSensorialCopiata[i, j];
                            break;
                        }
                    }
                }


                CePutereFuri[i, j] = -1;

                CePutereCopiezi[i, j] = -1;

                CePutereDuplicate[i, j] = -1;

                PutereSensorialCopiata[i, j] = -1;
            }
        }

    }



    // basic adica abilitatile.

    public void DefaultSet()
    {
        Runda = 0;
        for(int i = 0;i<EfectMort.Length;i++)
        {
            EfectMort[i] = 0;
            EfectMortRundaAsta[i] = 0;
        }
        for(int i = 0;i<CePutereRandomAi.Length;i++)
        {
            CePutereRandomAi[i] = -1;
            CeItemAi[i] = -1;
        }
        for(int i = 0;i<CeFragAi.Length;i++)
        {
            CeFragAi[i] = -1;
        }

        for(int i = 0;i<CePutereBasicAi.GetLength(0);i++)
        {
            for(int j = 0;j<CePutereBasicAi.GetLength(1);j++)
            {
                CePutereBasicAi[i, j] = -1;
                CePutereBasicAiRundaAsta[i, j] = -1;
                TinteleBotilor1[i, j] = -1;
                TinteleBotilor2[i, j] = -1;
                TinteleBotilorRandom1[i, j] = -1;
                TinteleBotilorRandom2[i, j] = -1;
                TinteleBotilorIteme1[i, j] = -1;
                TinteleBotilorIteme2[i, j] = -1;

            }
        }
        for(int i = 0;i<EfectDeflect.Length;i++)
        {
            EfectDeflect[i] = 0;
            EfectReflect[i] = 0;
            PeCineReflect[i] = -1;

            EfectGuardian[i] = 0;
            for(int j = 0;j<CineAdatGuardianEfect.GetLength(1);j++)
            {
                CineAdatGuardianEfect[i, j] = -1;
            }

            EfectProtect[i] = 1;
            CateVietiAi[i] = 1;

            RolulBotilor[i] = -1;

            DRidKillRatat[i] = 0;

            TinteleHidRid[i] = -1;

            RolurileTintelor[i] = -1;

            EfectPoison[i] = 0;

            for(int j = 0;j<25;j++)
            {
                FollowSpyE[i, j] = -1;
                TargetSpyE[i, j] = -1;
                CePutereFuri[i, j] = -1;
                CePutereCopiezi[i, j] = -1;
                CePutereDuplicate[i, j] = -1;
                PutereSensorialCopiata[i, j] = -1;

            }
        }
        GhostlyWhisperC = 0;

        Legati[0] = -1;
        Legati[1] = -1;

        NrBotiCuBlock = 0;
        NrBotiCuPidon = 0;
        NrBotiCuRedirect = 0;
        TinteleCupidon[0] = -1;
        TinteleCupidon[1] = -1;
        BotulCuPidon = -1;
        for (int i = 0;i<25;i++)
        {
            EfectBlocat[i] = 0;
            EfectRedirect[i] = 0;
            AntiBlock[i] = 0;
            AntiRedirect[i] = 0;
            AntiBAR[i] = 0;
            TinteleBlock[i] = -1;
            TinteleRedirect[i] = -1;
            PeCineRedirect[i] = -1;
            BotiiCuBlock[i] = -1;
            BotiiCuRedirect[i] = -1;
            ePeCineRedirect[i] = -1;
        }

    }




    public int BasicVerificare(int botul)
    {



        return 0;
    }



    // Primesti o putere de spy in fiecare runda. 
}
