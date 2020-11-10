using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameMainControl : MonoBehaviour
{
    public enum GameModeSelect{
        Rounded4v4,
        KillCount4v4
    }
    public GameModeSelect Mode;
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
    public GameObject[] Maps;
    public GameObject[] SpawnPoints;
    public GameObject Avatar;
    public GameObject[] Players;
    public Camera GameCamera;

    private int team1wincount;
    private int team2wincount;

    // Start is called before the first frame update
    void Start()
    {
        switch (Map)
        {
            case MapSelect.Map1:
                Instantiate(Maps[0], transform);
                break;
        }

        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach(GameObject SpawnPoint in SpawnPoints)
        {
            switch(SpawnPoint.transform.parent.name)
            {
                case "BottomSpawnFeild":
                    Instantiate(Avatar, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform.Find("Team1")).GetComponent<CharacterPreset>().Team = MyTeam;
                    break;
                case "TopSpawnFeild":
                    Instantiate(Avatar, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform.Find("Team2")).GetComponent<CharacterPreset>().Team = EnemyTeam;
                    break;
            }
        }

        Players = GameObject.FindGameObjectsWithTag("Player");
        System.Random RandomSeed = new System.Random(System.DateTime.Now.Minute);
        int RandNumber = RandomSeed.Next(0, 4);
        //Debug.Log(RandNumber);
        int PointerOfMe = 0;
        for (int i = 0; i < Players.Length; i++)
        {
            if(Players[i].GetComponent<CharacterPreset>().Team == MyTeam)
            {
                if (PointerOfMe == RandNumber)
                {
                    Players[i].GetComponent<CharacterPreset>().Type = CharacterPreset.Identity.Me;
                    Players[i].GetComponent<CharacterPreset>().WeaponType = MyWeapon;
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
        Team1.GetComponent<CheckTeammate>().enabled = true;
        Team2.GetComponent<CheckTeammate>().enabled = true;
        Invoke("EngageAllPlayer", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Mode == GameModeSelect.Rounded4v4)
        {
            if(Team1.surviving != Team2.surviving)
            {
                if(Team1.surviving && !Team2.surviving)
                {
                    Debug.Log(Team1.myTeam.ToString() + " team win!");
                    team1wincount++;
                }else if(!Team1.surviving && Team2.surviving)
                {
                    Debug.Log(Team2.myTeam.ToString() + " team win!");
                    team2wincount++;
                }
            }
        }
    }

    void GenerateIDAndWeapon(int i)
    {
        System.Random WeaponRandomSeed = new System.Random(System.DateTime.Now.Millisecond);
        int RandWeapon = WeaponRandomSeed.Next(0, 3);
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
        Players[i].GetComponent<CharacterPreset>().Type = CharacterPreset.Identity.AI;
    }

    void EngageAllPlayer()
    {
        foreach(GameObject player in Players)
        {
            switch(player.GetComponent<CharacterPreset>().Type)
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
    }
}
