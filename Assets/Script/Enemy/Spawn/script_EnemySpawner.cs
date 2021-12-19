using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_EnemySpawner : MonoBehaviour
{
    [Header("EnemyPrefab")]
    [SerializeField] GameObject enemyPrefab;

    public float respawnTime;
    
    Vector3 spawnPoint;
    bool isSpawn;

    // Start is called before the first frame update
    void Start()
    {
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
