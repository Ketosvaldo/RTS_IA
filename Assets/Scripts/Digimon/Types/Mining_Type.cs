public class Mining_Type : DigiTypes
{
    public override float GetCombat(float combatPoints)
    {
        return combatPoints * 1.2f;
    }

    public override float GetEnginner(float ngneerPoints)
    {
        return ngneerPoints * 1.3f;
    }

    public override float GetFarming(float farmPoints)
    {
        return farmPoints * 0.7f;
    }

    public override float GetIntelligence(float intPoints)
    {
        return intPoints * 0.8f;
    }

    public override float GetMining(float miningPoints)
    {
        return miningPoints * 2.5f;
    }
}