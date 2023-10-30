using System.Collections;
using UnityEngine;

/*Clase heredada de Buildings, para saber que hace cada función, checar la clase Buildings*/

public class Farm_Building : Buildings
{
    GameObject myBase;
    float foodUpg = 1;
    int level = 1;
    DigimonObject[] farmingDigimons = new DigimonObject[2];
    float delay = 30;
    float expEarned = 2;
    float energyRequired = 20f;
    Sprite buildSprite;

    public override void LevelUpBuild()
    {
        level += 1;
        Levels();
    }

    public override void StatUPG()
    {
        GameManager.instance.GetBase().FoodUpgrade(foodUpg);
    }

    public override void Levels()
    {
        switch (level)
        {
            case 2: delay = 45; expEarned = 4; foodUpg += 1; farmingDigimons = new DigimonObject[4] { farmingDigimons[0], farmingDigimons[1], null, null }; break;
            case 3: delay = 60; expEarned = 8; foodUpg += 2; farmingDigimons = new DigimonObject[6] { farmingDigimons[0], farmingDigimons[1], farmingDigimons[2], farmingDigimons[3], null, null }; break;
            case 4: delay = 80; expEarned = 12; foodUpg += 4; farmingDigimons = new DigimonObject[8] { farmingDigimons[0], farmingDigimons[1], farmingDigimons[2], farmingDigimons[3], farmingDigimons[4], farmingDigimons[5], null, null }; break;
        }
        StatUPG();
    }
    public override string AssignDigimon(DigimonObject digimonToAssign)
    {
        for(int i = 0; i < 2; i++)
        {
            if (farmingDigimons[i] == null)
            {
                farmingDigimons[i] = digimonToAssign;
                GameManager.instance.StartChildCoroutine(ActivateDelay(digimonToAssign));
                return "Tu " + digimonToAssign.name + " ahora est� cosechando.";
            }
        }
        return "No tienes espacio";
    }

    public override IEnumerator ActivateDelay(DigimonObject trainedDigimon)
    {
        yield return new WaitForSeconds(delay);
        trainedDigimon.farmPoints += foodUpg;
        trainedDigimon.ExpGained(expEarned);
    }

    public override void SetSprite(Sprite sprite)
    {
        buildSprite = sprite;
    }

    public override Sprite GetSprite()
    {
        return buildSprite;
    }

    public override void SetCardResource()
    {
        GameManager.instance.SetFarmCardResource(energyRequired);
    }

    public override void ConsumeResource()
    {
        GameManager.instance.ConsumeEnergy(energyRequired);
    }

    public override bool CanBuild()
    {
        return GameManager.instance.CheckEnergy(energyRequired);
    }
}