using System.Collections;
using UnityEngine;

/*Clase heredada de Buildings, para saber que hace cada función, checar la clase Buildings*/

public class Mine_Building : Buildings
{
    int level = 1;
    float delay = 30;
    float nutsUpg = 3;
    float expEarned = 20;
    DigimonObject[] miningDigimon = new DigimonObject[2];
    DigimonAI[] digimonAI = new DigimonAI[2];
    Sprite sprite;
    float energyRequired = 30f;
    float health = 700;
    bool isDeath;
    float nutsCost = 100;

    public override void LevelUpBuild(bool isEnemy = false)
    {
        if (isEnemy)
        {
            if (!GameManager.instance.CheckNuts(nutsCost))
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
        //GameManager.instance.GetBase().NutsUpgrade(nutsUpg);
    }

    public override void Levels()
    {
        switch (level)
        {
            case 2: nutsCost = 300; health = 1400; delay = 45; nutsUpg = 3; expEarned = 40; 
                miningDigimon = new DigimonObject[4] { miningDigimon[0], miningDigimon[1], null, null };
                digimonAI = new DigimonAI[4] { digimonAI[0], digimonAI[1], null, null }; break;
            case 3: nutsCost = 700; health = 2800; delay = 60; nutsUpg = 6; expEarned = 80; 
                miningDigimon = new DigimonObject[6] { miningDigimon[0], miningDigimon[1], miningDigimon[2], miningDigimon[3], null, null };
                digimonAI = new DigimonAI[6] { digimonAI[0], digimonAI[1], digimonAI[2], digimonAI[3], null, null }; break;
            case 4: health = 5600; delay = 75; nutsUpg = 12; expEarned = 160;
                miningDigimon = new DigimonObject[8] { miningDigimon[0], miningDigimon[1], miningDigimon[2], miningDigimon[3], miningDigimon[4], miningDigimon[5], null, null };
                digimonAI = new DigimonAI[8] { digimonAI[0], digimonAI[1], digimonAI[2], digimonAI[3], digimonAI[4], digimonAI[5], null, null }; break;
        }
        //StatUPG();
    }

    public override string AssignDigimon(DigimonObject digimonToAssign)
    {
        for (int i = 0; i < miningDigimon.Length; i++)
        {
            if (miningDigimon[i] == null)
            {
                digimonToAssign.SetBusy(true);
                miningDigimon[i] = digimonToAssign;
                GameManager.instance.StartChildCoroutine(ActivateDelay(digimonToAssign, i));
                GameManager.instance.GetBase().NutsUpgrade(digimonToAssign.miningPoints * 0.03f * nutsUpg);
                return "Tu " + digimonToAssign.name + " ahora esta minando.";
            }
        }
        return "No tienes espacio";
    }

    public override IEnumerator ActivateDelay(DigimonObject trainedDigimon, int arrayIndex)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.ActivateAlert("Tu " + trainedDigimon.GetName() + " ha terminado :D");
        GameManager.instance.GetBase().NutsUpgrade(-trainedDigimon.miningPoints * 0.03f * nutsUpg);
        trainedDigimon.farmPoints += nutsUpg;
        trainedDigimon.ExpGained(expEarned);
        trainedDigimon.SetBusy(false);
        miningDigimon[arrayIndex] = null;
    }

    public override IEnumerator ActivateDelayAI(DigimonAI trainedDigimon, int arrayIndex)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.GetEnemyBase().NutsUpgrade(-trainedDigimon.miningPoints * 0.03f * nutsUpg);
        trainedDigimon.farmPoints += nutsUpg;
        trainedDigimon.ExpGained(expEarned);
        trainedDigimon.SetBusy(false);
        digimonAI[arrayIndex] = null;
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

    public override void ConsumeResource(bool isEnemy = false)
    {
        GameManager.instance.ConsumeEnergy(energyRequired, isEnemy);
    }

    public override bool CanBuild()
    {
        return GameManager.instance.CheckEnergy(energyRequired, true);
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
                GameManager.instance.GetEnemyBase().NutsUpgrade(digimonToAssign.miningPoints * 0.03f * nutsUpg);
            }
        }
    }

    public override int GetSlotNumber()
    {
        return miningDigimon.Length;
    }
}