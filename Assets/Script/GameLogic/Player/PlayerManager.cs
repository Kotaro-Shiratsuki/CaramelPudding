using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤー初期位置")]
    Vector3 InitializePosition;

    // シングルトンインスタンス
    public static PlayerManager Instance { get { return _Instance; } }
    static PlayerManager _Instance = null;

    // プレイヤーキャラ
    PlayerStatus playerStatus;
    public PlayerStatus _PlayerStatus { get { return playerStatus; } }

    // プレイヤー生成完了イベント
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
