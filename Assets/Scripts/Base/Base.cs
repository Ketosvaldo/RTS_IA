using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Base : MonoBehaviour
{
    float health = 1000;
    float nrg = 40;
    float nrgprscnd = 1;
    float food = 10;
    float foodprscnd = 0;
    float nuts = 0;
    float nutsprscnd = 0;
    float actualTime;

    public GameObject Menu;

    int level = 1;

    private void Start()
    {
        actualTime = Time.deltaTime;
    }

    void Update()
    {
        nrg += nrgprscnd * actualTime;
        food += foodprscnd * actualTime;
        nuts += nutsprscnd * actualTime;
    }

    public void EnergyUpgrade(float newNrg)
    {
        nrgprscnd += newNrg;
    }

    public void FoodUpgrade(float newFood)
    {
        foodprscnd += newFood;
    }

    public void NutsUpgrade(float newNuts)
    {
        nutsprscnd += newNuts;
    }

    public void LevelUp()
    {
        level++;
        Levels();
    }

    void Levels()
    {
        switch (level)
        {
            case 2: EnergyUpgrade(1); health += 250; break;
            case 3: EnergyUpgrade(2); health += 250; break;
            case 4: EnergyUpgrade(5); health += 500; break;
            case 5: EnergyUpgrade(10); health += 1000; break;
        }
    }

    private void OnMouseDown()
    {
        Menu.SetActive(true);
    }
}
