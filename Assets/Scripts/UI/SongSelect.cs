using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using GameLoop;

[RequireComponent(typeof(Animator))]
public class SongSelect : MonoBehaviour {
	Animator animator;

	public TextMeshProUGUI bandNameText;
	public TextMeshProUGUI songNameText;
	public TextMeshProUGUI difficultyText;

	// Start is called before the first frame update
	void Start() {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {

	}

	public void SelectSong(string bandName, string songName) {
		var a = new GameLoop.SongScene(bandName, songName);
		GameLoopManager.gameLoop = new GameLoopScenes(
					new List<GameScene>() {
						new SongScene(bandName, songName),
						new ReturnToSongSelect(),
					}
				);
		StartCoroutine(GameObject.FindObjectOfType<GameLoop.GameLoopManager>().advance());
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
