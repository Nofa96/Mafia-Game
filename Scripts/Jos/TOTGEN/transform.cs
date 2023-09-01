using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class transform : MonoBehaviour
{

    public gen7 genS;


    public GameObject selectImage;

    public TextMeshProUGUI[] nrDeRuneText = new TextMeshProUGUI[4];

    public int runeNecesare = 4;

    public bool isworking;


    
    [Serializable]
    private class TransformData
    {
        public bool isworking;
        public int runaSelectata;
        public int counter;
    }

    public void SaveData()
    {
        TransformData data = new TransformData();
        data.isworking = isworking;
        data.runaSelectata = runaSelectata;
        data.counter = counter;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/transform_data.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/transform_data.json";
        if (File.Exists(path))
        {   
            string json = File.ReadAllText(path);
            TransformData data = JsonUtility.FromJson<TransformData>(json);
            isworking = data.isworking;
            runaSelectata = data.runaSelectata;
            counter = data.counter;

        }
    }

    
    public void Calculeaza()
    {
        if (isworking == true)
        {
            Debug.Log("isworking e  true");
            if (counter <= 0)
            {
                Debug.Log("counteru a facut cel putin 1 ciclu");

                Inventory.Rune[runaSelectata] -= runeNecesare;
                Inventory.Rune[runaSelectata + 1]++;
                int c = Math.Abs(counter); // secunde trecute 
                int a = c / countermax; // cate cicluri a facut
                Debug.Log("C: " + c);
                Debug.Log("A: " + a);
                if (a != 0)
                {
                    Debug.Log("nr de cicluri: " + a);
                    for (int i = 0; i < a; i++)
                    {
                        if (Inventory.Rune[runaSelectata] >= runeNecesare)
                        {
                            Inventory.Rune[runaSelectata] -= runeNecesare;
                            Inventory.Rune[runaSelectata + 1]++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (Inventory.Rune[runaSelectata] >= runeNecesare)
                    {
                        counter = countermax - (c - (a * countermax));
                    }
                    else
                    {
                        isworking = false;
                        genS.StopCoroutine("Count");
                        counter = 0;
                        textCounter.text = string.Empty;
                    }
                }
                else
                {
                    if (Inventory.Rune[runaSelectata] >= runeNecesare)
                    {
                        counter = countermax - (c - (a * countermax));
                    }
                    else
                    {
                        isworking = false;
                        genS.StopCoroutine("Count");
                        counter = 0;
                        textCounter.text = string.Empty;
                    }
                }
            }
            else Debug.Log("counteru inca n a facut niciun ciclu");
        }
        else Debug.Log("isworking e false");
    }
    public void StartOn()
    {
        
        Calculeaza();
        
        selectImage.gameObject.SetActive(false);
        ceva.gameObject.SetActive(false);
        isSelectOpen = 0;

    }

    public void CancelTrans()
    {
        isworking = false;
        genS.StopCoroutine("Count");
        counter = 0;
        runaSelectata = -1;
        textCounter.text = string.Empty;
        OpenSelect();

    }

    int isSelectOpen = 0;
    public GameObject ceva;
    public GameObject exit;

    public void OpenSelect()
    {
        if (isSelectOpen == 0)
        {
            selectImage.gameObject.SetActive(true);
            ceva.gameObject.SetActive(true);
            exit.gameObject.SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                nrDeRuneText[i].text = "(" + Inventory.Rune[i] + ")";
            }
            isSelectOpen = 1;
        } else
        {
            selectImage.gameObject.SetActive(false);
            ceva.gameObject.SetActive(false);
            exit.gameObject.SetActive(true);
            isSelectOpen = 0;
        }
    }
    int runaSelectata;
    public void Select(int i)
    {
        if (Inventory.Rune[i] >= runeNecesare)
        {
            runaSelectata = i;
            counter = countermax;
            genS.StopCoroutine("Count");
            genS.StartCoroutine("Count");
            isworking = true;
            OpenSelect();
            string formattedTime = string.Format("{0:00}:{1:00}", counter / 60, counter % 60);
            textCounter.text = formattedTime;
        }

    }


    public Text textCounter;
    public int counter = 0;
    public int countermax = 3600;



    private void OnApplicationQuit()
    {
        DateTime dateQuit = DateTime.Now;
        PlayerPrefs.SetString("dateQuit", dateQuit.ToString());
        SaveData();
        Debug.Log("quit");
    }
    private void Awake()
    {
        LoadData();
    }
    private void Start()
    {
        isStarted = true;
        StartOn();
        if (isworking)
        {
            genS.StartCoroutine("Count");

            string dateQuitString = PlayerPrefs.GetString("dateQuit", "");
            if (!dateQuitString.Equals(""))
            {
                DateTime dateQuit = DateTime.Parse(dateQuitString);
                DateTime dateNow = DateTime.Now;

                if (dateNow > dateQuit)
                {
                    TimeSpan timespan = dateNow - dateQuit;
                    counter -= (int)timespan.TotalSeconds;
                }

                PlayerPrefs.SetString("dateQuit", "");
            }
        }
        Calculeaza();
    }
    bool isStarted = false;
    DateTime pauseDateTime;
    private void OnApplicationPause(bool pause)
    {
        if(isStarted)
        {
            if(pause)
            {
                pauseDateTime = DateTime.Now;
            } else
            {
                counter -= (int)(DateTime.Now - pauseDateTime).TotalSeconds;
            }
        }
    }

    public void Update()
    {
        SaveData();


    }




}
