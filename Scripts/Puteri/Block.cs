using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Efecte efecte;

    public int NrBotiCuBlock;
    public int NrBotiCuRedirect;
    public int NrBotiCupidon;
    public int[] TinteleBlock = new int[25];
    public int[] TinteleCupidon = new int[2];
    public int[] TinteleRedirect = new int[25];
    public int[] PeCineRedirect = new int[25];
    public int[] BotiiCuBlock = new int[25];
    public int[] BotiiCuRedirect = new int[25];
    public int BotulCupidon;


    public int DatTotiPuterea = 0;
    public int[] cineadatputerea = new int[25];
    public int[] CePutereAi = new int[25];
    public int[,] tt = new int[50, 50];
    public int n; // indexu la ultimul bot la rand
    public int l; // se modifica mereu e nr de boti care au vizitat botul n;

    public int[] botii = new int[25];
    public int c = 0;
    public int q;

    public int nrboti;
    public void Start()
    {
        SetariDefault();
        PuteriE();

    }
    public void Seteaza()
    {
        for (int i = 0; i < efecte.NrBotiCuBlock; i++)
        { 
            TinteleBlock[i] = efecte.TinteleBlock[i];
            BotiiCuBlock[i] = efecte.BotiiCuBlock[i];
        }
        for(int i = 0;i<efecte.NrBotiCuRedirect;i++)
        {
            TinteleRedirect[i] = efecte.TinteleRedirect[i];
            PeCineRedirect[i] = efecte.PeCineRedirect[i];
            BotiiCuRedirect[i] = efecte.BotiiCuRedirect[i];
        }
        TinteleCupidon[0] = efecte.TinteleCupidon[0];
        TinteleCupidon[1] = efecte.TinteleCupidon[1];
        NrBotiCupidon = efecte.NrBotiCuPidon;
        BotulCupidon = efecte.BotulCuPidon;
        NrBotiCuBlock = efecte.NrBotiCuBlock;
        NrBotiCuRedirect = efecte.NrBotiCuRedirect;
    }
    public void SetariDefault()
    {

        /*

bot 1 - Block ->  2
Bot 2 - Block->  3
bot 3 - Block ->6
Bot 4 -> Block -> 2
bot 8 -> block -> 5
bot 5 -> Redirect ->  8 pe  7
bot 6 -> Redirect -> 3 pe 1
bot 7 -> Cupidon -> 8  si  4

bot 7  -> antiblock
bot  3 -> antibar
        

        efecte.BotulCuPidon = 7;
        efecte.NrBotiCuPidon = 1;
        efecte.NrBotiCuBlock = 5;
        efecte.NrBotiCuRedirect = 2;

        efecte.BotiiCuBlock[0] = 1;
        efecte.BotiiCuBlock[1] = 2;
        efecte.BotiiCuBlock[2] = 3;
        efecte.BotiiCuBlock[3] = 4;
        efecte.BotiiCuBlock[4] = 8;

        efecte.BotiiCuRedirect[0] = 5;
        efecte.BotiiCuRedirect[1] = 6;

        //////////////////////////////////


        efecte.AntiBlock[6] = 1;
        efecte.AntiBAR[8] = 1;

        efecte.TinteleCupidon[0] = 8;
        efecte.TinteleCupidon[1] = 5;


        efecte.TinteleBlock[0] = 5;
        efecte.TinteleBlock[1] = 7;
        efecte.TinteleBlock[2] = 2;
        efecte.TinteleBlock[3] = 6;
        efecte.TinteleBlock[4] = 6;


        efecte.TinteleRedirect[0] = 3;
        efecte.TinteleRedirect[1] = 2;

        efecte.PeCineRedirect[0] = 4;
        efecte.PeCineRedirect[1] = 7;
        */

        ///////////////////////////////////

        efecte.NrBotiCuRedirect = 2;

        efecte.BotiiCuRedirect[0] = 5;
        efecte.BotiiCuRedirect[1] = 3;

        efecte.TinteleRedirect[0] = 1;
        efecte.TinteleRedirect[1] = 2;

        efecte.PeCineRedirect[0] = 4;
        efecte.PeCineRedirect[1] = 6;

        Seteaza();


        DatTotiPuterea = 0;

        int k = NrBotiCupidon + NrBotiCuBlock + NrBotiCuRedirect;
        nrboti = k;

        // pun toti botii in int ul botii[]
        for (int i = 0; i < k; i++) { botii[i] = -1; }
        
        for (int i = 0; i < NrBotiCuBlock; i++) 
        {
            botii[i] = BotiiCuBlock[i]; 
        }
        for (int i = NrBotiCuBlock; i < NrBotiCuRedirect + NrBotiCuBlock; i++)
        {
            botii[i] = BotiiCuRedirect[i - NrBotiCuBlock];
        }
        if(NrBotiCupidon != 0)
        {
            botii[NrBotiCuBlock + NrBotiCuRedirect] = BotulCupidon;
        }

        // pun ce puteri au botii 
        for (int y = 0; y < NrBotiCuBlock; y++)
        {
            CePutereAi[BotiiCuBlock[y]] = 1;
        }
        for (int y = 0; y < NrBotiCuRedirect; y++)
        {
            CePutereAi[BotiiCuRedirect[y]] = 2;
        }
        if(NrBotiCupidon != 0)
        {
            CePutereAi[BotulCupidon] = 3;
        }
        // 1 -> block || 2 -> redirect || 3 -> cupidon 
        q = -1;
        c = 0;
    }

    public int i;
    public void DacaAiAntiBarSauAmbeleAntiDaiPuterea()
    {
        for(int i = 0;i<nrboti;i++)
        {
            if (cineadatputerea[botii[i]] != 1)
            {
                if ((efecte.AntiBAR[botii[i]] == 1) || (efecte.AntiBlock[botii[i]] == 1 && efecte.AntiRedirect[botii[i]] == 1))
                {

                    tt[0, 0] = botii[i];
                    tt[0, 1] = -1;
                    Verifica();
                    reset();

                }
            }
        }
    }
    public void DacaEstiRedirectSiDaiRedirectLaRedirect()
    {
        // doar te pui ca si cum ai dat puterea dar n ai dat nimic de fapt.
        for(int i = 0;i<NrBotiCuRedirect;i++)
        {
            for(int j = 0;j<NrBotiCuRedirect;j++)
            {
                if(TinteleRedirect[i] == BotulCupidon)
                {
                    if (cineadatputerea[BotiiCuRedirect[i]] != 1)
                    {
                        DatTotiPuterea++;
                        cineadatputerea[BotiiCuRedirect[i]] = 1;
                        TinteleRedirect[i] = -1;
                        PeCineRedirect[i] = -1;
                    }
                    break;
                }
                if (TinteleRedirect[i] == BotiiCuRedirect[i])
                {
                    if (cineadatputerea[BotiiCuRedirect[i]] != 1)
                    {
                        DatTotiPuterea++;
                        cineadatputerea[BotiiCuRedirect[i]] = 1;
                        TinteleRedirect[i] = -1;
                        PeCineRedirect[i] = -1;
                    }
                    break;
                }
            }
        }
        // sau redirect la cupidon.
        
    }
    public void DacaEstiRCsiAiAntiBlock()
    {
        // redirect
        for(int i = 0; i<NrBotiCuRedirect;i++)
        {
            if (efecte.AntiBlock[BotiiCuRedirect[i]] == 1)
            {
                if (cineadatputerea[BotiiCuRedirect[i]] != 1)
                {
                    tt[0, 0] = BotiiCuRedirect[i];
                    tt[0, 1] = -1;
                    Verifica();
                    reset();
                }
            }
        }
        if(efecte.AntiBlock[BotulCupidon] == 1)
        {
            if(cineadatputerea[BotulCupidon] != 1)
            {
                tt[0, 0] = BotulCupidon;
                tt[0, 1] = -1;
                Verifica();
                reset();
            }
        }
    }
    public void DacaEstiBlockSiAiAntiBlockSiNuEstiRedirectionat() 
    {
        int a = 0;
        for(int i = 0;i<NrBotiCuBlock;i++)
        {
            if(efecte.AntiBlock[BotiiCuBlock[i]] == 1)
            {
                if(cineadatputerea[BotiiCuBlock[i]] != 1)
                {
                    for(int j = 0;j<NrBotiCuRedirect;j++)
                    {
                        if(TinteleRedirect[j] == BotiiCuBlock[i] && efecte.AntiRedirect[BotiiCuBlock[i]] != 1 && efecte.AntiBAR[BotiiCuBlock[i]] != 1)
                        {
                            a = 1;
                        }
                    }
                    if(a == 0)
                    {
                        tt[0, 0] = BotiiCuBlock[i];
                        tt[0, 1] = -1;
                        Verifica();
                        reset();
                    }
                }
            }
        }
    }
    
    public int VerificaDacaAmbeleTinteIsiPotDaPuterea(int a)
    {
        l = 1;
        n = 1;
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                tt[i, j] = -1;
            }
        }

        tt[0, 0] = a;

        if (efecte.AntiBAR[a] != 1 && efecte.AntiBlock[a] != 1)
        {
            VerificaDacaAiFostBlocat(0);
        }

        if (CePutereAi[a] == 1)
        {
            if (efecte.AntiBAR[a] != 1 && efecte.AntiRedirect[a] != 1)
            {
                VerificaDacaAiFostRedirectionat(0);
            }
        }
        if (efecte.AntiBAR[a] != 1 && efecte.AntiBlock[a] != 1)
        {
            VerificaDacaAfostRedirectionatBlockPeTine(0);
        }
        VerificaDacaEstiTintaLuiCupidon(0);

        if(tt[0,1] == -1)
        {
            return 1;
        } else
        {
            return 0;
        }

    }
    
    public void PuteriE()
    {
        //  SetariDefault();
        int k = nrboti;
        for(int i = 0; DatTotiPuterea < k && c < 12; i++)
        { 
        if (DatTotiPuterea < k && c < 12)
        {
            DacaEstiRedirectSiDaiRedirectLaRedirect();
            DacaEstiRCsiAiAntiBlock();
            DacaEstiBlockSiAiAntiBlockSiNuEstiRedirectionat();
            DacaAiAntiBarSauAmbeleAntiDaiPuterea();
            if (i == nrboti)
            {
                i = 0;
                c++;
            }


            if (cineadatputerea[botii[i]] == 0)
            {
                tt[0, 0] = botii[i];
                n = 1;
                for (int p = 0; p < 20; p++)
                {
                    l = 1;
                    if (p != 0)
                    {
                        if (tt[p - 1, 1] == -1)
                        {
                            break; // daca botul anterior nu a fost vizitat de nimeni.
                        }
                        if (tt[p, 0] == -1)
                        {
                            break; // daca nu exista un bot de verificat break; // inutila cred
                        }
                    }

                    if (efecte.AntiBAR[tt[p, 0]] != 1 && efecte.AntiBlock[tt[p, 0]] != 1)
                    {
                        VerificaDacaAiFostBlocat(p);
                    }

                    if (CePutereAi[tt[p, 0]] == 1)
                    {
                        if (efecte.AntiBAR[tt[p, 0]] != 1 && efecte.AntiRedirect[tt[p, 0]] != 1)
                        {
                            VerificaDacaAiFostRedirectionat(p);
                        }
                    }
                    if (efecte.AntiBAR[tt[p, 0]] != 1 && efecte.AntiBlock[tt[p, 0]] != 1)
                    {
                        VerificaDacaAfostRedirectionatBlockPeTine(p);
                    }
                    VerificaDacaEstiTintaLuiCupidon(p);

                }

            }
            Verifica();
            reset();

        }
    }
    }
    public void reset()
    {
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                tt[i, j] = -1;
            }
        }
        q = -1;

    }
    public void Verifica()
    {
        
        for(int i = 0;i<20 && tt[i,0] != -1;i++)
        {
            if(tt[i,1] == -1)
            {
                q = tt[i, 0];
                break;
            }
        }
        if(q != -1 && cineadatputerea[BotulCupidon] == 1)
        {
            if (q == efecte.TinteleCupidon[0])
            {  
                if(VerificaDacaAmbeleTinteIsiPotDaPuterea(efecte.TinteleCupidon[1]) !=  1)
                {
                    q = -1;
                }
            }
            if(q == efecte.TinteleCupidon[1])
            {
                if(VerificaDacaAmbeleTinteIsiPotDaPuterea(efecte.TinteleCupidon[0]) != 1)
                {
                    q = -1;
                }
            }
        }
        
        
        if (q != -1)
        {
            Debug.Log("Q: " + q);

            //verific daca botul care urmeaza sa dea puterea e cu block;
            for (int j = 0; j < NrBotiCuBlock; j++)
            {
                if (BotiiCuBlock[j] == q)
                {
                    if (cineadatputerea[q] != 1)
                    {
                        if (efecte.AntiBlock[TinteleBlock[j]] != 1 && efecte.AntiBAR[TinteleBlock[j]] != 1)
                        {
                            if (((TinteleBlock[j] != TinteleCupidon[0] || (efecte.AntiBlock[TinteleCupidon[1]] != 1 && efecte.AntiBAR[TinteleCupidon[1]] != 1)) && ((TinteleBlock[j] != TinteleCupidon[1]) || (efecte.AntiBlock[TinteleCupidon[0]] != 1 && efecte.AntiBAR[TinteleCupidon[0]] != 1))) || (TinteleBlock[j] == BotulCupidon && efecte.AntiBAR[BotulCupidon] != 1 && efecte.AntiBlock[BotulCupidon] != 1)/* fie tinta pe care dai block desi e tinta lui cupidon e chiar cupidon */ )
                            {
                                if (efecte.AntiBlock[TinteleBlock[j]] != 1 && efecte.AntiBAR[TinteleBlock[j]] != 1)
                                {
                                    efecte.EfectBlocat[TinteleBlock[j]] = 1;

                                    //  daca tinta blocata are putere de block ii schimb tinta si il pun ca si cum a dat puterea.
                                    for (int e = 0; e < NrBotiCuBlock; e++)
                                    {
                                        if (TinteleBlock[j] == BotiiCuBlock[e])
                                        {
                                            if (cineadatputerea[BotiiCuBlock[e]] != 1)
                                            {
                                                DatTotiPuterea++;
                                                cineadatputerea[BotiiCuBlock[e]] = 1;
                                                TinteleBlock[e] = -1;
                                            }
                                            break;
                                        }
                                    }
                                    // la fel dar cu redirect;
                                    for (int e = 0; e < NrBotiCuRedirect; e++)
                                    {
                                        if (TinteleBlock[j] == BotiiCuRedirect[e])
                                        {
                                            if (cineadatputerea[BotiiCuRedirect[e]] != 1)
                                            {
                                                DatTotiPuterea++;
                                                cineadatputerea[BotiiCuRedirect[e]] = 1;
                                                PeCineRedirect[e] = -1;
                                                TinteleRedirect[e] = -1;
                                            }
                                            break;
                                        }
                                    }
                                    // la fel cu cupidon;
                                    if (TinteleBlock[j] == BotulCupidon)
                                    {
                                        if (cineadatputerea[BotulCupidon] != 1)
                                        {
                                            DatTotiPuterea++;
                                            cineadatputerea[BotulCupidon] = 1;
                                            TinteleCupidon[0] = -1;
                                            TinteleCupidon[1] = -1;
                                        }
                                    }

                                }
                                if (cineadatputerea[q] != 1)
                                {
                                    DatTotiPuterea++;
                                    cineadatputerea[q] = 1;
                                    TinteleBlock[j] = -1;
                                }
                                break;
                            }
                        }
                        else
                        {
                            int a = 0;
                            for (int i = 0; i < NrBotiCuRedirect; i++)
                            {
                                if (q == TinteleRedirect[i])
                                {
                                    if (efecte.AntiBAR[q] != 1 && efecte.AntiRedirect[q] != 1)
                                    {
                                        a = 1;
                                    }
                                }
                            }
                            if (a == 0)
                            {
                                DatTotiPuterea++;
                                cineadatputerea[q] = 1;
                                TinteleBlock[j] = -1;
                                a = 0;
                            }
                        }
                    }
                }
            }
            // verific daca botul care urmeaza sa dea putere e cu redirect;
            for (int j = 0; j < NrBotiCuRedirect; j++)
            {
                if (BotiiCuRedirect[j] == q)
                {
                    if (efecte.AntiRedirect[TinteleRedirect[j]] != 1 && efecte.AntiBAR[TinteleRedirect[j]] != 1)
                    {
                        if (((TinteleRedirect[j] != TinteleCupidon[0]) || efecte.AntiRedirect[TinteleCupidon[1]] != 1 && efecte.AntiBAR[TinteleCupidon[1]] != 1) && ((TinteleRedirect[j] != TinteleCupidon[1]) || efecte.AntiRedirect[TinteleCupidon[0]] != 1 && efecte.AntiBAR[TinteleCupidon[0]] != 1))
                        {

                            // j -> tinta pe care dau redirect
                            // 
                            efecte.EfectRedirect[TinteleRedirect[j]] = 1;
                            efecte.ePeCineRedirect[TinteleRedirect[j]] = PeCineRedirect[j];



                            if (cineadatputerea[BotiiCuRedirect[j]] != 1)
                            {
                                DatTotiPuterea++;
                                cineadatputerea[BotiiCuRedirect[j]] = 1;
                            }

                            for (int e = 0; e < NrBotiCuBlock; e++)
                            {
                                if (TinteleRedirect[j] == BotiiCuBlock[e])
                                {
                                    // nu i poti schimba tinta daca e blocat;
                                    if (efecte.EfectBlocat[BotiiCuBlock[e]] != 1)
                                    {
                                        TinteleBlock[e] = PeCineRedirect[j];
                                    }
                                    break;
                                }
                            }
                            TinteleRedirect[j] = -1;
                            PeCineRedirect[j] = -1;
                        }
                    } else
                    {
                        if (cineadatputerea[BotiiCuRedirect[j]] != 1)
                        {
                            DatTotiPuterea++;
                            cineadatputerea[BotiiCuRedirect[j]] = 1;
                            TinteleRedirect[j] = -1;
                            PeCineRedirect[j] = -1;
                        }
                    }
                }
            }
            // verific daca botul care urmeaza sa dea puterea e cupidon
            if (q == BotulCupidon)
            {
                if (cineadatputerea[BotulCupidon] != 1)
                {
                    efecte.Legati[0] = TinteleCupidon[0];
                    efecte.Legati[1] = TinteleCupidon[1];

                    VerificaTinteleDeAntiBAR();
                    DatTotiPuterea++;
                    cineadatputerea[BotulCupidon] = 1;

                    TinteleCupidon[0] = -1;
                    TinteleCupidon[1] = -1;
                }
            }
        }

        // daca deja am mers prin toti botii de 10 ori si mai sunt boti care n au daat puterea si nu o pot da => ciclu infinit toti blocati.
        if (c >= 10)
        {
            for (int i = 0; i < nrboti; i++)
            {
                if (cineadatputerea[botii[i]] != 1)
                {
                    efecte.EfectBlocat[botii[i]] = 1;
                    cineadatputerea[botii[i]] = 1;

                }
            }
        }

        VerificaTinteCupidon();
    }


    public int VerificaDacaEstiBlock(int b)
    {
        for(int i = 0;i<NrBotiCuBlock;i++)
        {
            if(BotiiCuBlock[i] == b)
            {
                return 1;
            }
        }
        return 0;
    }

    public int VerificaDacaEstiRedirect(int b)
    {
        for(int i = 0;i<NrBotiCuRedirect;i++)
        {
            if(BotiiCuRedirect[i] == b)
            {
                return 1;
            }
        }
        return 0;
    }

    public void VerificaTinteleDeAntiBAR()
    {

        if (efecte.AntiBlock[TinteleCupidon[0]] == 1)
        {
            efecte.AntiBlock[TinteleCupidon[1]] = 1;
        }
        if (efecte.AntiBlock[TinteleCupidon[1]] == 1)
        {
            efecte.AntiBlock[TinteleCupidon[0]] = 1;
        }
        if (efecte.EfectRedirect[TinteleCupidon[0]] == 1)
        {
            efecte.AntiRedirect[TinteleCupidon[1]] = 1;
        }
        if(efecte.AntiRedirect[TinteleCupidon[1]] == 1)
        {
            efecte.AntiRedirect[TinteleCupidon[0]] = 1;
        }
        if (efecte.AntiBAR[TinteleCupidon[0]] == 1)
        {
            efecte.AntiBAR[TinteleCupidon[1]] = 1;
        }
        if (efecte.AntiBAR[TinteleCupidon[1]] == 1)
        {
            efecte.AntiBAR[TinteleCupidon[0]] = 1;
        }

        //anti block;
        // daca prima tinta are antiblock
        if (efecte.AntiBlock[TinteleCupidon[1]] == 1 || efecte.AntiBAR[TinteleCupidon[1]] == 1)
        {
            // daca a 2 a tinta e block si e blocata
            if(VerificaDacaEstiBlock(TinteleCupidon[1]) == 1 && efecte.EfectBlocat[TinteleCupidon[1]] == 1)
            {
                // daca are redirect dar e si blocat ii schimbi tinta pe cea de la  redirect.
                if(efecte.EfectRedirect[TinteleCupidon[1]] == 1)
                {
                    if (efecte.AntiRedirect[TinteleCupidon[1]] == 1 || efecte.AntiBAR[TinteleCupidon[1]] == 1)
                    {
                        TinteleBlock[CIB(TinteleCupidon[1])] = efecte.TinteleBlock[CIB(TinteleCupidon[1])];
                    }
                    else
                    {
                        TinteleBlock[CIB(TinteleCupidon[1])] = efecte.PeCineRedirect[CIR(TinteleCupidon[1])];
                    }
                }
                // pui ca nu e blocata a 2 a tinta si ii pui tinta veche si etc.
                else // daca nu e redierctionat, dar totusi e blocat si e block.
                {
                        TinteleBlock[CIB(TinteleCupidon[1])] = efecte.TinteleBlock[CIB(TinteleCupidon[1])];
                }
                DatTotiPuterea--;
                cineadatputerea[TinteleCupidon[1]] = 0;
            }
            // daca esti redirect si esti blocat
            if (VerificaDacaEstiRedirect(TinteleCupidon[1]) == 1 && efecte.EfectBlocat[TinteleCupidon[1]] == 1)
            {
                // nu mai esti blocat si ai iar tintele normale.
                TinteleRedirect[CIR(TinteleCupidon[1])] = efecte.TinteleRedirect[CIR(TinteleCupidon[1])];
                PeCineRedirect[CIR(TinteleCupidon[1])] = efecte.PeCineRedirect[CIR(TinteleCupidon[1])];
                DatTotiPuterea--;
                cineadatputerea[TinteleCupidon[1]] = 0;
            }
            efecte.EfectBlocat[TinteleCupidon[1]] = 0;
        }
        if (efecte.AntiBlock[TinteleCupidon[0]] == 1 || efecte.AntiBAR[TinteleCupidon[0]] == 1)
        {
            // daca a 2 a tinta e block si e blocata
            if (VerificaDacaEstiBlock(TinteleCupidon[0]) == 1 && efecte.EfectBlocat[TinteleCupidon[0]] == 1)
            {
                // daca are redirect dar e si blocat ii schimbi tinta pe cea de la  redirect.
                if (efecte.EfectRedirect[TinteleCupidon[0]] == 1)
                {
                    if (efecte.AntiRedirect[TinteleCupidon[0]] == 1 || efecte.AntiBAR[TinteleCupidon[0]] == 1)
                    {
                        TinteleBlock[CIB(TinteleCupidon[0])] = efecte.TinteleBlock[CIB(TinteleCupidon[0])];
                    }
                    else
                    {
                        TinteleBlock[CIB(TinteleCupidon[0])] = efecte.PeCineRedirect[CIR(TinteleCupidon[0])];
                    }
                }
                // pui ca nu e blocata a 2 a tinta si ii pui tinta veche si etc.
                else
                {
                    TinteleBlock[CIB(TinteleCupidon[0])] = efecte.TinteleBlock[CIB(TinteleCupidon[0])];
                }
                DatTotiPuterea--;
                cineadatputerea[TinteleCupidon[0]] = 0;
            }
            // daca esti redirect si esti blocat
            if (VerificaDacaEstiRedirect(TinteleCupidon[0]) == 1 && efecte.EfectBlocat[TinteleCupidon[0]] == 1)
            {
                // nu mai esti blocat si ai iar tintele normale.
                TinteleRedirect[CIR(TinteleCupidon[0])] = efecte.TinteleRedirect[CIR(TinteleCupidon[0])];
                PeCineRedirect[CIR(TinteleCupidon[0])] = efecte.PeCineRedirect[CIR(TinteleCupidon[0])];
                DatTotiPuterea--;
                cineadatputerea[TinteleCupidon[0]] = 0;
            }
            efecte.EfectBlocat[TinteleCupidon[0]] = 0;

        }
        
        
        if (efecte.AntiRedirect[TinteleCupidon[0]] == 1 || efecte.AntiBAR[TinteleCupidon[0]] == 1)
        {
            if (VerificaDacaEstiBlock(TinteleCupidon[0]) == 1 &&  efecte.EfectRedirect[TinteleCupidon[0]] == 1 && efecte.EfectBlocat[TinteleCupidon[0]] == 0)
            {
                TinteleBlock[TinteleCupidon[0]] = efecte.TinteleBlock[TinteleCupidon[0]];
            }

            if (efecte.EfectRedirect[TinteleCupidon[0]] == 1)
            {
                efecte.EfectRedirect[TinteleCupidon[0]] = 0;
            }
        }
        if (efecte.AntiRedirect[TinteleCupidon[1]] == 1 || efecte.AntiBAR[TinteleCupidon[1]] == 1)
        {
            if (VerificaDacaEstiBlock(TinteleCupidon[1]) == 1 && efecte.EfectRedirect[TinteleCupidon[1]] == 1 && efecte.EfectBlocat[TinteleCupidon[1]] == 0)
            {
                TinteleBlock[TinteleCupidon[1]] = efecte.TinteleBlock[TinteleCupidon[1]];
            }

            if (efecte.EfectRedirect[TinteleCupidon[1]] == 1)
            {
                efecte.EfectRedirect[TinteleCupidon[1]] = 0;
            }
        }
    }
    public void VerificaDacaAiFostBlocat(int p)
    {
        for (int i = 0; i < NrBotiCuBlock; i++) // iau fiecare bot cu block pe rand
        {
            if (tt[p, 0] == TinteleBlock[i]) // si verific daca a dat pe mine. 
            {
                tt[p, l] = BotiiCuBlock[i]; // il pun in tablou 
                tt[n, 0] = BotiiCuBlock[i]; // il pun in tablou la verificat
                l++;
                n++;
            }
        }
    }

    public void VerificaDacaAiFostRedirectionat(int p)
    {
            for (int i = 0; i < NrBotiCuRedirect; i++)
            {
                if (tt[p, 0] == TinteleRedirect[i])
                {
                    tt[p, l] = BotiiCuRedirect[i];
                    tt[n, 0] = BotiiCuRedirect[i];
                    n++;
                    l++;
                }
            }
    }
    public void VerificaDacaAfostRedirectionatBlockPeTine(int p)
    {
        for (int i = 0; i < NrBotiCuRedirect; i++)
        {
            if (tt[p, 0] == PeCineRedirect[i])
            {
                for (int j = 0; j < NrBotiCuBlock; j++)
                {
                    if (TinteleRedirect[i] == BotiiCuBlock[j])
                    {
                        if (cineadatputerea[BotiiCuBlock[j]] != 1)
                        {
                            // salvez botul care a dat redirect la block
                            tt[p, l] = BotiiCuRedirect[i];
                            tt[n, 0] = BotiiCuRedirect[i];
                            l++;
                            n++;
                            // salvez botul cu block
                            tt[p, l] = BotiiCuBlock[j];
                            tt[n, 0] = BotiiCuBlock[j];
                            l++;
                            n++;
                        }
                    }
                }
            }
        }
    }
    public void VerificaDacaEstiTintaLuiCupidon(int p)
    {
        if (NrBotiCupidon != 0)
        {
            if (tt[p, 0] == TinteleCupidon[0] || tt[p, 0] == TinteleCupidon[1])
            {
                // daca tintele lui cupidon nu s  blocate sau e un block redirectionat pe tintele lui cupidon, tintele isi dau puterea
                if (RCB(TinteleCupidon[0]) == 0 && RCB(TinteleCupidon[1]) == 0)
                {
                    if (CePutereAi[TinteleCupidon[0]] == 1 || CePutereAi[TinteleCupidon[0]] == 2)
                    {
                        tt[n, 0] = TinteleCupidon[0];
                    }
                    if (CePutereAi[TinteleCupidon[1]] == 1 || CePutereAi[TinteleCupidon[1]] == 2)
                    {
                        tt[n + 1, 0] = TinteleCupidon[1];
                    }
                }
                else
                {
                    tt[p, l] = BotulCupidon;
                    tt[n, 0] = BotulCupidon;
                    l++;
                    n++;
                }
            }
        }
    }

    public void VerificaTinteCupidon()
    {
        //// adaug efectele de block/redirect la ambii boti daca unul dintre ei il au.
        if(NrBotiCupidon != 0)
        {
            if(cineadatputerea[BotulCupidon] == 1 && efecte.EfectBlocat[BotulCupidon] != 1)
            {
                //verific daca o tinta 0 e blocata si daca tinta blocata e blocata si daca cealalta tinta e block sau redirect le schimb puterile in nule si le pun ca au dat puterea
                if(efecte.EfectBlocat[efecte.TinteleCupidon[0]] == 1)
                {
                    
                    if(efecte.EfectBlocat[efecte.TinteleCupidon[1]] != 1)
                    {
                        efecte.EfectBlocat[efecte.TinteleCupidon[1]] = 1;
                        for (int i = 0; i < NrBotiCuBlock; i++)
                        {
                            if (efecte.TinteleCupidon[1] == BotiiCuBlock[i])
                            {
                                if (cineadatputerea[BotiiCuBlock[i]] != 1)
                                {
                                    DatTotiPuterea++;
                                    TinteleBlock[i] = -1;
                                    cineadatputerea[BotiiCuBlock[i]] = 1;
                                }
                                break;
                            }
                        }
                        for(int i = 0;i<NrBotiCuRedirect;i++)
                        {
                            if(efecte.TinteleCupidon[1] == BotiiCuRedirect[i])
                            {
                                if (cineadatputerea[efecte.TinteleCupidon[1]] != 1)
                                {
                                    DatTotiPuterea++;
                                    cineadatputerea[efecte.TinteleCupidon[1]] = 1;
                                    TinteleRedirect[BotiiCuRedirect[i]] = -1;
                                    PeCineRedirect[BotiiCuRedirect[i]] = -1;
                                }
                                break;
                            }
                        }
                    }
                }
                // la fel cu tinta 1
                if(efecte.EfectBlocat[efecte.TinteleCupidon[1]] == 1)
                {
                    if (efecte.EfectBlocat[efecte.TinteleCupidon[0]] != 1)
                    {
                        efecte.EfectBlocat[efecte.TinteleCupidon[0]] = 1;
                        for (int i = 0; i < NrBotiCuBlock; i++)
                        {
                            if (efecte.TinteleCupidon[0] == BotiiCuBlock[i])
                            {
                                if (cineadatputerea[BotiiCuBlock[i]] != 1)
                                {
                                    DatTotiPuterea++;
                                    TinteleBlock[i] = -1;
                                    cineadatputerea[BotiiCuBlock[i]] = 1;
                                }
                            }
                        }
                        for (int i = 0; i < NrBotiCuRedirect; i++)
                        {
                            if (efecte.TinteleCupidon[0] == BotiiCuRedirect[i])
                            {
                                if (cineadatputerea[efecte.TinteleCupidon[0]] != 1)
                                {
                                    DatTotiPuterea++;
                                    cineadatputerea[efecte.TinteleCupidon[0]] = 1;
                                    TinteleRedirect[BotiiCuRedirect[i]] = -1;
                                    PeCineRedirect[BotiiCuRedirect[i]] = -1;
                                }
                                break;
                            }
                        }
                    }
                }
                // verific daca tinta 0 e redirectionata si daca cel redirectionat e block(si daca nu e blocat sa i schimb tinta)si pun cealalta tinta ca fiind redirectionata
                if(efecte.EfectRedirect[efecte.TinteleCupidon[0]] == 1)
                {
                    // daca tinta 1 nu e deja redirectionata
                    if (efecte.EfectRedirect[efecte.TinteleCupidon[1]] != 1)
                    {
                        //o redirectionez.

                        // daca tinta 0 a lui cupidon e redirectionata o pun si pe tinta2 fiind redirectionata.
                        efecte.EfectRedirect[efecte.TinteleCupidon[1]] = 1;


                        efecte.ePeCineRedirect[efecte.TinteleCupidon[1]] = efecte.ePeCineRedirect[efecte.TinteleCupidon[0]];


                        // daca nu esti blocat
                        if (efecte.EfectBlocat[efecte.TinteleCupidon[1]]  != 1)
                        {
                            for(int i = 0;i<NrBotiCuBlock;i++)
                            {
                                // daca esti block
                                if(efecte.TinteleCupidon[1] == BotiiCuBlock[i])
                                {
                                    for(int j = 0;j<NrBotiCuRedirect;j++)
                                    {
                                        // afli pe cine ai primit redirect;
                                        if(cineadatputerea[BotiiCuRedirect[j]] == 1 && efecte.TinteleRedirect[j] == efecte.TinteleCupidon[1] && efecte.EfectBlocat[BotiiCuRedirect[j]] != 1)
                                        {
                                            TinteleBlock[i] = efecte.PeCineRedirect[j];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // la fel cu tinta 1;
                if (efecte.EfectRedirect[efecte.TinteleCupidon[1]] == 1)
                {
                    if (efecte.EfectRedirect[efecte.TinteleCupidon[0]] != 1)
                    {
                        efecte.EfectRedirect[efecte.TinteleCupidon[0]] = 1;
                        
                        efecte.ePeCineRedirect[efecte.TinteleCupidon[0]] = efecte.ePeCineRedirect[efecte.TinteleCupidon[1]];


                        if (efecte.EfectBlocat[efecte.TinteleCupidon[0]] != 1)
                        {
                            for (int i = 0; i < NrBotiCuBlock; i++)
                            {
                                if (efecte.TinteleCupidon[1] == BotiiCuBlock[i])
                                {
                                    for (int j = 0; j < NrBotiCuRedirect; j++)
                                    {
                                        // afli pe cine ai primit redirect;
                                        if (cineadatputerea[BotiiCuRedirect[j]] == 1 && efecte.TinteleRedirect[j] == efecte.TinteleCupidon[0] && efecte.EfectBlocat[BotiiCuRedirect[j]] != 1)
                                        {
                                            TinteleBlock[i] = efecte.PeCineRedirect[j];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public int RCB(int p)
    {
        // daca iei block;
        for (int i = 0; i < NrBotiCuBlock; i++) // iau fiecare bot cu block pe rand
        {
            if (p == TinteleBlock[i]) // si verific daca a dat pe mine. 
            {
                return 1;
            }
        }
        
        //redirect la un block pe p; si daca iei redirect;
    
            for (int i = 0; i < NrBotiCuRedirect; i++)
            {
                if (CePutereAi[p] == 1)
                {
                    if (p == TinteleRedirect[i])
                    {
                        return 1;
                    }
                }
                if (p == PeCineRedirect[i])
                {
                    for (int j = 0; j < NrBotiCuBlock; j++)
                    {
                        if (BotiiCuBlock[j] == TinteleRedirect[i])
                        {
                            return 1;
                        }
                    }
                }
            }

        return 0;

    }

    public int CIB(int p)
    {
        for(int i = 0;i<NrBotiCuBlock;i++)
        {
            if(BotiiCuBlock[i] == p)
            {
                return i;
            }
        }

        return -1;
    }
    public int CIR(int p) // cauta index redirect
    {
        for (int i = 0; i < NrBotiCuRedirect; i++)
        {
            if (BotiiCuRedirect[i] == p)
            {
                return i;
            }
        }
        return -1;
    }
}

