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

    public GameObject Menu;

    int level = 1;

    void Update()
    {
        nrg += nrgprscnd * Time.deltaTime;
        food += foodprscnd * Time.deltaTime;
        nuts += nutsprscnd * Time.deltaTime;
        GameManager.instance.UpdateUIResources();
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

    public float GetFood()
    {
        return food;
    }

    public float GetNuts()
    {
        return nuts;
    }

    public float GetEnergy()
    {
        return nrg;
    }

    public void SetEnergy(float energy)
    {
        nrg = energy;
    }

    public void SetNuts(float nuts)
    {
        this.nuts = nuts;
    }

    public void SetFood(float food)
    {
        this.food = food;
    }
}
