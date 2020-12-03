using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterPreset))]
public class NonPlayerAI : MonoBehaviour
{
    public GameObject[] AllChar;
    public GameObject Nearest;
    public CharacterPreset PresetScript;
    private float attackDistance;

    // Start is called before the first frame update
    void Start()
    {
        AllChar = GameObject.FindGameObjectsWithTag("Player");
        PresetScript = gameObject.GetComponent<CharacterPreset>();
        switch (PresetScript.WeaponType)
        {
            case CharacterPreset.WeaponSelect.Knife:
                attackDistance = 0.256f;
                break;
            case CharacterPreset.WeaponSelect.Sword:
                attackDistance = 0.384f;
                break;
            case CharacterPreset.WeaponSelect.Spear:
                attackDistance = 0.384f;
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
                StartCoroutine(PresetScript.Attack());
            }
            else
            {
                transform.Translate(Vector2.down * PresetScript.moveSpeed * Time.deltaTime, Space.Self);
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
            transform.Translate(Vector2.up * PresetScript.moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
