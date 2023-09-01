using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class buyAbility : MonoBehaviour
{

    public AbilitiesLibrary ab;


    public Slider slider;

    public int[] runeCost = new int[50];
    public double[] xpNecesar = new double[50];
    public double[] xpCurent = new double[50];
    public int[] TipulPuterii = new int[50];


    public TextMeshProUGUI currentXPtext;
    public TextMeshProUGUI maxXPtext;
    public TextMeshProUGUI RuneRequestText;
    public TextMeshProUGUI xpRequestText;
    public GameObject parinteImagine;

    public Image[] imageRune = new Image[5];

    public GameObject buttonBuy;

    int i;



    public void StartOpen()
    {
        i = ab.ii;
        currentXPtext.text = "" + gen.InK(xpCurent[i]);
        maxXPtext.text = "" + gen.InK(xpNecesar[i]);
        OffToateImaginile();
        parinteImagine.transform.GetChild(i).gameObject.SetActive(true);
        imageRune[TipulPuterii[i]].gameObject.SetActive(true);
        slider.minValue = 0;
        slider.value = (int)xpCurent[i];
        slider.maxValue = (int)xpNecesar[i];
        if (ab.puteriCumparate[i] == 1) { 
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
        for (int i = 0; i < 50; i++)
        {
            if (parinteImagine.transform.GetChild(i).gameObject.activeSelf)
            {
                parinteImagine.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < 5; i++) imageRune[i].gameObject.SetActive(false);
    }


    public int[] xpADDint = new int[]{100, 500, 1000, 5000, 25000};
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

    public void Buy()
    {
        if (xpCurent[i] >= xpNecesar[i])
        {
            if (Inventory.Rune[TipulPuterii[i]] >= runeCost[i])
            {
                ab.OpenBuyInfo();
                ab.puteriCumparate[i] = 1;
                Inventory.Rune[TipulPuterii[i]] -= runeCost[i];


                if (ab.filtruStare == 0) ab.BoughtNotBought(true);
                else if (ab.filtruStare == 1) ab.BoughtNotBought(false);
                else ab.All();
            }
            else Debug.Log("Nu ai destule Rune");
        }
        else Debug.Log("Nu ai destul XP");
       }



}
