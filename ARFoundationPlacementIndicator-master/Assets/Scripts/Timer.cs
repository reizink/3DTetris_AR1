using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public GameOver GameOver;
    public Text tellTime;

    private bool startTimer;
    private float secCount, angle;
    private int minCount;

    public GameObject cam, movePanel, rotPanel, gameParent;
    private Vector2 camPos;
    private int side = 5;

    // Start is called before the first frame update
    void Start()
    {
        tellTime.text = "00:00";
        cam = GameObject.FindWithTag("MainCamera");
        movePanel = GameObject.FindWithTag("MovePanel");
        rotPanel = GameObject.FindGameObjectWithTag("RotPanel");
        gameParent = GameObject.FindWithTag("GameParent");
    }

    // Update is called once per frame
    void Update()
    {

        if (startTimer)
        {
            secCount += Time.deltaTime;
            if (secCount >= 60)
            {
                minCount++;
                secCount = 0;
            }

            tellTime.text = minCount + ":" + (int)secCount;
            //Debug.Log("Timer: " + minCount + ":" + (int)secCount);


        }
        else
            secCount = 0;


        //2D rotation?
        camPos = new Vector2(cam.transform.position.x, cam.transform.position.z);
        angle = Vector2.SignedAngle(new Vector2(gameParent.transform.position.x, gameParent.transform.position.z), camPos);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        //movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, angle);
        //rotPanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, angle);

        side = findSide();
        if(side == 1)
        {
            movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 90);
            rotPanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 90);
        } else if(side == 2)
        {
            movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 180);
            rotPanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 180);
        }
        else if(side == 3)
        {
            movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 270);
            rotPanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 270);
        }
        else
        {
            movePanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
            rotPanel.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        }

    }

    public void startTime()
    {
        startTimer = true;
    }

    public int findSide()
    {
        float angle1;
        int tmp = 5;

        camPos = new Vector2(cam.transform.position.x, cam.transform.position.z);

        angle1 = Vector2.SignedAngle(new Vector2(1.5f, 1.5f), camPos);

        if (angle1 > 54 && angle1 < 180) //54 to 180
        {
            Debug.Log("player at Base");
            tmp = 0;
        }
        else if (angle1 > 0 && angle1 <= 54) //0 to 54
        {
            Debug.Log("player on Left");
            tmp = 1;
        }
        else if (angle1 > -54 && angle1 <= 0) //-54 to 0
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

}
