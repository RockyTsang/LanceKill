using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageHandler : MonoBehaviour
{
    public SystemSettings SettingsScript;
    public enum LanguageSelection
    {
        English,
        SimplifiedChinese,
        TraditionalChinese
    }
    public LanguageSelection language;
    public string English;
    public string SimplifiedChinese;
    public string TraditionalChinese;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (language)
        {
            case LanguageSelection.English:
                gameObject.GetComponent<Text>().text = English;
                break;
            case LanguageSelection.SimplifiedChinese:
                gameObject.GetComponent<Text>().text = SimplifiedChinese;
                break;
            case LanguageSelection.TraditionalChinese:
                gameObject.GetComponent<Text>().text = TraditionalChinese;
                break;
        }
    }
}
