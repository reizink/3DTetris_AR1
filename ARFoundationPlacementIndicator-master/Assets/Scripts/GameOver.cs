using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject currentScore;
    public InputField posOutput, gamePos;

    public Text myTime; //score
    public Text distance;

    private Text positionsList;

    private void Start()
    {
        myTime = currentScore.GetComponent<Text>();
        myTime.text = PlayerPrefs.GetString("currentScore");

        gamePos.text = PlayerPrefs.GetString("GameBoard");

        distance.text = "Distance Traveled: " + PlayerPrefs.GetFloat("Distance", 0.0f).ToString();
        posOutput.text = "Test Text";
        Debug.Log("Game Over Start test");
        Debug.Log("GameOver Script positions: " + posOutput.text);
        Debug.Log("X Test: " + PlayerPrefs.GetString("Xpositions"));
        //
        posOutput.text = "X: \n" + PlayerPrefs.GetString("Xpositions", "null") + "\nZ: \n" + PlayerPrefs.GetString("Zpositions", "null");
        //posOutput.text.Replace("\n", " ");
    }

    public void printPositions()
    {
        positionsList.text = "X: " + PlayerPrefs.GetString("Xpositions", "null") + "\nZ: " + PlayerPrefs.GetString("Zpositions", "null");

        posOutput.text = positionsList.text;

        Debug.Log("PositionsList: " + positionsList.text);
        //return positionsList;
    }
}
