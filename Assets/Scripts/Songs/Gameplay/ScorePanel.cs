using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Songs.Model;
using Songs.Gameplay;
using CustomInput;
using GameLoop;

public class ScorePanel : MonoBehaviour
{
    public Text scoreText;
	public Text messageText;
	public Text reminderText;
	public Song song;
	public Sprite idol;
	public SongSetup songSetup;
    public SongManager songManager;
	public InputManager input;
	public bool go = true;
    // Start is called before the first frame update
    
	void Awake()
	{
        if (!InputSettings.initalized)
        {
            InputSettings.setToDefault();
        }
    }
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (songManager.song.songOver(Time.time - songManager.songStartTime) && go)
		{
			updateScore();
			reminderText.text = "Press Any Key to Continue";
			if (Input.anyKeyDown)
			{
				Debug.Log("Next Song");
				StartCoroutine(Object.FindObjectOfType<GameLoopManager>().advance());
				go = false;
			}
		}
    }
    public void updateScore()
    {
		messageText.text = "Good Job!";
		scoreText.text = "\nScore: " + songManager.score + "\nPerfect: " + songManager.numPerfect + "\n" +
				"Good: " + songManager.numGood + "\n" +
				"Miss: " + songManager.numMiss + "\nHighest Combo : "
					+ songManager.highestCombo;
	}
}
