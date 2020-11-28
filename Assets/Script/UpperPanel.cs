using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpperPanel : MonoBehaviour
{
    public GameMainControl Main;
    public Text Team1;
    public Text Team2;

    // Start is called before the first frame update
    void OnEnable()
    {
        Team1 = transform.Find("Team1Counter").GetComponent<Text>();
        Team2 = transform.Find("Team2Counter").GetComponent<Text>();
        Team1.color = SetColor(Main.MyTeam);
        Team2.color = SetColor(Main.EnemyTeam);
        Team1.text = Main.team1wincount.ToString();
        Team2.text = Main.team2wincount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Team1.text = Main.team1wincount.ToString();
        Team2.text = Main.team2wincount.ToString();
    }

    Color SetColor(CharacterPreset.TeamSelect team)
    {
        switch (team)
        {
            case CharacterPreset.TeamSelect.Red:
                return Color.red;
            case CharacterPreset.TeamSelect.Yellow:
                return Color.yellow;
            case CharacterPreset.TeamSelect.Green:
                return Color.green;
            case CharacterPreset.TeamSelect.Blue:
                return Color.blue;
            default:
                Debug.Log("UpperPanel: SetColor Error!");
                return Color.white;
        }
    }
}
