using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject ContactObject;
            ContactObject = collision.gameObject;
            switch (ContactObject.GetComponent<CharacterPreset>().Type)
            {
                case CharacterPreset.Identity.Me:
                    ContactObject.GetComponent<AvatarControl>().moveSpeed -= 0.2f;
                    break;
                case CharacterPreset.Identity.TestObject:
                    ContactObject.GetComponent<NonPlayerAI>().moveSpeed -= 0.2f;
                    break;
                case CharacterPreset.Identity.AI:
                    ContactObject.GetComponent<NonPlayerAI>().moveSpeed -= 0.2f;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject ContactObject;
            ContactObject = collision.gameObject;
            switch (ContactObject.GetComponent<CharacterPreset>().Type)
            {
                case CharacterPreset.Identity.Me:
                    ContactObject.GetComponent<AvatarControl>().moveSpeed += 0.2f;
                    break;
                case CharacterPreset.Identity.TestObject:
                    ContactObject.GetComponent<NonPlayerAI>().moveSpeed += 0.2f;
                   break;
                case CharacterPreset.Identity.AI:
                    ContactObject.GetComponent<NonPlayerAI>().moveSpeed += 0.2f;
                    break;
            }
        }
    }
}
