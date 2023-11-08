using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DigimonListAttack : MonoBehaviour
{
    DigimonAI[] digimonActives;
    BuildAI[] buildActives;
    Base baseActive;

    public GameObject containerDigimonActive;

    public GameObject containerBuildActive;

    public GameObject containerBaseActive;

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

        buildActives = FindObjectsOfType<BuildAI>();

        foreach(BuildAI build in buildActives)
        {
            GameObject newObject = Instantiate(containerBuildActive, parentObjectList.transform, true);
            GameObject child1 = newObject.transform.GetChild(0).gameObject;
            GameObject child2 = newObject.transform.GetChild(1).gameObject;
            AssignDigimonToAttack props = newObject.GetComponent<AssignDigimonToAttack>();
            props.SetBuildObject(build);
            child1.GetComponent<Image>().sprite = build.sprite;
            child2.GetComponent<TextMeshProUGUI>().text = build.name;
        }

        baseActive = GameManager.instance.GetEnemyBase();

        GameObject neewObject = Instantiate(containerBaseActive, parentObjectList.transform, true);
        GameObject children2 = neewObject.transform.GetChild(1).gameObject;
        AssignDigimonToAttack _props = neewObject.GetComponent<AssignDigimonToAttack>();
        _props.SetTargetBase(baseActive);
        children2.GetComponent<TextMeshProUGUI>().text = baseActive.name;
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
