using UnityEngine;
using UnityEngine.UI;

/*Este sera el objeto a instanciar in-game segun la build elegida*/

public class Build_Object : MonoBehaviour
{
    public Sprite sprite;
    //Variable para guardar el nivel de nuestra Build
    public int level = 1;
    public float vida;
    public Slider slider;
    public Gradient gradien;
    public Color color;
    public Image Fill;
    
    
    Buildings build;
    public GameObject OptionsMenu;
    //Funcion publica que sirve para establecer las props del build elegido.
    public void SetBuild(Buildings newBuild)
    {
        build = newBuild;
        SetObjectProps();
        slider.maxValue = GetHealth();
        slider.value = GetHealth();
       // Fill.color = color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            MakeDamage(25);
        }

        
    }

    public void SetHealth()
    {

        slider.value = build.GetHealth();
    }

    private void OnMouseDown()
    {
        OptionsMenu.SetActive(true);
    }

    void SetObjectProps()
    {
        sprite = build.GetSprite();
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    //Funcion publica que sirve para aumentar el nivel del build
    public void LevelUpBuild()
    {
        if(build.GetLevel() == 4)
        {
            GameManager.instance.ActivateAlert("Nivel máximo alcanzado");
            return;
        }
        build.LevelUpBuild();
        level = build.GetLevel();
    }

    //Funcion publica que sirve para asignar a un Digimon a dicha build
    public void AssignDigimon(DigimonObject digimonToAssign)
    {
        GameManager.instance.ActivateAlert(build.AssignDigimon(digimonToAssign));
    }

    public void MakeDamage(float damage)
    {
        build.DamageHandler(damage);
        SetHealth();
        if (build.IsDeath() && gameObject)
            Destroy(gameObject);
    }

    public float GetHealth()
    {
        return build.GetHealth();
    }
}