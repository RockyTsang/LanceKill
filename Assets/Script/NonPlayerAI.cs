using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerAI : MonoBehaviour
{
    public GameObject[] AllChar;
    public GameObject Nearest;
    public float moveSpeed;
    private string attackAnimation;
    private float attackSpeed;
    private float attackDistance;

    // Start is called before the first frame update
    void Start()
    {
        AllChar = GameObject.FindGameObjectsWithTag("Player");
        moveSpeed = 0.5f;
        switch (gameObject.GetComponent<CharacterPreset>().WeaponType)
        {
            case CharacterPreset.WeaponSelect.Knife:
                attackAnimation = "KnifeAttack";
                attackSpeed = 0.2f;
                attackDistance = 0.256f;
                break;
            case CharacterPreset.WeaponSelect.Sword:
                attackAnimation = "SwordAttack";
                attackSpeed = 0.45f;
                attackDistance = 0.384f;
                break;
            case CharacterPreset.WeaponSelect.Spear:
                attackAnimation = "SpearAttack";
                attackSpeed = 1f;
                attackDistance = 0.512f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Find the nearest enemy
        float distanceThreshold = 1000;
        foreach (GameObject person in AllChar)
        {
            if (person.activeInHierarchy)
            {
                if (person.GetComponent<CharacterPreset>().Team != gameObject.GetComponent<CharacterPreset>().Team)
                {
                    float distance = Vector3.Distance(transform.localPosition, person.transform.localPosition);
                    if (distance < distanceThreshold)
                    {
                        distanceThreshold = distance;
                        Nearest = person;
                    }
                }
            }
        }
        
        // Attack when approaching enemy
        if(distanceThreshold <= attackDistance)
        {
            if (!GetComponentInChildren<Weapon>().Attacking)
            {
                this.GetComponent<Animator>().Play(attackAnimation);
                GetComponentInChildren<Weapon>().Attacking = true;
                Invoke("attacked", attackSpeed);
            }
            else
            {
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.Self);
            }   
        }
        else
        {
            // Rotate to the position of enemy
            Vector2 mypos2 = new Vector2(Camera.main.WorldToScreenPoint(gameObject.transform.position).x, Camera.main.WorldToScreenPoint(gameObject.transform.position).y);
            Vector2 targetpos2 = new Vector2(Camera.main.WorldToScreenPoint(Nearest.transform.position).x, Camera.main.WorldToScreenPoint(Nearest.transform.position).y);
            float angle = Vector2.SignedAngle(new Vector2(0, 1), targetpos2 - mypos2);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            // Move to the position of enemy
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime, Space.Self);
        }
    }

    void attacked()
    {
        GetComponentInChildren<Weapon>().Attacking = false;
    }
}
