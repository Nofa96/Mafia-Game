using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class gen7 : MonoBehaviour
{
    public transform trans;
    public Fragments frag;

    IEnumerator Count()
    {
        while (true)
        {
            if (trans.isworking)
            {
                trans.counter--;
                if(trans.counter <= 0)
                {
                    trans.Calculeaza();
                }
                int minutes = trans.counter / 60;
                int seconds = trans.counter % 60;
                string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
                trans.textCounter.text = formattedTime;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CountFrag()
    {
        while (true)
        {
            if(frag.isWorking)
            {
                frag.counter--;
                if(frag.counter <= 0)
                {
                    frag.Calculeaza();
                }
                int minutes = frag.counter / 60;
                int seconds = frag.counter % 60;
                string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
                frag.counterText.text = formattedTime;
                frag.slider.value = frag.counter;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
