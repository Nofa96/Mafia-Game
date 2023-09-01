using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
public static class SpecificulNivelelor
{



    [Serializable]
    public class SpecificLevel
    {
        public int NrMafie;
        public int[] NrDePuteriPerCar;
        public int[,] PuterileCar;
        public string[] NumeCar;

        public SpecificLevel(int NrMafie, int[] NrDePuteriPerCar, int[,] PuterileCar,string[] NumeCar)
        {
            this.NrMafie = NrMafie;
            this.NrDePuteriPerCar = NrDePuteriPerCar;
            this.PuterileCar = PuterileCar;
            this.NumeCar = NumeCar;

        }
        public SpecificLevel()
        {
            NrMafie = 0;
            NrDePuteriPerCar = new int[0];
            PuterileCar = new int[0,0];
            NumeCar = new string[0];
        }
    }


}
