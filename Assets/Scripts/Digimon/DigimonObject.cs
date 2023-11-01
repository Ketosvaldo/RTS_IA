using UnityEngine;

//Clase que llega el Prefab "DigimonObject", ya que será el Digimon a instanciar in-game
public class DigimonObject : MonoBehaviour
{
    //Todas las props del Digimon a instanciar
    public string digimonName;
    public float combatPoints;
    public float farmPoints;
    public float miningPoints;
    SpriteRenderer spriteRenderer;
    public DigiTypes type;
    public int level;
    public float exp = 0;
    public float maxExp = 10;

    Vector3 newPos;

    bool move;
    private void Update()
    {
        GameManager.instance.ConsumeFood(10 * Time.deltaTime);
        if (!move)
            return;
        transform.position = Vector3.MoveTowards(transform.position, newPos, 0.2f);
        if (transform.position == newPos)
            move = false;
    }

    //Función pública para establecer el sprite del Digimon
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
    //Función pública para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo combate
    public void SetCombatType()
    {
        type = new Combat_Type();
        SetStats();
    }
    //Función pública para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo minero
    public void SetMiningType()
    {
        type = new Mining_Type();
        SetStats();
    }
    //Función pública para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo granjero
    public void SetFarmType()
    {
        type = new Farm_Type();
        SetStats();
    }

    //Función pública que sirve para agregar experiencia al Digimon, en caso de alcanzar la experiencia máxima, este subirá de nivel
    public void ExpGained(float expEarned)
    {
        exp += expEarned;
        if(exp >= maxExp)
        {
            LevelUp();
        }
    }

    //Función que sirve para subir de nivel al Digimon
    void LevelUp()
    {
        level += 1;
        exp = 0;
        maxExp *= 2;
    }

    //Función que sirve para establecer los nuevos stats del Digimon una vez seleccionado el tipo de Digimon
    void SetStats()
    {
        combatPoints = type.GetCombat(combatPoints);
        farmPoints = type.GetFarming(farmPoints);
        miningPoints = type.GetMining(miningPoints);
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

    public string GetName()
    {
        return digimonName;
    }

    public void MoveToTarget(Vector3 Position)
    {
        move = true;
        newPos = Position;
    }
}
