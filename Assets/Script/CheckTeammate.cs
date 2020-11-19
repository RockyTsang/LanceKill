using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTeammate : MonoBehaviour
{
    public CharacterPreset[] teammates;
    public CharacterPreset.TeamSelect myTeam;
    public bool surviving;
    public GameMainControl mainProcess;
    
    // Start is called before the first frame update
    void Start()
    {
        teammates = GetComponentsInChildren<CharacterPreset>();
        myTeam = teammates[0].Team;
        surviving = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch(mainProcess.Mode)
        {
            case GameMainControl.GameModeSelect.Rounded4v4:
                if (AllDead())
                {
                    surviving = false;
                }
                break;
            case GameMainControl.GameModeSelect.KillCount4v4:
                foreach(CharacterPreset person in teammates)
                {
                    if (!person.gameObject.activeInHierarchy)
                    {
                        
                    }
                }
                break;
        }
        
    }

    // Check if all teammates are dead
    bool AllDead()
    {
        int accumulateDead = 0;
        for(int i = 0; i < teammates.Length; i++)
        {
            if (!teammates[i].gameObject.activeInHierarchy)
            {
                accumulateDead++;
            }
        }
        if(accumulateDead >= 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DestroyPlayers()
    {
        foreach(CharacterPreset person in teammates)
        {
            Destroy(person.gameObject);
        }
    }
}
