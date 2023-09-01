using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class gen
{
    public static string InK(double x)
    {
        if (x > 1000000000000000)
        {
            var exponentSci = Math.Floor(Math.Log10(x));
            var exponentEng = 3 * Math.Floor(exponentSci / 3);
            var letterOne = ((char)Math.Floor(((double)exponentEng - 3) / 3 % 26 + 93)).ToString();

            if ((double)exponentEng / 3 >= 27)
            {
                var letterTwo = ((char)(Math.Floor(((double)exponentEng - 3 * 26) / (3 * 26)) % 26 + 97)).ToString();
                return (x / Math.Pow(10, exponentEng)).ToString("F0") + letterTwo + letterOne;
            }
            if (x >= 1000000000000000)
                return (x / Math.Pow(10, exponentEng)).ToString("F0") + letterOne;
            return x.ToString("F0");
        }
        else if (x >= 1000000000000)
        {
            return (x / 1000000000000).ToString("F0") + "T";
        }
        else if (x >= 1000000000)
        {
            return (x / 1000000000).ToString("F0") + "B";
        }
        else if (x >= 1000000)
        {
            return (x / 1000000).ToString("F0") + "M";
        }
        else if (x >= 1000)
        {
            return (x / 1000).ToString("F0") + "K";
        }
        else if (x < 1000)
        {
            return x.ToString("F0");
        }

        return "";

    }

    public static IEnumerator DupaOsecundaOff(GameObject a)
    {
        yield return new WaitForSeconds(1f); // Pauză de o secundă
        // Codul funcției care trebuie executat după o secundă
        a.gameObject.SetActive(false);

    }

    public static void loadbool(ref bool a, string key)
    {

    }
    public static void savebool(ref bool a, string key)     // pentru salvare
    {
        PlayerPrefs.SetInt(key, a ? 1 : 0);

    }


}
