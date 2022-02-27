using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum  PlayerState
    {
        None,
        Wait,     //�ҋ@���
        Active,   //�A�N�e�B�u
        GameOver, //�Q�[���I�[�o�[
    }
    PlayerState state = PlayerState.None;
    public PlayerState GetState() { return state; }

    //�v���C���[�L�����̏������p
    [Header("InitProperty")]

    [SerializeField, Tooltip("�v���C���[�̏����ʒu")]
    Vector3 initPos;

    //�v���C���[�L�����̈ړ��p
    [Header("MoveProperty")]

    [SerializeField, Tooltip("���W�b�h�{�f�B")]
    Rigidbody playerRb;

    [SerializeField, Tooltip("�ړ����x")]
    float moveForce;

    [SerializeField, Tooltip("�ړ����x�ɑ΂���Ǐ]�x")]
    float moveForceMultiplier;

    [SerializeField, Tooltip("�ړ������ɂȂ�J����")]
    Camera FLCam;

    public bool isMove;

    //�v���C���[�L�����̃W�����v����
    [Header("JumpProperty")]

    [SerializeField, Tooltip("�W�����v�̍���")]
    float jumpPow;

    //�W�����v���邩�ǂ���
    bool isJump;

    //�ڒn����
    GroundCheck groundCheck;

    //���[�U�[�̓���
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
        //�v���C���[�L�������A�N�e�B�u�ł͂Ȃ��Ƃ����͂��󂯕t���Ȃ�
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

        //�J�����̕�������x-z���ʂ̒P�ʃx�N�g�����擾
        Vector3 camForward = Vector3.Scale(FLCam.transform.forward, new Vector3(1, 0, 1)).normalized;

        //���͂ƃJ�����̕�������ړ�����������
        direction = camForward * ver + FLCam.transform.right * hor;

        //�ړ������Ɉړ�
        playerRb.AddForce(moveForceMultiplier * ((direction * moveForce) - playerRb.velocity));

        //�L�����̌�����i�s������
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
        //�X�y�[�X�L�[��������ĂȂ��Ȃ珈�����Ȃ�
        if (!isJump) { return; }
        //�X�y�[�X��������Ă��ڒn���Ă��Ȃ������珈�����Ȃ�
        else if (!groundCheck.IsGround()) { isJump = false; return; }

        playerRb.AddForce(jumpPow * Vector3.up, ForceMode.Impulse);
        isJump = false;
    }
}
