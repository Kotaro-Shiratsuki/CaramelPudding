using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_EnemyDirector : MonoBehaviour
{
    //敵が生きているか
    public bool enemyAlive;

    //AIのステータス
    public enum EnemyAiState
    {
        Stop,      //停止
        Idol,      //待機(未実装
        Move,      //移動
        Chase,     //追跡
        Attack,    //攻撃
        Dead,      //死亡
        Back,      //後退(未実装
    }

    public EnemyAiState aiState = EnemyAiState.Stop;
    public EnemyAiState nextState;

    //ステータス確認のインターバル
    [Header("AiInterval")]
    public float aiTime;

    [Header("Player Detection")]
    public LayerMask playerLayer;
    public Transform playerTr;

    [Header("Range")]
    public float aggroRange;  //索敵範囲
    public float attackRange; //攻撃範囲


    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.Find("Player").transform;
        InitAi();
    }

    // Update is called once per frame
    void Update()
    {
        SetState();
    }

    private void InitAi()
    {
        enemyAlive = true;
        aiState = EnemyAiState.Idol;
    }

    
    bool isAiRunning;
    protected void SetState()
    {
        if(isAiRunning)
        {
            return;
        }

        AiMainRoutine();

        aiState = nextState;

        StartCoroutine("AiTimer");
    }

    IEnumerator AiTimer()
    {
        //エネミーAIが動いている
        isAiRunning = true;

        yield return new WaitForSeconds(aiTime);

        isAiRunning = false;

        yield break;
    }


    protected void AiMainRoutine()
    {
        if(nextState == EnemyAiState.Dead && enemyAlive)//停止フラグが立っていたらAIを止める
        {
            nextState = EnemyAiState.Stop;
            enemyAlive = false;
        }

        if (enemyAlive)
        {
            //索敵範囲にプレイヤーがいるか確認
            bool playerInAggroRange = Physics.CheckSphere(transform.position, aggroRange, playerLayer);
            //攻撃範囲にプレイヤーがいるか確認
            bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

            //プレイヤーが遠い
            if (!playerInAggroRange)
            {
                nextState = EnemyAiState.Move;
            }
            //索敵範囲にはいるが攻撃範囲に届いていない
            if (playerInAggroRange && !playerInAttackRange)
            {
                nextState = EnemyAiState.Chase;
            }
            //攻撃範囲圏内
            if(playerInAggroRange && playerInAttackRange)
            {
                nextState = EnemyAiState.Attack;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
