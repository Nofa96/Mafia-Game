using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public Efecte efecte;


    public void Comun()
    {

    }

    public void DeflectPutere(int botul, int tinta)
    {
        efecte.EfectDeflect[tinta] = 1;
        for (int i = 0; i < 25; i++)
        {
            if (efecte.TargetSpyE[tinta, i] == -1)
            {
                efecte.TargetSpyE[tinta, i] = botul;
                break;
            }
        }
    }
    int[] deflectDupa = new int[95];
    public void ReflectPutere(int[] tinta1, int[] tinta2, int nrdeboticureflect)
    {
        for (int i = 0; i < nrdeboticureflect; i++)
        {
            if (Verifica1(i, tinta1, tinta2, nrdeboticureflect) == 0)
            {
                deflectDupa[tinta1[i]] = 1;
            }
        }
        for (int i = 0; i < nrdeboticureflect; i++)
        {
            if (deflectDupa[tinta1[i]] == 1)
            {
                efecte.EfectDeflect[tinta1[i]] = 1;
                tinta1[i] = -1;
                tinta2[i] = -1;
            }
        }
        for (int i = 0; i < nrdeboticureflect; i++)
        {
            if (Verifica2(i, tinta1, tinta2, nrdeboticureflect) == 0)
            {
                if (tinta1[i] != -1)
                {
                    deflectDupa[tinta1[i]] = 1;
                }
            }
        }
        for (int i = 0; i < nrdeboticureflect; i++)
        {
            if (tinta1[i] != -1)
            {
                if (deflectDupa[tinta1[i]] == 1)
                {
                    efecte.EfectDeflect[tinta1[i]] = 1;
                    tinta1[i] = -1;
                    tinta2[i] = -1;
                }
            }
        }
        for (int i = 0; i < nrdeboticureflect; i++)
        {
            if (tinta1[i] != -1)
            {
                efecte.EfectReflect[tinta1[i]] = 1;
                efecte.PeCineReflect[tinta1[i]] = LastTinta(i, tinta1, tinta2, nrdeboticureflect);
            }
        }


    }
    public int LastTinta(int j, int[] tinta1, int[] tinta2, int nr)
    {
        // returneaza ultimul pe care se duce reflectul.
        int lastTinta = tinta2[j];

        for (int i = 0; i < nr; i++)
        {
            if (j != i)
            {
                if (tinta1[i] == lastTinta)
                {
                    lastTinta = tinta2[i];
                    break;
                }
            }
        }
        return lastTinta;
    }
    public int Verifica1(int j, int[] tinta1, int[] tinta2, int nr)
    {
        // verifica daca nu cumva 2 boti au dat reflect pe aceeasi tinta1 dar tinta2 e diferita (ex: bot 2 reflect bot5 pe bot6 iar bot 3 reflect bot5 pe bot7) daca se intampla asta 5 primeste deflect in loc de reflect.
        // 1 isi poate da puterea normal , 0 - primeste doar deflect.
        for (int i = 0; i < nr; i++)
        {
            if (tinta1[i] != -1 && tinta2[i] != -1)
            {
                if (i != j)
                {
                    if (tinta1[j] == tinta1[i] && tinta2[j] != tinta2[i])
                    {
                        return 0;
                    }
                }
            }
        }
        return 1;
    }
    public int Verifica2(int j, int[] tinta1, int[] tinta2, int nr)
    {
        int DeUndePornesti = tinta1[j];
        int BotulTinta2 = tinta2[j];
        // verific daca nu e: x reflect 5 pe 3, y reflect 3 pe 5 => 3 si 5 primesc deflect.
        int i = 0;
        while (Ver(j, tinta1, tinta2, nr, BotulTinta2) != -1 && BotulTinta2 != DeUndePornesti && i < 100)
        {
            i++;
            BotulTinta2 = Ver(j, tinta1, tinta2, nr, BotulTinta2);
        }

        if (BotulTinta2 == DeUndePornesti)
        {
            return 0;
        }
        else return 1;
    }
    public int Ver(int j, int[] tinta1, int[] tinta2, int nr, int botultinta2)
    {
        for (int i = 0; i < nr; i++)
        {
            if (tinta1[i] != -1 && tinta2[i] != -1)
            {
                if (i != j)
                {
                    if (tinta1[i] == botultinta2)
                    {
                        return tinta2[i]; // returneaza pe cine sa se duca reflectul ca botultinta2 primeste reflect de la botul i
                    }
                }
            }
        }

        return -1;
    }


    public void VerRefDef(int botul, int tinta) // verifica daca nu esti reflectat sau deflectat
    {
        if(efecte.PeCineRedirect[botul] != -1)
        {

        }
    }
    public void ReplacePutere(int tinta, int indexulputerii, int indexputere)
    {
        // interfata -> alegi rolul si puterea rolului dar in cod convertesc rolul in botul care are rolul ala(tinta)
        // indexputerea = in ce putere faci replace;
        // indexulputerii = care e puterea pe care o inlocuiesti 0 sau 1 din cele 2 puteri basic;


        if (efecte.PeCineReflect[tinta] != -1)
        {
            tinta = efecte.PeCineReflect[tinta];
        }
        if (efecte.EfectDeflect[tinta] != 1)
        {
            efecte.CePutereBasicAiRundaAsta[tinta, indexulputerii] = indexputere;
        }

    }
    public void GenjutsuPutere(int tinta)
    {
        if (efecte.EfectReflect[tinta] != 1)
        {
            tinta = efecte.PeCineReflect[tinta];
        }

        if (efecte.EfectDeflect[tinta] != 1)
        {
            int nr = -1; int k = 0;
            while (nr != -1 && k < 150)
            {
                k++;
                nr = Random.Range(0, efecte.NrBoti);
                if (efecte.EfectMort[nr] == 1 || efecte.Categorie[nr] != efecte.Categorie[tinta])
                {
                    nr = -1;
                }
            }
            if (nr == -1)
            {
                nr = tinta;            // nr = noua tinta a tintei. (pe care mafiot o sa dea puterea tinta)
            }
            int ix = -1; // ce putere sa dea pe nr;
            for (int i = 0;efecte.TinteleBotilor1[tinta,i] != -1 && i < 25; i++)
            {
                if (efecte.TinteleBotilor2[tinta, i] == -1)
                {
                    ix = i;
                    break;
                }
            }
            if (ix != -1)
            {
                efecte.TinteleBotilor1[tinta, ix] = nr;
            }
        }
    }
    public void Swap_AbilityPutere(int tinta1, int tinta2, int indexulputerii1, int indexulputerii2)
    {
        // in interfata se alege rolul eu primest tinta adica botul care are acel rol + indexul puterii care se schimba

        if (efecte.EfectReflect[tinta1] == 1)
        {
            tinta1 = efecte.PeCineReflect[tinta1];
        }
        if (efecte.EfectReflect[tinta2] == 1)
        {
            tinta2 = efecte.PeCineReflect[tinta2];
        }
        if (efecte.EfectDeflect[tinta1] != 1 && efecte.EfectDeflect[tinta2] != 1)
        {
            int a = efecte.CePutereBasicAiRundaAsta[tinta1, indexulputerii1];
            efecte.CePutereBasicAiRundaAsta[tinta1, indexulputerii1] = efecte.CePutereBasicAiRundaAsta[tinta2, indexulputerii2];
            efecte.CePutereBasicAiRundaAsta[tinta2, indexulputerii2] = a;
        }
    }
    public void AntiBlock(int tinta)
    {
        if (efecte.EfectReflect[tinta] == 1)
        {
            tinta = efecte.PeCineReflect[tinta];
        }
        if (efecte.EfectDeflect[tinta] != 1)
        {
            efecte.AntiBlock[tinta] = 1;
        }
    }
    public void AntiRedirect(int tinta)
    {
        if (efecte.EfectReflect[tinta] == 1)
        {
            tinta = efecte.PeCineReflect[tinta];
        }
        if (efecte.EfectDeflect[tinta] != 1)
        {
            efecte.AntiRedirect[tinta] = 1;
        }
    }
    public void AntiBAR(int tinta)
    {
        if (efecte.EfectReflect[tinta] == 1)
        {
            tinta = efecte.PeCineReflect[tinta];
        }
        if (efecte.EfectDeflect[tinta] != 1)
        {
            efecte.AntiBAR[tinta] = 1;
        }
    }
    public void KillingShield(int botul, int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }
            if (efecte.EfectDeflect[tinta] != 1)
            {
                for (int i = 0; i < 25; i++)
                {
                    if (efecte.TinteleBotilor2[tinta, i] == -1)
                    {
                        if (efecte.CePutereBasicAiRundaAsta[tinta, i] == efecte.ProtectIndex)
                        {
                            efecte.CePutereBasicAiRundaAsta[tinta, i] = efecte.KillIndex;
                        }
                    }
                }
            }
        }
    }
    public void Guardian(int botul, int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }
            if (efecte.EfectDeflect[tinta] != 1)
            {
                efecte.EfectGuardian[tinta] = 1;
                for (int i = 0; i < 25; i++)
                {
                    if (efecte.CineAdatGuardianEfect[tinta, i] == -1)
                    {
                        efecte.CineAdatGuardianEfect[tinta, i] = botul;
                    }
                }
            }
        }

    }
    public void Protect(int botul, int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }
            if (efecte.EfectDeflect[tinta] != 1)
            {
                efecte.EfectProtect[tinta] = 1;
            }
        }
    }
    public void ShieldPiercing(int botul, int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }
            if (efecte.EfectDeflect[tinta] != 1)
            {
                efecte.EfectProtect[tinta] = 0;
                efecte.EfectGuardian[tinta] = 0;
                for (int i = 0; i < 25; i++)
                {
                    if (efecte.CineAdatGuardianEfect[tinta, i] != -1)
                    {
                        efecte.CineAdatGuardianEfect[tinta, i] = -1;
                    }
                }
            }
        }
    }
    public void RidKill(int botul, int tinta, int rolul)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectReflect[tinta] != 1)
            {
                if (efecte.EfectDeflect[tinta] != 1)
                {
                    if (efecte.RolulBotilor[tinta] == rolul)
                    {
                        if (efecte.EfectGuardian[tinta] != 1)
                        {
                            if (efecte.EfectProtect[tinta] != 1)
                            {
                                efecte.CateVietiAi[tinta]--;
                                if (efecte.CateVietiAi[tinta] <= 0)
                                {
                                    efecte.EfectMortRundaAsta[tinta] = 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                if (efecte.CineAdatGuardianEfect[tinta, i] != -1)
                                {
                                    efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]]--;
                                    if (efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]] <= 0)
                                    {
                                        efecte.EfectMortRundaAsta[efecte.CineAdatGuardianEfect[tinta, i]] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void BRidKill(int botul, int tinta, int rolul)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectReflect[tinta] != 1)
            {
                if (efecte.EfectDeflect[tinta] != 1)
                {
                    if (efecte.RolulBotilor[tinta] == rolul)
                    {
                        if (efecte.EfectGuardian[tinta] != 1)
                        {
                            if (efecte.EfectProtect[tinta] != 1)
                            {
                                efecte.CateVietiAi[tinta]--;
                                if (efecte.CateVietiAi[tinta] <= 0)
                                {
                                    efecte.EfectMortRundaAsta[tinta] = 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                if (efecte.CineAdatGuardianEfect[tinta, i] != -1)
                                {
                                    efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]]--;
                                    if (efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]] <= 0)
                                    {
                                        efecte.EfectMortRundaAsta[efecte.CineAdatGuardianEfect[tinta, i]] = 1;
                                    }
                                }
                            }
                        }
                    } else
                    {

                        if (efecte.EfectGuardian[botul] != 1)
                        {
                            if (efecte.EfectProtect[botul] != 1)
                            {

                                efecte.CateVietiAi[botul]--;
                                if (efecte.CateVietiAi[botul] <= 0)
                                {
                                    efecte.EfectMortRundaAsta[botul] = 1;
                                }
                            }
                        } else
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                if (efecte.CineAdatGuardianEfect[botul, i] != -1)
                                {
                                    efecte.CateVietiAi[efecte.CineAdatGuardianEfect[botul, i]]--;
                                    if (efecte.CateVietiAi[efecte.CineAdatGuardianEfect[botul, i]] <= 0)
                                    {
                                        efecte.EfectMortRundaAsta[efecte.CineAdatGuardianEfect[botul, i]] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void DRidKill(int botul, int tinta, int rolul)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectReflect[tinta] != 1)
            {
                if (efecte.EfectDeflect[tinta] != 1)
                {
                    if (efecte.RolulBotilor[tinta] == rolul)
                    {
                        if (efecte.EfectGuardian[tinta] != 1)
                        {
                            if (efecte.EfectProtect[tinta] != 1)
                            {
                                efecte.CateVietiAi[tinta]--;
                                if (efecte.CateVietiAi[tinta] <= 0)
                                {
                                    efecte.EfectMortRundaAsta[tinta] = 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                if (efecte.CineAdatGuardianEfect[tinta, i] != -1)
                                {
                                    efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]]--;
                                    if (efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]] <= 0)
                                    {
                                        efecte.EfectMortRundaAsta[efecte.CineAdatGuardianEfect[tinta, i]] = 1;
                                    }
                                }
                            }
                        }
                    } else
                    {
                        efecte.DRidKillRatat[botul]++;
                        if (efecte.DRidKillRatat[botul] >= 2)
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                if (efecte.CePutereBasicAi[botul, i] == efecte.DRidKillIndex)
                                {
                                    efecte.CePutereBasicAi[botul, i] = -1;
                                }
                            }
                            // sterge puterea din puteribasic.
                        }
                    }
                }
            }
        }
    }
    public void BountyHunter(int botul, int tinta, int rolul)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectReflect[tinta] != 1)
            {
                if (efecte.EfectDeflect[tinta] != 1)
                {
                    if (efecte.RolulBotilor[tinta] == rolul)
                    {
                        if (efecte.EfectGuardian[tinta] != 1)
                        {
                            if (efecte.EfectProtect[tinta] != 1)
                            {
                                efecte.CateVietiAi[tinta]--;
                                if (efecte.CateVietiAi[tinta] <= 0)
                                {
                                    efecte.EfectMortRundaAsta[tinta] = 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 25; i++)
                            {
                                if (efecte.CineAdatGuardianEfect[tinta, i] != -1)
                                {
                                    efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]]--;
                                    if (efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]] <= 0)
                                    {
                                        efecte.EfectMortRundaAsta[efecte.CineAdatGuardianEfect[tinta, i]] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void KillPutere(int botul, int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }

            if (efecte.EfectDeflect[tinta] != 1)
            {
                if (efecte.EfectGuardian[tinta] != 1)
                {
                    if (efecte.EfectProtect[tinta] != 1)
                    {
                        efecte.CateVietiAi[tinta]--;
                        if (efecte.CateVietiAi[tinta] <= 0)
                        {
                            efecte.EfectMortRundaAsta[tinta] = 1;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 25; i++)
                    {
                        if (efecte.CineAdatGuardianEfect[tinta, i] != -1)
                        {
                            efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]]--;
                            if (efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta, i]] <= 0)
                            {
                                efecte.EfectMortRundaAsta[efecte.CineAdatGuardianEfect[tinta, i]] = 1;
                            }
                        }
                    }
                }
            }
        }
    }
    public void HidRid(int botul, int[] tintele, int[] rolurile)
    {
        for (int i = 0; i < 25; i++)
        {
            if (rolurile[i] != -1)
            {
                RidKill(botul, tintele[i], rolurile[i]);
            }
        }
    }
    public void Poison(int botul, int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }

            if (efecte.EfectDeflect[tinta] != 1)
            {
                if (efecte.EfectPoison[tinta] != -1)
                {
                    efecte.EfectPoison[tinta] = efecte.Runda;
                }
                // pui ca viziteaza.
            }

        }
    }
    public void Rogue(int botul, int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }

            if (efecte.EfectDeflect[tinta] != 1)
            {
                int nr = 0;
                for (int i = 0; i < 25; i++)
                {
                    if (efecte.CePutereBasicAiRundaAsta[tinta, i] != -1)
                    {
                        nr++;
                    }
                }
                nr = Random.Range(1, nr + 1);
                int nr2 = 1; // la ce putere esti.
                // nr => a cata putere o furi.
                for (int i = 0; i < 25; i++)
                {
                    if (efecte.CePutereFuri[botul, i] == -1)
                    {
                        for (int j = 0; j < 25; j++)
                        {
                            if (efecte.CePutereBasicAiRundaAsta[tinta, j] != -1)
                            {
                                if (nr2 >= nr)
                                {
                                    efecte.CePutereFuri[botul, i] = efecte.CePutereBasicAiRundaAsta[tinta, j];
                                   // efecte.DeLaCineAiFurat[botul, i] = tinta;
                                    break;
                                }
                                nr2++;
                            }
                        }
                        break;
                    }
                }
            }

        }
    }
    public void Copy(int botul, int tinta)
    {
        if (efecte.Categorie[botul] != efecte.Categorie[tinta])
        {
            if (efecte.EfectBlocat[botul] != 1)
            {
                if (efecte.EfectRedirect[botul] == 1)
                {
                    tinta = efecte.ePeCineRedirect[botul];
                }
                if (efecte.EfectReflect[tinta] == 1)
                {
                    tinta = efecte.PeCineReflect[tinta];
                }

                if (efecte.EfectDeflect[tinta] != 1)
                {
                    int nr = 0;
                    for (int i = 0; i < 25; i++)
                    {
                        if (efecte.CePutereBasicAiRundaAsta[tinta, i] != -1)
                        {
                            nr++;
                        }
                    }
                    nr = Random.Range(1, nr + 1);
                    int nr2 = 1; // la ce putere esti.
                                 // nr => a cata putere o furi.
                    for (int i = 0; i < 25; i++)
                    {
                        if (efecte.CePutereCopiezi[botul, i] == -1)
                        {
                            for (int j = 0; j < 25; j++)
                            {
                                if (efecte.CePutereBasicAiRundaAsta[tinta, j] != -1)
                                {
                                    if (nr2 >= nr)
                                    {
                                        efecte.CePutereCopiezi[botul, i] = efecte.CePutereBasicAiRundaAsta[tinta, j];
                                        break;
                                    }
                                    nr2++;
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
    public void Duplicate(int botul, int tinta, int indexputere)
    {

        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }

            if (efecte.EfectDeflect[tinta] != 1)
            {
                for (int i = 0; i < 25; i++)
                {
                    if (efecte.CePutereDuplicate[tinta, i] == -1)
                    {
                        efecte.CePutereDuplicate[tinta, i] = indexputere;
                        break;
                    }
                }
            }
        }
    }
    public void Revive(int botul,int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }
            if (efecte.EfectDeflect[tinta] != 1)
            {
                efecte.EfectMortRundaAsta[tinta] = 0;
                if(efecte.EfectMort[tinta] == 1)
                efecte.EfectMort[tinta] = 0;
                if(efecte.Runda - efecte.EfectPoison[tinta] >= 2)
                {
                    efecte.EfectPoison[tinta] = 0;
                }
            }
        }
    }
    public void Trap(int botul, int tinta1, int tinta2)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] != 1)
            {
                if (efecte.EfectReflect[tinta1] != 1 && efecte.EfectReflect[tinta2] != 1)
                {
                    if (efecte.EfectDeflect[tinta1] != 1 && efecte.EfectDeflect[tinta2]!= 1)
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            if (efecte.TargetSpyE[tinta1, i] == tinta2)
                            {
                                if (efecte.EfectGuardian[tinta2] != 1)
                                {
                                    if (efecte.EfectProtect[tinta2] != 1)
                                    {
                                        efecte.CateVietiAi[tinta2]--;
                                        if (efecte.CateVietiAi[tinta2] <= 0)
                                        {
                                            efecte.EfectMortRundaAsta[tinta2] = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < 25; j++)
                                    {
                                        if (efecte.CineAdatGuardianEfect[tinta2, j] != -1)
                                        {
                                            efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta2, j]]--;
                                            if (efecte.CateVietiAi[efecte.CineAdatGuardianEfect[tinta2, j]] <= 0)
                                            {
                                                efecte.EfectMortRundaAsta[efecte.CineAdatGuardianEfect[tinta2, j]] = 1;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    public int GhostlyWhisper(int botul,int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }
            if (efecte.EfectDeflect[tinta] != 1)
            {
                if (efecte.GhostlyWhisperC < 2)
                {
                    if (efecte.EfectMort[tinta] == 1)
                    {
                        efecte.GhostlyWhisperC++;
                        return efecte.RolulBotilor[tinta];
                    }
                }
            }
        }
        return -1;
    }
    public bool Inspect(int botul, int tinta, int indexputere)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }
            if (efecte.EfectDeflect[tinta] != 1)
            {
                for (int i = 0; i < 25; i++)
                {
                    if (efecte.CePutereBasicAi[tinta, i] == indexputere)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public int SpyExact(int botul,int tinta)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            if (efecte.EfectRedirect[botul] == 1)
            {
                tinta = efecte.ePeCineRedirect[botul];
            }
            if (efecte.EfectReflect[tinta] == 1)
            {
                tinta = efecte.PeCineReflect[tinta];
            }
            if (efecte.EfectDeflect[tinta] != 1)
            {
                return efecte.RolulBotilor[tinta];
            }
        }
        return -1;
    }
    public int Sensorial(int botul)
    {
        if (efecte.EfectBlocat[botul] != 1)
        {
            int[] PuteriStranse = new int[100];
            for (int i = 0; i < 100; i++) PuteriStranse[i] = -1; 
            int NrP = 0;
            for (int i = 0; i < 25; i++)
            {
                if(efecte.TargetSpyE[botul,i] != -1)
                {
                    int CineTeaVizitat = efecte.TargetSpyE[botul, i];
                    for(int j = 0;j<25;j++)
                    {
                        if(efecte.TinteleBotilor1[CineTeaVizitat,j] == botul || efecte.TinteleBotilor2[CineTeaVizitat,j]== botul)
                        {
                            PuteriStranse[NrP] = efecte.CePutereBasicAiRundaAsta[CineTeaVizitat, j];
                            NrP++;
                        }
                    }
                }
            }
            int a = 0;
            for(int i = 0;i<25;i++)
            {
                if(efecte.PutereSensorialCopiata[botul,i] == -1 && PuteriStranse[a] != -1)
                {
                    efecte.PutereSensorialCopiata[botul, i] = PuteriStranse[a];
                    a++;
                    if (a == NrP) break;
                }
            }
        }

        return -1;
    }



}
