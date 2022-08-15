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

    private Vector3 startPos;
    private Vector3 curPos, pastPos;
    private float totalDist;

    private float secs;
    private int mins, storePrev;
    private string xPosRec, zPosRec;
    //private bool testOnce;

    void Start()
    {
        startPos = player.transform.position;
        curPos = startPos;
        pastPos = startPos;

        secs = mins = 0;
        storePrev = 1;
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

        int tmp = 5; //error number
        tmp = getOrientation();
        Debug.Log("Orientation side: " + tmp);

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
