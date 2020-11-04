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
    public CharacterPreset.TeamSelect EnemyTeam;
    public CharacterPreset.WeaponSelect MyWeapon;
    public GameObject[] Maps;
    public GameObject[] SpawnPoints;
    public GameObject Avatar;
    public GameObject[] Players;
    public Camera GameCamera;

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
                    Instantiate(Avatar, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform).GetComponent<CharacterPreset>().Team = MyTeam;
                    break;
                case "TopSpawnFeild":
                    Instantiate(Avatar, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform).GetComponent<CharacterPreset>().Team = EnemyTeam;
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
