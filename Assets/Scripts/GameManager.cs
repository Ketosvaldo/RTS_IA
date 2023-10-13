using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Recursos UI")]
    public TextMeshProUGUI FoodUI;
    public TextMeshProUGUI NutsUI;
    public TextMeshProUGUI EnergyUI;
    [Header("Alerta UI")]
    public TextMeshProUGUI AlertUI;

    [Header("Costos de Builds UI")] 
    public TextMeshProUGUI FarmEnergyCostUI;
    public TextMeshProUGUI MineEnergyCostUI;
    public TextMeshProUGUI GymEnergyCostUI;
    public TextMeshProUGUI GymNutsCostUI;

    Base playerBase;

    private void Awake()
    {
        instance = this;
        playerBase = GameObject.FindGameObjectWithTag("MyBase").GetComponent<Base>();
    }

    public void StartChildCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }

    public void UpdateUIResources()
    {
        FoodUI.text = ((int)playerBase.GetFood()).ToString();
        NutsUI.text = ((int)playerBase.GetNuts()).ToString();
        EnergyUI.text = ((int)playerBase.GetEnergy()).ToString();
    }

    public void ConsumeEnergy(float energy)
    {
        if (playerBase.GetEnergy() - energy < 0)
            return;
        playerBase.SetEnergy(playerBase.GetEnergy() - energy);
    }

    public void ConsumeFood(float food)
    {
        if (playerBase.GetFood() - food < 0)
            return;
        playerBase.SetFood(playerBase.GetFood() - food);
    }

    public void ConsumeNuts(float nuts)
    {
        if (playerBase.GetNuts() - nuts < 0)
            return;
        playerBase.SetNuts(playerBase.GetNuts() - nuts);
    }

    public void ActivateAlert(string message)
    {
        AlertUI.gameObject.SetActive(true);
        AlertUI.text = message;
        Invoke("DeactivateAlert", 3f);
    }

    public bool CheckEnergy(float energy)
    {
        return playerBase.GetEnergy() - energy > 0;
    }

    public bool CheckNuts(float nuts)
    {
        return playerBase.GetNuts() - nuts > 0;
    }

    void DeactivateAlert()
    {
        AlertUI.gameObject.SetActive(false);
    }

    public void SetFarmCardResource(float energyResource)
    {
        FarmEnergyCostUI.text = ((int)energyResource).ToString();
    }
    
    public void SetMineCardResource(float energyResource)
    {
        MineEnergyCostUI.text = ((int)energyResource).ToString();
    }

    public void SetGymCardResource(float energyResource, float nutsResource)
    {
        GymEnergyCostUI.text = ((int)energyResource).ToString();
        GymNutsCostUI.text = ((int)nutsResource).ToString();
    }

    public Base GetBase()
    {
        return playerBase;
    }
}
