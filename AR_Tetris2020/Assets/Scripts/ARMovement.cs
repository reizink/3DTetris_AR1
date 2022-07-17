using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMovement : MonoBehaviour
{
    // CurrentBlock

    public Blocks blocksScript;


    public static int Xgrid = 10;
    public static int Ygrid = 10;
    public static int Zgrid = 10;
    //
    public Vector3 rotPoiint;
    // save fallen blocks position
    public static Transform[,,] fillGrid = new Transform[Xgrid, Ygrid + 5, Zgrid];

    public GameObject currentBlock;

    private void Update()
    {
        currentBlock = GameObject.FindWithTag("CurrentBlock");

        blocksScript = currentBlock.GetComponent<Blocks>();
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
            int Xpos = Mathf.RoundToInt(children.transform.position.x);
            int Ypos = Mathf.RoundToInt(children.transform.position.y);
            int Zpos = Mathf.RoundToInt(children.transform.position.z);

            // if any exceed the bouder limit return false
            if (Xpos < 0 || Xpos >= Xgrid ||
                Ypos < 0 ||
                Zpos < 0 || Zpos >= Zgrid)
            {
                return false;
            }
            // check if position already taken
            if (fillGrid[Xpos, Ypos, Zpos] != null)
            {
                return false;
            }
        }
        return true;
    }

    bool validMoveShadow()
    {
        foreach (Transform children in GameObject.FindWithTag("shadow").transform)
        {
            int Xpos = Mathf.RoundToInt(children.transform.position.x);
            int Ypos = Mathf.RoundToInt(children.transform.position.y);
            int Zpos = Mathf.RoundToInt(children.transform.position.z);

            if (Ypos < 0)
            {
                return false;
            }
            if (fillGrid[Xpos, Ypos, Zpos] != null)
            {
                return false;
            }

            // deal with shadow in button but block are over it
            for (int i = Ypos; i < 10; i++)
            {
                if (fillGrid[Xpos, i, Zpos] != null)
                    GameObject.FindWithTag("shadow").transform.position = new Vector3(GameObject.FindWithTag("shadow").transform.position.x,
                                                                                        i + 1,
                                                                                        GameObject.FindWithTag("shadow").transform.position.z);
            }

        }
        return true;
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
    }

    //rotation
    public void RotForward()
    {
        blocksScript.RotZ = true;
    }

    public void RotBack() //Z
    {
        blocksScript.RotZ = true;
    }

    public void RotLeft() //x
    {
        blocksScript.RotX = true;
    }

    public void RotRight() //Y
    {
        blocksScript.RotY = true;
    }
}
