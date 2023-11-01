using UnityEngine;
using UnityEngine.UI;

/*Esta clase es para crear los Builds (Edificios), van en la UI*/
public class Build_Card : MonoBehaviour
{
    //Variable para obtener la build a crear
    Buildings build;

    //Variable para saber que build crear, se establece como pública porque se modifica en inspector
    public string buildName;
    //Variable para guardar la referencia del Sprite del card, se establece en automático
    public Sprite sprite;

    // Al iniciar, obtiene la referencia al Sprite, crea las configuraciones del card según la build,
    // Establece el sprite y por último establece los recursos a consumir según el card.
    void Start()
    {
        sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        SetBuilding(buildName);
        SetSprite();
        build.SetCardResource();
    }

    //Función para establecer el build a crear según el nombre de buildName
    void SetBuilding(string buildingName)
    {
        switch(buildingName)
        {
            case "Farm": build = new Farm_Building(); break;
            case "Gym": build = new Gym_Building(); break;
            case "Mine": build = new Mine_Building(); break;
        }
    }

    //Función para establecer el sprite a la card
    void SetSprite()
    {
        build.SetSprite(sprite);
    }
    
    //Función pública cuando se selecciona dicha Card, esta tirará un mensaje de error en caso
    //de que al jugador no cuente con los recursos suficientes. En caso de que si, establecerá
    //la build a instanciar en Build Manager
    public void ChooseBuild()
    {
        if (!build.CanBuild())
        {
            GameManager.instance.ActivateAlert("No te alcanza pa");
            return;
        }
        BuildManager.Instance.SetBuildInfo(build);
        SetBuilding(buildName);
        SetSprite();
    }
}