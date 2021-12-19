using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_CheckGround : MonoBehaviour
{
    //接地判定
    [Header("GroundCheck")]
    Vector3 rayPosition;
    Ray ray;
    public float distance;
    RaycastHit hit;

    //重力
    [Header("Gravity")]
    Rigidbody playerRg;
    public float gravity;


    // Start is called before the first frame update
    void Start()
    {
        playerRg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(IsGround());
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    public bool IsGround()
    {
        rayPosition = transform.position;
        ray = new Ray( rayPosition + Vector3.up * 0.1f, Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

        if(Physics.Raycast(ray,out hit,distance))
        {
            if(hit.collider.tag == "Ground")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void Gravity()
    {
        if(!IsGround())
        {
            playerRg.AddForce(Vector3.down * gravity);
        }
        else
        {
            playerRg.AddForce(Vector3.down * gravity / 5.0f);
        }
    }
}
