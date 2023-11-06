using UnityEngine;

/*Este ser� el objeto a instanciar in-game seg�n la build elegida*/

public class Build_Object : MonoBehaviour
{
    public Sprite sprite;
    //Variable para guardar el nivel de nuestra Build
    public int level = 1;
    
    Buildings build;
    public GameObject OptionsMenu;
    //Funci�n p�blica que sirve para establecer las props del build elegido.
    public void SetBuild(Buildings newBuild)
    {
        build = newBuild;
        SetObjectProps();
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

    //Funci�n p�blica que sirve para aumentar el nivel del build
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

    //Funci�n p�blica que sirve para asignar a un Digimon a dicha build
    public void AssignDigimon(DigimonObject digimonToAssign)
    {
        GameManager.instance.ActivateAlert(build.AssignDigimon(digimonToAssign));
    }

    public void MakeDamage(float damage)
    {
        build.DamageHandler(damage);
        if (build.IsDeath())
            Destroy(gameObject);
    }
}