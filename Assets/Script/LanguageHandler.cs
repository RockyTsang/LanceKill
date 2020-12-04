using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
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
        English = 0,
        SimplifiedChinese = 1,
        TraditionalChinese = 2
    }
    public LanguageSelection language;
    public string English;
    public string SimplifiedChinese;
    public string TraditionalChinese;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                language = LanguageSelection.English;
                break;
            case 1:
                language = LanguageSelection.SimplifiedChinese;
                break;
            case 2:
                language = LanguageSelection.TraditionalChinese;
                break;
        }
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

    public string PreviewDescription(float skillCD)
    {
        switch (language)
        {
            case LanguageSelection.English:
                return English + skillCD.ToString() + "s";
            case LanguageSelection.SimplifiedChinese:
                return SimplifiedChinese + skillCD.ToString() + "s";
            case LanguageSelection.TraditionalChinese:
                return TraditionalChinese + skillCD.ToString() + "s";
            default:
                return "Preview Description Error!";
        }
    }
}
