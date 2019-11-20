using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(Animator))]
public class SongSelect : MonoBehaviour
{
    Animator animator;

    public TextMeshProUGUI bandNameText;
    public TextMeshProUGUI songNameText;
    public TextMeshProUGUI difficultyText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSong(string bandName, string songName) {
        var a = new GameLoop.SongScene(bandName, songName);
        GameObject.FindObjectOfType<GameLoop.GameLoopManager>().nextScene = a;
        StartCoroutine(SceneTransition.LoadScene(a.getSceneType()));
    }

    public void HighlightSong(SongEntry se) {
        
        Songs.Gameplay.SongSetup ss = GameObject.FindObjectOfType<Songs.Gameplay.SongSetup>();

        GameObject.FindObjectOfType<Enneagram>().CreateEnneagram(se.songName);
        //Songs.Model.Song s = ss.readSong(se.bandName, se.songName);
        //bandNameText.text = se.bandName;
        //songNameText.text = se.songName;
        //float diff = ss.getDifficulty(s.notes);
        //difficultyText.text = "" + diff;
    }
}
