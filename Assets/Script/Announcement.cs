using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Announcement : MonoBehaviour
{
    public Text MainMessage;
    public Text Team1;
    public Text Team2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResult(string WinTeam, int team1count, int team2count)
    {
        MainMessage.text = WinTeam + " team win!";
        Team1.text = team1count.ToString();
        Team2.text = team2count.ToString();
    }

    public void HideWindow()
    {
        gameObject.SetActive(false);
    }
}
