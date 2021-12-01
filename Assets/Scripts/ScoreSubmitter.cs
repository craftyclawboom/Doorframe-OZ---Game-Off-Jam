using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSubmitter : MonoBehaviour
{
    public TMP_InputField scoreInput;
    public TMP_InputField usernameInput;
    LeaderboardManager leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        leaderboard = GetComponent<LeaderboardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubmitScore()
    {
        int score = int.Parse(scoreInput.text);
        leaderboard.SendLeaderboard(score, usernameInput.text);
        
    }
}
