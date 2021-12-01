using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;
    public int MainMenuIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    // Update is called once per frame
    void Login()
    {
        float num = Random.Range(1, 9999999999999999);

        if(PlayerPrefs.GetFloat("PlayerLogin_") != 0)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = PlayerPrefs.GetFloat("PlayerLogin_").ToString(),

                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
        }
        else
        {
            PlayerPrefs.SetFloat("PlayerLogin_", num);

            var request = new LoginWithCustomIDRequest
            {
                CustomId = num.ToString(),

                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
        }
        
    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful Playfab login.");
        if (SceneManager.GetActiveScene().buildIndex == MainMenuIndex)
        {
            Debug.Log("Downloading Leaderboard Data...");
            GetLeaderboard();
        }
    }
    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score, string displayName)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate
                {
                    StatisticName = "Global Leaderboard",
                    Value = -score
                }
            }
        };
        var usernamerequest = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(usernamerequest, OnUsernameUpdate, OnError);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
        GetLeaderboard();
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful Leaderboard Sent");
    }

    void OnUsernameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Username successfully inputted");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Global Leaderboard",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);

    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = Mathf.Abs(item.StatValue).ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            Debug.Log(string.Format("PLACE: {0} | ID: {1} | VALUE: {2}",
                item.Position, item.DisplayName, item.StatValue));
        }
    }

}
