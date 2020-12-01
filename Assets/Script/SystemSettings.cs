using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemSettings : MonoBehaviour
{
    public LanguageHandler[] Handlers;
    
    // Start is called before the first frame update
    void Start()
    {
        Handlers = FindObjectsOfType<LanguageHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResolution(Dropdown ResolutionDropdowm)
    {
        switch (ResolutionDropdowm.value)
        {
            case 0:
                Screen.SetResolution(480, 270, false);
                break;
            case 1:
                Screen.SetResolution(640, 360, false);
                break;
            case 2:
                Screen.SetResolution(720, 405, false);
                break;
            case 3:
                Screen.SetResolution(854, 480, false);
                break;
            case 4:
                Screen.SetResolution(960, 540, false);
                break;
            case 5:
                Screen.SetResolution(1024, 576, false);
                break;
            case 6:
                Screen.SetResolution(1280, 720, false);
                break;
            case 7:
                Screen.SetResolution(1366, 768, false);
                break;
            case 8:
                Screen.SetResolution(1600, 900, false);
                break;
            case 9:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 10:
                Screen.SetResolution(2560, 1440, false);
                break;
            case 11:
                Screen.SetResolution(3840, 2160, false);
                break;
            default:
                Screen.SetResolution(1920, 1080, false);
                Debug.Log("Resolution Error!");
                break;
        }
    }

    public void SetLanguage(Dropdown LanguageDropdown)
    {
        switch (LanguageDropdown.value)
        {
            case 0:
                foreach(LanguageHandler texts in Handlers)
                {
                    texts.language = LanguageHandler.LanguageSelection.English;
                }
                break;
            case 1:
                foreach (LanguageHandler texts in Handlers)
                {
                    texts.language = LanguageHandler.LanguageSelection.SimplifiedChinese;
                }
                break;
            case 2:
                foreach (LanguageHandler texts in Handlers)
                {
                    texts.language = LanguageHandler.LanguageSelection.TraditionalChinese;
                }
                break;
        }
    }
}
