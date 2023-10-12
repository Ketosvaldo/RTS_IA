public class Enginner_Type : DigiTypes
{
    public override float GetCombat(float combatPoints)
    {
        return combatPoints;
    }

    public override float GetEnginner(float ngneerPoints)
    {
        return ngneerPoints * 2.5f;
    }

    public override float GetFarming(float farmPoints)
    {
        return farmPoints * 0.5f;
    }

    public override float GetIntelligence(float intPoints)
    {
        return intPoints * 1.5f;
    }

    public override float GetMining(float miningPoints)
    {
        return miningPoints * 1.4f;
    }
}