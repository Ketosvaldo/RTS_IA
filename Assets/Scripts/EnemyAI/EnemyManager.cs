using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyManager : MonoBehaviour
{
    private Base enemyBase;
    public GameObject digimonObject;
    public GameObject buildObject;
    public Sprite[] buildSprites;
    private void Start()
    {
        enemyBase = GetComponent<Base>();
        StartCoroutine(TakeDecision());
    }

    IEnumerator TakeDecision()
    {
        yield return new WaitForSeconds(5f);
        float randomNum = Random.Range(0, 2);
        switch (randomNum)
        {
            case 0: SpawnDigimon(); break;
            case 1: SpawnBuild(); break;
        }
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
        else if(enemyBase.GetNuts() < 100)
        {
            character = new Tentomon_Character();
        }
        //Spawn Agumon
        else
        {
            character = new Agumon_Character();
        }
        DigimonAI props = digimonObject.GetComponent<DigimonAI>();
        props.combatPoints = character.combatPoints;
        props.farmPoints = character.farmPoints;
        props.miningPoints = character.miningPoints;
    }

    void SpawnBuild()
    {
        Buildings build;
        //Spawn Farm
        if (enemyBase.GetConsumeFood() > enemyBase.foodprscnd)
        {
            build = new Farm_Building();
            build.SetSprite(buildSprites[0]);
        }
        //Spawn Mine
        else if (enemyBase.GetNuts() < 100)
        {
            build = new Mine_Building();
            build.SetSprite(buildSprites[1]);
        }
        //Spawn Gym
        else
        {
            build = new Gym_Building();
            build.SetSprite(buildSprites[2]);
        }
        if (!build.CanBuild())
            return;
        build.ConsumeResource(true);
        GameObject newObject = Instantiate(buildObject);
        BuildAI props = newObject.GetComponent<BuildAI>();
        props.SetBuild(build);
        return;
    }
}