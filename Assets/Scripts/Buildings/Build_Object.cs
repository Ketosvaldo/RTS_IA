using UnityEngine;

/*Este ser� el objeto a instanciar in-game seg�n la build elegida*/

public class Build_Object : MonoBehaviour
{
    public Sprite sprite;
    //Variable para guardar el nivel de nuestra Build
    public int level;
    Buildings build;

    //Funci�n p�blica que sirve para establecer las props del build elegido.
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

    //Funci�n p�blica que sirve para aumentar el nivel del build
    public void LevelUpBuild()
    {
        build.LevelUpBuild();
    }

    //Funci�n p�blica que sirve para asignar a un Digimon a dicha build
    public void AssignDigimon(DigimonObject digimonToAssign)
    {
        build.AssignDigimon(digimonToAssign);
    }
}