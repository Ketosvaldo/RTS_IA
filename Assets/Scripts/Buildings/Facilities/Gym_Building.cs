using System.Collections;
using UnityEngine;

/*Clase heredada de Buildings, para saber que hace cada función, checar la clase Buildings*/

public class Gym_Building : Buildings
{
    int level = 1;
    DigimonObject[] trainingDigimons = new DigimonObject[3];
    float combatUpg = 3;
    float delay = 30;
    float expEarned = 2;
    float energyRequired = 30f;
    float nutsRequired = 15;
    Sprite sprite;
    float health = 500;
    bool isDeath;

    public override string AssignDigimon(DigimonObject digimonToAssign)
    {
        for (int i = 0; i < 2; i++)
        {
            if (trainingDigimons[i] == null)
            {
                trainingDigimons[i] = digimonToAssign;
                GameManager.instance.StartChildCoroutine(ActivateDelay(digimonToAssign, i));
                return "Tu " + digimonToAssign.name + " ahora esta entrenando.";
            }
        }
        return "No tienes espacio";
    }

    public override void Levels()
    {
        switch (level)
        {
            case 2: health = 1000; expEarned = 3; delay = 40; combatUpg += 3; trainingDigimons = new DigimonObject[4] { trainingDigimons[0], trainingDigimons[1], null, null }; break;
            case 3: health = 2000; expEarned = 5; delay = 40; combatUpg += 6; trainingDigimons = new DigimonObject[6] { trainingDigimons[0], trainingDigimons[1], trainingDigimons[2], trainingDigimons[3], null, null }; break;
            case 4: health = 4000; expEarned = 8; delay = 40; combatUpg += 12; trainingDigimons = new DigimonObject[8] { trainingDigimons[0], trainingDigimons[1], trainingDigimons[2], trainingDigimons[3], trainingDigimons[4], trainingDigimons[5], null, null }; break;
        }
    }

    public override void LevelUpBuild()
    {
        if (level == 4)
            return;
        level += 1;
        Levels();
    }

    public override void StatUPG()
    {
        return;
    }

    public override IEnumerator ActivateDelay (DigimonObject trainedDigimon, int arrayIndex)
    {
        yield return new WaitForSeconds(delay);
        trainedDigimon.combatPoints += combatUpg;
        trainedDigimon.ExpGained(expEarned);
        GameManager.instance.ActivateAlert("Tu " + trainedDigimon.GetName() + " termino de entrenar :D");
        trainedDigimon.SetBusy(false);
        trainingDigimons[arrayIndex] = null;
    }

    public override void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }

    public override void SetCardResource()
    {
        GameManager.instance.SetGymCardResource(energyRequired, nutsRequired);
    }

    public override void ConsumeResource()
    {
        GameManager.instance.ConsumeEnergy(energyRequired);
        GameManager.instance.ConsumeNuts(nutsRequired);
    }

    public override bool CanBuild()
    {
        return GameManager.instance.CheckEnergy(energyRequired) && GameManager.instance.CheckNuts(nutsRequired);
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