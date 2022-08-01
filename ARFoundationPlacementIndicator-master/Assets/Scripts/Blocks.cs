
// Tetris 3D
// note: cinemachine package is used for camera control

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blocks : MonoBehaviour
{
    //
    private float prevTime;
    public float fallTime = 0.5f;
    // dimension of the grid cube
    public static int Xgrid = 10;//10 /4= 2.5
    public static int Ygrid = 10;//10
    public static int Zgrid = 10;//10
                                 //
    public Vector3 rotPoiint;
    // save fallen blocks position
    public static Transform[,,] fillGrid = new Transform[Xgrid, Ygrid + 5, Zgrid];
    // a game over flag
    private bool gameOver = false;
    bool keyPress = false;

    bool drop = false;

    public bool MoveL, MoveR, MoveB, MoveF;
    public bool RotX, RotY, RotZ, TestGrid;

    public Timer Timer;
    public string score = "00:00";

    // Start is called before the first frame update
    void Start()
    {
        Timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        Debug.Log("Timer: " + Timer.name);
        PlayerPrefs.SetString("currentScore", "0");
        //test convertPos
        //Debug.Log("convertPos: " + convertPos(0.325f));
        //Debug.Log("convertPos: " + convertPos(2.625f));
        //Debug.Log("convertPos: " + convertPos(6));
    }

    // Update is called once per frame
    void Update()
    {
        // check movement of shadow
        if (validMoveShadow())
        {
            GameObject.FindWithTag("shadow").transform.position -= new Vector3(0, 0.25f, 0);

            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0.25f, 0);
            }
        }

        // move the block down
        //    	
        if (Time.time - prevTime > fallTime)
        {
            transform.position += new Vector3(0, -0.25f, 0);

            if (!validMove())
            {  // undo the move
                transform.position -= new Vector3(0, -0.25f, 0);
                // add the block to grid
                addBlocks();
                drop = true;
                // check for line
                checkFullLine();
                // destroy the shadow
                Destroy(GameObject.FindWithTag("shadow"));
                this.tag = "Untagged";

                //Debug.Log(
                // disable and access spawn script
                this.enabled = false;
                FindObjectOfType<Spawn>().spawnBlock();

            }
            prevTime = Time.time;
        }

        //// move
        // quick down movement
        if (Input.GetKeyUp(KeyCode.Z))
        {
            transform.position += new Vector3(0, -0.25f, 0);
            if (!validMove())
            { // if movement is not valid undo it
                transform.position -= new Vector3(0, -0.25f, 0);
            }
        }

        // x-left
        if (Input.GetKeyUp(KeyCode.LeftArrow) || MoveL)
        {
            transform.position += new Vector3(-0.25f, 0, 0);
            // move the shadow
            GameObject.FindWithTag("shadow").transform.position += new Vector3(-0.25f, 0, 0);
            keyPress = true;
            //FindObjectOfType<addShadow>().dropShadow();
            //Instantiate(this.gameObject, new Vector3(5,0,5), Quaternion.identity);
            if (!validMove())
            { // if not valid undo
                transform.position -= new Vector3(-0.25f, 0, 0);
                // move the shadow
                GameObject.FindWithTag("shadow").transform.position -= new Vector3(-0.25f, 0, 0);
            }
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0.25f, 0);
            }
            MoveL = false;
        }
        // x-right
        if (Input.GetKeyUp(KeyCode.RightArrow) || MoveR)
        {
            transform.position += new Vector3(0.25f, 0, 0);
            // move the shadow
            GameObject.FindWithTag("shadow").transform.position += new Vector3(0.25f, 0, 0);
            keyPress = true;

            if (!validMove())
            { // if not valid undo
                transform.position -= new Vector3(0.25f, 0, 0);
                // move the shadow
                GameObject.FindWithTag("shadow").transform.position -= new Vector3(0.25f, 0, 0);
            }
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0.25f, 0);
            }
            MoveR = false;
        }
        // z-right
        if (Input.GetKeyUp(KeyCode.DownArrow) || MoveB)
        {
            transform.position += new Vector3(0, 0, -0.25f);
            // move the shadow
            GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0, -0.25f);
            keyPress = true;

            if (!validMove())
            { // if movement is not valid undo it
                transform.position -= new Vector3(0, 0, -0.25f);
                // move the shadow
                GameObject.FindWithTag("shadow").transform.position -= new Vector3(0, 0, -0.25f);
            }
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0.25f, 0);
            }
            MoveB = false;
        }
        // z-left
        if (Input.GetKeyUp(KeyCode.UpArrow) || MoveF)
        {
            transform.position += new Vector3(0, 0, 0.25f);
            // move the shadow
            GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0, 0.25f);
            keyPress = true;

            if (!validMove())
            { // if not valid undo
                transform.position -= new Vector3(0, 0, 0.25f);
                // move the shadow
                GameObject.FindWithTag("shadow").transform.position -= new Vector3(0, 0, 0.25f);
            }
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0.25f, 0);
            }
            MoveF = false;
        }

        //// rotate
        // A,S,D -> rotate in x,y,z axis
        // x
        if (Input.GetKeyUp(KeyCode.A) || RotX)
        { //
            transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(1, 0, 0), 90);
            // rotate shadow
            GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(1, 0, 0), 90);

            //Debug.Log(GameObject.FindWithTag("shadow").transform.position.y);
            if (!validMove())
            { // rotate back if it is not a valid move
                transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(1, 0, 0), -90);
                // rotate shadow
                GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(1, 0, 0), -90);
            }

            // if shadow is below y-axis move +1 up
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0.25f, 0);
            }
            RotX = false;
        }
        // y
        if (Input.GetKeyUp(KeyCode.S) || RotY)
        {
            transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0, 1, 0), 90);
            // rotate shadow
            GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0, 1, 0), 90);

            if (!validMove())
            { // rotate back if it is not a valid move
                transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0, 1, 0), -90);
                // rotate shadow
                GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0, 1, 0), -90);
            }
            // if shadow is below y-axis move +1 up
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0.25f, 0);
            }
            RotY = false;
        }

        //z
        if (Input.GetKeyUp(KeyCode.D) || RotZ)
        {
            transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0, 0, 1), 90);
            //rotate shadow
            GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0, 0, 1), 90);

            if (!validMove())
            { //rotate back if not valid
                transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0, 0, 1), -90);
                //rotate shadow
                GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0, 0, 1), -90);
            }
            //shadow is below y-axis move up
            if (!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0, 0.25f, 0);
            }
            RotZ = false;
        }

        // check for game over
        if (gameOver)
        { // load the game over scene
            score = Timer.tellTime.ToString();
            PlayerPrefs.SetString("currentScore", score); //

            SceneManager.LoadScene("GameOver");
        }

        if (TestGrid)
        {
            lookGrid();
            TestGrid = false;
        }
    }

    // define a function to check whether the movement is valid
    public bool validMove()
    {
        foreach (Transform children in transform)
        {

            //for all children
            float Xpos = children.transform.position.x;
            //Xpos = Mathf.RoundToInt(children.transform.position.x);
            float Ypos = children.transform.position.y;
            float Zpos = children.transform.position.z;

            //if exceed limit return false
            if (convertPos(Xpos) < 0 || convertPos(Xpos) >= Xgrid ||
                convertPos(Ypos) < 0 ||
                convertPos(Zpos) < 0 || convertPos(Zpos) >= Zgrid)
            {
                return false;
            }
            //check if taken
            if (fillGrid[convertPos(Xpos), convertPos(Ypos), convertPos(Zpos)] != null)
            {
                return false;
            }
        }
        return true;
    }

    //change it: whether reach end of fall on top of other block
    bool reachBottom()
    {
        foreach (Transform children in transform)
        {

            //for all children
            float Ypos = children.transform.position.y;
            //Mathf.RoundToInt(children.transform.position.y);
            //if any exceed limit return false
            if (convertPos(Ypos) <= 0)
            {
                Debug.Log("Reach Bottom");
                return false;
            }
        }

        return true;
    }

    //track fallen blocks
    void addBlocks()
    {
        foreach (Transform children in transform)
        {
            // for all children
            float Xpos = children.transform.position.x;
            //Xpos = Mathf.RoundToInt(children.transform.position.x);
            float Ypos = children.transform.position.y;
            float Zpos = children.transform.position.z;

            fillGrid[convertPos(Xpos), convertPos(Ypos), convertPos(Zpos)] = children;
            //Debug.Log("*******Added at: " + convertPos(Xpos) + " " + convertPos(Ypos) + " " + convertPos(Zpos) + "\nPos: " + Xpos + " " + Ypos + " " + Zpos);

            // check game over
            if (fillGrid[convertPos(Xpos), 10, convertPos(Zpos)] != null) //10
            {
                Debug.Log("Pos X: " + Xpos + "\nPos Y: " + Ypos + "\nPos Z: " + Zpos);
                gameOver = true;
                Debug.Log("game over");
            }
        }

    }

    //after block landed check full lines
    void checkFullLine()
    {
        for (int y = 0; y < Ygrid; y++)
        {
            checkPlane(y);
        }
    }

    //check rows xz
    void checkPlane(int y)
    {
        for (int x = 0; x < Xgrid; x++)
        {
            //check x 
            if (isLineX(x, y))
            { //if exists
                for (int z = 0; z < Zgrid; z++)
                {
                    Destroy(fillGrid[x, y, z].gameObject);
                    fillGrid[x, y, z] = null;
                }
                //Debug.Log("x:" + x);
                //Debug.Log("y:" + y);

                lineDownX(x, y);
            }
        }
        for (int z = 0; z < Zgrid; z++)
        {
            //check z
            if (isLineZ(y, z))
            { //if exists
                for (int x = 0; x < Xgrid; x++)
                {
                    Destroy(fillGrid[x, y, z].gameObject);
                    fillGrid[x, y, z] = null;
                }
                //Debug.Log("z:" + z);
                //Debug.Log("y:" + y);

                lineDownZ(z, y);
            }
        }
    }

    //check for line x
    bool isLineX(int x, int y)
    {
        for (int z = 0; z < Zgrid; z++)
        {
            if (fillGrid[x, y, z] == null)
            { //one block is empty false
                return false;
            }
        }
        return true;
    }

    //check for z
    bool isLineZ(int y, int z)
    {
        //bool isLine;
        for (int x = 0; x < Xgrid; x++)
        {
            if (fillGrid[x, y, z] == null)
            { //if one block is empty return false
                return false;
            }
        }
        return true;
    }

    void lineDownX(int x, int j)
    {
        for (int y = j; y < 10; y++)
        {
            for (int z = 0; z < Zgrid; z++)
            {
                if (fillGrid[x, y, z] != null)
                {
                    //Debug.Log("Test LineDown X");
                    fillGrid[x, y - 1, z] = fillGrid[x, y, z];
                    fillGrid[x, y, z] = null;
                    fillGrid[x, y - 1, z].transform.position -= new Vector3(0, 0.25f, 0); //1
                }
            }
        }
    }

    void lineDownZ(int z, int j)
    {
        for (int y = j; y < 10; y++)
        {
            for (int x = 0; x < Xgrid; x++)
            {
                if (fillGrid[x, y, z] != null)
                {
                    //Debug.Log("Test LineDown Z");
                    fillGrid[x, y - 1, z] = fillGrid[x, y, z];
                    fillGrid[x, y, z] = null;
                    fillGrid[x, y - 1, z].transform.position -= new Vector3(0, 0.25f, 0); //1
                }
            }
        }
    }

    //check valid shadow place

    bool validMoveShadow()
    {
        foreach (Transform children in GameObject.FindWithTag("shadow").transform)
        {
            float Xpos = children.transform.position.x;
            //Mathf.RoundToInt(children.transform.position.x);
            float Ypos = children.transform.position.y;
            float Zpos = children.transform.position.z;

            if (convertPos(Ypos) < 0)
            {
                //Debug.Log("Valid Shadow 1 : " + convertPos(Ypos));
                return false;
            }
            if (fillGrid[convertPos(Xpos), convertPos(Ypos), convertPos(Zpos)] != null)
            {
                //Debug.Log("Valid Shadow FillGrid: " + convertPos(Xpos) + " " + convertPos(Ypos) + " " + convertPos(Zpos));
                return false;
            }

            //shadow in button?
            for (int i = convertPos(Ypos); i < 10; i++)
            {
                if (fillGrid[convertPos(Xpos), i, convertPos(Zpos)] != null)
                {
                    GameObject.FindWithTag("shadow").transform.position = new Vector3(GameObject.FindWithTag("shadow").transform.position.x, convertPos(i) + 0.25f, GameObject.FindWithTag("shadow").transform.position.z); //i+1
                    //Debug.Log("Deal with Shadow in Button 2?");
                }
                //Debug.Log("Shadow Loop");
            }

        }
        return true;
    }




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

        tmp = Mathf.Round((input - .125f) * 4) / 4;
        output = (int)Mathf.Round(tmp * 4);

        return output;
    }


    void lookGrid()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    if (fillGrid[x, y, z] != null)
                    {
                        Debug.Log("Grid full at: " + x + " " + y + " " + z);
                    }
                }
            }
        }
    }

}
