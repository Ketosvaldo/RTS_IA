using System.Collections;
using UnityEngine;

/*Clase heredada de Buildings, para saber que hace cada función, checar la clase Buildings*/

public class Mine_Building : Buildings
{
    GameObject myBase;
    int level = 1;
    float delay = 30;
    float nutsUpg = 3;
    DigimonObject[] miningDigimon = new DigimonObject[2];
    Sprite sprite;
    float energyRequired = 30f;
    public override void LevelUpBuild()
    {
        level += 1;
        Levels();
    }

    public override void StatUPG()
    {
        GameManager.instance.GetBase().NutsUpgrade(nutsUpg);
    }

    public override void Levels()
    {
        switch (level)
        {
            case 2: delay = 45; nutsUpg = 3; miningDigimon = new DigimonObject[4] { miningDigimon[0], miningDigimon[1], null, null }; break;
            case 3: delay = 60; nutsUpg = 6; miningDigimon = new DigimonObject[6] { miningDigimon[0], miningDigimon[1], miningDigimon[2], miningDigimon[3], null, null }; break;
            case 4: delay = 75; nutsUpg = 12; miningDigimon = new DigimonObject[8] { miningDigimon[0], miningDigimon[1], miningDigimon[2], miningDigimon[3], miningDigimon[4], miningDigimon[5], null, null }; break;
        }
        StatUPG();
    }

    public override string AssignDigimon(DigimonObject digimonToAssign)
    {
        for (int i = 0; i < 2; i++)
        {
            if (miningDigimon[i] == null)
            {
                miningDigimon[i] = digimonToAssign;
                GameManager.instance.StartChildCoroutine(ActivateDelay(digimonToAssign));
                return "Tu " + digimonToAssign.name + " ahora est� minando.";
            }
        }
        return "No tienes espacio";
    }

    public override IEnumerator ActivateDelay(DigimonObject trainedDigimon)
    {
        yield return new WaitForSeconds(delay);
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
        GameManager.instance.SetMineCardResource(energyRequired);
    }

    public override void ConsumeResource()
    {
        GameManager.instance.ConsumeEnergy(30f);
    }

    public override bool CanBuild()
    {
        return GameManager.instance.CheckEnergy(energyRequired);
    }
}