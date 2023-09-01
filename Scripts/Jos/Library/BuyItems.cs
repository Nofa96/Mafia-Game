using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuyItems : MonoBehaviour
{

    public Items it;

    public int nr_ability = 13;

    public Slider slider;

    public int[] runeCost = new int[50];
    public double[] xpNecesar = new double[50];
    public double[] xpCurent = new double[50];


    public TextMeshProUGUI currentXPtext;
    public TextMeshProUGUI maxXPtext;
    public TextMeshProUGUI RuneRequestText;
    public TextMeshProUGUI xpRequestText;
    public GameObject parinteImagine;

    public GameObject buttonBuy;

    public GameObject runeSpecial;
    public GameObject runeLegedary;

    int i;
    int tipulPuterii1 = 3;
    int tipulPuterii2 = 4;

    public void StartOpen()
    {
        i = it.ii;
        currentXPtext.text = "" + gen.InK(xpCurent[i]);
        maxXPtext.text = "" + gen.InK(xpNecesar[i]);
        OffToateImaginile();
        if (i <= 12) runeSpecial.gameObject.SetActive(true);
        else runeLegedary.gameObject.SetActive(true);
        parinteImagine.transform.GetChild(i).gameObject.SetActive(true);
        RuneRequestText.text = "" + runeCost[i];
        xpRequestText.text = "" + gen.InK(xpNecesar[i]);
        slider.minValue = 0;
        slider.value = (int)xpCurent[i];
        slider.maxValue = (int)xpNecesar[i];

        if (it.itemsCumparate[i] == 1)
        {
            buttonBuy.gameObject.SetActive(false);
            RuneRequestText.text = "0";
            xpRequestText.text = "0";
        }
        else
        {
            buttonBuy.gameObject.SetActive(true);
            RuneRequestText.text = "" + runeCost[i];
            xpRequestText.text = "" + gen.InK(xpNecesar[i]);
        }
    }

    public void OffToateImaginile()
    {
        for (int i = 0; i < nr_ability; i++)
        {
            if (parinteImagine.transform.GetChild(i).gameObject.activeSelf)
            {
                parinteImagine.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        runeSpecial.gameObject.SetActive(false);
        runeLegedary.gameObject.SetActive(false);
    }

    public int[] xpADDint = new int[] { 100, 500, 1000, 5000, 25000 };
    public void AddXP(int j)
    {
        if (xpCurent[i] < xpNecesar[i])
        {
            if (Inventory.XP[j] >= 1)
            {
                xpCurent[i] += xpADDint[j];
                slider.value = (int)xpCurent[i];
                currentXPtext.text = "" + xpCurent[i];
                Inventory.XP[j] -= 1;
            }
        }
    }
    public void plm(int tipulPuterii)
    {
        if (Inventory.Rune[tipulPuterii] >= runeCost[i])
        {
            it.OpenBuyInfo();
            it.itemsCumparate[i] = 1;
            Inventory.Rune[tipulPuterii] -= runeCost[i];



            if (it.filtruStare == 0) it.BoughtNotBought(true);
            else if (it.filtruStare == 1) it.BoughtNotBought(false);
            else it.All();
        }
        else Debug.Log("Nu ai destule Rune");
    }
    public void Buy()
    {
        if (xpCurent[i] >= xpNecesar[i])
        {
            if (i <= 12) plm(tipulPuterii1);
            else plm(tipulPuterii2);
        }
        else Debug.Log("Nu ai destul XP");
    }


}
