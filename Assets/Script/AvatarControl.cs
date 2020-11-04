using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarControl : MonoBehaviour
{
    private bool crushCoolDown;
    private bool longAttackCoolDown;
    public float moveSpeed;
    private string attackAnimation;
    private string longAttackAnimation;
    private float attackSpeed;
    private float longAttackSpeed;
    private float longAttackCD;

    // Start is called before the first frame update
    void Start()
    {
        crushCoolDown = false;
        longAttackCoolDown = false;
        moveSpeed = 0.5f;
        switch (gameObject.GetComponent<CharacterPreset>().WeaponType)
        {
            case CharacterPreset.WeaponSelect.Knife:
                attackAnimation = "KnifeAttack";
                longAttackAnimation = "KnifeLongAttack";
                attackSpeed = 0.333f;
                longAttackSpeed = 0.5f;
                longAttackCD = 2f;
                break;
            case CharacterPreset.WeaponSelect.Sword:
                attackAnimation = "SwordAttack";
                longAttackAnimation = "SwordLongAttack";
                attackSpeed = 0.75f;
                longAttackSpeed = 1.5f;
                longAttackCD = 10f;
                break;
            case CharacterPreset.WeaponSelect.Spear:
                attackAnimation = "SpearAttack";
                longAttackAnimation = "SpearLongAttack";
                attackSpeed = 1f;
                longAttackSpeed = 1f;
                longAttackCD = 5f;
                break;
        }
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !crushCoolDown)// Press shift to crush
        {
            moveSpeed += 2.5f;
            crushCoolDown = true;// Lock the crush until cool down end
            Invoke("stopCrush", 0.5f);// Reset moveSpeed after 0.5s
            Invoke("crushCoolDownEnd", 5f);// Unlock crush

        }
        // Movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, Space.World);
        }
        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            if (!GetComponentInChildren<Weapon>().Attacking)
            {
                this.GetComponentInChildren<Animator>().Play(attackAnimation);
                GetComponentInChildren<Weapon>().Attacking = true;
                Invoke("attacked", attackSpeed);
            }
        }
        // LongAttack
        if (Input.GetMouseButtonDown(1))
        {
            if (!longAttackCoolDown && !GetComponentInChildren<Weapon>().Attacking)
            {
                this.GetComponentInChildren<Animator>().Play(longAttackAnimation);
                GetComponentInChildren<Weapon>().Attacking = true;
                longAttackCoolDown = true;
                Invoke("attacked", longAttackSpeed);
                Invoke("longAttackCoolDownEnd", longAttackCD);// Unlock Long Attack
            }
        }
    }

    // Speed decrease
    void stopCrush()
    {
        moveSpeed -= 2.5f;
    }

    // Unlock crush
    void crushCoolDownEnd()
    {
        crushCoolDown = false;
    }

    void longAttackCoolDownEnd()
    {
        longAttackCoolDown = false;
    }

    void attacked()
    {
        GetComponentInChildren<Weapon>().Attacking = false;
    }
}
