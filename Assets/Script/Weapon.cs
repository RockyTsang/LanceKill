using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool Attacking;
    public CharacterPreset.WeaponSelect WeaponType;
    public Sprite[] WeaponBody;

    // Start is called before the first frame update
    void Start()
    {
        Attacking = false;

        // Set weapon
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
                break;
            case CharacterPreset.WeaponSelect.Spear:
                gameObject.GetComponent<SpriteRenderer>().sprite = WeaponBody[2];
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 5.12f);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Attacking)
        {
            if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "PartOfPlayer")
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
                            HitTarget.HealthPoint -= 5;
                        }
                        else
                        {
                            HitTarget.HealthPoint -= 15;
                        }
                    }
                }
            }
        }       
    }
}
