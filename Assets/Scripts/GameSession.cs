using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {

    int score = 0;

    // If you want to use a seperate "Splash/PreLoad screen" then use this approach
    /*
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    */

    // Singleton approach:

    private void Awake()
    {
        SetUpSingleton(); 
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
