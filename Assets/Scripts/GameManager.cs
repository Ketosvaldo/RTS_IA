using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Se declara como static para poder ser llamado desde cualquier código sin tener una referencia
    public static GameManager instance;
    
    //Referencias públicas de la UI de los recursos 
    [Header("Recursos UI")]
    public TextMeshProUGUI FoodUI;
    public TextMeshProUGUI NutsUI;
    public TextMeshProUGUI EnergyUI;
    //Referencia pública del mensaje de Alerta
    [Header("Alerta UI")]
    public TextMeshProUGUI AlertUI;
    //Referencia pública de la UI que representa los costos de construir una Build
    [Header("Costos de Builds UI")] 
    public TextMeshProUGUI FarmEnergyCostUI;
    public TextMeshProUGUI MineEnergyCostUI;
    public TextMeshProUGUI GymEnergyCostUI;
    public TextMeshProUGUI GymNutsCostUI;
    //Referencia de la base del jugador
    Base playerBase;

    //Al iniciar, obtendrá la referencia de la base del jugador en automático.
    private void Awake()
    {
        instance = this;
        playerBase = GameObject.FindGameObjectWithTag("MyBase").GetComponent<Base>();
    }

    //Función pública para iniciar una Corrutina desde scripts que no heredan de Monobehaviour
    public void StartChildCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }

    /*
    Actualiza los valores de los recursos con los que contamos, se establece como pública para ser llamada desde
    otro script
    */
    public void UpdateUIResources()
    {
        FoodUI.text = ((int)playerBase.GetFood()).ToString();
        NutsUI.text = ((int)playerBase.GetNuts()).ToString();
        EnergyUI.text = ((int)playerBase.GetEnergy()).ToString();
    }

    //Función pública que sirve para consumir el recurso energía con un parámetro que determina la cantidad de
    //energía a consumir
    public void ConsumeEnergy(float energy)
    {
        if (playerBase.GetEnergy() - energy < 0)
            return;
        playerBase.SetEnergy(playerBase.GetEnergy() - energy);
    }

    //Función pública que sirve para consumir el recurso comida con un parámetro que determina la cantidad de
    //comida a consumir
    public void ConsumeFood(float food)
    {
        if (playerBase.GetFood() - food < 0)
            return;
        playerBase.SetFood(playerBase.GetFood() - food);
    }

    //Función pública que sirve para consumir el recurso tuercas con un parámetro que determina la cantidad de
    //tuercas a consumir
    public void ConsumeNuts(float nuts)
    {
        if (playerBase.GetNuts() - nuts < 0)
            return;
        playerBase.SetNuts(playerBase.GetNuts() - nuts);
    }

    //Función pública que sirve para activar un mensaje en UI para el jugador, donde dicho mensaje se escribe
    //como parámetro
    public void ActivateAlert(string message)
    {
        AlertUI.gameObject.SetActive(true);
        AlertUI.text = message;
        Invoke("DeactivateAlert", 3f);
    }

    //Función que retorna verdadero o falso para verificar que contamos con la cantidad de energía necesaria
    //para realizar un proceso
    public bool CheckEnergy(float energy)
    {
        return playerBase.GetEnergy() - energy > 0;
    }

    //Función que retorna verdadero o falso para verificar que contamos con la cantidad de tuercas necesaria
    //para realizar un proceso
    public bool CheckNuts(float nuts)
    {
        return playerBase.GetNuts() - nuts > 0;
    }

    void DeactivateAlert()
    {
        AlertUI.gameObject.SetActive(false);
    }

    //Función que sirve para establecer los recursos que requiere construir una granja en la card
    public void SetFarmCardResource(float energyResource)
    {
        FarmEnergyCostUI.text = ((int)energyResource).ToString();
    }

    //Función que sirve para establecer los recursos que requiere construir una mina en la card
    public void SetMineCardResource(float energyResource)
    {
        MineEnergyCostUI.text = ((int)energyResource).ToString();
    }

    //Función que sirve para establecer los recursos que requiere construir un gimnasio en la card
    public void SetGymCardResource(float energyResource, float nutsResource)
    {
        GymEnergyCostUI.text = ((int)energyResource).ToString();
        GymNutsCostUI.text = ((int)nutsResource).ToString();
    }

    //Función pública para obtener la referencia a la base del jugador
    public Base GetBase()
    {
        return playerBase;
    }
}
