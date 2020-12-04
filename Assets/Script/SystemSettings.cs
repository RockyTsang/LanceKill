using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemSettings : MonoBehaviour
{
    public LanguageHandler[] Handlers;

    public Dropdown ResolutionDropdown;
    public Dropdown LanguageDropdown;
    private int[] WidthSet = { 480, 640, 720, 854, 960, 1024, 1280, 1366, 1600, 1920, 2560, 3840 };
    private int[] HeightSet = { 270, 360, 405, 480, 540, 576, 720, 768, 900, 1080, 1440, 2160 };
    
    // Start is called before the first frame update
    void Start()
    {
        Handlers = GameObject.FindObjectsOfType<LanguageHandler>(true);
        CheckSettings(PlayerPrefs.GetInt("Width"),PlayerPrefs.GetInt("Height"), PlayerPrefs.GetInt("Language"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckSettings(int width, int height, int language)
    {
        int WidthMatch = FindWidth(width);
        int HeightMatch = FindHeight(height);
        if (WidthMatch != -1 && HeightMatch != -1 && WidthMatch == HeightMatch)
        {
            ResolutionDropdown.value = WidthMatch;
        }
        else
        {
            ResolutionDropdown.value = 9;
        }

        if(language >= 0 && language <= 2)
        {
            LanguageDropdown.value = language;
        }
        else
        {
            LanguageDropdown.value = 0;
        }
    }

    int FindWidth(int width)
    {
        int match = -1;
        for (int i = 0; i < WidthSet.Length; i++) 
        {
            if(width == WidthSet[i])
            {
                match = i;
                break;
            }
        }
        return match;
    }

    int FindHeight(int height)
    {
        int match = -1;
        for (int i = 0; i < HeightSet.Length; i++)
        {
            if (height == HeightSet[i])
            {
                match = i;
                break;
            }
        }
        return match;
    }

    public void SetResolution()
    {
        Screen.SetResolution(WidthSet[ResolutionDropdown.value], HeightSet[ResolutionDropdown.value], false);
        PlayerPrefs.SetInt("Width", WidthSet[ResolutionDropdown.value]);
        PlayerPrefs.SetInt("Height", HeightSet[ResolutionDropdown.value]);
    }

    public void SetLanguage()
    {
        switch (LanguageDropdown.value)
        {
            case 0:
                PlayerPrefs.SetInt("Language", 0);
                foreach(LanguageHandler texts in Handlers)
                {
                    texts.language = LanguageHandler.LanguageSelection.English;
                }
                break;
            case 1:
                PlayerPrefs.SetInt("Language", 1);
                foreach (LanguageHandler texts in Handlers)
                {
                    texts.language = LanguageHandler.LanguageSelection.SimplifiedChinese;
                }
                break;
            case 2:
                PlayerPrefs.SetInt("Language", 2);
                foreach (LanguageHandler texts in Handlers)
                {
                    texts.language = LanguageHandler.LanguageSelection.TraditionalChinese;
                }
                break;
        }
    }
}
