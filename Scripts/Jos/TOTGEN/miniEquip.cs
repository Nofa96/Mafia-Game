using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class miniEquip : MonoBehaviour
{

    public int[] spatiiDeblocate = new int[3];
    public int[] puteriEchipate = new int[3];

    public GameObject[] puteriIcon = new GameObject[3];

    public GameObject nrPuteriFragText; 

    public TextMeshProUGUI selectText;

    public GameObject panouFrag;
    public GameObject Panou;

    public GameObject bgFrag;

    public GameObject[] lacata = new GameObject[2];
    public GameObject[] xuri = new GameObject[2];

    public GameObject PanouConfirmEquip;

    public Color32 color;

    [Serializable]
    private class MiniEquipData
    {
        public int[] spatiiDeblocate;
        public int[] puteriEchipate;


    }

    public void SaveData()
    {
        MiniEquipData data = new MiniEquipData();
        data.spatiiDeblocate = spatiiDeblocate;
        data.puteriEchipate = puteriEchipate;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/miniEquip_data.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/miniEquip_data.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MiniEquipData data = JsonUtility.FromJson<MiniEquipData>(json);
            puteriEchipate = data.puteriEchipate;
            spatiiDeblocate = data.spatiiDeblocate;
        }
    }




    public void OpenPanou(int k)
    {
        if (spatiiDeblocate[k] == 1)
        {
            indexSpatiu = k;

            panouFrag.gameObject.SetActive(false);
            Panou.gameObject.SetActive(true);
            for (int i = 0; i < 30; i++)
            {
                nrPuteriFragText.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = "x" + Inventory.NrPuteriFrag[i];
            }
            for (int i = 0; i < 30; i++)
            {
                if (Inventory.NrPuteriFrag[i] == 0) nrPuteriFragText.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 99, 99, 255);
                else nrPuteriFragText.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);

            }
            for (int i = 0; i < 30; i++)
            {
                bgFrag.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color32(89, 89, 89, 255);
            }
            selectText.GetComponent<TextMeshProUGUI>().color = new Color32(97, 97, 97, 255);
            indexPutere = -1;
        }
    }

    int indexSpatiu;
    int indexPutere;
    public void SelectFrag(int i)
    {
        if(indexPutere != -1)
        {
            bgFrag.transform.GetChild(indexPutere).gameObject.GetComponent<Image>().color = new Color32(89, 89, 89, 255);
        }
        indexPutere = i;
        selectText.GetComponent<TextMeshProUGUI>().color = new Color32(207, 207, 207, 255);

        bgFrag.transform.GetChild(indexPutere).gameObject.GetComponent<Image>().color = new Color32(95, 108, 90, 255);

    }
    public void SelectFragButton()
    {
        if (indexPutere != -1)
        {
            int a = Inventory.NrPuteriFrag[indexPutere];
            int b = 1;
            for (int i = 0; i < 3; i++)
                if (puteriEchipate[i] == indexPutere && i != indexSpatiu)
                    b++;

            Debug.Log("a: " + a + "    b: " + b);
            if (a >= b)
            {
                if (puteriEchipate[indexSpatiu] != -1)
                {
                    puteriIcon[indexSpatiu].transform.GetChild(puteriEchipate[indexSpatiu]).gameObject.SetActive(false);
                }
                puteriEchipate[indexSpatiu] = indexPutere;
                puteriIcon[indexSpatiu].transform.GetChild(indexPutere).gameObject.SetActive(true);
                selectText.GetComponent<TextMeshProUGUI>().color = new Color32(97, 97, 97, 255);
                Panou.gameObject.SetActive(false);
                panouFrag.gameObject.SetActive(true);


            }
        }
    }
    public void Exit()
    {
        Panou.gameObject.SetActive(false);
        panouFrag.gameObject.SetActive(true);

    }

    public void StartOn()
    {
        for (int i = 0; i < 3; i++)
            if (puteriEchipate[i] != -1)
                puteriIcon[i].transform.GetChild(puteriEchipate[i]).gameObject.SetActive(true);
        for (int i = 0; i < 2; i++)
            if (spatiiDeblocate[i + 1] != 1)
            {
                lacata[i].gameObject.SetActive(true);
                xuri[i].gameObject.SetActive(false);
            }
            else
            {
                lacata[i].gameObject.SetActive(false);
                xuri[i].gameObject.SetActive(true);
            }
        PanouConfirmEquip.gameObject.SetActive(false);
    }
    int indexBuySpace;
    public void TryToBuySpace(int i)
    {
        indexBuySpace = i;
        PanouConfirmEquip.gameObject.SetActive(true);
    }
    public int[] priceSpace = new int[2];
    public void ConfirmBuySpace()
    {
        if (spatiiDeblocate[indexBuySpace] != 1)
        {
            if (Inventory.gems >= priceSpace[indexBuySpace-1])
            {
                Inventory.gems -= priceSpace[indexBuySpace-1];
                spatiiDeblocate[indexBuySpace] = 1;
                lacata[indexBuySpace-1].gameObject.SetActive(false);
                xuri[indexBuySpace - 1].gameObject.SetActive(true);
                PanouConfirmEquip.gameObject.SetActive(false);
            }
        }
    }
    public void CancelBuySpace()
    {
        PanouConfirmEquip.gameObject.SetActive(false);
    }

    public void X(int i)
    {
        if(puteriEchipate[i] != -1)
        {
            puteriIcon[i].transform.GetChild(puteriEchipate[i]).gameObject.SetActive(false);
        }
        puteriEchipate[i] = -1;
    }
}
