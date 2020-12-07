using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    public GameObject PrimaryWeapon;
    public CharacterPreset.WeaponSelect WeaponType;
    private int Damage;
    public Sprite[] WeaponBody;

    // Start is called before the first frame update
    void Start()
    {
        // Set secondary weapon
        WeaponType = gameObject.transform.parent.GetComponent<CharacterPreset>().WeaponType;
        switch (WeaponType)
        {
            case CharacterPreset.WeaponSelect.Knife:
                gameObject.GetComponent<SpriteRenderer>().sprite = WeaponBody[0];
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.28f, 1.28f);
                break;
            case CharacterPreset.WeaponSelect.Sword:
                gameObject.GetComponent<SpriteRenderer>().sprite = WeaponBody[1];
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.17f, 2.56f);
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.75f);
                break;
            case CharacterPreset.WeaponSelect.Spear:
                Debug.Log("Secondary Spear?");
                break;
            case CharacterPreset.WeaponSelect.Musou:
                gameObject.GetComponent<SpriteRenderer>().sprite = WeaponBody[2];
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 5.12f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Damage = PrimaryWeapon.GetComponent<Weapon>().Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PrimaryWeapon.GetComponent<Weapon>().Attacking)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PartOfPlayer")
            {
                CharacterPreset HitTarget;
                if (collision.gameObject.name != "Weapon(Clone)" && collision.gameObject.name != "SecondaryWeapon(Clone)")
                {
                    HitTarget = collision.gameObject.GetComponent<CharacterPreset>();
                    if (HitTarget.Team != GetComponentInParent<CharacterPreset>().Team)
                    {
                        Vector2 mypos2 = new Vector2(gameObject.transform.parent.gameObject.transform.position.x, gameObject.transform.parent.gameObject.transform.position.y);// Get screen position of body
                        Vector2 targetpos2 = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);//Get screen position of target
                        Vector2 angle = targetpos2 - mypos2;// Calculate angle of body and enemy
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(angle * 500f);
                        if (collision.gameObject.GetComponentInChildren<Weapon>().Attacking)
                        {
                            HitTarget.HealthPoint -= (Damage - 10 - HitTarget.armor);
                        }
                        else
                        {
                            HitTarget.HealthPoint -= (Damage - HitTarget.armor);
                        }
                    }
                }
            }
        }
    }
}
