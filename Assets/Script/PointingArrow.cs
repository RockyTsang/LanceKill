using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingArrow : MonoBehaviour
{
    public CameraFollowAvatar MainCamera;
    public GameObject PointedTarget;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        switch (PointedTarget.GetComponent<CharacterPreset>().Team)
        {
            case CharacterPreset.TeamSelect.Red:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case CharacterPreset.TeamSelect.Yellow:
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case CharacterPreset.TeamSelect.Green:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case CharacterPreset.TeamSelect.Blue:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(MainCamera.playerTransform.localPosition, PointedTarget.transform.localPosition);
        if (MainCamera && PointedTarget)
        {
            if (PointedTarget.activeInHierarchy)
            {
                if(distance > 1.5)
                {
                    SetTransparent(1);
                    // Rotate to the position of enemy
                    Vector2 mypos2 = new Vector2(Camera.main.WorldToScreenPoint(MainCamera.transform.position).x, Camera.main.WorldToScreenPoint(MainCamera.transform.position).y);
                    Vector2 targetpos2 = new Vector2(Camera.main.WorldToScreenPoint(PointedTarget.transform.position).x, Camera.main.WorldToScreenPoint(PointedTarget.transform.position).y);
                    float angle = Vector2.SignedAngle(new Vector2(0, 1), targetpos2 - mypos2);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                }
                else
                {
                    SetTransparent(0);
                }
            }
            else
            {
                SetTransparent(0);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void SetTransparent(float alpha)
    {
        Color SetColor = gameObject.GetComponent<SpriteRenderer>().color;
        SetColor.a = alpha;
        gameObject.GetComponent<SpriteRenderer>().color = SetColor;
    }
}
