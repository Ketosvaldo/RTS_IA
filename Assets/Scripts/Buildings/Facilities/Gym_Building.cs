using System.Collections;
using UnityEngine;

/*Clase heredada de Buildings, para saber que hace cada función, checar la clase Buildings*/

public class Gym_Building : Buildings
{
    int level = 1;
    DigimonObject[] trainingDigimons = new DigimonObject[2];
    DigimonAI[] digimonAI = new DigimonAI[2];
    float combatUpg = 3;
    float delay = 30;
    float expEarned = 20;
    float energyRequired = 30f;
    float nutsRequired = 15;
    Sprite sprite;
    float health = 500;
    bool isDeath;
    float nutsCost = 200;

    public override string AssignDigimon(DigimonObject digimonToAssign)
    {
        for (int i = 0; i < 2; i++)
        {
            if (trainingDigimons[i] == null)
            {
                digimonToAssign.SetBusy(true);
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
            case 2: nutsCost = 400; health = 1000; expEarned = 40; delay = 40; combatUpg += 3; 
                trainingDigimons = new DigimonObject[4] { trainingDigimons[0], trainingDigimons[1], null, null };
                digimonAI = new DigimonAI[4] { digimonAI[0], digimonAI[1], null, null }; break;
            case 3: nutsCost = 700;health = 2000; expEarned = 80; delay = 40; combatUpg += 6; 
                trainingDigimons = new DigimonObject[6] { trainingDigimons[0], trainingDigimons[1], trainingDigimons[2], trainingDigimons[3], null, null };
                digimonAI = new DigimonAI[6] { digimonAI[0], digimonAI[1], digimonAI[2], digimonAI[3], null, null }; break;
            case 4: health = 4000; expEarned = 160; delay = 40; combatUpg += 12; 
                trainingDigimons = new DigimonObject[8] { trainingDigimons[0], trainingDigimons[1], trainingDigimons[2], trainingDigimons[3], trainingDigimons[4], trainingDigimons[5], null, null };
                digimonAI = new DigimonAI[8] { digimonAI[0], digimonAI[1], digimonAI[2], digimonAI[3], digimonAI[4], digimonAI[5], null, null }; break;
        }
    }

    public override void LevelUpBuild(bool isEnemy = false)
    {
        if (isEnemy)
        {
            if (!GameManager.instance.CheckNuts(nutsCost, true))
                return;
            if (level == 4)
                return;
            level += 1;
            GameManager.instance.ConsumeNuts(nutsCost, true);
            Levels();
            return;
        }
        if (!GameManager.instance.CheckNuts(nutsCost))
        {
            GameManager.instance.ActivateAlert("No te alcanza pa ");
            return;
        }

        if (level == 4)
            return;
        level += 1;
        GameManager.instance.ConsumeNuts(nutsCost);

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

    public override void ConsumeResource(bool isEnemy = false)
    {
        GameManager.instance.ConsumeEnergy(energyRequired, isEnemy);
        GameManager.instance.ConsumeNuts(nutsRequired, isEnemy);
    }

    public override bool CanBuild()
    {
        return GameManager.instance.CheckEnergy(energyRequired, true) && GameManager.instance.CheckNuts(nutsRequired, true);
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

    public override IEnumerator ActivateDelayAI(DigimonAI trainedDigimon, int arrayIndex)
    {
        yield return new WaitForSeconds(delay);
        trainedDigimon.combatPoints += combatUpg;
        trainedDigimon.ExpGained(expEarned);
        trainedDigimon.SetBusy(false);
        digimonAI[arrayIndex] = null;
    }

    public override void AssignDigimon(DigimonAI digimonToAssign)
    {
        for (int i = 0; i < digimonAI.Length; i++)
        {
            if (digimonAI[i] == null)
            {
                digimonAI[i] = digimonToAssign;
                digimonToAssign.SetBusy(true);
                digimonToAssign.attack = false;
                GameManager.instance.StartChildCoroutine(ActivateDelayAI(digimonToAssign, i));
            }
        }
    }
    
    public override int GetSlotNumber()
    {
        return trainingDigimons.Length;
    }
}