using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Manager man;

    public static int nrDeLevele = 50;
    public static int level;

    public GameObject canvasJoc;
    public GameObject canvasBaza;

    public void Play()
    {
        man.PlayLevel();
        canvasBaza.gameObject.SetActive(false);
        canvasJoc.gameObject.SetActive(true);
    }



}
