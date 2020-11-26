using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarControl : MonoBehaviour
{
    public CharacterPreset PresetScript;

    // Start is called before the first frame update
    void Start()
    {
        PresetScript = gameObject.GetComponent<CharacterPreset>();
        GameObject.Find("HPBarFrame").GetComponent<HPBar>().Me = gameObject;
        GameObject.Find("HPBarFrame").GetComponent<HPBar>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInChildren<Weapon>().Attacking)
        {
            Vector3 vpos3 = Camera.main.WorldToScreenPoint(transform.position);// Get screen position of avatar
            float angle = Vector2.SignedAngle(new Vector2(0, 1), Input.mousePosition - vpos3);// Calculate angle of mouse and front
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !PresetScript.crushCoolDown)// Press shift to crush
        {
            StartCoroutine(PresetScript.Crush());
        }
        // Movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * PresetScript.moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * PresetScript.moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * PresetScript.moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * PresetScript.moveSpeed * Time.deltaTime, Space.World);
        }
        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PresetScript.Attack());
        }
        // LongAttack
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(PresetScript.LongAttack());
        }
    }
}
