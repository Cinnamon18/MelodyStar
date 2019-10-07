using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public Text scoreText;
    public Text scoreGreat;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }
    public void updateScore()
    {
        scoreText.text = "Perfect:" + Songs.Gameplay.SongManager.getPerfect() + "\n" +
            "Good: " + Songs.Gameplay.SongManager.getGood() + "\n" +
            "Miss: " + Songs.Gameplay.SongManager.getMiss();


    }
}
