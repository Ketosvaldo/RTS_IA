public class Mining_Type : DigiTypes
{
    public override float GetCombat(float combatPoints)
    {
        return combatPoints * 1.2f;
    }

    public override float GetFarming(float farmPoints)
    {
        return farmPoints * 0.7f;
    }

    public override float GetMining(float miningPoints)
    {
        return miningPoints * 2.5f;
    }
}