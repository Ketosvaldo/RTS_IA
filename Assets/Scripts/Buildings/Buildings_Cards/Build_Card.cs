using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build_Card : MonoBehaviour
{
    Buildings build;

    public string buildName;
    public Sprite sprite;

    void Start()
    {
        sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        SetBuilding(buildName);
        SetSprite();
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