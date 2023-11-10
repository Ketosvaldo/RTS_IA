using System.Collections;
using UnityEngine;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour
{
    //Se declara como static para poder ser llamado desde cualquier codigo sin tener una referencia
    public static GameManager instance;
    
    //Referencias p�blicas de la UI de los recursos 
    [Header("Recursos UI")]
    public TextMeshProUGUI FoodUI;
    public TextMeshProUGUI NutsUI;
    public TextMeshProUGUI EnergyUI;
    //Referencia p�blica del mensaje de Alerta
    [Header("Alerta UI")]
    public TextMeshProUGUI AlertUI;
    //Referencia p�blica de la UI que representa los costos de construir una Build
    [Header("Costos de Builds UI")] 
    public TextMeshProUGUI FarmEnergyCostUI;
    public TextMeshProUGUI MineEnergyCostUI;
    public TextMeshProUGUI GymEnergyCostUI;
    public TextMeshProUGUI GymNutsCostUI;
    [Header("Lista de Digimon")]
    public GameObject DigimonList;
    public GameObject DigimonListAttack;
    [Header("Tipos de Digimon")]
    public GameObject DigimonType;
    //Referencia de la base del jugador
    Base playerBase;
    Base enemyBase;

    //Cosas
    DigimonObject actualDigimon;



    //Al iniciar, obtendr� la referencia de la base del jugador en autom�tico.
    private void Awake()
    {
        instance = this;
        playerBase = GameObject.FindGameObjectWithTag("MyBase").GetComponent<Base>();
        enemyBase = GameObject.FindGameObjectWithTag("EnemyBase").GetComponent<Base>();
        Application.targetFrameRate = 60;
    }

    //Funci�n p�blica para iniciar una Corrutina desde scripts que no heredan de Monobehaviour
    public void StartChildCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }

    /*
    Actualiza los valores de los recursos con los que contamos, se establece como p�blica para ser llamada desde
    otro script
    */
    public void UpdateUIResources()
    {
        FoodUI.text = ((int)playerBase.GetFood()).ToString();
        NutsUI.text = ((int)playerBase.GetNuts()).ToString();
        EnergyUI.text = ((int)playerBase.GetEnergy()).ToString();
    }

    //Funci�n p�blica que sirve para consumir el recurso energ�a con un par�metro que determina la cantidad de
    //energ�a a consumir
    public void ConsumeEnergy(float energy, bool isEnemy = false)
    {
        if (isEnemy)
        {
            if (enemyBase.GetEnergy() - energy < 0)
                return;
            enemyBase.SetEnergy(enemyBase.GetEnergy() - energy);
            Debug.Log("No mames borrate alv");
            return;
        }
        if (playerBase.GetEnergy() - energy < 0)
            return;
        playerBase.SetEnergy(playerBase.GetEnergy() - energy);
    }

    //Funci�n p�blica que sirve para consumir el recurso comida con un par�metro que determina la cantidad de
    //comida a consumir
    public void ConsumeFood(float food, bool isEnemy = false)
    {
        if (isEnemy)
        {
            if (enemyBase.GetFood() - food < 0)
                return;
            enemyBase.SetFood(enemyBase.GetFood() - food);
            return;
        }
        if (playerBase.GetFood() - food < 0)
            return;
        playerBase.SetFood(playerBase.GetFood() - food);
    }

    //Funci�n p�blica que sirve para consumir el recurso tuercas con un par�metro que determina la cantidad de
    //tuercas a consumir
    public void ConsumeNuts(float nuts, bool isEnemy = false)
    {
        if (isEnemy)
        {
            if (enemyBase.GetNuts() - nuts < 0)
                return;
            enemyBase.SetNuts(enemyBase.GetNuts() - nuts);
            return;
        }
        if (playerBase.GetNuts() - nuts < 0)
            return;
        playerBase.SetNuts(playerBase.GetNuts() - nuts);
    }

    //Funci�n p�blica que sirve para activar un mensaje en UI para el jugador, donde dicho mensaje se escribe
    //como par�metro
    public void ActivateAlert(string message)
    {
        AlertUI.gameObject.SetActive(true);
        AlertUI.text = message;
        Invoke("DeactivateAlert", 3f);
    }

    //Funci�n que retorna verdadero o falso para verificar que contamos con la cantidad de energ�a necesaria
    //para realizar un proceso
    public bool CheckEnergy(float energy, bool isEnemy = false)
    {
        if (isEnemy)
            return enemyBase.GetEnergy() - energy > 0;
        return playerBase.GetEnergy() - energy > 0;
    }

    //Funci�n que retorna verdadero o falso para verificar que contamos con la cantidad de tuercas necesaria
    //para realizar un proceso
    public bool CheckNuts(float nuts, bool isEnemy = false)
    {
        if (isEnemy)
            return enemyBase.GetNuts() - nuts > 0;
        return playerBase.GetNuts() - nuts > 0;
    }

    void DeactivateAlert()
    {
        AlertUI.gameObject.SetActive(false);
    }

    //Funci�n que sirve para establecer los recursos que requiere construir una granja en la card
    public void SetFarmCardResource(float energyResource)
    {
        FarmEnergyCostUI.text = ((int)energyResource).ToString();
    }

    //Funci�n que sirve para establecer los recursos que requiere construir una mina en la card
    public void SetMineCardResource(float energyResource)
    {
        MineEnergyCostUI.text = ((int)energyResource).ToString();
    }

    //Funci�n que sirve para establecer los recursos que requiere construir un gimnasio en la card
    public void SetGymCardResource(float energyResource, float nutsResource)
    {
        GymEnergyCostUI.text = ((int)energyResource).ToString();
        GymNutsCostUI.text = ((int)nutsResource).ToString();
    }

    //Funci�n p�blica para obtener la referencia a la base del jugador
    public Base GetBase()
    {
        return playerBase;
    }

    public Base GetEnemyBase()
    {
        return enemyBase;
    }

    public void SetDigimonList(bool state)
    {
        DigimonList.SetActive(state);
    }

    public void SetDigimonListAttack(bool state, DigimonObject _digimon)
    {
        actualDigimon = _digimon;
        DigimonListAttack.SetActive(true);
    }

    public void SetBuildToAssignDigimon(Build_Object build)
    {
        DigimonList.GetComponent<DigimonListActive>().buildToAssign = build;
    }
    
    public void SetDigimonTypeGO(bool state, DigimonObject _digimon)
    {
        DigimonType.SetActive(state);
        DigimonType.GetComponent<SetDigimonType>().digimon = _digimon;
    }

    public DigimonObject GetActualDigimon()
    {
        return actualDigimon;
    }

    public void SetActualDigimon(DigimonObject _digimon)
    {
        actualDigimon = _digimon;
    }
}