using UnityEngine;

/*Esta es la clase de la base del jugador, aquí se encuentra todo lo que requiere la base*/
public class Base : MonoBehaviour
{
    //Propiedades de la base al iniciar el juego
    float health = 1000;
    float nrg = 40;
    public float nrgprscnd = 1;
    float food = 10;
    public float foodprscnd = 0;
    float nuts = 0;
    public float nutsprscnd = 0;

    [SerializeField] private bool isEnemy;
    
    //GameObject que despliega un menú para utilizar funciones de la base
    public GameObject Menu;

    //Empieza la base en nivel 1
    int level = 1;

    /*Función Update se utiliza únicamente para añadir recursos por segundo y actualizar la UI*/
    void Update()
    {
        nrg += nrgprscnd * Time.deltaTime;
        food += foodprscnd * Time.deltaTime;
        nuts += nutsprscnd * Time.deltaTime;
        GameManager.instance.UpdateUIResources();
    }

    //Función pública para mejorar la cantidad de energía por segundo
    public void EnergyUpgrade(float newNrg)
    {
        nrgprscnd += newNrg;
    }

    //Función pública para mejorar la cantidad de comida por segundo
    public void FoodUpgrade(float newFood)
    {
        foodprscnd += newFood;
    }

    //Función pública para mejorar la cantidad de tuercas por segundo
    public void NutsUpgrade(float newNuts)
    {
        nutsprscnd += newNuts;
    }

    //Función pública para aumentar el nivel de la base
    public void LevelUp()
    {
        level++;
        Levels();
    }

    //Esta función establece las mejoras cuando subes de nivel
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

    //Retorna la cantidad actual de comida
    public float GetFood()
    {
        return food;
    }

    //Retorna la cantidad actual de tuercas
    public float GetNuts()
    {
        return nuts;
    }

    //Retorna la cantidad actual de energía
    public float GetEnergy()
    {
        return nrg;
    }

    //Establece la cantidad de energia
    public void SetEnergy(float energy)
    {
        nrg = energy;
    }

    //Establece la cantidad de tuercas
    public void SetNuts(float nuts)
    {
        this.nuts = nuts;
    }

    //Establece la cantidad de comida
    public void SetFood(float food)
    {
        this.food = food;
    }

    public float GetConsumeFood()
    {
        float totalFood = 0;
        
        if (isEnemy)
        {
            DigimonAI[] digimons = FindObjectsOfType<DigimonAI>();
            foreach (DigimonAI digi in digimons)
            {
                totalFood += digi.consumeFood;
            }
        }
        else
        {
            DigimonObject[] digimons = FindObjectsOfType<DigimonObject>();
            foreach (DigimonObject digi in digimons)
            {
                totalFood += digi.consumeFood;
            }
        }
        
        return totalFood;
    }

    public void MakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
