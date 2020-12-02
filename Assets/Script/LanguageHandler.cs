using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageHandler : MonoBehaviour
{
    public SystemSettings SettingsScript;
    
    public enum TextType
    {
        SimpleText,
        ScriptControlled
    }
    public TextType TypeOfText;
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
        if(TypeOfText == TextType.SimpleText)
        {
            switch (language)
            {
                case LanguageSelection.English:
                    GetComponent<Text>().text = English;
                    break;
                case LanguageSelection.SimplifiedChinese:
                    GetComponent<Text>().text = SimplifiedChinese;
                    break;
                case LanguageSelection.TraditionalChinese:
                    GetComponent<Text>().text = TraditionalChinese;
                    break;
            }
        }
    }

    public string ColorTranslate(string color)
    {
        switch (language)
        {
            case LanguageSelection.English:
                return color + English;
            case LanguageSelection.SimplifiedChinese:
                switch (color)
                {
                    case "Red":
                        return "红" + SimplifiedChinese;
                    case "Yellow":
                        return "黄" + SimplifiedChinese;
                    case "Green":
                        return "绿" + SimplifiedChinese;
                    case "Blue":
                        return "蓝" + SimplifiedChinese;
                    default:
                        return "Color Translate Error!";
                }
            case LanguageSelection.TraditionalChinese:
                switch (color)
                {
                    case "Red":
                        return "紅" + TraditionalChinese;
                    case "Yellow":
                        return "黃" + TraditionalChinese;
                    case "Green":
                        return "綠" + TraditionalChinese;
                    case "Blue":
                        return "藍" + TraditionalChinese;
                    default:
                        return "Color Translate Error!";
                }
            default:
                return "Color Translate Error!";
        }
    }
}
