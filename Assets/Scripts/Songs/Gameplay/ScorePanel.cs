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
	public GameObject Panel;
    public Text scoreText;
	public Text messageText;
	public Text reminderText;
	public Song song;
	public Sprite Idol;
	public Sprite Rock;
	public Sprite Chiptune;
	public Sprite Frog;
	public Image CharacterImage;
	public SongSetup songSetup;
    public SongManager songManager;
	public InputManager input;
	public bool go;
	public bool goScore;
    // Start is called before the first frame update
    
	void Awake()
	{
        if (!InputSettings.initalized)
        {
            InputSettings.setToDefault();
        }
		reminderText.text = "Hello";
		Panel.SetActive(false);
	}
	void Start()
    {
		go = true;
		goScore = true;
	}

    // Update is called once per frame
    void Update()
     {
		if (songManager.song.songOver(Time.time - songManager.songStartTime) && go)
		{
			if(goScore)
			{
				updateScore();
				StartCoroutine(BlinkText());
				goScore = false;
			}
			if (Input.anyKeyDown)
			{
				Debug.Log("Next Scene");
				StartCoroutine(Object.FindObjectOfType<GameLoopManager>().advance());
				go = false;
			}
		}
    }
    public void updateScore()
    {
		Panel.SetActive(true);
		reminderText.text = "Press Any Key to Continue";
		messageText.text = "Good Job!";
		scoreText.text = "\nScore: " + songManager.score + "\nPerfect: " + songManager.numPerfect + "\n" +
				"Good: " + songManager.numGood + "\n" +
				"Miss: " + songManager.numMiss + "\nHighest Combo : "
					+ songManager.highestCombo;
		if (songManager.bandName.Equals("JPop")) {
			CharacterImage.sprite = Idol;
		} else if (songManager.bandName.Equals("Metal"))
		{
			CharacterImage.sprite = Rock;
		} else if (songManager.bandName.Equals("Chiptune"))
		{
			CharacterImage.sprite = Chiptune;
		} else if (songManager.bandName.Equals("Frog"))
		{
			CharacterImage.sprite = Frog;
		} else
		{
			CharacterImage.sprite = Idol;
		}
	}
	public IEnumerator BlinkText()
	{
		//blink it forever. You can set a terminating condition depending upon your requirement
		while (1 > 0)
		{
			//set the Text's text to blank
			reminderText.text = "";
			//display blank text for 0.5 seconds
			yield return new WaitForSeconds(.5f);
			//display “I AM FLASHING TEXT” for the next 0.5 seconds
			reminderText.text = "Press Any Key to Continue";
			yield return new WaitForSeconds(.5f);
		}
	}
}
