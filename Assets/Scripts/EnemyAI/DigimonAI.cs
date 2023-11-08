using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigimonAI : MonoBehaviour
{
    //Todas las props del Digimon a instanciar
    public string digimonName;
    public float combatPoints;
    public float farmPoints;
    public float miningPoints;
    SpriteRenderer spriteRenderer;
    public DigiTypes type;
    public int level;
    public float exp = 0;
    public float maxExp = 10;
    [SerializeField]
    int evolution;
    public float consumeFood;

    public Slider slider;
    public Sprite[] digimonEvolutions;

    Vector3 newPos;
    bool isBusy;

    bool move;

    public float health;

    public bool attack;

    GameObject[] DigimonAllies;
    GameObject[] BuildAllies;

    Build_Object buildTarget;
    private void Update()
    {
        GameManager.instance.ConsumeFood(consumeFood * Time.deltaTime, true);
        if (!move)
            return;
        transform.position = Vector3.MoveTowards(transform.position, newPos, 0.2f);
        if (transform.position == newPos)
        {
            move = false;
            if (attack)
            {
                buildTarget.MakeDamage(combatPoints);
                StartCoroutine(StartAttack());
            }
        }
    }

    private void Start()
    {
        level = 1;
        exp = 0;
        maxExp = 10;
        evolution = 1;
        consumeFood = 0.5f;
        StartCoroutine(TakeDecision());
    }

    IEnumerator TakeDecision()
    {
        yield return new WaitForSeconds(2f);
        int randomNum = Random.Range(0, 10);
        if (randomNum > 8 && !isBusy)
        {
            DigimonAllies = GameObject.FindGameObjectsWithTag("DigimonAlly");
            BuildAllies = GameObject.FindGameObjectsWithTag("BuildAlly");
            if (DigimonAllies.Length != 0)
            {
                foreach (GameObject digimon in DigimonAllies)
                {
                    float digimonPower = digimon.GetComponent<DigimonObject>().combatPoints;
                    if (combatPoints > digimonPower)
                    {
                        newPos = digimon.transform.position;
                        move = true;
                        attack = true;
                        break;
                    }
                }
            }

            if(BuildAllies.Length != 0)
            {
                GameObject targetBuild = null;
                float distance = 0;

                foreach (GameObject build in BuildAllies)
                {
                    if (targetBuild == null)
                    {
                        distance = Vector3.Distance(transform.position, build.transform.position);
                        targetBuild = build;
                    }
                    else
                    {
                        float tempDistance = Vector3.Distance(transform.position, build.transform.position);
                        if (distance > tempDistance)
                        {
                            distance = tempDistance;
                            targetBuild = build;
                        }
                    }
                }
                newPos = targetBuild.transform.position;
                buildTarget = targetBuild.GetComponent<Build_Object>();
                move = true;
                attack = true;
                SetBusy(true);
            }
        }
        StartCoroutine(TakeDecision());
    }

    //Funcion publica para establecer el sprite del Digimon
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
    //Funci�n p�blica para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo combate
    public void SetCombatType()
    {
        type = new Combat_Type();
        SetStats();
    }
    //Funci�n p�blica para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo minero
    public void SetMiningType()
    {
        type = new Mining_Type();
        SetStats();
    }
    //Funci�n p�blica para ser llamado in-game que sirve para establecer que nuestro Digimon se vuelva uno de tipo granjero
    public void SetFarmType()
    {
        type = new Farm_Type();
        SetStats();
    }

    //Funci�n p�blica que sirve para agregar experiencia al Digimon, en caso de alcanzar la experiencia m�xima, este subir� de nivel
    public void ExpGained(float expEarned)
    {
        exp += expEarned;
        if(exp >= maxExp)
        {
            LevelUp();
        }
    }

    //Funci�n que sirve para subir de nivel al Digimon
    void LevelUp()
    {
        level += 1;
        exp = 0;
        SetLevelStats();
        maxExp *= 2;
    }

    //Funci�n que sirve para establecer los nuevos stats del Digimon una vez seleccionado el tipo de Digimon
    void SetStats()
    {
        combatPoints = type.GetCombat(combatPoints);
        farmPoints = type.GetFarming(farmPoints);
        miningPoints = type.GetMining(miningPoints);
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

    public string GetName()
    {
        return digimonName;
    }

    public void MoveToTarget(Vector3 Position)
    {
        move = true;
        newPos = Position;
    }

    public void SetBusy(bool state)
    {
        isBusy = state;
    }

    public bool IsBusy()
    {
        return isBusy;
    }

    void SetLevelStats()
    {
        switch (level)
        {
            case 2:
                combatPoints += 2;
                farmPoints += 3;
                miningPoints += 2; 
                break;
            case 3:
                combatPoints += 3;
                farmPoints += 2;
                miningPoints += 2;
                break;
            case 4:
                combatPoints += 2;
                farmPoints += 2;
                miningPoints += 3;
                break;
            case 5:
                combatPoints += 3;
                farmPoints += 3;
                miningPoints += 2;
                break;
            case 6:
                combatPoints += 2;
                farmPoints += 3;
                miningPoints += 3;
                break;
            case 7:
                combatPoints += 3;
                farmPoints += 2;
                miningPoints += 3;
                break;
            case 8:
                combatPoints += 4;
                farmPoints += 3;
                miningPoints += 2;
                break;
            case 9:
                combatPoints += 2;
                farmPoints += 4;
                miningPoints += 3;
                break;
            case 10:
                combatPoints += 3;
                farmPoints += 2;
                miningPoints += 4;
                break;
        }
    }

    public bool CanEvolve()
    {
        return level >= 8;
    }

    public void Evolve()
    {
        if (evolution == 3)
        {
            GameManager.instance.ActivateAlert("Este digimon est� en su mayor etapa");
            return;
        }
        evolution += 1;
        farmPoints *= 2;
        combatPoints *= 2;
        miningPoints *= 2;
        health *= 2;
        consumeFood *= 2.5f;
        SetSprite(digimonEvolutions[evolution - 2]);
        ResetStats();
    }

    public void MakeDamage(float damage)
    {
        health -= damage;
        SetHealth();
        if (health <= 0)
            Destroy(gameObject);
    }

    public void SetHealth()
    {
        //slider.value = health;
    }

    void ResetStats()
    {
        exp = 0;
        level = 1;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(2f);
        if (attack)
        {
            if(buildTarget != null)
            {
                buildTarget.MakeDamage(combatPoints);
                StartCoroutine(StartAttack());
            }
            else
            {
                attack = false;
            }
        }
    }
}
