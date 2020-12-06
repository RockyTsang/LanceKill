using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public GameObject Me;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Me)
        {
            transform.Find("HPBar").GetComponent<Image>().fillAmount = (float)Me.GetComponent<CharacterPreset>().HealthPoint / 200;
        }
    }
}
