using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_GameSceneDirector : MonoBehaviour
{
    private void Awake()
    {
        //Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
