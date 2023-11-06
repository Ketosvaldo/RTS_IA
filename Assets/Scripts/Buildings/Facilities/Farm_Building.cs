using System.Collections;
using UnityEngine;

/*Clase heredada de Buildings, para saber que hace cada función, checar la clase Buildings*/

public class Farm_Building : Buildings
{
    float foodUpg = 1;
    int level = 1;
    DigimonObject[] farmingDigimons = new DigimonObject[2];
    float delay = 30;
    float expEarned = 2;
    float energyRequired = 20f;
    Sprite buildSprite;
    float health = 1000;
    bool isDeath;

    public override void LevelUpBuild()
    {
        if (level == 4)
            return;
        level += 1;
        Levels();
    }

    public override void StatUPG()
    {
        foreach(DigimonObject digimon in farmingDigimons)
        {
            if(digimon != null)
            GameManager.instance.GetBase().FoodUpgrade(digimon.farmPoints * 0.03f);
        }
    }

    public override void Levels()
    {
        switch (level)
        {
            case 2: health = 2000; delay = 45; expEarned = 4; foodUpg += 1; farmingDigimons = new DigimonObject[4] { farmingDigimons[0], farmingDigimons[1], null, null }; break;
            case 3: health = 4000; delay = 60; expEarned = 8; foodUpg += 2; farmingDigimons = new DigimonObject[6] { farmingDigimons[0], farmingDigimons[1], farmingDigimons[2], farmingDigimons[3], null, null }; break;
            case 4: health = 8000; delay = 80; expEarned = 12; foodUpg += 4; farmingDigimons = new DigimonObject[8] { farmingDigimons[0], farmingDigimons[1], farmingDigimons[2], farmingDigimons[3], farmingDigimons[4], farmingDigimons[5], null, null }; break;
        }
        StatUPG();
    }
    public override string AssignDigimon(DigimonObject digimonToAssign)
    {
        for(int i = 0; i < farmingDigimons.Length; i++)
        {
            if (farmingDigimons[i] == null)
            {
                farmingDigimons[i] = digimonToAssign;
                GameManager.instance.StartChildCoroutine(ActivateDelay(digimonToAssign, i));
                GameManager.instance.GetBase().FoodUpgrade(digimonToAssign.farmPoints * 0.03f);
                farmingDigimons[i] = null;
                return "Tu " + digimonToAssign.GetName() + " ahora esta cosechando.";
            }
        }
        return "No tienes espacio";
    }

    public override IEnumerator ActivateDelay(DigimonObject trainedDigimon, int arrayIndex)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.ActivateAlert("Tu " + trainedDigimon.GetName() + " ha terminado :D");
        GameManager.instance.GetBase().FoodUpgrade(-trainedDigimon.farmPoints * 0.03f);
        trainedDigimon.farmPoints += foodUpg;
        trainedDigimon.ExpGained(expEarned);
        trainedDigimon.SetBusy(false);
        farmingDigimons[arrayIndex] = null;
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

    public override int GetLevel()
    {
        return level;
    }

    public override void DamageHandler(float damage)
    {
        health -= damage;
        if (health <= 0)
            isDeath = true;
    }

    public override bool IsDeath()
    {
        return isDeath;
    }
    
    public override float GetHealth()
    {
        return health;
    }
}