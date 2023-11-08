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
    public Sprite[] digimonSprites;
    GameObject[] enemyNodes;
    Node[] nodeEnemy;

    string buildName;
    private void Start()
    {
        enemyBase = GetComponent<Base>();
        StartCoroutine(TakeDecision());
        enemyNodes = GameObject.FindGameObjectsWithTag("EnemyNode");
        nodeEnemy = new Node[enemyNodes.Length];
        for(int i = 0; i < enemyNodes.Length; i++)
        {
            nodeEnemy[i] = enemyNodes[i].GetComponent<Node>();
        }
    }

    IEnumerator TakeDecision()
    {
        yield return new WaitForSeconds(6f);
        float randomNum = Random.Range(0, 10);
        if (randomNum < 4)
            SpawnDigimon();
        else if (randomNum > 7)
            SpawnBuild();
        StartCoroutine(TakeDecision());
    }

    void SpawnDigimon()
    {
        DigimonCharacters character;
        
        int randomNum = Random.Range(0, 10);
        Debug.Log(randomNum);
        if (randomNum < 4)
        {
            character = new Palmon_Character();
            character.SetSprite(digimonSprites[0]);
        }
        else if (randomNum > 3 && randomNum < 7)
        {
            character = new Tentomon_Character();
            character.SetSprite(digimonSprites[1]);
        }
        else
        {
            character = new Agumon_Character();
            character.SetSprite(digimonSprites[2]);
        }

        if (!GameManager.instance.CheckEnergy(character.DigiCost, true))
            return;
        GameManager.instance.ConsumeEnergy(character.DigiCost, true);
        Vector3 pos = GameManager.instance.GetEnemyBase().transform.position;
        GameObject newObject = Instantiate(digimonObject, new Vector3(pos.x, pos.y + 2, pos.z), Quaternion.identity);
        Debug.Log("Es una Digimon");
        newObject.transform.Rotate(new Vector3(60, 0, 0));
        DigimonAI props = newObject.GetComponent<DigimonAI>();
        props.combatPoints = character.combatPoints;
        props.farmPoints = character.farmPoints;
        props.miningPoints = character.miningPoints;
        props.health = character.vida;
        props.name = character.name;
        newObject.name = character.name + " AI";
        props.SetSprite(character.GetSprite());
    }

    void SpawnBuild()
    {
        Buildings build;
        //Spawn Farm
        if (enemyBase.GetConsumeFood() > enemyBase.foodprscnd)
        {
            build = new Farm_Building();
            buildName = "Farm AI";
            build.SetSprite(buildSprites[0]);
        }
        //Spawn Mine
        else if (enemyBase.GetNuts() < 100)
        {
            build = new Mine_Building();
            buildName = "Mine AI";
            build.SetSprite(buildSprites[1]);
        }
        //Spawn Gym
        else
        {
            build = new Gym_Building();
            buildName = "Gym AI";
            build.SetSprite(buildSprites[2]);
        }
        if (!build.CanBuild())
            return;
        Vector3 pos = Vector3.zero;
        foreach(Node node in nodeEnemy)
        {
            if (!node.GetFull())
            {
                node.SetFull(true);
                pos = node.gameObject.transform.position;
                break;
            }
        }
        if (pos == Vector3.zero)
            return;
        build.ConsumeResource(true);
        GameObject newObject = Instantiate(buildObject, new Vector3(pos.x, pos.y + 2f, pos.z), Quaternion.identity);
        newObject.name = buildName;
        newObject.transform.Rotate(new Vector3(60, 0, 0));
        BuildAI props = newObject.GetComponent<BuildAI>();
        props.SetBuild(build);
        return;
    }
}