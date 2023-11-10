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
    public int farmCount = 0;
    public int mineCount = 0;
    public int gymCount = 0;
    public int palmonCount = 0;
    public int tentomonCount = 0;
    public int agumonCount = 0;

    public Sprite[] palmonEvolutions;
    public Sprite[] tentomonEvolutions;
    public Sprite[] agumonEvolutions;

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
        yield return new WaitForSeconds(3f);
        if (Random.Range(0, 10) > 3)
        {
            if (GetBuildAverage() > 1 || GetBuildAverage() == 0)
                SpawnBuild();
            else
                SpawnDigimon();
        }
        StartCoroutine(TakeDecision());
    }

    void SpawnDigimon()
    {
        DigimonCharacters character;

        float consumeFood = GameManager.instance.GetEnemyBase().GetConsumeFood();
        float foodPrScnd = GameManager.instance.GetEnemyBase().foodprscnd;
        if (consumeFood > foodPrScnd && GameManager.instance.GetEnemyBase().GetFood() < 50 || palmonCount <= 2 )
        {
            character = new Palmon_Character();
            character.SetSprite(digimonSprites[0]);
            
        }
        else if (tentomonCount < 3) // (randomNum > 3 && randomNum < 7)
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
        Debug.Log("Es un Digimon");
        newObject.transform.Rotate(new Vector3(60, 0, 0));
        DigimonAI props = newObject.GetComponent<DigimonAI>();
        props.combatPoints = character.combatPoints;
        props.farmPoints = character.farmPoints;
        props.miningPoints = character.miningPoints;
        props.health = character.vida;
        props.name = character.name;
        newObject.name = character.name + " AI";
        switch (character.name)
        {
            case "Palmon":
                palmonCount++;
                props.digimonEvolutions = palmonEvolutions;
                break;
            case "Tentomon":
                tentomonCount++;
                props.digimonEvolutions = tentomonEvolutions;
                break;
            default:
                props.digimonEvolutions = agumonEvolutions;

                break;
        }
        props.SetSprite(character.GetSprite());
    }

    void SpawnBuild()
    {
        Buildings build;
        //Spawn Farm
        if (farmCount < 2)
        {
            build = new Farm_Building();
            buildName = "Farm AI";
            build.SetSprite(buildSprites[0]);
            
        }
        //Spawn Mine
        else if (farmCount >= 2 && mineCount < 3 &&  palmonCount > 2)
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
        switch (buildName)
        {
            case "Farm AI":
                farmCount++;
                break;
            case "Mine AI":
                mineCount++;

                break;
        }
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
    }

    float GetBuildAverage()
    {
        BuildAI[] buildAis = FindObjectsOfType<BuildAI>();
        float totalSlots = 0;
        foreach (BuildAI builds in buildAis)
        {
            totalSlots += builds.GetActualSlots();
        }

        float totalDigimon = FindObjectsOfType<DigimonAI>().Length;

        Debug.Log("totalDigimon: " + totalDigimon);
        Debug.Log("totalSlots: " + totalSlots);

        if (totalSlots == 0)
            return 0;
        if (totalDigimon == 0)
            return 1;
        Debug.Log("Res: " + totalDigimon / totalSlots);
        return totalDigimon / totalSlots;
    }
}