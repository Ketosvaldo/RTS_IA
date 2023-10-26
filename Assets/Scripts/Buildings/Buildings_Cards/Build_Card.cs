using UnityEngine;
using UnityEngine.UI;

/*Esta clase es para crear los Builds (Edificios), van en la UI*/
public class Build_Card : MonoBehaviour
{
    //Variable para obtener la build a crear
    Buildings build;

    //Variable para saber que build crear, se establece como pública porque se modifica en inspector
    public string buildName;
    //Variable 
    public Sprite sprite;

    void Start()
    {
        sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        SetBuilding(buildName);
        SetSprite();
        build.SetCardResource();
    }

    void SetBuilding(string buildingName)
    {
        switch(buildingName)
        {
            case "Farm": build = new Farm_Building(); break;
            case "Gym": build = new Gym_Building(); break;
            case "Mine": build = new Mine_Building(); break;
        }
    }

    void SetSprite()
    {
        build.SetSprite(sprite);
    }

    public void ChooseBuild()
    {
        if (!build.CanBuild())
        {
            GameManager.instance.ActivateAlert("No te alcanza pa");
            return;
        }
        BuildManager.Instance.SetBuildInfo(build);
    }
}