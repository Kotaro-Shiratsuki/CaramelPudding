using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class script_EnemyChase : MonoBehaviour
{
    NavMeshAgent agent;

    //外部
    script_EnemyDirector enemyDirector;

    // Start is called before the first frame update
    void Start()
    {
        enemyDirector = GetComponent<script_EnemyDirector>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //AIがChaseモードなら
        if(enemyDirector.aiState == script_EnemyDirector.EnemyAiState.Chase)
        {
            AI_ChaseMode();
        }
    }

    public void AI_ChaseMode()
    {
        //プレイヤーを追いかける
        agent.SetDestination(enemyDirector.playerTr.position);
    }
}
