using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
public class Fragments : MonoBehaviour
{

    public gen7 genS;
    public miniEquip me;

    public int counter;
    public int counterMax;
    public TextMeshProUGUI counterText;

    public bool isWorking = false;

    public int[] spatiiOcupate = new int[4];
    public int[] spatiiDeblocate = new int[4];
    public int[] tipulSelectat = new int[4];

    public GameObject allim;

    public GameObject claimText;
    public TextMeshProUGUI nrDeClaimText;

    public Slider slider;

    public GameObject panouFrag;



    [Serializable]
    private  class FragmentsData
    {
        public bool isWorking;
        public int counter;
        public int[] spatiiOcupate;
        public int[] spatiiDeblocate;
        public int[] tipulSelectat;
        public int[] claim;
        public int g;
    }

    public void SaveData()
    {
        FragmentsData data = new FragmentsData();
        data.isWorking = isWorking;
        data.counter = counter;
        data.spatiiOcupate = spatiiOcupate;
        data.spatiiDeblocate = spatiiDeblocate;
        data.tipulSelectat = tipulSelectat;
        data.claim = claim;
        data.g = g;


        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/fragment_data.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/fragment_data.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            FragmentsData data = JsonUtility.FromJson<FragmentsData>(json);
            isWorking = data.isWorking;
            counter = data.counter;
            spatiiOcupate = data.spatiiOcupate;
            spatiiDeblocate = data.spatiiDeblocate;
            tipulSelectat = data.tipulSelectat;
            claim = data.claim;
            g = data.g;
        }
    }


    public int Spatiu()
    {
        for(int i = 0;i<4;i++)
        {
            if (spatiiOcupate[i] == 0 && spatiiDeblocate[i] == 1) return i;
        }
        return -1;
    }
    
    public int[] runeCost = new int[5];
    public int[] xpCost = new int[5];
    public bool calculXP(int i)
    {
        int[] b = { 100, 500, 1000, 5000, 25000 };
        for (int j = 0;j<5;j++)
        {
            if (Inventory.Rune[j] >= xpCost[i] / b[j] && xpCost[i] % b[j] == 0)
            {
                Inventory.Rune[j] -= xpCost[i] / b[j];
                return true;
            }
        }
        int suma = xpCost[i];

        int[] c = { 0, 0, 0, 0, 0 };
        for(int j = 4;j>= 0;j--)
        {
            if(b[j] <= suma)
            {
                int k = suma / b[j];
                if(Inventory.Rune[j] >= k)
                {
                    c[j] = k;
                    suma -= b[j] * k;
                }
            }
        }
        if(suma == 0)
        {
            for(int h = 0;h<5;h++)
            {
                Inventory.Rune[h] -= c[h];
            }
            return true;
        }

        return false;
    }


    public void IncepeTimerul()
    {
        counter = counterMax;
        genS.StopCoroutine("CountFrag");
        genS.StartCoroutine("CountFrag");

    }

    public void Select(int i)
    {
        int a = Spatiu();
        if (a != -1)
        {
            if (Inventory.Rune[i] >= runeCost[i])
            {
                if (calculXP(i))
                {
                    tipulSelectat[a] = i;
                    spatiiOcupate[a] = 1;
                    for(int j = 0;j<5;j++)
                    {
                        if(i != j) allim.transform.GetChild(a).GetChild(j).gameObject.SetActive(false);
                        else allim.transform.GetChild(a).GetChild(j).gameObject.SetActive(true);
                    }
                    if(a == 0)
                    {
                        isWorking = true;
                        IncepeTimerul();
                    }
                }
                else Debug.Log("n ai suficient XP");
            }
            else Debug.Log("Nu ai suficiente rune");
        }
        else Debug.Log("Nu ai spatiu");
    }

    public void Calculeaza()
    {
        if (isWorking)
        {
            if (counter <= 0)
            {
                Primeste(0);
                Muta(0);
                int c = Math.Abs(counter);
                int a = c / counterMax;
                if (a != 0)
                {
                    for (int i = 0; i < a; i++)
                    {
                        if (spatiiOcupate[0] == 1)
                        {
                            Primeste(i);
                            Muta(i);
                        }
                    }
                    if (spatiiOcupate[0] == 1)
                    {
                        counter = counterMax - (c - (a * counterMax));
                    }
                    else
                    {
                        isWorking = false;
                        genS.StopCoroutine("Count");
                        counter = 0;
                        counterText.text = string.Empty;
                        slider.value = 0;
                    }
                }
                else
                {
                    if (spatiiOcupate[0] == 1)
                    {
                        counter = counterMax - (c - (a * counterMax));
                    }
                    else
                    {
                        isWorking = false;
                        genS.StopCoroutine("CountFrag");
                        counter = 0;
                        counterText.text = string.Empty;
                        slider.value = 0;
                    }
                }
            }
        }
    }
    public void Muta(int k)
    {

        for (int i = k; i < 3; i++)
        {
            if (spatiiOcupate[i + 1] == 1)
            {
                spatiiOcupate[i] = 1;
                tipulSelectat[i] = tipulSelectat[i + 1];
            }
            else
            {
                spatiiOcupate[i] = 0;
                tipulSelectat[i] = -1;
                break;
            }
        }
        spatiiOcupate[3] = 0;
        tipulSelectat[3] = -1;

        Im();
    }
    public void Im()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                allim.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
            }
            if(tipulSelectat[i] != -1)
            allim.transform.GetChild(i).GetChild(tipulSelectat[i]).gameObject.SetActive(true);
        }
    }

    public int nrPuteri = 30;
    public int[] claim = new int[100];
    public void Primeste(int k)
    {
        // k -> spatiu
        int puterea = UnityEngine.Random.Range(0, nrPuteri);
        for(int i = 0;i<100;i++)
        {
            if(claim[i] == -1)
            {
                claim[i] = puterea;
                break;
            }
        }
        claimText.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        nrDeClaimText.text = "x" + nrDeClaim();
    }
    public void ClaimF()
    {
        if (claim[0] != -1)
        {
            Inventory.NrPuteriFrag[claim[0]]++;

            claim[0] = -1;
            for (int i = 0; i < 99; i++)
            {
                if (claim[i + 1] != -1)
                {
                    claim[i] = claim[i + 1];
                }
                else
                {

                    claim[i] = -1;
                    break;
                }
            }
            if(claim[0] == -1)
            {
                claimText.gameObject.GetComponent<Image>().color = new Color32(63, 63, 63, 255);
                nrDeClaimText.text = string.Empty;
            } else
            {
                nrDeClaimText.text = "x" + nrDeClaim();
            }
        }
    }
    public int nrDeClaim()
    {
        int a = 0;
        for(int i = 0;i<100;i++)
        {
            if (claim[i] != -1) a++;
            else break;
        } 
        return a;
    }



    public void X(int i)
    {
        Muta(i);
        if(spatiiOcupate[0] == 0)
        {
            isWorking = false;
            genS.StopCoroutine("CountFrag");
            counter = 0;
            counterText.text = string.Empty;
        }
    }
    public int[] costSpatiu = new int[2];
    public GameObject PanouConfirm;
    public GameObject Lacat1;
    public GameObject Lacat2;
    public GameObject x1;
    public GameObject x2;
    int iSpatiu;
    public void TryBuySpatiu(int i)
    {
        iSpatiu = i;
        PanouConfirm.gameObject.SetActive(true);
    }

    public void ConfirmBuySpatiu()
    {
        if (iSpatiu == 2) BuySpatiu2();
        if (iSpatiu == 3) BuySpatiu3();
    }
    public void CancelBuySpatiu()
    {
        PanouConfirm.gameObject.SetActive(false);
    }
    public void BuySpatiu2() // pt frag
    {
        if(Inventory.gems >= costSpatiu[0])
        {
            Inventory.gems -= costSpatiu[0];
            Lacat2.gameObject.SetActive(false);
            x2.gameObject.SetActive(true);
            spatiiDeblocate[2] = 1;
            PanouConfirm.gameObject.SetActive(false);
        }
        else Debug.Log("N ai destui bani");
    }
    public void BuySpatiu3() // pt frag
    {
        if (Inventory.gems >= costSpatiu[1])
        {
            Inventory.gems -= costSpatiu[1];
            Lacat1.gameObject.SetActive(false);
            x1.gameObject.SetActive(true);
            spatiiDeblocate[3] = 1;
            PanouConfirm.gameObject.SetActive(false);
        }
        else Debug.Log("N ai destui bani");
    }
    public void StartOn()
    {
        Calculeaza();

        Im();

        if (claim[0] == -1)
        {
            claimText.gameObject.GetComponent<Image>().color = new Color32(63, 63, 63, 255);
            nrDeClaimText.text = string.Empty;
        }
        else
        {
            claimText.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            nrDeClaimText.text = "x" + nrDeClaim();
        }

        if (spatiiDeblocate[2] == 1)
        {
            Lacat2.gameObject.SetActive(false);
            x2.gameObject.SetActive(true);
        }
        else
        {
            Lacat2.gameObject.SetActive(true);
            x2.gameObject.SetActive(false);
        }
        if (spatiiDeblocate[3] == 1)
        {
            Lacat1.gameObject.SetActive(false);
            x1.gameObject.SetActive(true);
        }
        else
        {
            Lacat1.gameObject.SetActive(true);
            x1.gameObject.SetActive(false);
        }
        PanouConfirm.gameObject.SetActive(false);
        panouFrag.gameObject.SetActive(true);

        me.StartOn();
    
    }
    private void OnApplicationQuit()
    {
        DateTime dateQuit = DateTime.Now;
        PlayerPrefs.SetString("dateQuit2", dateQuit.ToString());
        Debug.Log("quit");
    }

    public int g = 0;
    private void Start()
    {
        if(g == 0)
        {
            g++;
            for (int i = 0;i< claim.Length;i++)
            {
                claim[i] = -1;
            }

            g++;
        }
        
        
        if (isWorking)
        {
            genS.StartCoroutine("CountFrag");

            string dateQuitString = PlayerPrefs.GetString("dateQuit2", "");
            if (!dateQuitString.Equals(""))
            {
                DateTime dateQuit = DateTime.Parse(dateQuitString);
                DateTime dateNow = DateTime.Now;

                if (dateNow > dateQuit)
                {
                    TimeSpan timespan = dateNow - dateQuit;
                    counter -= (int)timespan.TotalSeconds;
                }

                PlayerPrefs.SetString("dateQuit2", "");
            }

            
        }

        slider.minValue = 0;
        slider.maxValue = counterMax;
        Calculeaza();
        StartOn();



    }

    public sterglafinal slf;
    public bool test;
    private void Update()
    {

        if (test)
        {
            slf.x();
            test = false;
        }
    }
}
