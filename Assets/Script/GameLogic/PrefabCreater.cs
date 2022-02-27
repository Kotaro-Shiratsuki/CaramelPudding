using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCreater : MonoBehaviour
{
    public static PrefabCreater instance { get { return _Instance; } }
    static PrefabCreater _Instance = null;

    public delegate void OnLoadFinish(GameObject obj);

    public enum Category
    {
        Player,
        Enemy,
        Ground,
        Wall,
        FieldObj,
    }

    // Start is called before the first frame update
    void Start()
    {
        _Instance = this;
    }

    public void Create(Category category, string name, OnLoadFinish onFinish)
    {
        //�f�B���N�g���������߂�
        string directory = " ";
        switch (category)
        {
            case Category.Player:
                directory = "Prefabs/Player/";
                break;
        }

        string fileName = directory + name;

        // �񓯊����[�h
        // �v���n�u�̓ǂݍ���
        var req = Resources.LoadAsync(fileName);
        StartCoroutine(LoadWait(fileName, req, onFinish));
    }

    IEnumerator LoadWait(string fileName,ResourceRequest req,OnLoadFinish onFinish)
    {
        // �Ԃ��ׂ��R�[���o�b�N���Ȃ��ꍇ�͂����ŏ�������߂�
        if (onFinish == null) yield break;

        // ���[�h���I���܂őҋ@
        while (!req.isDone) yield return null;

        // �t�@�C�������݂��Ȃ��Ƃ�
        if (req.asset == null)
        {
            onFinish.Invoke(null);
            Debug.LogError("���[�h���s : " + name);
        }
        // �t�@�C�������݂���Ƃ�
        else
        {
            // Prefab���Q�[���I�u�W�F�N�g�Ƃ��ăq�G�����L�[��ɐ�������
            GameObject obj = GameObject.Instantiate(req.asset as GameObject);
            if (obj == null)
            {
                onFinish.Invoke(null);
                Debug.LogError("���[�h���s : " + name);
            }
            else
            {
                onFinish.Invoke(obj);
            }
        }
    }
}
