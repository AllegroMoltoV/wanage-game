using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftMovement : MonoBehaviour
{
    public enum LiftDirection
    {
        NONE = 0,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public LiftDirection liftDirection;
    public float movementSpeed;

    private InputManager inputManager;
    private int inputFrameCounter;
    private Vector3 initialPosition;

    // ËáílÇí¥Ç¶ÇÈÇ‹Ç≈ÇÕëùâ¡ÇµËáílÇí¥Ç¶ÇΩå„ÇÕå∏è≠Ç∑ÇÈ
    private const int INPUT_FRAME_THRESHOLD = 20;
    private const float DELTA_SPEED_UP = 0.01f;
    private const float DELTA_SPEED_DOWN = 0.002f;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 0.0f;
        inputFrameCounter = 0;
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputManager.isPressed)
        {
            if (inputFrameCounter < INPUT_FRAME_THRESHOLD)
            {
                inputFrameCounter++;
                movementSpeed += DELTA_SPEED_UP;
            }
            else if (movementSpeed > 0.0f)
            {
                movementSpeed = Mathf.Max(movementSpeed - DELTA_SPEED_DOWN, 0.0f);
            }

            updatePosition();
        }
        else
        {
            movementSpeed = 0.0f;
            inputFrameCounter = 0;

            initializePosition();
        }
    }

    private void updatePosition()
    {
        switch (liftDirection)
        {
            case LiftDirection.UP:
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed, transform.position.z);
                    break;
                }
            case LiftDirection.DOWN:
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed, transform.position.z);
                    break;
                }
            case LiftDirection.LEFT:
                {
                    transform.position = new Vector3(transform.position.x - movementSpeed, transform.position.y, transform.position.z);
                    break;
                }
            case LiftDirection.RIGHT:
                {
                    transform.position = new Vector3(transform.position.x + movementSpeed, transform.position.y, transform.position.z);
                    break;
                }
        }
    }

    private void initializePosition()
    {
        transform.position = initialPosition;
    }
}
