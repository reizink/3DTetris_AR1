﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMovement : MonoBehaviour
{
    // CurrentBlock

    public Blocks blocksScript;
    public TouchMovement touchMovement;
    //public OrientControls OrientControls;
    //public TrackPos TrackPos;
    public GameObject cam;
    private Vector2 camPos;
    private int side = 5;


    public static int Xgrid = 10;
    public static int Ygrid = 10;
    public static int Zgrid = 10;
    //
    public Vector3 rotPoiint;
    // save fallen blocks position
    public static Transform[,,] fillGrid = new Transform[Xgrid, Ygrid + 5, Zgrid];

    public GameObject currentBlock;
    public bool isPlaying = false;

    private void Update()
    {
        //TrackPos = GameObject.FindWithTag("Track").GetComponent<TrackPos>();

        if (isPlaying)
        {
            currentBlock = GameObject.FindWithTag("CurrentBlock");
            blocksScript = currentBlock.GetComponent<Blocks>();
            //OrientControls = GameObject.FindWithTag("center").GetComponent<OrientControls>();
            cam = GameObject.FindWithTag("MainCamera");
            side = findSide();

            //
            touchMovement = GameObject.FindGameObjectWithTag("TouchParent").GetComponent<TouchMovement>();
            //Debug.Log("Touch Movement: " + touchMovement.name);
            //touch move
            if (side == 1) //player on left //TrackPos.curSide == 1
            {
                if (TouchMovement.swipeUp)
                {
                    //MoveBack();
                    MoveRight();
                }
                if (TouchMovement.swipeDown)
                {
                    //MoveForward();
                    MoveLeft();
                }
                if (TouchMovement.swipeLeft)
                {
                    //MoveLeft();
                    //MoveForward();
                    MoveBack();
                }
                if (TouchMovement.swipeRight)
                {
                    //MoveRight();
                    //MoveBack();
                    MoveForward();
                }

                //touch rot
                if (TouchMovement.RswipeUp)
                {
                    //RotBack();
                    RotRight();
                }
                if (TouchMovement.RswipeDown)
                {
                    //RotForward();
                    RotLeft();
                }
                if (TouchMovement.RswipeLeft)
                {
                    //RotLeft();
                    RotBack();
                }
                if (TouchMovement.RswipeRight)
                {
                    //RotRight();
                    RotForward();
                }
            }
            else if (side == 2) //player at back/upper side //2
            {
                if (TouchMovement.swipeUp)
                {
                    //MoveBack();
                    MoveForward();
                }
                if (TouchMovement.swipeDown)
                {
                    //MoveForward();
                    MoveBack();
                }
                if (TouchMovement.swipeLeft)
                {
                    //MoveLeft();
                    MoveRight();
                }
                if (TouchMovement.swipeRight)
                {
                    //MoveRight();
                    MoveLeft();
                }

                //touch rot
                if (TouchMovement.RswipeUp)
                {
                    //RotBack();
                    RotForward();
                }
                if (TouchMovement.RswipeDown)
                {
                    //RotForward();
                    RotBack();
                }
                if (TouchMovement.RswipeLeft)
                {
                    //RotLeft();
                    RotRight();
                }
                if (TouchMovement.RswipeRight)
                {
                    //RotRight();
                    RotLeft();
                }
            }
            else if (side == 3) //on right // OrientControls.mySide //3
            {
                if (TouchMovement.swipeUp)
                {
                    //MoveBack();
                    MoveLeft();
                }
                if (TouchMovement.swipeDown)
                {
                    //MoveForward();
                    MoveRight();
                }
                if (TouchMovement.swipeLeft)
                {
                    //MoveLeft();
                    //MoveBack();
                    MoveForward();
                }
                if (TouchMovement.swipeRight)
                {
                    //MoveRight();
                    //MoveForward();
                    MoveBack();
                }

                //touch rot
                if (TouchMovement.RswipeUp)
                {
                    //RotBack();
                    RotLeft();
                }
                if (TouchMovement.RswipeDown)
                {
                    //RotForward();
                    RotRight();
                }
                if (TouchMovement.RswipeLeft)
                {
                    //RotLeft();
                    RotForward();
                }
                if (TouchMovement.RswipeRight)
                {
                    //RotRight();
                    RotBack();
                }
            }
            else //0
            {
                if (TouchMovement.swipeUp)
                {
                    MoveBack();
                    Debug.Log("swiped Up");
                }
                if (TouchMovement.swipeDown)
                {
                    MoveForward();
                    Debug.Log("swiped Down");
                }
                if (TouchMovement.swipeLeft)
                {
                    MoveLeft();
                    Debug.Log("swiped Left");
                }
                if (TouchMovement.swipeRight)
                {
                    MoveRight();
                    Debug.Log("swiped Right");
                }

                //touch rot
                if (TouchMovement.RswipeUp)
                {
                    RotBack();
                }
                if (TouchMovement.RswipeDown)
                {
                    RotForward();
                }
                if (TouchMovement.RswipeLeft)
                {
                    RotLeft();
                }
                if (TouchMovement.RswipeRight)
                {
                    RotRight();
                }
            }
            //Debug.Log("orient side: " + OrientControls.mySide);
            //Debug.Log("Track side: " + TrackPos.curSide);
        }
    }

    public void ButtonClicked()
    {
        currentBlock = GameObject.FindWithTag("CurrentBlock");

        blocksScript = currentBlock.GetComponent<Blocks>();
        Debug.Log(blocksScript);
    }

    public bool validMove()
    {
        foreach (Transform children in transform)
        {
            // get x,y,z position as int
            // for all childrens - in this case cubes
            float Xpos = children.transform.position.x;
            float Ypos = children.transform.position.y;
            float Zpos = children.transform.position.z;

            // if any exceed the bouder limit return false
            if (convertPos(Xpos) < 0 || convertPos(Xpos) >= Xgrid ||
                convertPos(Ypos) < 0 ||
                convertPos(Zpos) < 0 || convertPos(Zpos) >= Zgrid)
            {
                return false;
            }
            // check if position already taken
            if (fillGrid[convertPos(Xpos), convertPos(Ypos), convertPos(Zpos)] != null)
            {
                Debug.Log("FillGrid Position Taken: " + Xpos + ", " + Ypos + ", " + Zpos);
                Debug.Log("FillGrid Pos 1-10 Taken: " + convertPos(Xpos) + ", " + convertPos(Ypos) + ", " + convertPos(Zpos));
                return false;
            }
        }
        return true;
    }

    bool validMoveShadow()
    {
        foreach (Transform children in GameObject.FindWithTag("shadow").transform)
        {
            float Xpos = children.transform.position.x;
            float Ypos = children.transform.position.y;
            float Zpos = children.transform.position.z;

            if (convertPos(Ypos) < 0)
            {
                return false;
            }
            if (fillGrid[convertPos(Xpos), convertPos(Ypos), convertPos(Zpos)] != null)
            {
                return false;
            }

            // deal with shadow in button but block are over it
            for (int i = convertPos(Ypos); i < 10; i++)
            {
                if (fillGrid[convertPos(Xpos), i, convertPos(Zpos)] != null)
                {
                    GameObject.FindWithTag("shadow").transform.position = new Vector3(GameObject.FindWithTag("shadow").transform.position.x, convertPos(i) + 0.25f, GameObject.FindWithTag("shadow").transform.position.z); //i+1
                    Debug.Log("Deal with Shadow in Button?");
                }
            }

        }
        return true;
    }

    public int findSide()
    {
        float angle;
        int tmp = 5;

        camPos = new Vector2(cam.transform.position.x, cam.transform.position.z);

        angle = Vector2.SignedAngle(new Vector2(1.5f, 1.5f), camPos);

        if (angle > 54 && angle < 180) //54 to 180
        {
            Debug.Log("player at Base");
            tmp = 0;
        }
        else if (angle > 0 && angle <= 54) //0 to 54
        {
            Debug.Log("player on Left");
            tmp = 1;
        }
        else if (angle > -54 && angle <= 0) //-54 to 0
        {
            Debug.Log("player on Upper side");
            tmp = 2;
        }
        else // -54 to -180
        {
            Debug.Log("Player on Right");
            tmp = 3;
        }

        return tmp;
    }

    // Button calls for Blocks script

    public void MoveForward() //zleft
    {
        blocksScript.MoveL = true;
    }

    public void MoveBack() //zright
    {
        blocksScript.MoveR = true;
    }

    public void MoveLeft()
    {
        blocksScript.MoveF = true;
    }

    public void MoveRight()
    {
        blocksScript.MoveB = true;

        //blocksScript.TestGrid = true; //////////////
    }

    //rotation
    public void RotForward()
    {
        blocksScript.RotZ = true;
    }

    public void RotBack() //Z
    {
        blocksScript = currentBlock.GetComponent<Blocks>();
        blocksScript.RotZ = true;
    }

    public void RotLeft() //x
    {
        blocksScript = currentBlock.GetComponent<Blocks>();
        blocksScript.RotX = true;
    }

    public void RotRight() //Y
    {
        blocksScript = currentBlock.GetComponent<Blocks>();
        blocksScript.RotY = true;
    }


    public void setPlaying()
    {
        isPlaying = true;
    }

    //copied from Blocks.cs
    float convertPos(int input)
    {
        //take pos 1-10 to pos by .25
        float output;

        output = input / 4.0f;

        return output;
    }

    int convertPos(float input)
    {
        //take pos by .25 to 1-10
        int output;
        float tmp;

        tmp = Mathf.Round((input - .125f) * 4) / 4; ////
        output = (int)Mathf.Round(tmp * 4);

        return output;
    }
}
