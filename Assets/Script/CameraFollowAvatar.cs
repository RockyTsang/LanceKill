using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAvatar : MonoBehaviour
{
    private GameObject MyAvatar;
    private GameObject[] PlayerList;
    private PointingArrow[] ArrowList;
    private GameObject ObservingTeammate;
    public Transform playerTransform; // 移动的物体
    public Vector3 deviation; // 偏移量

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerList = GetComponentInParent<GameMainControl>().Players;
        ArrowList = GameObject.Find("ArrowGroup").GetComponentsInChildren<PointingArrow>();
        foreach(GameObject Avatar in PlayerList)
        {
            if(Avatar.GetComponent<CharacterPreset>().Type == CharacterPreset.Identity.Me)
            {
                MyAvatar = Avatar;
            }
        }
        int j = 0;
        for(int i = 0; i < PlayerList.Length; i++)
        {
            if(PlayerList[i].GetComponent<CharacterPreset>().Team != MyAvatar.GetComponent<CharacterPreset>().Team)
            {
                ArrowList[j].MainCamera = this;
                ArrowList[j].PointedTarget = PlayerList[i];
                ArrowList[j].enabled = true;
                j++;
            }
        }
        playerTransform = MyAvatar.transform;
        transform.position = new Vector3(MyAvatar.transform.position.x, MyAvatar.transform.position.y + 0.3f, transform.position.z);
        deviation = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerList = GetComponentInParent<GameMainControl>().Players; 
        if (MyAvatar)
        {
            if (MyAvatar.activeInHierarchy)
            {
                transform.position = playerTransform.position + deviation; // 相机的位置 = 移动物体的位置 + 偏移量
            }
            else
            {
                for (int i = 0; i < PlayerList.Length; i++)
                {
                    if (PlayerList[i].activeInHierarchy && PlayerList[i].GetComponent<CharacterPreset>().Team == MyAvatar.GetComponent<CharacterPreset>().Team)
                    {
                        transform.position = PlayerList[i].transform.position + deviation;
                        break;
                    }
                }
            }
        }
    }

    public void ResetArrow()
    {
        foreach(PointingArrow arrow in ArrowList)
        {
            arrow.enabled = false;
        }
    }
}
