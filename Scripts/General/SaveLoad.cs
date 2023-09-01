using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public miniEquip me;
    public Fragments frag;
    public transform trans;


    private void Awake()
    {
        Inventory.LoadData();
        me.LoadData();
        frag.LoadData();
        trans.LoadData();
    }

    private void OnApplicationQuit()
    {
        Inventory.SaveData();
        me.SaveData();
        frag.SaveData();
        trans.SaveData();
    }
}
