using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongEntry : MonoBehaviour
{
    public string bandName;
    public string songName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickThisSong() {
        GameObject.FindObjectOfType<SongSelect>().SelectSong(bandName, songName);
    }
}
