using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_EnemyState : MonoBehaviour
{
    public struct State
    {
        public int hp;
        public int attack;
        public int diffence;

        public void SetState(int _hp,int _at, int _di)
        {
            hp = _hp;
            attack = _at;
            diffence = _di;
        }
    }

    State enState = new State();

    //外部
    script_EnemyDirector director;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<script_EnemyDirector>();
        enState.SetState(200, 70, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int attack)
    {
        int dmg = attack - enState.diffence;
        enState.hp -= dmg;

        Debug.Log(enState.hp);

        if(enState.hp <= 0)
        {
            director.nextState = script_EnemyDirector.EnemyAiState.Dead;
        }
    }
}
