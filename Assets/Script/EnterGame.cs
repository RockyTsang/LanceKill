using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class EnterGame : MonoBehaviour
{
    public GameObject UIScene;
    public GameObject GameScene;
    public GameObject[] ModePanels; 
    private GameMainControl.GameModeSelect Mode;
    public Dropdown TeamSelection;
    private CharacterPreset.TeamSelect MyTeam;
    private CharacterPreset.TeamSelect EnemyTeam;
    public Dropdown WeaponSelection;
    private CharacterPreset.WeaponSelect MyWeapon;
    public Dropdown RoundsOrKills;
    private int WinUnit;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject panel in ModePanels)
        {
            if (panel.activeInHierarchy)
            {
                switch (panel.name)
                {
                    case "Competitive4v4":
                        Mode = GameMainControl.GameModeSelect.Rounded4v4;
                        switch (RoundsOrKills.value)
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
                                Debug.Log("Illegal win unit!");
                                WinUnit = 0;
                                break;
                        }
                        break;
                    case "TeamDeadmatch4v4":
                        Mode = GameMainControl.GameModeSelect.KillCount4v4;
                        switch (RoundsOrKills.value)
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
                                Debug.Log("Illegal win unit!");
                                WinUnit = 0;
                                break;
                        }
                        break;
                }
            }
        }
        switch (TeamSelection.value)
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
        }
        EnemyTeam = QuickSetTeam(MyTeam);
        switch (WeaponSelection.value)
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
        }
    }

    public void GameStart()
    {
        GameMainControl mainProcess = GameScene.GetComponent<GameMainControl>();
        mainProcess.Mode = Mode;
        mainProcess.MyTeam = MyTeam;
        mainProcess.EnemyTeam = EnemyTeam;
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
}
