using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientControls : MonoBehaviour
{

    public GameObject BRcorner, BLcorner, URcorner, ULcorner, gameCenter;
    public TrackPos TrackPos;
    public float playerAngle;
    public int mySide;
    // mySide bottom is default
    // bottom/base = 0, Left = 1, Upper = 2, Right = 3;

    private GameObject myPlayer;
    private float centerX, centerZ, angleBL, angleBR, angleUL, angleUR;
    private Vector2 centerAngle;

    // Start is called before the first frame update
    void Start()
    {
        centerX = gameCenter.transform.position.x;
        centerZ = gameCenter.transform.position.z;
        centerAngle = new Vector2(centerX, centerZ);

        PlayerPrefs.SetString("GameBoard", centerAngle.ToString());

        angleBL = Vector2.SignedAngle(centerAngle, new Vector2(BLcorner.transform.position.x, BLcorner.transform.position.z)); //54
        angleBR = Vector2.SignedAngle(centerAngle, new Vector2(BRcorner.transform.position.x, BRcorner.transform.position.z)); //180
        angleUL = Vector2.SignedAngle(centerAngle, new Vector2(ULcorner.transform.position.x, ULcorner.transform.position.z)); //0
        angleUR = Vector2.SignedAngle(centerAngle, new Vector2(URcorner.transform.position.x, URcorner.transform.position.z)); //-54
        Debug.Log("Starting angles: " + angleBL + "\n" + angleBR + "\n" + angleUL + "\n" + angleUR);

        if (GameObject.FindWithTag("Track").GetComponent<TrackPos>() != null && TrackPos.player != null)
        {
            TrackPos = GameObject.FindWithTag("Track").GetComponent<TrackPos>();
            myPlayer = TrackPos.player;
            Debug.Log("Found Tracking***");
        }
        else
            myPlayer = GameObject.FindWithTag("center");
        
    }

    // Update is called once per frame
    void Update()
    {
        playerAngle = Vector2.SignedAngle(centerAngle, new Vector2(myPlayer.transform.position.x, myPlayer.transform.position.z));

        Debug.Log("Player angle: " + playerAngle);

        if (playerAngle > angleBL && playerAngle < angleBR) //54 to 180
        {
            Debug.Log("player at Base");
            mySide = 0;
        }
        else if (playerAngle > angleUL && playerAngle < angleBL) //0 to 54
        {
            Debug.Log("player on Left");
            mySide = 1;
        }
        else if (playerAngle > angleUR && playerAngle < angleUL) //-54 to 0
        {
            Debug.Log("player on Upper side");
            mySide = 2;
        }
        else // -54 to -180
        {
            Debug.Log("Player on Right");
            mySide = 3;
        }
    }
}
