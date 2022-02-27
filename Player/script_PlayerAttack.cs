using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_PlayerAttack : MonoBehaviour
{
    [Header("AttackProperty")]
    public int attackDmg;
    public float attackTime;
    public bool isAttack;
    public Collider attackHit;
    public bool isAttackHit;

    [Header("Sword")]
    public GameObject sword_stay;
    public GameObject sword_attack;

    Animator anim;
    //フラグ
    private const string keyAttack = "Attack";

    //外部
    private script_PlayerMove move;
    private script_PlayerJump jump;
    private script_PlayerState state;

    private void Awake()
    {
        sword_stay.SetActive(true);
        sword_attack.SetActive(false);
        attackHit.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<script_PlayerState>();
        move = GetComponent<script_PlayerMove>();
        jump = GetComponent<script_PlayerJump>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (isAttack)
            {
                return;
            }

            StartCoroutine("PlayerAttack");
            anim.SetTrigger(keyAttack);
            //Debug.Log(isAttack);
        }    
    }

    IEnumerator PlayerAttack()
    {
        sword_stay.SetActive(false);
        sword_attack.SetActive(true);
        isAttack = true;

        yield return new WaitForSeconds(attackTime);

        sword_stay.SetActive(true);
        sword_attack.SetActive(false);
        isAttack = false;

        yield break;
    }

    //アニメーションイベント
    public void AttackStart()
    {
        attackHit.enabled = true;
    }
    public void AttackEnd()
    {
        attackHit.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            if(isAttackHit)
            {
                return;
            }

            StartCoroutine(state.PlayerAttackHit(other));
        }
    }
}
