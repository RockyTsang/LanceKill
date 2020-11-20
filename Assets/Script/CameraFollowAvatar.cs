using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAvatar : MonoBehaviour
{
    private GameObject MyAvatar;
    public Transform playerTransform; // 移动的物体
    public Vector3 deviation; // 偏移量

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject Avatar in GetComponentInParent<GameMainControl>().Players)
        {
            if(Avatar.GetComponent<CharacterPreset>().Type == CharacterPreset.Identity.Me)
            {
                MyAvatar = Avatar;
            }
        }
        playerTransform = MyAvatar.transform;
        transform.position = new Vector3(MyAvatar.transform.position.x, MyAvatar.transform.position.y + 0.3f, transform.position.z);
        deviation = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(MyAvatar)
        {
            if (MyAvatar.activeInHierarchy)
            {
                transform.position = playerTransform.position + deviation; // 相机的位置 = 移动物体的位置 + 偏移量
            }
        }
    }
}
