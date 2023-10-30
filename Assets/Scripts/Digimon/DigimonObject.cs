using UnityEngine;

//Clase que llega el Prefab "DigimonObject", ya que ser� el Digimon a instanciar in-game
public class DigimonObject : MonoBehaviour
{
    //Todas las props del Digimon a instanciar
    public float combatPoints;
    public float farmPoints;
    public float miningPoints;
    SpriteRenderer spriteRenderer;
    public DigiTypes type;
    public int level;
    public float exp = 0;
    public float maxExp = 10;

    //Funci�n p�blica para establecer el sprite del Digimon
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
    //Funci�n p�blica para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo combate
    public void SetCombatType()
    {
        type = new Combat_Type();
        SetStats();
    }
    //Funci�n p�blica para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo minero
    public void SetMiningType()
    {
        type = new Mining_Type();
        SetStats();
    }
    //Funci�n p�blica para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo granjero
    public void SetFarmType()
    {
        type = new Farm_Type();
        SetStats();
    }

    //Funci�n p�blica que sirve para agregar experiencia al Digimon, en caso de alcanzar la experiencia m�xima, este subir� de nivel
    public void ExpGained(float expEarned)
    {
        exp += expEarned;
        if(exp >= maxExp)
        {
            LevelUp();
        }
    }

    //Funci�n que sirve para subir de nivel al Digimon
    void LevelUp()
    {
        level += 1;
        exp = 0;
        maxExp *= 2;
    }

    //Funci�n que sirve para establecer los nuevos stats del Digimon una vez seleccionado el tipo de Digimon
    void SetStats()
    {
        combatPoints = type.GetCombat(combatPoints);
        farmPoints = type.GetFarming(farmPoints);
        miningPoints = type.GetMining(miningPoints);
    }
}
