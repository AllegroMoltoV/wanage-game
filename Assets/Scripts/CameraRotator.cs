using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private GravityManager gravityManager;

    // Start is called before the first frame update
    void Start()
    {
        gravityManager = GameObject.Find("GravityManager").GetComponent<GravityManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, gravityManager.angle);
    }
}
