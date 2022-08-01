using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text myTime; //score


    private void Start()
    {
        myTime.text = PlayerPrefs.GetString("currentScore");
    }
}
