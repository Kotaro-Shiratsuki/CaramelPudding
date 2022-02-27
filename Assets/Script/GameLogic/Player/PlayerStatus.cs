using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum  PlayerState
    {
        None,
        Wait,     //待機状態
        Active,   //アクティブ
        GameOver, //ゲームオーバー
    }
    PlayerState state = PlayerState.None;
    public PlayerState GetState() { return state; }

    //プレイヤーキャラの初期化用
    [Header("InitProperty")]

    [SerializeField, Tooltip("プレイヤーの初期位置")]
    Vector3 initPos;

    //プレイヤーキャラの移動用
    [Header("MoveProperty")]

    [SerializeField, Tooltip("リジッドボディ")]
    Rigidbody playerRb;

    [SerializeField, Tooltip("移動速度")]
    float moveForce;

    [SerializeField, Tooltip("移動速度に対する追従度")]
    float moveForceMultiplier;

    [SerializeField, Tooltip("移動方向になるカメラ")]
    Camera FLCam;

    public bool isMove;

    //プレイヤーキャラのジャンプ処理
    [Header("JumpProperty")]

    [SerializeField, Tooltip("ジャンプの高さ")]
    float jumpPow;

    //ジャンプするかどうか
    bool isJump;

    //接地判定
    GroundCheck groundCheck;

    //ユーザーの入力
    private float hor;
    private float ver;

    public void Init(Vector3 pos)
    {
        if(state == PlayerState.None) { return; }

        initPos = pos;
        transform.localPosition = pos;

        state = PlayerState.Wait;
    }

    public void SetStateActive()
    {
        state = PlayerState.Active;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーキャラがアクティブではないとき入力を受け付けない
        if(state != PlayerState.Active) { return; }
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerInput()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) { isJump = true; }

    }

    void PlayerMove()
    {
        var direction = Vector3.zero;

        //カメラの方向からx-z平面の単位ベクトルを取得
        Vector3 camForward = Vector3.Scale(FLCam.transform.forward, new Vector3(1, 0, 1)).normalized;

        //入力とカメラの方向から移動方向を決定
        direction = camForward * ver + FLCam.transform.right * hor;

        //移動方向に移動
        playerRb.AddForce(moveForceMultiplier * ((direction * moveForce) - playerRb.velocity));

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

    void PlayerJump()
    {
        //スペースキーが押されてないなら処理しない
        if (!isJump) { return; }
        //スペースが押されても接地していなかったら処理しない
        else if (!groundCheck.IsGround()) { isJump = false; return; }

        playerRb.AddForce(jumpPow * Vector3.up, ForceMode.Impulse);
        isJump = false;
    }
}
