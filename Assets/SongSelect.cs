using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SongSelect : MonoBehaviour
{
    Animator animator;

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
        StartCoroutine(SceneTransition.LoadScene(a.getSceneType()));
    }
}
