using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DigimonListAttack : MonoBehaviour
{
    DigimonAI[] digimonActives;

    public GameObject containerDigimonActive;

    public GameObject parentObjectList;

    private void OnEnable()
    {
        digimonActives = FindObjectsOfType<DigimonAI>();

        foreach (DigimonAI digimon in digimonActives)
        {
            GameObject newObject = Instantiate(containerDigimonActive, parentObjectList.transform, true);
            GameObject child1 = newObject.transform.GetChild(0).gameObject;
            GameObject child2 = newObject.transform.GetChild(1).gameObject;
            AssignDigimonToAttack props = newObject.GetComponent<AssignDigimonToAttack>();
            props.SetDigimonAIObject(digimon);
            child1.GetComponent<Image>().sprite = digimon.GetSprite();
            child2.GetComponent<TextMeshProUGUI>().text = digimon.name;
        }
    }

    private void OnDisable()
    {
        GameManager.instance.SetActualDigimon(null);
        for (int i = parentObjectList.transform.childCount; i > 0; i--)
        {
            Destroy(parentObjectList.transform.GetChild(i - 1).gameObject);
        }
    }
}
