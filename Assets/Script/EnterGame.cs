using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class EnterGame : MonoBehaviour
{
    public GameObject UIScene;
    public GameObject GameScene;
    public GameMainControl.GameModeSelect Mode;
    public CharacterPreset.TeamSelect MyTeam;
    public CharacterPreset.TeamSelect EnemyTeam;
    public CharacterPreset.WeaponSelect MyWeapon;
    public int WinUnit;
    
    // Start is called before the first frame update
    void Start()
    {
        WinUnit = 7;
        GameMainControl mainProcess = GameScene.GetComponent<GameMainControl>();
        mainProcess.Mode = GameMainControl.GameModeSelect.Rounded4v4;
        mainProcess.MyTeam = CharacterPreset.TeamSelect.Red;
        EnemyTeam = QuickSetTeam(CharacterPreset.TeamSelect.Red);
        mainProcess.MyWeapon = CharacterPreset.WeaponSelect.Knife;
        mainProcess.WinUnit = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        GameMainControl mainProcess = GameScene.GetComponent<GameMainControl>();
        mainProcess.Mode = Mode;
        mainProcess.MyTeam = MyTeam;
        EnemyTeam = QuickSetTeam(MyTeam);
        mainProcess.MyWeapon = MyWeapon;
        mainProcess.WinUnit = WinUnit;
        UIScene.SetActive(false);
        GameScene.SetActive(true);
    }

    CharacterPreset.TeamSelect QuickSetTeam(CharacterPreset.TeamSelect ExcludedTeam)
    {
        switch (ExcludedTeam)
        {
            case CharacterPreset.TeamSelect.Red:
                return CharacterPreset.TeamSelect.Blue;
            case CharacterPreset.TeamSelect.Yellow:
                return CharacterPreset.TeamSelect.Red;
            case CharacterPreset.TeamSelect.Green:
                return CharacterPreset.TeamSelect.Yellow;
            case CharacterPreset.TeamSelect.Blue:
                return CharacterPreset.TeamSelect.Green;
            default:
                Debug.Log("Enemy team setting error!");
                return CharacterPreset.TeamSelect.Red;
        }
    }

    public void SetGameMode(string modeName)
    {
        switch (modeName)
        {
            case "comp4v4":
                Mode = GameMainControl.GameModeSelect.Rounded4v4;
                break;
            case "dead4v4":
                Mode = GameMainControl.GameModeSelect.KillCount4v4;
                break;
            default:
                Debug.Log("Gamemode error! Received: " + modeName);
                break;
        }
    }

    public void SetTeam(Dropdown options)
    {
        int teamColor = options.value;
        switch (teamColor)
        {
            case 0:
                MyTeam = CharacterPreset.TeamSelect.Red;
                break;
            case 1:
                MyTeam = CharacterPreset.TeamSelect.Yellow;
                break;
            case 2:
                MyTeam = CharacterPreset.TeamSelect.Green;
                break;
            case 3:
                MyTeam = CharacterPreset.TeamSelect.Blue;
                break;
            default:
                Debug.Log("Team color error! Received: " + teamColor);
                break;
        }
    }

    public void SetWeapon(Dropdown options)
    {
        int weaponType = options.value;
        switch (weaponType)
        {
            case 0:
                MyWeapon = CharacterPreset.WeaponSelect.Knife;
                break;
            case 1:
                MyWeapon = CharacterPreset.WeaponSelect.Sword;
                break;
            case 2:
                MyWeapon = CharacterPreset.WeaponSelect.Spear;
                break;
            default:
                Debug.Log("Weapon setting error! Received: " + weaponType);
                break;
        }
    }

    public void SetWinUnit(Dropdown option)
    {
        int Counter = option.value;
        string mode = option.transform.parent.name;
        switch(mode)
        {
            case "Competitive4v4":
                switch (Counter)
                {
                    case 0:
                        WinUnit = 7;
                        break;
                    case 1:
                        WinUnit = 9;
                        break;
                    case 2:
                        WinUnit = 11;
                        break;
                    case 3:
                        WinUnit = 13;
                        break;
                    default:
                        Debug.Log("Winning condition error!");
                        break;
                }
                break;
            case "TeamDeadmatch4v4":
                switch (Counter)
                {
                    case 0:
                        WinUnit = 50;
                        break;
                    case 1:
                        WinUnit = 100;
                        break;
                    case 2:
                        WinUnit = 150;
                        break;
                    default:
                        Debug.Log("Winning condition error!");
                        break;
                }
                break;
        }
    }
}
