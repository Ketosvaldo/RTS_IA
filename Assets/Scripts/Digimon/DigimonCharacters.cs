using UnityEngine;

//Clase genérica que guarda las propiedades del Digimon
public class DigimonCharacters
{
    public string name;
    public float combatPoints;
    public float farmPoints;
    public float miningPoints;
    public float DigiCost;
    Sprite digiSprite;

    public void SetSprite(Sprite digiSprite)
    {
        this.digiSprite = digiSprite;
    }
}