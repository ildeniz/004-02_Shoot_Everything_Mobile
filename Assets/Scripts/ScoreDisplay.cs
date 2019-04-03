using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    TextMeshProUGUI scoreText;
    GameSession gameSession;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = gameSession.GetScore().ToString();
	}
}
