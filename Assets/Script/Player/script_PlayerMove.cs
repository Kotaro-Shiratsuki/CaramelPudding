using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_PlayerMove : MonoBehaviour
{

    Rigidbody playerRg;
    public float hor;
    public float ver;
    [Header("Movement")]
    public float moveForce;
    public float moveForceMultiplier;
    public bool isMove;

    [Header("Avoid")]
    public float avoidForce;
    public float avoidInterval;
    public bool isAvoid;

    //移動方向の基準にするカメラ
    [Header("Camera")]
    [SerializeField]Camera freeLoockCam;

    //外部
    private script_PlayerAttack attack;

    // Start is called before the first frame update
    void Start()
    {
        playerRg = GetComponent<Rigidbody>();
        attack = GetComponent<script_PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if(!attack.isAttack)
        {
            PlayerMove();
        }
    }

    void PlayerMove()
    {
        var direction = Vector3.zero;

        //カメラの方向からx-z平面の単位ベクトルを取得
        Vector3 camForward = Vector3.Scale(freeLoockCam.transform.forward, new Vector3(1, 0, 1)).normalized;

        //入力とカメラの方向から移動方向を決定
        direction = camForward * ver + freeLoockCam.transform.right * hor;

        //移動方向に移動
        playerRg.AddForce(moveForceMultiplier * ((direction * moveForce) - playerRg.velocity));

        //マウスの右クリックで加速
        if (Input.GetMouseButtonDown(1))
        {
            if (isAvoid)
            {
                return;
            }

            StartCoroutine(PlayerAvoid(transform.forward));
        }

        //キャラの向きを進行方向に
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            isMove = true;
        }
        else
        {
            isMove = false;
        }
    }

    IEnumerator PlayerAvoid(Vector3 dir_)
    {
        isAvoid = true;
        playerRg.AddForce(dir_ * avoidForce,ForceMode.Impulse);

        yield return new WaitForSeconds(avoidInterval);

        isAvoid = false;

        yield break;
    }
}
