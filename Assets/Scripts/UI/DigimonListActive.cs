using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DigimonListActive : MonoBehaviour
{
    DigimonObject[] digimonActives;

    public GameObject containerDigimonActive;

    public GameObject parentObjectList;

    public Build_Object buildToAssign;

    private void OnEnable()
    {
        digimonActives = FindObjectsOfType<DigimonObject>();

        foreach (DigimonObject digimon in digimonActives)
        {
            GameObject newObject = Instantiate(containerDigimonActive, parentObjectList.transform, true);
            GameObject child1 = newObject.transform.GetChild(0).gameObject;
            GameObject child2 = newObject.transform.GetChild(1).gameObject;
            AssignDigimonButton props = newObject.GetComponent<AssignDigimonButton>();
            props.SetDigimonObject(digimon);
            child1.GetComponent<Image>().sprite = digimon.GetSprite();
            child2.GetComponent<TextMeshProUGUI>().text = digimon.name;
        }
    }

    private void OnDisable()
    {
        for(int i = parentObjectList.transform.childCount; i > 0; i--)
        {
            Destroy(parentObjectList.transform.GetChild(i - 1).gameObject);
        }
    }
}