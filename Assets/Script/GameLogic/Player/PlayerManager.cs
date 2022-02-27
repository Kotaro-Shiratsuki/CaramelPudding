using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField, Tooltip("�v���C���[�����ʒu")]
    Vector3 InitializePosition;

    // �V���O���g���C���X�^���X
    public static PlayerManager Instance { get { return _Instance; } }
    static PlayerManager _Instance = null;

    // �v���C���[�L����
    PlayerStatus playerStatus;
    public PlayerStatus _PlayerStatus { get { return playerStatus; } }

    // �v���C���[���������C�x���g
    public delegate void OnCreatePlayerChara();

    // Start is called before the first frame update
    void Start()
    {
        _Instance = this;
    }

    public void CreatePlayerChara(OnCreatePlayerChara onFinish)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
