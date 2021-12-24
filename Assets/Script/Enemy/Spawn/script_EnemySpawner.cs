using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_EnemySpawner : MonoBehaviour
{
    [Header("EnemyPrefab")]
    [SerializeField] GameObject enemyPrefab;

    [Header("SpawnProperty")]
    public float respawnTime;
    public int maxEnemySpawn;
    
    Vector3 spawnPoint;
    bool isSpawn;

    //外部
    script_EnemyDirector enemyState;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = GetComponent<script_EnemyDirector>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawn)
        {
            return;
        }

        StartCoroutine("EnemySpawn");
    }

    private IEnumerator EnemySpawn()
    {
        var enemy = Instantiate(enemyPrefab, spawnPoint,Quaternion.identity);
        isSpawn = true;

        yield return new WaitForSeconds(respawnTime);

        isSpawn = false;

        yield break;
    }
}
