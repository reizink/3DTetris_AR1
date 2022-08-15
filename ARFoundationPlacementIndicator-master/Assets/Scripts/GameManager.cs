using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	// quit game
	public void quitGame()
	{
		Application.Quit();
	}

	// play game again
	public void playAgain() //gest
	{
		SceneManager.LoadScene("GestScene"); //GameScene, SampleScene
	}

	// start playing
	public void startPlay() //gest
	{
		SceneManager.LoadScene("GestScene"); //GameScene
    }

	// back to start
	public void back()
	{
		SceneManager.LoadScene("PlayScene");
	}

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void play2D()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void playagain2()
    {
        SceneManager.LoadScene("DemoScene");
    }
}
