using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class script_EnemyMove : MonoBehaviour
{
    Vector3 targetPoint; //移動したい目的地
    NavMeshAgent agent;
    bool isPointSet;     //目的地があるか

    [Header("MoveRange")]
    public float moveRange; //一回の移動で動く範囲

    //外部
    script_EnemyDirector enemyDirector;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyDirector = GetComponent<script_EnemyDirector>();
    }

    // Update is called once per frame
    void Update()
    {      
        //AIの状態がMoveなら
        if(enemyDirector.aiState == script_EnemyDirector.EnemyAiState.Move)
        {
            AI_MoveMode(moveRange);
        }
    }

    public void AI_MoveMode(float moveRange)
    {
        //目的地が設定されていなかったら
        if(!isPointSet)
        {
            SearchTargetPoint(moveRange);
        }

        //目的地が設定されたら移動する
        if(isPointSet)
        {
            agent.SetDestination(targetPoint);
        }
        
        //到着したら新しい移動先を作る
        if(!agent.hasPath || agent.velocity.magnitude == 0)
        {
            enemyDirector.nextState = script_EnemyDirector.EnemyAiState.Idol;
            isPointSet = false;
            Debug.Log(isPointSet);

        }
    }

    private void SearchTargetPoint(float moveRange)
    {
        //地面レイヤーを取得
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        //ランダムな方向に目的地を設定
        float targetX = Random.Range(-moveRange, moveRange);
        float targetZ = Random.Range(-moveRange, moveRange);
        targetPoint = new Vector3(transform.position.x + targetX, transform.position.y, transform.position.z + targetZ);
        NavMeshHit hit;

        //設定した目的地が移動可能なら、そこを目的地とする
        if(NavMesh.SamplePosition(targetPoint,out hit,5.0f,NavMesh.AllAreas))
        {
            Debug.DrawRay(hit.position, Vector3.up, Color.red, 10f);
            targetPoint = hit.position;
            isPointSet = true;
        }
    }
}
