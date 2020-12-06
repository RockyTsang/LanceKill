using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterPreset : MonoBehaviour
{
    public enum TeamSelect
    {
        Red = 0,
        Yellow = 1,
        Green = 2,
        Blue = 3
    };
    public TeamSelect Team;
    public enum Identity
    {
        Me,
        AI,
        TestObject
    }
    public Identity Type;
    public enum WeaponSelect
    {
        Knife,
        Sword,
        Spear
    }
    public WeaponSelect WeaponType;
    public GameObject WeaponPrefab;
    public GameObject SecondaryWeaponPrefab;
    public RuntimeAnimatorController Controller;
    public Transform SpawnPosition;
    public int HealthPoint;
    private bool living;

    public bool crushCoolDown;
    public bool longAttackCoolDown;
    public bool healCoolDown;
    public float moveSpeed;
    private string attackAnimation;
    private string longAttackAnimation;
    private int attackDamage;
    private int longAttackDamage;
    private float attackSpeed;
    private float longAttackSpeed;
    public float longAttackCD;
    public int armor;

    public float longAttackTimer;
    public float crushTimer;
    public float healTimer;

    void OnEnable()
    {
        crushCoolDown = false;
        longAttackCoolDown = false;
        moveSpeed = 0.6f - ((float)armor / 100);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set player HP
        HealthPoint = 200;
        living = true;

        // Set player color
        switch (Team)
        {
            case TeamSelect.Red:
                GetComponent<SpriteRenderer>().color = Color.red;
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "LeftHand" || child.gameObject.name == "RightHand")
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
                break;
            case TeamSelect.Yellow:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "LeftHand" || child.gameObject.name == "RightHand")
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                }
                break;
            case TeamSelect.Green:
                GetComponent<SpriteRenderer>().color = Color.green;
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "LeftHand" || child.gameObject.name == "RightHand")
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }
                break;
            case TeamSelect.Blue:
                GetComponent<SpriteRenderer>().color = Color.blue;
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "LeftHand" || child.gameObject.name == "RightHand")
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                }
                break;
            default:
                GetComponent<SpriteRenderer>().color = Color.white;
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "LeftHand" || child.gameObject.name == "RightHand")
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                break;
        }

        // Intantiate weapon
        switch (WeaponType)
        {
            case WeaponSelect.Knife:
                Instantiate(WeaponPrefab, transform.position + new Vector3(0.12f, 0.1f, 0), Quaternion.Euler(0, 0, -90), transform);
                Instantiate(SecondaryWeaponPrefab, transform.position + new Vector3(-0.12f, 0.1f, 0), Quaternion.Euler(0, 0, 90), transform);
                transform.Find("SecondaryWeapon(Clone)").GetComponent<SecondaryWeapon>().PrimaryWeapon = transform.Find("Weapon(Clone)").gameObject;
                transform.Find("SecondaryWeapon(Clone)").gameObject.SetActive(true);
                attackAnimation = "KnifeAttack";
                longAttackAnimation = "KnifeLongAttack";
                attackDamage = 50;
                longAttackDamage = 30;
                attackSpeed = 0.333f;
                longAttackSpeed = 0.5f;
                longAttackCD = 2f;
                break;
            case WeaponSelect.Sword:
                Instantiate(WeaponPrefab, transform.position + new Vector3(0.1f, 0.2f, 0), Quaternion.Euler(0, 0, 0), transform);
                Instantiate(SecondaryWeaponPrefab, transform.position + new Vector3(0.1f, 0.2f, 0), Quaternion.Euler(0, 0, 0), transform);
                transform.Find("SecondaryWeapon(Clone)").GetComponent<SecondaryWeapon>().PrimaryWeapon = transform.Find("Weapon(Clone)").gameObject;
                attackAnimation = "SwordAttack";
                longAttackAnimation = "SwordLongAttack";
                attackDamage = 30;
                longAttackDamage = 50;
                attackSpeed = 0.75f;
                longAttackSpeed = 1.5f;
                longAttackCD = 10f;
                break;
            case WeaponSelect.Spear:
                Instantiate(WeaponPrefab, transform.position + new Vector3(-0.1f, 0.1f, 0), Quaternion.Euler(0, 0, 90), transform);
                attackAnimation = "SpearAttack";
                longAttackAnimation = "SpearLongAttack";
                attackDamage = 40;
                longAttackDamage = 40;
                attackSpeed = 1f;
                longAttackSpeed = 1f;
                longAttackCD = 5f;
                break;
        }
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Controller; 

        // Add control script
        switch (Type)
        {
            case Identity.Me:
                gameObject.AddComponent<AvatarControl>().enabled = false;
                break;
            case Identity.TestObject:
                break;
            case Identity.AI:
                gameObject.AddComponent<NonPlayerAI>().enabled = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(longAttackTimer > 0)
        {
            longAttackTimer -= Time.deltaTime;
        }
        if(crushTimer > 0)
        {
            crushTimer -= Time.deltaTime;
        }
        if(healTimer > 0)
        {
            healTimer -= Time.deltaTime;
        }
        if (living)
        {
            if(HealthPoint <= 0)
            {
                gameObject.SetActive(false);
                if(GameObject.Find("GameScene").GetComponent<GameMainControl>().Mode == GameMainControl.GameModeSelect.KillCount4v4)
                {
                    GameObject.Find("GameScene").GetComponent<GameMainControl>().CallSlained(this);
                }
            }
        }
        if (gameObject.activeInHierarchy)
        {
            living = true;
        }
        else
        {
            living = false;
        }
    }

    public void ResetBody()
    {
        transform.position = SpawnPosition.position;
        HealthPoint = 100;
        longAttackTimer = 0;
        crushTimer = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.Find("LeftHand").transform.position = transform.position + new Vector3(-0.1f, 0.1f, 0f);
        transform.Find("RightHand").transform.position = transform.position + new Vector3(0.1f, 0.1f, 0f);
        transform.Find("Weapon(Clone)").GetComponent<Weapon>().Attacking = false;
        switch (WeaponType)
        {
            case WeaponSelect.Knife:
                transform.Find("Weapon(Clone)").transform.position = transform.position + new Vector3(0.12f, 0.1f, 0);
                transform.Find("Weapon(Clone)").transform.rotation = Quaternion.Euler(0, 0, -90);
                transform.Find("SecondaryWeapon(Clone)").transform.position = transform.position + new Vector3(-0.12f, 0.1f, 0);
                transform.Find("SecondaryWeapon(Clone)").transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case WeaponSelect.Sword:
                transform.Find("Weapon(Clone)").transform.position = transform.position + new Vector3(0.1f, 0.2f, 0);
                transform.Find("Weapon(Clone)").transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Find("SecondaryWeapon(Clone)").transform.position = transform.position + new Vector3(0.1f, 0.2f, 0);
                transform.Find("SecondaryWeapon(Clone)").transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Find("SecondaryWeapon(Clone)").gameObject.SetActive(false);
                break;
            case WeaponSelect.Spear:
                transform.Find("Weapon(Clone)").transform.position = transform.position + new Vector3(-0.1f, 0.1f, 0);
                transform.Find("Weapon(Clone)").transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }

    public IEnumerator Attack()
    {
        if (!GetComponentInChildren<Weapon>().Attacking)
        {
            GetComponentInChildren<Weapon>().Damage = attackDamage;
            this.GetComponentInChildren<Animator>().Play(attackAnimation);
            GetComponentInChildren<Weapon>().Attacking = true;
            yield return new WaitForSeconds(attackSpeed);
            GetComponentInChildren<Weapon>().Attacking = false;
        }
    }

    public IEnumerator LongAttack()
    {
        if (!longAttackCoolDown && !GetComponentInChildren<Weapon>().Attacking)
        {
            GetComponentInChildren<Weapon>().Damage = longAttackDamage;
            this.GetComponentInChildren<Animator>().Play(longAttackAnimation);
            GetComponentInChildren<Weapon>().Attacking = true;
            longAttackCoolDown = true;
            yield return new WaitForSeconds(longAttackSpeed);
            GetComponentInChildren<Weapon>().Attacking = false;
            longAttackTimer = longAttackCD;
            yield return new WaitForSeconds(longAttackCD);
            longAttackCoolDown = false;
        }
    }

    public IEnumerator Crush()
    {
        if (!crushCoolDown)
        {
            moveSpeed += 2.5f;
            crushCoolDown = true;// Lock the crush until cool down end
            yield return new WaitForSeconds(0.5f);
            moveSpeed -= 2.5f;
            crushTimer = 5f;
            yield return new WaitForSeconds(5f);
            crushCoolDown = false;
        }
    }

    public IEnumerator Heal()
    {
        if (!healCoolDown)
        {
            if(HealthPoint > 150)
            {
                HealthPoint = 200;
            }
            else
            {
                HealthPoint += 50;
            }
            healCoolDown = true;
            healTimer = 20f;
            yield return new WaitForSeconds(20f);
            healCoolDown = false;
        }
    }
}
