using UnityEngine;
using UnityEngine.UI;

public class DigimonCards : MonoBehaviour
{
    DigimonCharacters character;
    GameObject childObject;
    public string DigimonName;
    public Sprite sprite;

    public GameObject DigimonGO;
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
        GameObject DigiObject = Instantiate(DigimonGO, new Vector3(-42, 3, 0), Quaternion.identity);
        DigiObject.transform.Rotate(new Vector3(60, 0, 0));
        DigimonObject props = DigiObject.GetComponent<DigimonObject>();
        props.combatPoints = character.combatPoints;
        props.miningPoints = character.miningPoints;
        props.farmPoints = character.farmPoints;
        props.SetSprite(sprite);
        DigiObject.name = character.name;
    }
}