using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SendLeaderboardWin : MonoBehaviour
{
    public LeaderboardManager leaderboardAPI;
    public TMP_InputField usernameInput;
    public Button submit;
    public GameObject firstPage;
    public GameObject secondPage;

    private void Update()
    {
        if(usernameInput.text.Trim().Length == 0)
        {
            submit.interactable = false;
        }
        else
        {
            submit.interactable = true;
        }
    }

    // Start is called before the first frame update
    public void SendLeaderboard()
    {
        leaderboardAPI.SendLeaderboard(GameManager.instance.timeToBeat, usernameInput.text.Trim());
        ClearPopup();
        leaderboardAPI.GetLeaderboard();
    }
    void ClearPopup()
    {
        firstPage.SetActive(false);
        secondPage.SetActive(true);
    }
}
