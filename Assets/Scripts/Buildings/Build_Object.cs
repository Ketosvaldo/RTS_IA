using UnityEngine;

/*Este será el objeto a instanciar in-game según la build elegida*/

public class Build_Object : MonoBehaviour
{
    public Sprite sprite;
    //Variable para guardar el nivel de nuestra Build
    public int level;
    Buildings build;

    //Función pública que sirve para establecer las props del build elegido.
    public void SetBuild(Buildings newBuild)
    {
        build = newBuild;
        SetObjectProps();
    }

    void SetObjectProps()
    {
        sprite = build.GetSprite();
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    //Función pública que sirve para aumentar el nivel del build
    public void LevelUpBuild()
    {
        build.LevelUpBuild();
    }

    //Función pública que sirve para asignar a un Digimon a dicha build
    public void AssignDigimon(DigimonObject digimonToAssign)
    {
        build.AssignDigimon(digimonToAssign);
    }
}