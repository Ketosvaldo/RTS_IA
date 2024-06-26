using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildAI : MonoBehaviour
{
    public Sprite sprite;
    //Variable para guardar el nivel de nuestra Build
    public int level = 1;
    public float vida;
    public Slider slider;
    public Gradient gradien;
    public Color color;
    public Image Fill;

    Buildings build;
    //Funcion publica que sirve para establecer las props del build elegido.
    private void Start()
    {
        StartCoroutine(TakeDecision());
    }
    public void SetBuild(Buildings newBuild)
    {
        build = newBuild;
        SetObjectProps();
        slider.maxValue = GetHealth();
        slider.value = GetHealth();
    }

    public void SetHealth()
    {
        slider.value = build.GetHealth();
    }

    //Hace que La AI decida que digimon pondra en su base
    IEnumerator TakeDecision()
    {
        yield return new WaitForSeconds(2f);
        float randomNum = Random.Range(0, 10);
        if (randomNum < 3)
        {
            GameObject[] allDigimonsAIGO = GameObject.FindGameObjectsWithTag("DigimonEnemy");
            DigimonAI[] digimonsAI = new DigimonAI[allDigimonsAIGO.Length];
            for(int i = 0; i < allDigimonsAIGO.Length; i++)
            {
                digimonsAI[i] = allDigimonsAIGO[i].GetComponent<DigimonAI>();
                if (!digimonsAI[i].IsBusy())
                {
                    AssignDigimon(digimonsAI[i]);
                    Vector3 buildPos = transform.position;
                    digimonsAI[i].MoveToTarget(new Vector3(buildPos.x, buildPos.y + 0.01f, buildPos.z));
                    if(digimonsAI[i].IsBusy())
                        break;
                }
            }
        }
        else if(randomNum > 2 && randomNum < 6)
        {
            LevelUpBuild();
        }
        StartCoroutine(TakeDecision());
    }

    void SetObjectProps()
    {
        sprite = build.GetSprite();
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    //Funcion publica que sirve para aumentar el nivel del build
    public void LevelUpBuild()
    {
        if (build.GetLevel() == 4)
        {
            return;
        }
        build.LevelUpBuild(true);
        level = build.GetLevel();
    }

    //Funcion publica que sirve para asignar a un Digimon a dicha build
    public void AssignDigimon(DigimonAI digimonToAssign)
    {
        build.AssignDigimon(digimonToAssign);
    }

    public void MakeDamage(float damage)
    {
        build.DamageHandler(damage);
        SetHealth();
        if (build.IsDeath())
        {
            EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
            switch (name)
            {
                case "Farm AI":
                    enemyManager.farmCount -= 1;
                    break;
                case "Mine AI":
                    enemyManager.mineCount -= 1;
                    break;
            }
            Destroy(gameObject);
        }
            
    }

    public float GetHealth()
    {
        return build.GetHealth();
    }

    public int GetActualSlots()
    {
        return build.GetSlotNumber();
    }
}
