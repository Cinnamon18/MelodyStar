using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Songs.Model;
using Songs.Gameplay;
using CustomInput;

public class ScorePanel : MonoBehaviour
{
    public Text scoreText;
	public Song song;
	public SongSetup songSetup;
    public SongManager songManager;
	public InputManager input;
    // Start is called before the first frame update
    
	void Awake()
	{
    }
	void Start()
    {
        if (!InputSettings.initalized)
        {
            InputSettings.setToDefault();
        }
        song = songManager.song;
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }
    public void updateScore()
    {
        if (song.songOver(Time.time - songManager.getStartTime()))
        {
            scoreText.text = "Perfect:" + songManager.numPerfect + "\n" +
                    "Good: " + songManager.numGood + "\n" +
                    "Miss: " + songManager.numMiss + "\n" + "Score: " + songManager.score + "Highest Combo: "; //+ songManager.getHighestCombo(); 
        }
	}
}
