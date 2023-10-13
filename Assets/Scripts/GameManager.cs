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
}
