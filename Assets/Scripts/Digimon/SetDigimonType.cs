using UnityEngine;

public class SetDigimonType : MonoBehaviour
{
    public DigimonObject digimon;

    public void SetFarmType()
    {
        digimon.SetFarmType();
        DeactivateThis();
    }

    public void SetCombatType()
    {
        digimon.SetCombatType();
        DeactivateThis();
    }

    public void SetMinerType()
    {
        digimon.SetMiningType();
        DeactivateThis();
    }

    void DeactivateThis()
    {
        gameObject.SetActive(false);
    }
}
