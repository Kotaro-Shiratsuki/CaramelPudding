using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_PlayerAnimator : MonoBehaviour
{
    Animator anim;

    //フラグの名前
    private const string keyRun = "isRun";
    private const string keyJump = "isJump";

    //外部
    script_PlayerMove move;
    script_PlayerJump jump;
    script_PlayerAttack attack;
    script_CheckGround ground;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        jump = GetComponent<script_PlayerJump>();
        move = GetComponent<script_PlayerMove>();
        attack = GetComponent<script_PlayerAttack>();
        ground = GetComponent<script_CheckGround>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnim();
        JumpAnim();
    }

    private void MoveAnim()
    {
        if(move.isMove && ground.IsGround())
        {
            anim.SetBool(keyRun, true);
        }
        else
        {
            anim.SetBool(keyRun, false);
        }
    }

    bool jumpNow;
    private void JumpAnim()
    {
        if (jumpNow && ground.IsGround())
        {
            anim.SetBool(keyJump, false);
            jumpNow = false;
        }

        if (jump.isJump)
        {
            anim.SetBool(keyJump, true);
            jumpNow = true;
        }
    }
}
