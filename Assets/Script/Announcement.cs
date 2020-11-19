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
        switch (WinTeam)
        {
            case "Red":
                MainMessage.color = Color.red;
                break;
            case "Yellow":
                MainMessage.color = Color.yellow;
                break;
            case "Green":
                MainMessage.color = Color.green;
                break;
            case "Blue":
                MainMessage.color = Color.blue;
                break;
        }
        Team1.text = team1count.ToString();
        Team2.text = team2count.ToString();
        this.gameObject.SetActive(true);
    }

    public IEnumerator HideWindow(float time)
    {
        Debug.Log("Waiting");
        yield return new WaitForSecondsRealtime(time);
        Debug.Log("Waited");
        this.gameObject.SetActive(false);
    }
}
