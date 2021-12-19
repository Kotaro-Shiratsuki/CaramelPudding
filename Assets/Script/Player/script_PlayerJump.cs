using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_PlayerJump : MonoBehaviour
{
    Rigidbody playerRg;
    bool isGround;
    [Header("Jump")]
    public float jumpPow;
    public bool isJump;

    // Start is called before the first frame update
    void Start()
    {
        playerRg = GetComponent<Rigidbody>();
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = GetComponent<script_CheckGround>().IsGround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isJump)
            {
                return;
            }

            StartCoroutine("PlayerJump");
            //Debug.Log(isJump);
            //Debug.Log(isGround);
        }

    }

    private void FixedUpdate()
    {
        PlayerJump();
    }

    private IEnumerator PlayerJump()
    {
        isJump = true;

        if (!isGround)
        {
            isJump = false;
            yield break;
        }

        playerRg.AddForce(jumpPow * Vector3.up, ForceMode.Impulse);

        yield return new WaitForSeconds(0.1f);

        isJump = false;

        yield break;
        
    }
}
