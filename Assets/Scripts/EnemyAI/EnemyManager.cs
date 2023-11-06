using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Base enemyBase;
    public GameObject digimonObject;
    private void Start()
    {
        enemyBase = GameObject.FindGameObjectWithTag("EnemyBase").GetComponent<Base>();
        StartCoroutine(TakeDecision());
    }

    IEnumerator TakeDecision()
    {
        yield return new WaitForSeconds(2f);
        SpawnDigimon();
        SpawnBuild();
        StartCoroutine(TakeDecision());
    }

    void SpawnDigimon()
    {
        Instantiate(digimonObject);
        DigimonCharacters character;
        //Spawn Palmon
        if (enemyBase.GetConsumeFood() > enemyBase.foodprscnd)
        {
            character = new Palmon_Character();
        }
        //Spawn Tentomon
        else if(enemyBase.GetNuts() < 30)
        {
            character = new Tentomon_Character();
        }
        //Spawn Agumon
        else
        {
            character = new Agumon_Character();
        }
        DigimonCharacters props = digimonObject.GetComponent<DigimonCharacters>();
        props.combatPoints = character.combatPoints;
        props.farmPoints = character.farmPoints;
        props.miningPoints = character.miningPoints;
    }

    void SpawnBuild()
    {
        return;
    }
}