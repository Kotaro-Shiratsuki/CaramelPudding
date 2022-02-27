using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_PlayerState : MonoBehaviour
{
    public struct PlayerState
    {
        public int hp;         //体力
        public int attack;     //攻撃
        public int difence;    //防御
        public int critical;   //クリティカル

        public void SetState(int _hp,int _at,int _di,int _cr)
        {
            hp = _hp;
            attack = _at;
            difence = _di;
            critical = _cr;
        }
    }

    PlayerState plState = new PlayerState();

    public float criticalRate = 1.5f;

    //外部
    script_PlayerAttack plAttack;

    // Start is called before the first frame update
    void Start()
    {
        plAttack = GetComponent<script_PlayerAttack>();
        plState.SetState(550, 100, 50, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PlayerAttackHit(Collider collider)
    {
        plAttack.isAttackHit = true;

        var hitEnemy = collider.GetComponent<script_EnemyState>();

        if (null == hitEnemy) { yield break; }

        int dice = Random.Range(0, 100);

        if (dice < plState.critical)
        {
            hitEnemy.Damage((int)(plState.attack * criticalRate));
            Debug.Log("critical");
        }
        else
        {
            hitEnemy.Damage(plState.attack);
        }

        yield return new WaitForSeconds(0.5f);

        plAttack.isAttackHit = false;

        yield break;
    }
}
