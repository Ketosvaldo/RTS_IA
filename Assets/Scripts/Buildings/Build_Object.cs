using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Object : MonoBehaviour
{
    public Sprite sprite;
    public int level;
    Buildings build;

    public void SetBuild(Buildings newBuild)
    {
        build = newBuild;
        SetObjectProps();
    }

    void SetObjectProps()
    {
        sprite = build.GetSprite();
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void LevelUpBuild()
    {
        build.LevelUpBuild();
    }

    public void AssignDigimon(DigimonObject digimonToAssign)
    {
        build.AssignDigimon(digimonToAssign);
    }
}