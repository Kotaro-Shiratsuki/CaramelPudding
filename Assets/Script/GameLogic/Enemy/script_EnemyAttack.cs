using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class script_EnemyAttack : MonoBehaviour
{
    NavMeshAgent agent;

    //攻撃プロパティ
    [Header("AttackProperty")]
    public float postMotion;   //硬直時間（次の動作に移るまでの時間
    public float bulletSpeed;
    public bool isAttack;
    public int attackDmg;

    //距離プロパティ
    [Header("Calc.Dist")]
    public GameObject bulletPre;
    public Transform shootPos;

    //角度調整
    public float smooth = 0.2f;
    public float maxSpeed = 3.0f;
    private float yVelocity;

    //外部
    script_EnemyDirector director;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<script_EnemyDirector>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemyが倒されていた場合攻撃しない
        if(director.nextState == script_EnemyDirector.EnemyAiState.Dead)
        {
            return;
        }
        //AIがAttackモードなら
        if (director.aiState == script_EnemyDirector.EnemyAiState.Attack)
        {
            agent.SetDestination(transform.position);
            ForwardToPlayer();
            AI_AttackMode();
        }

    }

    private void ForwardToPlayer()
    {
        //正面を向いたベクトルと、プレイヤーまでのベクトル
        var current = transform.forward;
        var target = director.playerTr.position - transform.position;

        //x-z平面における法線ベクトル
        var plane = Vector3.up;

        //x-z平面に映したベクトル
        var from = Vector3.ProjectOnPlane(current, plane);
        var to = Vector3.ProjectOnPlane(target, plane);

        //y軸を回転軸にしたプレイヤーまでの角度
        var targetAngle = Vector3.SignedAngle(from, to, plane) * Mathf.Rad2Deg;

        //回転をスムーズにする
        var smoothAngle = Mathf.SmoothDamp(transform.eulerAngles.y, targetAngle, ref yVelocity, smooth, maxSpeed);
        transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

    }

    public void AI_AttackMode()
    {
        if (isAttack)
        {
            return;
        }

        StartCoroutine("AI_Attack");
    }

    IEnumerator AI_Attack()
    {
        isAttack = true;

        yield return new WaitForSeconds(postMotion / 2);

        var shot = Instantiate(bulletPre, shootPos.position, transform.rotation);
        var speed = transform.forward * bulletSpeed;
        shot.GetComponent<Rigidbody>().velocity = speed;

        yield return new WaitForSeconds(postMotion);

        Destroy(shot);
        isAttack = false;

        yield break;

    }
}
