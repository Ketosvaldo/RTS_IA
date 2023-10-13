using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym_Building : Buildings
{
    int level = 1;
    DigimonObject[] trainingDigimons = new DigimonObject[3];
    float combatUpg = 3;
    float delay = 30;
    float expEarned = 2;
    Sprite sprite;

    public override string AssignDigimon(DigimonObject digimonToAssign)
    {
        for (int i = 0; i < 2; i++)
        {
            if (trainingDigimons[i] == null)
            {
                trainingDigimons[i] = digimonToAssign;
                GameManager.instance.StartChildCoroutine(ActivateDelay(digimonToAssign));
                return "Tu " + digimonToAssign.name + " ahora está entrenando.";
            }
        }
        return "No tienes espacio";
    }

    public override void Levels()
    {
        switch (level)
        {
            case 2: expEarned = 3; delay = 40; combatUpg = 3; trainingDigimons = new DigimonObject[4] { trainingDigimons[0], trainingDigimons[1], null, null }; break;
            case 3: expEarned = 5; delay = 40; combatUpg = 6; trainingDigimons = new DigimonObject[6] { trainingDigimons[0], trainingDigimons[1], trainingDigimons[2], trainingDigimons[3], null, null }; break;
            case 4: expEarned = 8; delay = 40; combatUpg = 12; trainingDigimons = new DigimonObject[8] { trainingDigimons[0], trainingDigimons[1], trainingDigimons[2], trainingDigimons[3], trainingDigimons[4], trainingDigimons[5], null, null }; break;
        }
    }

    public override void LevelUpBuild()
    {
        level += 1;
        Levels();
    }

    public override void StatUPG()
    {
        return;
    }

    public override IEnumerator ActivateDelay (DigimonObject trainedDigimon)
    {
        yield return new WaitForSeconds(delay);
        trainedDigimon.combatPoints += combatUpg;
        trainedDigimon.ExpGained(expEarned);
    }

    public override void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public override Sprite GetSprite()
    {
        return sprite;
    }

    public override void ConsumeResource()
    {
        throw new System.NotImplementedException();
    }
}