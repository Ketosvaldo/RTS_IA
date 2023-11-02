using UnityEngine;

public class DigimonButtons : MonoBehaviour
{
    public bool type;
    public bool close;
    public bool attack;

    public DigimonObject thisDigimon;

    private void OnMouseDown()
    {
        if (!type && !close && !attack)
            return;
        if (type)
        {
            if (thisDigimon.level < 5)
            {
                GameManager.instance.ActivateAlert("Necesita ser nivel 5 minimo");
                return;
            }
            GameManager.instance.SetDigimonTypeGO(true, thisDigimon);
            transform.parent.gameObject.SetActive(false);
            return;
        }
        if (attack)
        {
            //GameManager.instance.SetDigimonList(true);
            //transform.parent.gameObject.SetActive(false);
            return;
        }
        if (close)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
    }
}