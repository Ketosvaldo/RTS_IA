using UnityEngine;

//Clase que sirve para controlar la build a crear, as� como manejar las props de dichas builds para
//poder ser instanciadas despu�s
public class BuildManager : MonoBehaviour
{
    //Se declara un static para ser llamadod desde cualquier script sin tener una referencia directa
    public static BuildManager Instance;
    //Aqui se agrega en inspecto el prefab "BuildingsObject" para ser instanciado
    public GameObject buildingGO;
    //Con este guardamos el build a construir
    Buildings props;

    private void Awake()
    {
        Instance = this;
    }

    //Funci�n para guardar las props del build a construir, en caso de que el par�metro build sea nulo,
    //no hace nada, pero si no, entonces consume los recursos necesarios para construir dicha build
    public void SetBuildInfo(Buildings build)
    {
        props = build;
        if (props == null)
            return;
        props.ConsumeResource();
    }

    //Retorna la referencia de build para obtener sus props, se declara como p�blica para llamarse desde cualquier lugar
    public Buildings GetBuildInfo()
    {
        return props;
    }

    //Retorna el prefab a utilizar, se declara como p�blica para llamarse desde cualquier lugar
    public GameObject GetPrefab()
    {
        return buildingGO;
    }
}
