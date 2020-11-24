using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPreview : MonoBehaviour
{
    public Dropdown Team;
    public Dropdown Weapon;
    public Image LeftHand;
    public Image RightHand;
    public Image PrimaryWeapon;
    public Image SecondaryWeapon;
    public Sprite[] WeaponSprites; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Team.value)
        {
            case 0:
                gameObject.GetComponent<Image>().color = Color.red;
                LeftHand.color = Color.red;
                RightHand.color = Color.red;
                break;
            case 1:
                gameObject.GetComponent<Image>().color = Color.yellow;
                LeftHand.color = Color.yellow;
                RightHand.color = Color.yellow;
                break;
            case 2:
                gameObject.GetComponent<Image>().color = Color.green;
                LeftHand.color = Color.green;
                RightHand.color = Color.green;
                break;
            case 3:
                gameObject.GetComponent<Image>().color = Color.blue;
                LeftHand.color = Color.blue;
                RightHand.color = Color.blue;
                break;
        }
        switch (Weapon.value)
        {
            case 0:
                PrimaryWeapon.sprite = WeaponSprites[0];
                SecondaryWeapon.sprite = WeaponSprites[0];
                PrimaryWeapon.transform.position = transform.position + new Vector3(60, 50, 0);
                PrimaryWeapon.transform.rotation = Quaternion.Euler(0, 0, -90);
                SecondaryWeapon.transform.position = transform.position + new Vector3(-60, 50, 0);
                SecondaryWeapon.transform.rotation = Quaternion.Euler(0, 0, 90);
                PrimaryWeapon.SetNativeSize();
                SecondaryWeapon.SetNativeSize();
                PrimaryWeapon.gameObject.SetActive(true);
                SecondaryWeapon.gameObject.SetActive(true);
                break;
            case 1:
                PrimaryWeapon.sprite = WeaponSprites[1];
                PrimaryWeapon.transform.position = transform.position + new Vector3(50, 100, 0);
                PrimaryWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
                PrimaryWeapon.SetNativeSize();
                PrimaryWeapon.gameObject.SetActive(true);
                SecondaryWeapon.gameObject.SetActive(false);
                break;
            case 2:
                PrimaryWeapon.sprite = WeaponSprites[2];
                PrimaryWeapon.transform.position = transform.position + new Vector3(-50, 50, 0);
                PrimaryWeapon.transform.rotation = Quaternion.Euler(0, 0, 90);
                PrimaryWeapon.SetNativeSize();
                PrimaryWeapon.gameObject.SetActive(true);
                SecondaryWeapon.gameObject.SetActive(false);
                break;
        }
    }
}
