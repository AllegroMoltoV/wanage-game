using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool isPressed;
    public Vector2 positionPressed;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        positionPressed = new Vector2();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isPressed = checkPressed();
        if (isPressed)
        {
            positionPressed = getPositionPressed();
        }
        else
        {
            positionPressed = new Vector2();
        }
    }

    private bool checkPressed()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        if (Input.GetMouseButton(0))
        {
            return true;
        } 
        else 
        {
            return false;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                return true;
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
#endif // UNITY_EDITOR
    }

    private Vector2 getPositionPressed()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        Vector2 ret = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#else
        Touch touch = Input.GetTouch(0);
        Vector2 ret = new Vector2(touch.position.x, touch.position.y);
#endif
        return ret;
    }
}
