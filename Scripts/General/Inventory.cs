using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class Inventory
{

    public static double coins;
    public static int gems;

    public static int[] Rune = new int[5];
    public static int[] XP = new int[5]; // 100 | 500 | 1000 | 5000 | 25000 | 

    public static int[] NrPuteriFrag = new int[30];


    [Serializable]
    private class InventoryData
    {
        public double coins;
        public int gems;

        public int[] Rune;
        public int[] XP; // 100 | 500 | 1000 | 5000 | 25000 | 

        public int[] NrPuteriFrag;
    }

    public static void SaveData()
    {
        InventoryData data = new InventoryData();
        data.Rune = Rune;
        data.XP = XP;
        data.coins = coins;
        data.gems = gems;
        data.NrPuteriFrag = NrPuteriFrag;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/inventory_data.json", json);
    }

    public static void LoadData()
    {
        string path = Application.persistentDataPath + "/inventory_data.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);

            Rune = data.Rune;
            XP = data.XP;
            coins = data.coins;
            gems = data.gems;
            NrPuteriFrag = data.NrPuteriFrag;
        }
    }



    /*
    // common - 8
    public static int Block_2;
    public static int Block_2_;

    
    public int LuckySpy;
    public int SpyD_3;
    public int Guardian;
    public int Cupidon_3;
    public int Inspect; // vezi daca are cienva block de la tine. 
    public int Redirect_4;
    // Rare - 13
    public int Protect;
    public int Protect_;
    public int Block;
    public int FollowSpy;
    public int Reflect_2;
    public int BountyHunter;
    public int DoubleProtect_2;
    public int Redirect_3;
    public int Rogue_3;
    public int Genjutsu_3;
    public int BridKill;
    public int DridKill;
    public int Arest_2_10;
    // Epic - 9
    public int Arest_1_10;
    public int Target_Follow_Spy;
    public int AntiBlock;
    public int AntiRedirect;
    public int Kill__1;
    public int ShieldPiercing;
    public int Sensory;
    public int Rid_Kill_2;
    public int Karma;
    // Speciale - 9
    public int Redirect;
    public int RidKill;
    public int Copy;
    public int Duplicate;
    public int Deflect;
    public int Rogue;
    public int Mirage;
    public int Cupidon;
    public int chameleon;
    // Legendary - 17
    public int DoubleProtect;
    public int Chose;
    public int Revive__1;
    public int Arest_1_20;
    public int RevengeBlock;
    public int AntiBR;
    public int SpyExact_2;
    public int SpyAbility;
    public int Genjutsu;
    public int Swap_Ability;
    public int Swap_Target;
    public int Replace;
    public int Reflect;
    public int Hid_Rid;
    public int Trap;
    public int Kill;
    public int Kill_;

    */
}