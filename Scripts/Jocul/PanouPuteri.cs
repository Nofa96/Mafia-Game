using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanouPuteri : MonoBehaviour
{
    public Efecte efecte;
    public Manager man;
    public CaractereJoc carjoc;

    public GameObject[] allAbility = new GameObject[50];

    public Transform AbilityParinte;

    public GameObject[] abilitati = new GameObject[25];
    
    public int abilitateaDeschisa;

    public GameObject TargetsParinte;
    public GameObject targetPrefab;

    public int indexuPuteriiInPanou;

    public GameObject[] targeturi = new GameObject[25];
    public int[] targeturiint = new int[25];
    public string[] targeturiName = new string[25];
    public int indextargeturi = 0;


    public void StergeTargets()
    {
        for(int i = 0;i<25;i++)
        {
            if (targeturi[i] != null)
                DestroyObject(targeturi[i]);
            targeturiint[i] = 0;
        }
    }
    public void SpawnTargets(int a, int b)
    {
        for (int i = 0; i < efecte.NrBoti; i++)
        {
            if (efecte.Categorie[i] == a && efecte.EfectMort[i] != b)
            {
                targeturiint[indextargeturi] = i;
                targeturi[indextargeturi] = Instantiate(targetPrefab, TargetsParinte.transform.position, Quaternion.identity, TargetsParinte.transform);
                targeturi[indextargeturi].gameObject.SetActive(true);
                if (a == 1)
                {
                    targeturi[indextargeturi].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = man.specificlevel[LevelManager.level].NumeCar[i];
                    targeturiName[indextargeturi] = man.specificlevel[LevelManager.level].NumeCar[i];
                }
                else
                {
                    targeturi[indextargeturi].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = CharacterBuyUp.carName[i];
                    targeturiName[indextargeturi] = CharacterBuyUp.carName[i];
                }
                indextargeturi++;
            }
        }
    }

    // 0 - mafioti doar // 1 - team // 2 - toti - mafia primii // 3 - toti - townii primii  // 4 mortii 
    public void OpenPanou(int ModulAfisare)
    {
        // deschide panoul.
        StergeTargets();
        indextargeturi = 0;
        if(ModulAfisare == 2)
        {
            SpawnTargets(1,1);
            SpawnTargets(0,1);
        }
        else if(ModulAfisare == 3)
        {
            SpawnTargets(0,1);
            SpawnTargets(1,1);
        }
        else if(ModulAfisare == 0)
        {
            SpawnTargets(1,1);
        }
        else if(ModulAfisare == 1)
        {
            SpawnTargets(0,1);
        }
        else if(ModulAfisare == 4)
        {
            SpawnTargets(0, 0);
            SpawnTargets(1, 0);
        }
        

    }

    public string[] tagPutere = new string[5];
    public int tipulPuterii;

    public void targetButton(GameObject a)
    {
        int childIndex = a.transform.GetSiblingIndex();
        childIndex--;

        GameObject foundObject = Findgameobject(abilitati[abilitateaDeschisa], tagPutere[tipulPuterii]);
        if (foundObject != null)
        {
            foundObject.GetComponent<TextMeshProUGUI>().text = targeturiName[childIndex];
        }

        // pun tinta in efecte.tinte // tinta[botul, abilitateaDeschisa] = targeturiint[childIndex];
        

    }
    public void takeIndexAbility(GameObject a) // atasata la butonul de select la abilitate
    {
        abilitateaDeschisa = a.transform.GetSiblingIndex();
    }
    public GameObject Findgameobject(GameObject parent, string tag)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).tag == tag)
            {
                return parent.transform.GetChild(i).gameObject;
            }
            GameObject child = Findgameobject(parent.transform.GetChild(i).gameObject, tag);
            if (child != null)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void StergeAbilitati()
    {
        for(int i = 0;i<25;i++)
        {
            if(abilitati[i] != null)
            DestroyObject(abilitati[i]);
        }
    }
    public void SpawnAbility(int botul)
    {
        StergeAbilitati();
        for(int i = 0;i<5;i++)
        {
            efecte.CePutereBasicAiRundaAsta[botul, i] = Random.RandomRange(0,5+1);
        }
        for(int i = 0;i < 25 && efecte.CePutereBasicAiRundaAsta[botul,i] != -1;i++)
        {
            abilitati[i] = Instantiate(allAbility[efecte.CePutereBasicAiRundaAsta[botul, i]], AbilityParinte.position, Quaternion.identity, AbilityParinte);
        }
    }

    private void Start()
    {
      //  SpawnAbility(1);
      //  OpenPanou(2);
    }

}
