using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackPos : MonoBehaviour
{

    public GameObject player;
    public GameObject GamePoint;
    public List<float> Xpositions;
    public List<float> Zpositions;

    public int curSide = 5;
    private GameObject movePanel;
    private GameObject RotPanel;

    private Vector3 startPos;
    private Vector3 curPos, pastPos;
    private float totalDist;

    private float secs;
    private int mins, storePrev;
    private string xPosRec, zPosRec;
    private float angle;

    void Start()
    {
        startPos = player.transform.position;
        curPos = startPos;
        pastPos = startPos;

        secs = mins = 0;
        storePrev = 1;

        movePanel = GameObject.FindWithTag("MovePanel");
        RotPanel = GameObject.FindWithTag("RotPanel");
    }

    void Update()
    {
        //total distance traveled
        curPos = player.transform.position;

        if (curPos != pastPos)
        {
            float dist = Vector3.Distance(pastPos, curPos);
            pastPos = curPos;
            totalDist += dist;

            print("Distance to other: " + dist);
        }

        PlayerPrefs.SetFloat("Distance", totalDist);

        positionLists();

        //int tmp = 5; //error number
        curSide = getOrientation();
        Debug.Log("Orientation side: " + curSide);

        //rotate panel??
        angle = Vector2.SignedAngle(new Vector2(GamePoint.transform.position.x, GamePoint.transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z));
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        //movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, angle);
        //RotPanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, angle);

        Debug.Log("Panel Angle: " + angle);
        /*
        if (curSide == 0)
        {
            //rotate Move panel
            movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 180);
        }
        else if(curSide == 1)
        {
            //rotate Move panel
            movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 90);
        }
        else if (curSide == 3)
        {
            //rotate Move panel
            movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, -90);
        }
        else //test is side 2, upper side, no
        {
            movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        }*/

    }

    public void positionLists()
    {
        //time played
        secs += Time.deltaTime;
        if (secs >= 60)
        {
            mins++;
            secs = 0;
        }

        if ((int)secs % 5 == 0 && (int)secs != storePrev)
        {
            Xpositions.Add(curPos.x);
            Zpositions.Add(curPos.z);

            //Debug.Log("5 Second Loop Works: " + Xpositions[1]);
            storePrev = (int)secs;

            xPosRec = "";
            zPosRec = "";
            for (int i = 0; i < Xpositions.Count; i++)
            {
                xPosRec += Xpositions[i].ToString() + "\n";
                zPosRec += Zpositions[i].ToString() + "\n";
            }

            PlayerPrefs.SetString("Xpositions", xPosRec);
            PlayerPrefs.SetString("Zpositions", zPosRec);

            Debug.Log("Recorded X: " + xPosRec);
            Debug.Log("Recorded Z: " + zPosRec);
        }
    }



    public int getOrientation()
    {
        int side = 0;

        if (Vector2.SignedAngle( new Vector2(GamePoint.transform.position.x, GamePoint.transform.position.z), curPos) > 45)
        {
            Debug.Log("player at Base");
            side = 0;
        }
        else if (Vector2.SignedAngle(new Vector2(GamePoint.transform.position.x, GamePoint.transform.position.z), curPos) > 0)
        {
            Debug.Log("player at Left");
            side = 1;
        }
        else if (Vector2.SignedAngle(new Vector2(GamePoint.transform.position.x, GamePoint.transform.position.z), curPos) > -53)
        {
            Debug.Log("player at Upper side");
            side = 2;
        }
        else
        {
            Debug.Log("player on right");
            side = 3;
        }

        return side;
    }
}
