public class Farm_Type : DigiTypes
{
    public override float GetCombat(float combatPoints)
    {
        return combatPoints * 0.5f;
    }

    public override float GetFarming(float farmPoints)
    {
        return farmPoints * 2.5f;
    }

    public override float GetMining(float miningPoints)
    {
        return miningPoints * 1.5f;
    }
}