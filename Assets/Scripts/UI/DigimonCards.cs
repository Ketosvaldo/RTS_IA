using UnityEngine;
using UnityEngine.UI;

//Esta clase sirve para las cards en UI de cada Digimon, lo importante aqu� es escribir el nombre del Digimon

public class DigimonCards : MonoBehaviour
{
    //Referencia al Digimona a guardar
    DigimonCharacters character;
    GameObject childObject;
    public string DigimonName;
    public Sprite sprite;

    public GameObject DigimonGO;

    [SerializeField]
    Sprite[] evolutionSprite = new Sprite[2];
    void Start()
    {
        childObject = transform.GetChild(0).gameObject;
        sprite = childObject.GetComponent<Image>().sprite;
        SetCharacter(DigimonName);
        SetSprite();
    }

    void SetCharacter(string digiName)
    {
        switch(digiName)
        {
            case "Agumon": character = new Agumon_Character(); break;
            case "Tentomon": character = new Tentomon_Character(); break;
            case "Palmon": character = new Palmon_Character(); break;
        }
    }

    void SetSprite()
    {
        character.SetSprite(sprite);
    }

    public void SpawnDigimon()
    {
        Debug.Log("Estoy creando a: " + character.name);
        if (!GameManager.instance.CheckEnergy(character.DigiCost))
        {
            GameManager.instance.ActivateAlert("No te alcanza pa");
            return;
        }
        GameManager.instance.ConsumeEnergy(character.DigiCost);
        Vector3 position = GameManager.instance.GetBase().transform.position;
        GameObject DigiObject = Instantiate(DigimonGO, new Vector3(position.x, position.y + 2, position.z), Quaternion.identity);
        DigiObject.transform.Rotate(new Vector3(60, 0, 0));
        DigimonObject props = DigiObject.GetComponent<DigimonObject>();
        props.health = character.vida;
        props.combatPoints = character.combatPoints;
        props.miningPoints = character.miningPoints;
        props.farmPoints = character.farmPoints;
        props.digimonName = character.name;
        props.SetSprite(sprite);
        DigiObject.name = character.name;
        DigiObject.tag = "DigimonAlly";
        props.digimonEvolutions[0] = evolutionSprite[0];
        props.digimonEvolutions[1] = evolutionSprite[1];
    }
}