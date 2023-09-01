using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Powers : MonoBehaviour
{
    public buyPower bp;

    public GameObject panou;


    public GameObject buyPanel;

    public GameObject parintePuteri;
    public int[] powersCumparate = new int[13];

    public GameObject[] BnBAll = new GameObject[3]; // butoanele pt bought not bought all 

    public int ii;

    public GameObject rayCastBlock;

    public GameObject rayCastBlock2;
    private void Start()
    {
        StartON();
    }
    public void StartON()
    {
        BoughtNotBought(true);
        ApasatSauNuBnBAll(0);

        rayCastBlock2.gameObject.SetActive(false);
        filtruStare = 0;
    }

    public void OpenAbility(int i)
    {
        rayCastBlock2.gameObject.SetActive(true);
        StartCoroutine(gen.DupaOsecundaOff(rayCastBlock2));

        ii = i;
        bp.StartOpen();
        OpenBuyInfo();
    }


    public void OpenBuyInfo()
    {
        Animator anim = buyPanel.GetComponent<Animator>();
        bool a = anim.GetBool("openbuy");
        if (a == true)
        {
            rayCastBlock.gameObject.SetActive(true);
        }
        else
        {
            rayCastBlock.gameObject.SetActive(false);
        }
        anim.SetBool("openbuy", !a);

    }

    public int filtruStare = 0;
    public void BoughtNotBought(bool a)
    {
        for (int i = 0; i < bp.nr_ability; i++)
        {
            if (powersCumparate[i] == 1) parintePuteri.transform.GetChild(i).gameObject.SetActive(a);
            else parintePuteri.transform.GetChild(i).gameObject.SetActive(!a);
        }
    }

    public void All()
    {
        for (int i = 0; i < bp.nr_ability; i++)
        {
            parintePuteri.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ApasatSauNuBnBAll(int i)
    {
        filtruStare = i;
        for (int k = 0; k < 3; k++) BnBAll[k].GetComponent<Image>().color = new Color32(95, 95, 95, 255);
        BnBAll[i].GetComponent<Image>().color = new Color32(65, 65, 65, 255);
    }



}
