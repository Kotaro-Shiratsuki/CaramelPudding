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
        //ディレクトリ名を決める
        string directory = " ";
        switch (category)
        {
            case Category.Player:
                directory = "Prefabs/Player/";
                break;
        }

        string fileName = directory + name;

        // 非同期ロード
        // プレハブの読み込み
        var req = Resources.LoadAsync(fileName);
        StartCoroutine(LoadWait(fileName, req, onFinish));
    }

    IEnumerator LoadWait(string fileName,ResourceRequest req,OnLoadFinish onFinish)
    {
        // 返すべきコールバックがない場合はここで処理をやめる
        if (onFinish == null) yield break;

        // ロードが終わるまで待機
        while (!req.isDone) yield return null;

        // ファイルが存在しないとき
        if (req.asset == null)
        {
            onFinish.Invoke(null);
            Debug.LogError("ロード失敗 : " + name);
        }
        // ファイルが存在するとき
        else
        {
            // Prefabをゲームオブジェクトとしてヒエラルキー上に生成する
            GameObject obj = GameObject.Instantiate(req.asset as GameObject);
            if (obj == null)
            {
                onFinish.Invoke(null);
                Debug.LogError("ロード失敗 : " + name);
            }
            else
            {
                onFinish.Invoke(obj);
            }
        }
    }
}
