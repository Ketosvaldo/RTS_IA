using UnityEngine;

public class AssignDigimonButton : MonoBehaviour
{
    public DigimonObject digimon;

    bool confirm;

    public void Confirm()
    {
        if (!confirm)
        {
            confirm = true;
            return;
        }
        AssignDigimonToBuild();
    }

    public void SetDigimonObject(DigimonObject _digimon)
    {
        digimon = _digimon;
    }

    void AssignDigimonToBuild()
    {
        digimon.SetBusy(true);
        DigimonListActive lista = FindObjectOfType<DigimonListActive>();
        lista.buildToAssign.AssignDigimon(digimon);
        Vector3 buildPos = lista.buildToAssign.gameObject.transform.position;
        print(buildPos);
        digimon.MoveToTarget(new Vector3(buildPos.x, buildPos.y + 0.01f, buildPos.z));
        lista.gameObject.SetActive(false);
    }
}