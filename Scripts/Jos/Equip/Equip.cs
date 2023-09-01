using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class Equip : MonoBehaviour
{
    // pt save data: PuteriFolositeBasic || PuterileBotilor || g || PuteriFolositeRandom || 

    //  public Caractere car;
    public CharacterBuyUp car;

    public AbilitiesLibrary ab;
    public Powers pw;
    public Buffs bf;
    public Items it;

    public GameObject[] Caractere = new GameObject[10];
    public Animator[] AnimatorCar = new Animator[10];
    public GameObject[] Verde = new GameObject[5];
    public GameObject[] Rosu = new GameObject[5];

    public int[,] PuterileBotilor = new int[10, 5]; // pe -1 si index // carputeriindex[0,1] e indexu la a 2 a putere a primului bot


    public int[] PuteriFolositeBasic = new int[60]; // 0 si 1
    public int CurrentSelectedBasic; // pe -1 si index

    public int[] PuteriFolositeRandom = new int[13]; // 0 si 1
    public int CurrentSelectedRandom; // pe -1 si index

    public int[] PuteriFolositeBuff = new int[13]; // 0 si 1
    public int CurrentSelectedBuff; // pe -1 si index

    public int[] ItemeFolosite = new int[20];
    public int CurrentSelectedItem;

    public GameObject Puteri0ParentBasic;  // puterile pt slot
    public GameObject Puteri1ParentBasic;  // puterile pt panou

    public GameObject Puteri0RnPr;  // puterile pt slot
    public GameObject Puteri1RnPr;  // puterile pt panou

    public GameObject Puteri0ParentBuff;  // puterile pt slot
    public GameObject Puteri1ParentBuff;  // puterile pt panou

    public GameObject Iteme0Parent;
    public GameObject Iteme1Parent;

    public GameObject ContentPuteriBasic;
    public GameObject ContentPuteriRandom;
    public GameObject ContentPuteriBuff;
    public GameObject ContentIteme;

    public GameObject Comun;

    public Vector2[] locate = new Vector2[5];

    public GameObject Next;
    public GameObject NextInvers;

    public int NrDePuteriBasic;
    public int NrDePuteriRandom;
    public int NrDePuteriBuff;
    public int NrDeIteme;

    public int NrPRD;
    public int NrPRDRandom;
    public int NrPRDBuff;
    public int NrPRDIteme;





    public TextMeshProUGUI NoPowerAvailableText;
    public Button GoToLibrary;

    public TextMeshProUGUI BGText;
    public Button SelectButton;

    public GameObject EquipObj;


    public Color32 cardColor =  new Color32(56, 180, 75, 150);
    public void tpPutere(int index, GameObject puterea) // tp puterea la slotul index.
    {
        puterea.transform.localPosition = locate[index];
        puterea.gameObject.SetActive(true);
    }
    public void RosuVerde(int k) // pune rosu sau verde si puterile pe care le are caracterul k.
    {
        for(int i = 0;i< NrDePuteriBasic; i++)
        { 
            if(PuteriFolositeBasic[i] == 1)
                Puteri0ParentBasic.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0;i<NrDePuteriRandom;i++)
        {
            if (PuteriFolositeRandom[i] == 1)
                Puteri0RnPr.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0;i<NrDePuteriBuff;i++)
        {
            if(PuteriFolositeBuff[i] == 1)
            {
                Puteri0ParentBuff.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        for(int i = 0;i<NrDeIteme;i++)
        {
            if(ItemeFolosite[i] == 1)
            {
                Iteme0Parent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            Verde[i].gameObject.SetActive(false);
            Rosu[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            if (i < car.carLevel[k])
            {
                Verde[i].gameObject.SetActive(true);
                if (i < 2)
                {
                    if (PuterileBotilor[k, i] != -1)
                    {
                        tpPutere(i, Puteri0ParentBasic.transform.GetChild(PuterileBotilor[k, i]).gameObject);
                    }
                }
                else if (i == 2)
                {
                    if(PuterileBotilor[k,i] != -1)
                    {
                        tpPutere(i, Puteri0RnPr.transform.GetChild(PuterileBotilor[k, i]).gameObject);
                    }
                }
                else if(i == 3)
                {
                    if (PuterileBotilor[k, i] != -1)
                    {
                        tpPutere(i, Puteri0ParentBuff.transform.GetChild(PuterileBotilor[k, i]).gameObject);
                    }
                }
                else if(i == 4)
                {
                    if(PuterileBotilor[k,i] != -1)
                    tpPutere(i, Iteme0Parent.transform.GetChild(PuterileBotilor[k, i]).gameObject);
                }
            }
            else
            {
                Rosu[i].gameObject.SetActive(true);
            }
        }
    
     
    }

    public GameObject TextSelect;


    public void FunctiaDistrugatoare()
    {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    PuterileBotilor[i, j] = -1;
                }
            }
            for (int i = 0; i < NrDePuteriBasic; i++)
            {
                PuteriFolositeBasic[i] = 0;
            }
            for (int i = 0; i < NrDePuteriRandom; i++)
            {
                PuteriFolositeRandom[i] = 0;
            }
            for (int i = 0; i < NrDePuteriBuff; i++)
            {
                PuteriFolositeBuff[i] = 0;
            }
            for (int i = 0; i < NrDeIteme; i++)
            {
                ItemeFolosite[i] = 0;
            }
            CurrentSelectedBasic = -1;
            CurrentSelectedRandom = -1;
            CurrentSelectedBuff = -1;
            CurrentSelectedItem = -1;

            Comun.gameObject.SetActive(false);
    }
    public void Start()
    {

        
        StartOn();
        for (int i = 0; i < 10; i++)
        {
            x[i] = Caractere[i].transform.localPosition.x;
            y[i] = Caractere[i].transform.localPosition.y;

            AnimatorCar[i] = Caractere[i].GetComponent<Animator>();
            AnimatorCar[i].speed = 2;
        }
    }
    public void StartOn()
    {
        for (int i = 0; i < NrDePuteriBasic; i++)
        {
            Puteri0ParentBasic.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0;i<NrDePuteriRandom;i++)
        {
            Puteri0RnPr.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0;i<NrDePuteriBuff;i++)
        {
            Puteri0ParentBuff.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0;i<NrDeIteme;i++)
        {
            Iteme0Parent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 1; i < 10; i++)
            Caractere[i].gameObject.SetActive(false);
        Caractere[0].gameObject.SetActive(true);
        Next.gameObject.SetActive(true);
        NextInvers.gameObject.SetActive(false);
        a = 0;
        amax = car.nrCarDeblocate-1;
        RosuVerde(0);


    }

    public int a;
    public int amax;
    public float[] x = new float[10];
    public float[] y = new float[10];



    public void NextCar()
    {
        if (a < amax)
        {
            if(a != 0)
            {
                AnimatorCar[a - 1].enabled = false;
                Caractere[a - 1].gameObject.SetActive(false);
                AnimatorCar[a - 1].enabled = true;
            }
            if(Caractere[a].gameObject.activeSelf)
            {
                AnimatorCar[a].Play("T1");
            } else
            {
                Caractere[a].gameObject.SetActive(true);
                AnimatorCar[a].Play("T1");
            }
            StartCoroutine(Of1s(a));
            Caractere[a + 1].gameObject.SetActive(true);
            AnimatorCar[a + 1].Play("Y1");
            a++;
            RosuVerde(a);



            if (a == 1)
            {
                NextInvers.gameObject.SetActive(true);
            }
            if (a == amax)
            {
                Next.gameObject.SetActive(false);
            }
        }
    }
    public void NextDarInversCar()
    {
        if (a != 0)
        {
            if (a != 9)
            {
                AnimatorCar[a + 1].enabled = false;
                Caractere[a + 1].gameObject.SetActive(false);
                AnimatorCar[a + 1].enabled = true;
            }
            if (Caractere[a].gameObject.activeSelf)
            {
                AnimatorCar[a].Play("P1");
            } else
            {
                Caractere[a].gameObject.SetActive(true);
                AnimatorCar[a].Play("P1");
            }
            StartCoroutine(Of1s(a));
            Caractere[a - 1].gameObject.SetActive(true);
            AnimatorCar[a - 1].Play("R1");
            
            a--;
            RosuVerde(a);

            if (a == 0)
            {
                NextInvers.gameObject.SetActive(false);
            }
            if (a == amax-1)
            {
                Next.gameObject.SetActive(true);
            }
        }
    }


    IEnumerator Of1s(int b)
    {
        yield return new WaitForSeconds(0.33333333f);

            Caractere[b].gameObject.SetActive(false);
            Caractere[b].transform.localPosition = new Vector2(x[b], y[b]);
            Caractere[b].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }



    public GameObject PanouPuteriBasic;
    public GameObject PanouPuteriRandom;
    public GameObject PanouPuteriBuff;
    public GameObject PanouIteme;

    int Slot;
    public void OpenSelectionAbility(int k)
    {   // k = ce slot deschizi;
        Comun.gameObject.SetActive(true);
        SelectButton.gameObject.SetActive(true);
        BGText.gameObject.SetActive(true);
        NoPowerAvailableText.gameObject.SetActive(false);
        GoToLibrary.gameObject.SetActive(false);
        if (k == 0 || k == 1)
        {
            OpenPuterileBasicDisponibile(k);
        }
        else if(k == 2)
        {
            OpenPuteriRandomDisponibile(k);
        }
        else if(k == 3)
        {
            OpenPuteriBuffDisponibile(k);
        }
        else if(k == 4)
        {
            OpenItemeDisponibile(k);
        }

    }
    
    public void NoPowerAvailable()
    {
        NoPowerAvailableText.gameObject.SetActive(true);
        NoPowerAvailableText.text = "You don't have any ability";
        GoToLibrary.gameObject.SetActive(true);
        SelectButton.gameObject.SetActive(false);
        BGText.gameObject.SetActive(false);
    }
    public void OpenItemeDisponibile(int k)
    {

        Slot = k;
        CurrentSelectedItem = -1;
        NrPRDIteme = 0;
        PanouIteme.gameObject.SetActive(true);
        TextSelect.GetComponent<Text>().color = new Color32(149, 149, 149, 200);

        for (int i = 0; i < NrDeIteme; i++)
        {
            if (it.itemsCumparate[i] == 1 && ItemeFolosite[i] != 1)
            {
                Iteme1Parent.transform.GetChild(i).gameObject.SetActive(true);
                NrPRDIteme++;
            }
            else
            {
                Iteme1Parent.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        if (NrPRDIteme == 0)
        {
            NoPowerAvailable();
        }
       
    }
    public void OpenPuteriBuffDisponibile(int k)
    {
        Slot = k;
        CurrentSelectedBuff = -1;
        NrPRDBuff = 0;
        PanouPuteriBuff.gameObject.SetActive(true);
        TextSelect.GetComponent<Text>().color = new Color32(149, 149, 149, 200);

        for (int i = 0; i < NrDePuteriBuff; i++)
        {
            if (bf.buffsCumparate[i] == 1 && PuteriFolositeBuff[i] != 1)
            {
                Puteri1ParentBuff.transform.GetChild(i).gameObject.SetActive(true);
                NrPRDBuff++;
            }
            else
            {
                Puteri1ParentBuff.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        if (NrPRDBuff == 0)
        {
            NoPowerAvailable();
        }


    }
    public void OpenPuteriRandomDisponibile(int k)
    {
        Slot = k;
        CurrentSelectedRandom = -1;
        NrPRDRandom = 0;
        PanouPuteriRandom.gameObject.SetActive(true);
        TextSelect.GetComponent<Text>().color = new Color32(149, 149, 149, 200);

        for (int i = 0; i < NrDePuteriRandom; i++)
        {
            if (pw.powersCumparate[i] == 1 && PuteriFolositeRandom[i] != 1)
            {
                Puteri1RnPr.transform.GetChild(i).gameObject.SetActive(true);
                NrPRDRandom++;
            }
            else
            {
                Puteri1RnPr.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        if (NrPRDRandom == 0)
        {
            NoPowerAvailable();
        }
    }
    public void OpenPuterileBasicDisponibile(int k)
    {
        Slot = k;
        CurrentSelectedBasic = -1;
        NrPRD = 0;
        PanouPuteriBasic.gameObject.SetActive(true);
        TextSelect.GetComponent<Text>().color = new Color32(149, 149, 149, 200);

        for (int i = 0; i < NrDePuteriBasic; i++)
        {
            if (ab.puteriCumparate[i] == 1 && PuteriFolositeBasic[i] != 1)
            {
                Puteri1ParentBasic.transform.GetChild(i).gameObject.SetActive(true);
                NrPRD++;
            }
            else
            {
                Puteri1ParentBasic.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if(NrPRD == 0)
        {
            NoPowerAvailable();
        }
    }



    public void GoToLibraryf()
    {
        Exit();
        if (Slot == 0 || Slot == 1)
        {
            // aprinzi shop ul la puteri basic
        }
        else if(Slot == 1)
        {

        } else if(Slot == 2)
        {

        }else if(Slot == 3)
        {

        }
        EquipObj.gameObject.SetActive(false);
    }
    public void SelectEfect(GameObject Puteri1Parinte, int currentselected)
    {
        if (currentselected != -1)
        {
            Puteri1Parinte.transform.GetChild(currentselected).GetChild(1).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            Puteri1Parinte.transform.GetChild(currentselected).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
            Puteri1Parinte.transform.GetChild(currentselected).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
        }
        else TextSelect.GetComponent<Text>().color = new Color32(224, 224, 224, 255);
    }

    public void SelectEfect(int i, GameObject Puteri1Parinte)
    {
        Puteri1Parinte.transform.GetChild(i).GetChild(1).gameObject.GetComponent<Image>().color = cardColor;
        Puteri1Parinte.transform.GetChild(i).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        Puteri1Parinte.transform.GetChild(i).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
    }
    public void SelectPower(int i)
    {
        SelectEfect(Puteri1ParentBasic, CurrentSelectedBasic);

        SelectEfect(i, Puteri1ParentBasic);
        CurrentSelectedBasic = i;

    }

    public void SelectPutereRandom(int i)
    {
        SelectEfect(Puteri1RnPr, CurrentSelectedRandom);

        SelectEfect(i, Puteri1RnPr);
        CurrentSelectedRandom = i;
    }

    public void SelectPutereBuff(int i)
    {
        SelectEfect(Puteri1ParentBuff, CurrentSelectedBuff);

        SelectEfect(i, Puteri1ParentBuff);
        CurrentSelectedBuff = i;
    }
    public void SelectItem(int i)
    {
        SelectEfect(Iteme1Parent, CurrentSelectedItem);

        SelectEfect(i, Iteme1Parent);
        CurrentSelectedItem = i;

    }
    public void Selectt(int b, ref int CurrentSelected, ref int[] PuteriFolosite, GameObject Puteri0, GameObject Puteri1, GameObject Panou)
    {
        if (CurrentSelected != -1)
        {
            PuteriFolosite[CurrentSelected] = 1;
            if (b != -1) // ai deja o putere
            {
                PuteriFolosite[b] = 0;
                Puteri0.transform.GetChild(b).gameObject.SetActive(false);
                Puteri1.transform.GetChild(b).GetChild(1).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                Puteri1.transform.GetChild(b).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
                Puteri1.transform.GetChild(b).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
            }
            PuterileBotilor[a, Slot] = CurrentSelected;
            tpPutere(Slot, Puteri0.transform.GetChild(CurrentSelected).gameObject);

            Panou.gameObject.SetActive(false);

            CurrentSelected = -1;
        }
    }

    public void Select() // butonul 
    {
        Comun.gameObject.SetActive(false);
        int b = PuterileBotilor[a, Slot];
        if (Slot == 0 || Slot == 1)
        {
            Selectt(b, ref CurrentSelectedBasic,ref PuteriFolositeBasic, Puteri0ParentBasic, Puteri1ParentBasic, PanouPuteriBasic);
        }
        else if(Slot == 2)
        {
            Selectt(b, ref CurrentSelectedRandom, ref PuteriFolositeRandom, Puteri0RnPr, Puteri1RnPr, PanouPuteriRandom);
        }
        else if(Slot == 3)
        {
            Selectt(b, ref CurrentSelectedBuff, ref PuteriFolositeRandom, Puteri0ParentBuff, Puteri1ParentBuff, PanouPuteriBuff);  
        }
        else if(Slot == 4)
        {
            Selectt(b, ref CurrentSelectedItem, ref ItemeFolosite, Iteme0Parent, Iteme1Parent, PanouIteme);
        }
    }

    public void Exitt(GameObject Panou, ref int CurrentSelected, GameObject Puteri1)
    {
        Panou.gameObject.SetActive(false);
        if (CurrentSelected != -1)
        {
            Puteri1.transform.GetChild(CurrentSelected).GetChild(1).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            Puteri1.transform.GetChild(CurrentSelected).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
            Puteri1.transform.GetChild(CurrentSelected).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
        }
        CurrentSelected = -1;
    }
    public void Exit()
    {
        Comun.gameObject.SetActive(false);
        if (Slot == 0 || Slot == 1)
        {
            Exitt(PanouPuteriBasic, ref CurrentSelectedBasic, Puteri1ParentBasic);
        }
        else if(Slot == 2)
        {
            Exitt(PanouPuteriRandom, ref CurrentSelectedRandom, Puteri1RnPr);
        }
        else if(Slot == 3)
        {
            Exitt(PanouPuteriBuff, ref CurrentSelectedBuff, Puteri1ParentBuff);
        }
        else if(Slot == 4)
        {
            Exitt(PanouIteme, ref CurrentSelectedItem, Iteme1Parent);
        }
    }
    public void Test(int i, GameObject Puteri0Parinte, GameObject Puteri1Parinte)
    {
        Puteri0Parinte.transform.GetChild(PuterileBotilor[a, i]).gameObject.SetActive(false);
        Puteri1Parinte.transform.GetChild(PuterileBotilor[a, i]).GetChild(1).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        Puteri1Parinte.transform.GetChild(PuterileBotilor[a, i]).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
        Puteri1Parinte.transform.GetChild(PuterileBotilor[a, i]).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);

        PuterileBotilor[a, i] = -1;
    }
    public void ResetCharacter()
    {
        for(int i = 0;i<2;i++)
        if (PuterileBotilor[a, i] != -1)
        {
            PuteriFolositeBasic[PuterileBotilor[a, i]] = 0;
            Test(i, Puteri0ParentBasic, Puteri1ParentBasic);
            PuterileBotilor[a, i] = -1;
        }
        if (PuterileBotilor[a,2] != -1)
        {
            PuteriFolositeRandom[PuterileBotilor[a, 2]] = 0;
            Test(2, Puteri0RnPr, Puteri1RnPr);
            PuterileBotilor[a, 2] = -1;
        }
        if(PuterileBotilor[a,3] != -1)
        {
            PuteriFolositeBuff[PuterileBotilor[a, 3]] = 0;
            Test(3, Puteri0ParentBuff, Puteri1ParentBuff);
            PuterileBotilor[a, 3] = -1;
        }
        if(PuterileBotilor[a,4] != -1)
        {
            ItemeFolosite[PuterileBotilor[a, 4]] = 0;
            Test(4, Iteme0Parent, Iteme1Parent);
            PuterileBotilor[a, 4] = -1;
        }
    
    
    }
    public void ResetAllCharacters()
    {
        int at = a;
        a = 0;
        for(int i = 0;i<10;i++)
        {
            ResetCharacter();
            a++;
        }
        a = at;

    }


    private void OnApplicationQuit()
    {
        Comun.gameObject.SetActive(false);

        Slot = 0; Exit();
        Slot = 2; Exit();
        Slot = 3; Exit();
        Slot = 4; Exit();
    }


}   
