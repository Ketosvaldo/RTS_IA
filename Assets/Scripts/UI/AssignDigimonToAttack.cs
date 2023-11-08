using UnityEngine;

public class AssignDigimonToAttack : MonoBehaviour
{
    DigimonAI targetDigimon;

    DigimonObject actualDigimon;
    bool confirm;

    public void Confirm()
    {
        if (!confirm)
        {
            confirm = true;
            return;
        }
        StartAttack();
    }

    public void SetDigimonAIObject(DigimonAI _digimon)
    {
        targetDigimon = _digimon;
    }

    void StartAttack()
    {
        actualDigimon = GameManager.instance.GetActualDigimon();
        actualDigimon.SetBusy(true);
        actualDigimon.isAttacking = true;
        actualDigimon.digimonTarget = targetDigimon;
        Vector3 digimonPos = targetDigimon.transform.position;
        actualDigimon.MoveToTarget(new Vector3(digimonPos.x, digimonPos.y + 0.01f, digimonPos.z));
        FindObjectOfType<DigimonListAttack>().gameObject.SetActive(false);
    }
}