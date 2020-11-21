using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject Lobby;
    public GameObject GameModeSelect;
    public GameObject Settings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToLobby()
    {
        GameModeSelect.SetActive(false);
        Settings.SetActive(false);
        Lobby.SetActive(true);
    }
}
