using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyDupaStart : MonoBehaviour
{

    public Manager man;
    public Equip equip;
    private void Awake()
    {
        // Manager
        int[] NrPuteriPerCar = {1,2,3,4,5};
        int[,] PuterileCar = { {1,2 },{1,1},{0,3 },{4,5},{0,5} };

        man.specificlevel[0] = new SpecificulNivelelor.SpecificLevel(5,NrPuteriPerCar,PuterileCar,CharacterBuyUp.carName);
        man.specificlevel[1] = new SpecificulNivelelor.SpecificLevel(5, NrPuteriPerCar, PuterileCar, CharacterBuyUp.carName);
        man.specificlevel[2] = new SpecificulNivelelor.SpecificLevel(5, NrPuteriPerCar, PuterileCar, CharacterBuyUp.carName);

        // Equip
        equip.FunctiaDistrugatoare();



        Destroy(gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
