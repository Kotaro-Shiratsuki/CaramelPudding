using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class script_TitleDirector : MonoBehaviour
{
    [Header("仮タイトル")]
    [SerializeField] Text title;

    bool isSceneChange;
    // Start is called before the first frame update
    void Start()
    {
        title.text = "仮タイトル";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isSceneChange)
            {
                return;
            }
            StartCoroutine("ChangeToGame");
        }
    }

    IEnumerator ChangeToGame()
    {
        isSceneChange = true;

        yield return new WaitForSeconds(2f);

        isSceneChange = false;
        SceneManager.LoadScene(1);
    }

    public void OnStartButton_Down()
    {
        StartCoroutine("ChangeToGame");
    }
}
