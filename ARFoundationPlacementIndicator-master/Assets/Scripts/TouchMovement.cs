using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{

    //public ARMovement ARMoveScript;

    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown; //movement
    public static bool RswipeLeft, RswipeRight, RswipeUp, RswipeDown; //rotation
    //public List<Touch> touches = new List<Touch>(); 

    private bool isDrag = false;
    private Vector2 startTouch, swipeDelta;

    // Start is called before the first frame update
    void Start()
    {
        //ARMoveScript = GameObject.FindGameObjectWithTag("MovePanel").GetComponent<ARMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        /*for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, touchPosition, Color.blue);
        }*/
        
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        RswipeLeft = RswipeRight = RswipeUp = RswipeDown = false;

        //mobile swipe input
        if (Input.touches.Length > 0) //works
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDrag = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDrag = false;
                Reset();
            }
        }

        //calc distance
        swipeDelta = Vector2.zero;
        if (isDrag)
        {
            //Debug.Log("Touch Drag"); //works
            if (Input.touches.Length > 0) //? error
            {
                swipeDelta = Input.touches[0].position - startTouch;
                //Debug.Log("Touch script Delta: " + swipeDelta);

            }
            //Debug.DrawLine(startTouch, Input.touches[0].position, Color.green);
        }
        //swipeDelta.Normalize();

        //if (Input.touchCount == 1)
        {
           

            //Debug.Log("Touch Delta Mag: " + swipeDelta.magnitude);
            //cross distance
            if (swipeDelta.magnitude > 110)
            {
                //direction
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        swipeLeft = true;
                        //ARMoveScript.MoveLeft();
                        //Debug.DrawLine(Vector3.zero, startTouch, Color.blue);
                    }
                    else
                    {
                        swipeRight = true;
                        //ARMoveScript.MoveRight();
                        //Debug.DrawLine(Vector3.zero, startTouch, Color.blue);
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        swipeDown = true;
                        //ARMoveScript.MoveForward();
                        //Debug.DrawLine(Vector3.zero, startTouch, Color.blue);
                    }
                    else
                    {
                        swipeUp = true;
                        //ARMoveScript.MoveBack();
                        //Debug.DrawLine(Vector3.zero, startTouch, Color.blue);
                    }
                }

                Reset();
            }
        
        }

        //multi-touch swipe for rotations
        if (Input.touchCount > 1)
        {
            //cross distance
            if (swipeDelta.magnitude > 0.1f)
            {
                //direction
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (x > y)
                {
                    if (x < 0)
                    {
                        RswipeLeft = true;
                        //ARMoveScript.RotLeft();
                        //Debug.DrawLine(Vector3.zero, startTouch, Color.red);
                    }
                    else
                    {
                        RswipeRight = true;
                        //ARMoveScript.RotRight();
                        //Debug.DrawLine(Vector3.zero, startTouch, Color.red);
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        RswipeDown = true;
                        //ARMoveScript.RotForward();
                        //Debug.DrawLine(Vector3.zero, startTouch, Color.red);
                    }
                    else
                    {
                        RswipeUp = true;
                        //ARMoveScript.RotBack();
                        //Debug.DrawLine(Vector3.zero, startTouch, Color.red);
                    }
                }

                Reset();
            }

        }
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
        swipeDelta = Vector2.zero;
        isDrag = false;
    }

    // Button calls for Blocks script
    //MoveForward() //zleft
    //blocksScript.MoveL = true;     //etc...
}
