public class Combat_Type : DigiTypes
{
    public override float GetCombat(float combatPoints)
    {
        return combatPoints * 2.5f;
    }

    public override float GetFarming(float farmPoints)
    {
        return farmPoints * 0.5f;
    }

    public override float GetMining(float miningPoints)
    {
        return miningPoints * 1.6f;
    }
}