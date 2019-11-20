using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class SongEntry : MonoBehaviour
{
    public string bandName;
    public string songName;

    public TextMeshProUGUI songText;
    public TextMeshProUGUI bandText;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickThisSong() {
        GameObject.FindObjectOfType<SongSelect>().SelectSong(bandName, songName);
    }

    public void Initialize() {
        //Songs.Gameplay.SongSetup ss = GameObject.FindObjectOfType<Songs.Gameplay.SongSetup>();
        //Songs.Model.Song s = ss.readSong(bandName, songName);

        //SongSelect songSelect = GameObject.FindObjectOfType<SongSelect>();

        songText.text = songName;
        bandText.text = bandName;
    }
}
