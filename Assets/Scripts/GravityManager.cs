using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public float angle;

    private InputManager inputManager;
    private Vector3 gravity;

    private const float DELTA_ANGLE = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        gravity = new Vector3(0, -1, 0);
        Physics.gravity = gravity;
        angle = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputManager.isPressed)
        {
            Vector2 pos = inputManager.positionPressed;
            if (pos.x > Screen.width / 2 && pos.y < Screen.height / 4)
            {
                angle -= DELTA_ANGLE;
            }
            else if (pos.x <= Screen.width / 2 && pos.y < Screen.height / 4)
            {
                angle += DELTA_ANGLE;
            }
        }

        if (angle != 0.0f)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 newGravity = rotation * gravity;
            Physics.gravity = newGravity;
        }
    }
}
