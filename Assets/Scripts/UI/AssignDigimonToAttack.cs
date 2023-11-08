using UnityEngine;

public class AssignDigimonToAttack : MonoBehaviour
{
    DigimonAI targetDigimon;

    DigimonObject actualDigimon;
    bool confirm;

    BuildAI targetBuild;

    Base targetBase;
    public void ConfirmAttackDigimon()
    {
        if (!confirm)
        {
            confirm = true;
            return;
        }
        StartAttack();
    }

    public void ConfirmAttackBuild()
    {
        if (!confirm)
        {
            confirm = true;
            return;
        }
        StartAttackBuild();
    }

    public void ConfirmAttackBase()
    {
        if (!confirm)
        {
            confirm = true;
            return;
        }
        AttackBase();
    }

    public void SetDigimonAIObject(DigimonAI _digimon)
    {
        targetDigimon = _digimon;
    }

    public void SetBuildObject(BuildAI build)
    {
        targetBuild = build;
    }

    public void SetTargetBase(Base baseTarget)
    {
        targetBase = baseTarget;
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

    void StartAttackBuild()
    {
        actualDigimon = GameManager.instance.GetActualDigimon();
        actualDigimon.SetBusy(true);
        actualDigimon.isAttacking = true;
        actualDigimon.buildTarget = targetBuild;
        Vector3 buildPos = targetBuild.transform.position;
        actualDigimon.MoveToTarget(new Vector3(buildPos.x, buildPos.y + 0.01f, buildPos.z));
        FindObjectOfType<DigimonListAttack>().gameObject.SetActive(false);
    }

    void AttackBase()
    {
        actualDigimon = GameManager.instance.GetActualDigimon();
        actualDigimon.SetBusy(true);
        actualDigimon.isAttacking = true;
        actualDigimon.baseTarget = targetBase;
        Vector3 basePos = targetBase.transform.position;
        actualDigimon.MoveToTarget(new Vector3(basePos.x, basePos.y + 1.8f, basePos.z));
        FindObjectOfType<DigimonListAttack>().gameObject.SetActive(false);
    }
}