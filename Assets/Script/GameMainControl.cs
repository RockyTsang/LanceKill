using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameMainControl : MonoBehaviour
{
    public enum GameModeSelect{
        Rounded4v4,
        KillCount4v4
    }
    public GameModeSelect Mode;
    public int WinUnit;
    public enum MapSelect
    {
        Map1
    }
    public MapSelect Map;
    public CharacterPreset.TeamSelect MyTeam;
    public CheckTeammate Team1;
    public CharacterPreset.TeamSelect EnemyTeam;
    public CheckTeammate Team2;
    public CharacterPreset.WeaponSelect MyWeapon;
    public int MyArmor;
    public GameObject[] Maps;
    public GameObject[] SpawnPoints;
    public GameObject Avatar;
    public GameObject[] Players;
    public Camera GameCamera;

    public UIControl UILobby;
    public Announcement AnnouncementWindow;
    public GameObject PauseWindow;
    private bool PauseWindowActive = false;
    public Text CountDownText;
    private bool Countingdown = false;

    public int team1wincount;
    public int team2wincount;

    // Start is called before the first frame update
    void OnEnable()
    { 
        // Set map
        switch (Map)
        {
            case MapSelect.Map1:
                Instantiate(Maps[0], transform).name = "Map";
                break;
        }

        // instantiate avatars to each spawnpoints
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach(GameObject SpawnPoint in SpawnPoints)
        {
            switch(SpawnPoint.transform.parent.name)
            {
                case "BottomSpawnFeild":
                    GameObject newPlayer1 = Instantiate(Avatar, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform.Find("Team1"));
                    newPlayer1.GetComponent<CharacterPreset>().Team = MyTeam;
                    newPlayer1.GetComponent<CharacterPreset>().SpawnPosition = SpawnPoint.transform;
                    break;
                case "TopSpawnFeild":
                    GameObject newPlayer2 = Instantiate(Avatar, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform.Find("Team2"));
                    newPlayer2.GetComponent<CharacterPreset>().Team = EnemyTeam;
                    newPlayer2.GetComponent<CharacterPreset>().SpawnPosition = SpawnPoint.transform;
                    break;
            }
        }

        switch (MyWeapon)
        {
            case CharacterPreset.WeaponSelect.Knife:
                GameObject.Find("LongAttackIcon").GetComponent<Image>().sprite = GameObject.Find("LongAttackIcon").GetComponent<SkillCoolDown>().IconSprites[0];
                break;
            case CharacterPreset.WeaponSelect.Sword:
                GameObject.Find("LongAttackIcon").GetComponent<Image>().sprite = GameObject.Find("LongAttackIcon").GetComponent<SkillCoolDown>().IconSprites[1];
                break;
            case CharacterPreset.WeaponSelect.Spear:
                GameObject.Find("LongAttackIcon").GetComponent<Image>().sprite = GameObject.Find("LongAttackIcon").GetComponent<SkillCoolDown>().IconSprites[2];
                break;
            case CharacterPreset.WeaponSelect.Musou:
                GameObject.Find("LongAttackIcon").GetComponent<Image>().sprite = GameObject.Find("LongAttackIcon").GetComponent<SkillCoolDown>().IconSprites[3];
                break;
        }

        // Set identity of avatars
        Players = GameObject.FindGameObjectsWithTag("Player");
        System.Random RandomSeed = new System.Random(System.DateTime.Now.Minute);
        int RandNumber = RandomSeed.Next(0, 4);
        int PointerOfMe = 0;
        for (int i = 0; i < Players.Length; i++)
        {
            if(Players[i].GetComponent<CharacterPreset>().Team == MyTeam)
            {
                if (PointerOfMe == RandNumber)
                {
                    Players[i].GetComponent<CharacterPreset>().Type = CharacterPreset.Identity.Me;
                    Players[i].GetComponent<CharacterPreset>().WeaponType = MyWeapon;
                    Players[i].GetComponent<CharacterPreset>().armor = MyArmor;
                }
                else
                {
                    GenerateIDAndWeapon(i);
                }
                PointerOfMe++;
            }
            else
            {
                GenerateIDAndWeapon(i);
            }
            Players[i].GetComponent<CharacterPreset>().enabled = true;
        }
        GameCamera.gameObject.GetComponent<CameraFollowAvatar>().enabled = true;

        team1wincount = 0;
        team2wincount = 0;
        GameObject.Find("UpperPanel").GetComponent<UpperPanel>().Main = this;
        GameObject.Find("UpperPanel").GetComponent<UpperPanel>().enabled = true;
        Team1.GetComponent<CheckTeammate>().enabled = true;
        Team2.GetComponent<CheckTeammate>().enabled = true;
        EngagingCountDown = 3;
        InvokeRepeating("EngageAllPlayer", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        PauseWindowActive = PauseWindow.activeInHierarchy;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Countingdown)
            {
                if (!PauseWindowActive)
                {
                    PauseWindow.SetActive(true);
                    PauseGame();
                }
                else
                {
                    PauseWindow.SetActive(false);
                    ResumeGame();
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Turn-based mode
        if (Mode == GameModeSelect.Rounded4v4)
        {
            if(Team1.surviving != Team2.surviving)
            {
                PauseGame();
                
                // Determine win and lose
                if (Team1.surviving && !Team2.surviving)
                {
                    team1wincount++;
                    AnnouncementWindow.ShowResult(Team1.myTeam.ToString(), team1wincount, team2wincount);
                }else if(!Team1.surviving && Team2.surviving)
                {
                    team2wincount++;
                    AnnouncementWindow.ShowResult(Team2.myTeam.ToString(), team1wincount, team2wincount);
                }
                AnnouncementWindow.gameObject.SetActive(true);
                StartCoroutine(AnnouncementWindow.HideWindow(3));
                if (team1wincount >= Mathf.Round((float)WinUnit / 2) || team2wincount >= Mathf.Round((float)WinUnit / 2))
                {
                    StartCoroutine(EndGame(3));
                }
                else
                {
                    StartCoroutine(ResetRound());
                }
            }
        }

        // Deadmatch mode
        if(Mode == GameModeSelect.KillCount4v4)
        {
            if(team1wincount >= WinUnit || team2wincount >= WinUnit)
            {
                PauseGame();

                // Determine win and lose
                if (team1wincount >= WinUnit)
                {
                    AnnouncementWindow.ShowResult(Team1.myTeam.ToString(), team1wincount, team2wincount);
                }
                else if (team2wincount >= WinUnit)
                {
                    AnnouncementWindow.ShowResult(Team2.myTeam.ToString(), team1wincount, team2wincount);
                }
                AnnouncementWindow.gameObject.SetActive(true);
                StartCoroutine(AnnouncementWindow.HideWindow(3));
                StartCoroutine(EndGame(3));
            }
        }
    }

    // Random set weapon
    void GenerateIDAndWeapon(int i)
    {
        System.Random RandomSeed = new System.Random(System.DateTime.Now.Millisecond);
        int RandWeapon = RandomSeed.Next(0, 3);
        int RandArmor = RandomSeed.Next(0, 3);
        switch (RandWeapon)
        {
            case 0:
                Players[i].GetComponent<CharacterPreset>().WeaponType = CharacterPreset.WeaponSelect.Knife;
                break;
            case 1:
                Players[i].GetComponent<CharacterPreset>().WeaponType = CharacterPreset.WeaponSelect.Sword;
                break;
            case 2:
                Players[i].GetComponent<CharacterPreset>().WeaponType = CharacterPreset.WeaponSelect.Spear;
                break;
        }
        switch (RandArmor)
        {
            case 0:
                Players[i].GetComponent<CharacterPreset>().armor = 0;
                break;
            case 1:
                Players[i].GetComponent<CharacterPreset>().armor = 10;
                break;
            case 2:
                Players[i].GetComponent<CharacterPreset>().armor = 20;
                break;
        }
        Players[i].GetComponent<CharacterPreset>().Type = CharacterPreset.Identity.AI;
    }

    // Start all player's movement
    private int EngagingCountDown;
    void EngageAllPlayer()
    {
        switch (EngagingCountDown)
        {
            case 3:
                CountDownText.text = "3";
                CountDownText.gameObject.SetActive(true);
                Countingdown = true;
                EngagingCountDown--;
                break;
            case 2:
                CountDownText.text = "2";
                EngagingCountDown--;
                break;
            case 1:
                CountDownText.text = "1";
                EngagingCountDown--;
                break;
            case 0:
                switch (CountDownText.GetComponent<LanguageHandler>().language)
                {
                    case LanguageHandler.LanguageSelection.English:
                        CountDownText.text = CountDownText.GetComponent<LanguageHandler>().English;
                        break;
                    case LanguageHandler.LanguageSelection.SimplifiedChinese:
                        CountDownText.text = CountDownText.GetComponent<LanguageHandler>().SimplifiedChinese;
                        break;
                    case LanguageHandler.LanguageSelection.TraditionalChinese:
                        CountDownText.text = CountDownText.GetComponent<LanguageHandler>().TraditionalChinese;
                        break;
                }
                this.CancelInvoke();
                foreach (GameObject player in Players)
                {
                    switch (player.GetComponent<CharacterPreset>().Type)
                    {
                        case CharacterPreset.Identity.Me:
                            player.GetComponent<AvatarControl>().enabled = true;
                            break;
                        case CharacterPreset.Identity.TestObject:
                            break;
                        case CharacterPreset.Identity.AI:
                            player.GetComponent<NonPlayerAI>().enabled = true;
                            break;
                    }
                }
                Invoke("CloseCountDown", 0.5f);
                break;
        } 
    }

    void CloseCountDown()
    {
        CountDownText.gameObject.SetActive(false);
        Countingdown = false;
    }

    public void CallSlained(CharacterPreset PresetScript)
    {
        StartCoroutine(Slained(PresetScript));
    }
    
    IEnumerator Slained(CharacterPreset PresetScript)
    {
        if(PresetScript.Team == MyTeam)
        {
            team2wincount++;
        }else if(PresetScript.Team == EnemyTeam)
        {
            team1wincount++;
        }
        yield return new WaitForSecondsRealtime(3);
        PresetScript.ResetBody();
        PresetScript.gameObject.SetActive(true);
    }

    public void PauseWindowCallBack(bool quit)
    {
        if (quit)
        {
            PauseWindow.SetActive(false);
            StartCoroutine(EndGame(0));
        }
        else
        {
            PauseWindow.SetActive(false);
            ResumeGame();
        }
    }

    void PauseGame()
    {
        foreach (GameObject player in Players)
        {
            if (player.GetComponent<CharacterPreset>().Type == CharacterPreset.Identity.Me)
            {
                player.GetComponent<AvatarControl>().enabled = false;
            }
            else if (player.GetComponent<CharacterPreset>().Type == CharacterPreset.Identity.AI)
            {
                player.GetComponent<NonPlayerAI>().enabled = false;
            }
        }
        Time.timeScale = 0;
    }
    
    void ResumeGame()
    {
        foreach (GameObject player in Players)
        {
            if (player.GetComponent<CharacterPreset>().Type == CharacterPreset.Identity.Me)
            {
                player.GetComponent<AvatarControl>().enabled = true;
            }
            else if (player.GetComponent<CharacterPreset>().Type == CharacterPreset.Identity.AI)
            {
                player.GetComponent<NonPlayerAI>().enabled = true;
            }
        }
        Time.timeScale = 1;
    }

    IEnumerator ResetRound()
    {
        foreach (GameObject player in Players)
        {
            player.GetComponent<CharacterPreset>().ResetBody();
            player.gameObject.SetActive(true);
            //Debug.Log(player.activeInHierarchy);
        }
        Team1.surviving = true;
        Team2.surviving = true;
        yield return new WaitForSecondsRealtime(4);
        Time.timeScale = 1;
        EngagingCountDown = 3;
        InvokeRepeating("EngageAllPlayer", 1f, 1f);
    }

    IEnumerator EndGame(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Team1.DestroyPlayers();
        Team2.DestroyPlayers();
        GameObject.Find("HPBarFrame").GetComponent<HPBar>().enabled = false;
        GameObject.Find("LongAttackIcon").GetComponent<SkillCoolDown>().enabled = false;
        GameObject.Find("CrushIcon").GetComponent<SkillCoolDown>().enabled = false;
        GameObject.Find("HealIcon").GetComponent<SkillCoolDown>().enabled = false;
        GameObject.Find("UpperPanel").GetComponent<UpperPanel>().enabled = false;
        Team1.surviving = true;
        Team2.surviving = true;
        Team1.GetComponent<CheckTeammate>().enabled = false;
        Team2.GetComponent<CheckTeammate>().enabled = false;
        team1wincount = 0;
        team2wincount = 0;
        Destroy(transform.Find("Map").gameObject);
        Players = new GameObject[] { null };
        GameCamera.GetComponent<CameraFollowAvatar>().ResetArrow();
        UILobby.BackToLobby();
        gameObject.SetActive(false);
        UILobby.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
