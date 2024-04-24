using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int maxMonsters = 10;
    [SerializeField] private List<GameObject> monsterPrefab;
    [SerializeField] private float spawnRate = 1;
    Transform player;
    private List<EnemyAi> monsters = new List<EnemyAi>(); 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //This is not good practice. 
        StartCoroutine(SpawnMonster());
    }

    private void Update()
    {
        CheckDestroyMonster();
    }

    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            if (monsters.Count < maxMonsters )
            { 
                var monster = Instantiate(monsterPrefab[UnityEngine.Random.Range(0, monsterPrefab.Count)], GetSpawnPoint().position, Quaternion.identity);
                monsters.Add(monster.GetComponent<EnemyAi>());
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    Transform GetSpawnPoint()
    {
        var spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        Debug.Log("From player to spawnpoint - "+ Vector3.Distance(player.transform.position, spawnPoint.position));
        while (Vector3.Distance(player.transform.position, spawnPoint.position) < 5)
        {
            spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        }         

        return spawnPoint;
    }

    private void CheckDestroyMonster()
    {
        foreach (var monster in monsters)
        {
            if (monster == null)
            {
                monsters.Remove(monster);
                break;
            }
        }
    }
}
