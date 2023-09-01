using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public Efecte efecte;
    public CharacterBuyUp car;
    public Equip equip;
    public miniEquip mn;

    public int nrCar;


    
    public SpecificulNivelelor.SpecificLevel[] specificlevel= new SpecificulNivelelor.SpecificLevel[10];


    
    public void PlayLevel()
    {
        efecte.DefaultSet();

        Set();

        efecte.ResetCumArVeni();

    }

    public void Set()
    {
        nrCar = car.nrCarDeblocate;
        efecte.Runda = 1;
        efecte.NrBoti = nrCar + specificlevel[LevelManager.level].NrMafie;



        for (int i = 0; i < 25; i++)
        {
            efecte.Categorie[i] = -1;
            efecte.Ordinea[i] = -1;
            for (int j = 0; j < 25; j++)
            {
                efecte.CePutereBasicAi[j, i] = -1;
            }
        }
        for (int i = 0; i < 10; i++)
        {
            efecte.CePutereRandomAi[i] = -1;
            efecte.CeItemAi[i] = -1;
        }
        for (int i = 0; i < efecte.NrBoti; i++)
        {
            if (i < nrCar) efecte.Categorie[i] = 0;
            else efecte.Categorie[i] = 1;
        }

        for (int i = 0; i < nrCar; i++)
        {
            for (int j = 0; j < 2; j++) // 2 spatii pt puteri basic exista
            {
                if (equip.PuterileBotilor[i, j] != -1)
                {
                    efecte.CePutereBasicAi[i, j] = equip.PuterileBotilor[i, j];
                }
            }
            if (equip.PuterileBotilor[i, 2] != -1)
                efecte.CePutereRandomAi[i] = equip.PuterileBotilor[i, 2];

            if (equip.PuterileBotilor[i, 4] != -1)
                efecte.CeItemAi[i] = equip.PuterileBotilor[i, 4];

        }
        if (efecte.CeFragAi.Length == mn.puteriEchipate.Length && efecte.CeFragAi.GetType() == mn.puteriEchipate.GetType())
            efecte.CeFragAi = mn.puteriEchipate;

    }
    public GameObject abilityPanou;
    public void OpenAbility()
    {
        abilityPanou.gameObject.SetActive(true);
    }

    private void Start()
    {
       

    }
    



 

}
