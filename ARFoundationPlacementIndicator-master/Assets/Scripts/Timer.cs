using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public GameOver GameOver;
    public Text tellTime;

    private bool startTimer;
    private float secCount;
    private int minCount;

    // Start is called before the first frame update
    void Start()
    {
        tellTime.text = "00:00";
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
    }

    public void startTime()
    {
        startTimer = true;
    }

}
