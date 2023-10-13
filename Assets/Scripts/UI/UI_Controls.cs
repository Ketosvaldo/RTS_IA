using UnityEngine;

public class UI_Controls : MonoBehaviour
{
    public GameObject[] UI_ActivateElements;
    public GameObject[] UI_DeactivateElements;

    public void UpdateUI()
    {
        ActivateUI();
        DeactivateUI();
    }
    void ActivateUI()
    {
        if (UI_ActivateElements == null)
            return;
        for (int i = 0; i < UI_ActivateElements.Length; i++)
        {
            UI_ActivateElements[i].SetActive(true);
        }
    }

    void DeactivateUI()
    {
        if (UI_DeactivateElements == null)
            return;
        for (int i = 0; i < UI_DeactivateElements.Length; i++)
        {
            UI_DeactivateElements[i].SetActive(false);
        }
    }
}
