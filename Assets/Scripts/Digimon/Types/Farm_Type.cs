public class Farm_Type : DigiTypes
{
    public override float GetCombat(float combatPoints)
    {
        return combatPoints * 0.5f;
    }

    public override float GetEnginner(float ngneerPoints)
    {
        return ngneerPoints * 1f;
    }

    public override float GetFarming(float farmPoints)
    {
        return farmPoints * 2.5f;
    }

    public override float GetIntelligence(float intPoints)
    {
        return intPoints * 0.6f;
    }

    public override float GetMining(float miningPoints)
    {
        return miningPoints * 1.5f;
    }
}