public class Intelligent_Type : DigiTypes
{
    public override float GetCombat(float combatPoints)
    {
        return combatPoints * 0.5f;
    }

    public override float GetEnginner(float ngneerPoints)
    {
        return ngneerPoints * 1.8f;
    }

    public override float GetFarming(float farmPoints)
    {
        return farmPoints * 0.4f;
    }

    public override float GetIntelligence(float intPoints)
    {
        return intPoints * 2.5f;
    }

    public override float GetMining(float miningPoints)
    {
        return miningPoints * 0.8f;
    }
}