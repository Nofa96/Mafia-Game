using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaractereJoc : MonoBehaviour
{
    public Manager man;
    public PanouPuteri pp;

    public int nr_car;

    public GameObject[] caractere = new GameObject[10];

    public GameObject nextButton;
    public GameObject previousButton;

    public int botul;
    public void Start()
    {
        nr_car = man.nrCar;
        StartOn();
    }
    public void StartOn()
    {
        previousButton.gameObject.SetActive(false);
        botul = 0;
        for (int i = 1; i < 10; i++) caractere[i].gameObject.SetActive(false);
        caractere[0].gameObject.SetActive(true);
    }
    public void Next()
    {
        caractere[botul].gameObject.SetActive(false);
        botul++;
        caractere[botul].gameObject.SetActive(true);
        if (botul == 1) previousButton.gameObject.SetActive(true);
        if (botul == nr_car-1) nextButton.gameObject.SetActive(false);

        Schimbari(botul);
    }

    public void Previous()
    {
        caractere[botul].gameObject.SetActive(false);
        botul--;
        caractere[botul].gameObject.SetActive(true);
        if (botul == 0) previousButton.gameObject.SetActive(false);
        if (botul == nr_car-2) nextButton.gameObject.SetActive(true);

        Schimbari(botul);
    }

    public void Schimbari(int botul)
    {
        pp.SpawnAbility(botul);
    }
}
