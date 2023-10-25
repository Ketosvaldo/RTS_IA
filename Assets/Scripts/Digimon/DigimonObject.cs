using UnityEngine;

public class DigimonObject : MonoBehaviour
{
    public float combatPoints;
    public float enginneerPoints;
    public float farmPoints;
    public float intelligentPoints;
    public float miningPoints;
    SpriteRenderer spriteRenderer;
    public DigiTypes type;
    public int level;
    public float exp = 0;
    public float maxExp = 10;
    // Update is called once per frame
    //void Update()
    //{

    //}

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
    public void SetCombatType()
    {
        type = new Combat_Type();
        SetStats();
    }
    public void SetMiningType()
    {
        type = new Mining_Type();
        SetStats();
    }
    public void SetFarmType()
    {
        type = new Farm_Type();
        SetStats();
    }

    public void ExpGained(float expEarned)
    {
        exp += expEarned;
        if(exp >= maxExp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level += 1;
        exp = 0;
        maxExp *= 2;
    }

    void SetStats()
    {
        combatPoints = type.GetCombat(combatPoints);
        farmPoints = type.GetFarming(farmPoints);
        miningPoints = type.GetMining(miningPoints);
    }
}
