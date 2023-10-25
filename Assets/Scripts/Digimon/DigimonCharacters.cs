using UnityEngine;

public class DigimonCharacters
{
    public string name;
    public float combatPoints;
    public float farmPoints;
    public float miningPoints;
    Sprite digiSprite;

    public void SetSprite(Sprite digiSprite)
    {
        this.digiSprite = digiSprite;
    }
}