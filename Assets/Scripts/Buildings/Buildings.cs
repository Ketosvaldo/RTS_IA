using System.Collections;
using UnityEngine;

public abstract class Buildings
{
    public abstract void StatUPG();
    public abstract void LevelUpBuild();
    public abstract void Levels();
    public abstract bool CanBuild();
    public abstract void ConsumeResource();
    public abstract string AssignDigimon(DigimonObject digimonToAssign);
    public abstract IEnumerator ActivateDelay(DigimonObject trainedDigimon);
    public abstract void SetSprite(Sprite sprite);
    public abstract Sprite GetSprite();
}