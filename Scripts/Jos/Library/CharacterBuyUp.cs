using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class CharacterBuyUp : MonoBehaviour
{

    public int[] carLevel = new int[10];
    public int[] carDeblocat = new int[10];
    public int nrCarDeblocate = 5;
    public double[] carCostBaniBuy = new double[5];
    public TextMeshProUGUI[] numeText = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] levelText = new TextMeshProUGUI[10];
    public string[] numeCar = new string[10];

    public GameObject panouConfirmBuy;
    public TextMeshProUGUI confirmText;

    public GameObject[] culori = new GameObject[10];

    public Scrollbar srolbar;

    public int j;

    public static string[] carName = {"name1","name2","name3", "name4", "name5", "name6", "name7", "name8", "name9","name10"};

    public void Start()
    {


        for(int i = 0;i<10;i++)
        {
            if (carDeblocat[i] != 1)
            {
                numeText[i].text = numeCar[i] + " $" + gen.InK(carCostBaniBuy[i-5]);
            }
            else numeText[i].text = numeCar[i];
        }
        for(int i = 0;i<10;i++)
        {
            Culori(i);
        }
        for (int i = 0; i < 10; i++) levelText[i].text = "" + carLevel[i];

        StartOn();
    }

    public void StartOn()
    {
        srolbar.value = 0;
    }

    public void Culori(int a)
    {
        for(int i = 0;i<6;i++)
        {
            culori[a].transform.GetChild(i).gameObject.SetActive(false);
        }
        culori[a].transform.GetChild(carLevel[a]).gameObject.SetActive(true);
    }
    public void ClickPanouButton(int j)
    {
        this.j = j;
        if (carDeblocat[j] != 1)
        {
            panouConfirmBuy.gameObject.SetActive(true);
            confirmText.text = "Are you sure you want to buy this character for $" + gen.InK(carCostBaniBuy[j-5]);
        }
        else
        {
            upgradePan.gameObject.SetActive(true);
            for (int i = 0; i < 10; i++) caractere.transform.GetChild(i).gameObject.SetActive(false);
            caractere.transform.GetChild(j).gameObject.SetActive(true);

            UpgradeONst();
        }
    }


    public void ConfirmPanouOF()
    {
        panouConfirmBuy.gameObject.SetActive(false);
    }
    public void BuyCar()
    {
        if(carDeblocat[j] != 1)
        {
            if(Inventory.coins >= carCostBaniBuy[j-5])
            {
                panouConfirmBuy.gameObject.SetActive(false);
                Inventory.coins -= carCostBaniBuy[j -5];
                carDeblocat[j] = 1;
                nrCarDeblocate++;
                carLevel[j] = 1;
                numeText[j].text = numeCar[j];
                levelText[j].text = "" + carLevel[j];
            }
        }
    }


    //////////////////////////////////////// upgrade
    public TextMeshProUGUI upgradeButtonText;
    public GameObject upgradePan;
    public GameObject caractere;
    public Slider slider;
    public TextMeshProUGUI currentXPtext;
    public TextMeshProUGUI maxXPtext;
    public TextMeshProUGUI currentLeveltext;
    public TextMeshProUGUI nextLeveltext;
    public TextMeshProUGUI initialXP;
    public TextMeshProUGUI textXP;

    public double[] costXpPerLevel = new double[5];
    public double[] xpCurent = new double[10];
    public double[] costBaniUpPerLevel = new double[5];
    

    public void UpgradeONst()
    {
        slider.minValue = 0;
        if (carLevel[j] < 5)
        {
            textXP.gameObject.SetActive(true);
            maxXPtext.gameObject.SetActive(true);
            currentLeveltext.gameObject.SetActive(true);
            nextLeveltext.gameObject.SetActive(true);
            initialXP.gameObject.SetActive(true);
            slider.maxValue = (int)costXpPerLevel[carLevel[j]];
            slider.value = (int)xpCurent[j];
            upgradeButtonText.text = "UPGRADE $" + costBaniUpPerLevel[carLevel[j]];
            currentXPtext.text = "" +  xpCurent[j];
            maxXPtext.text = "" + costXpPerLevel[carLevel[j]];
            currentLeveltext.text = "" + carLevel[j];
            nextLeveltext.text = "" + (carLevel[j] + 1);
        }
        else
        {
            slider.maxValue = 1;
            slider.value = 1;
            upgradeButtonText.text = "MAX LEVEL";
            currentXPtext.text = "MAX LEVEL";
            maxXPtext.gameObject.SetActive(false);
            currentLeveltext.gameObject.SetActive(false);
            nextLeveltext.gameObject.SetActive(false);
            initialXP.gameObject.SetActive(false);
            textXP.gameObject.SetActive(false);
        }
    }


    public int[] xpADDint = new int[] { 100, 500, 1000, 5000, 25000 };

    public void AddXP(int i)
    {
        if (carLevel[j] < 5)
        {
            if (xpCurent[j] < costXpPerLevel[carLevel[j]])
            {
                if (Inventory.XP[i] >= 1)
                {
                    xpCurent[j] += xpADDint[i];
                    slider.value = (int)xpCurent[j];
                    currentXPtext.text = "" + xpCurent[j];
                    Inventory.XP[i] -= 1;
                }
            }
        }
    }

    
    public void Upgrade()
    {
        if (carLevel[j] < 5)
        {
            if (Inventory.coins >= costBaniUpPerLevel[carLevel[j]] && xpCurent[j] >= costXpPerLevel[carLevel[j]])
            {
                carLevel[j]++;
                Inventory.coins -= costBaniUpPerLevel[carLevel[j] - 1];
                levelText[j].text = "" + carLevel[j];
                xpCurent[j] = 0;
                Culori(j);
                UpgradeONst();
            }
        }
    }

    public void ExitUpgrade()
    {
        upgradePan.gameObject.SetActive(false);
    }

}
