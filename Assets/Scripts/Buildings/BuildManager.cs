using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public GameObject buildingGO;
    Buildings props;

    private void Awake()
    {
        Instance = this;
    }

    public void SetBuildInfo(Buildings build)
    {
        props = build;
        props.ConsumeResource();
    }

    public Buildings GetBuildInfo()
    {
        return props;
    }

    public GameObject GetPrefab()
    {
        return buildingGO;
    }
}
