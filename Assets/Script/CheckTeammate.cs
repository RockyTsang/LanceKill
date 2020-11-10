using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTeammate : MonoBehaviour
{
    public CharacterPreset[] teammates;
    public CharacterPreset.TeamSelect myTeam;
    public bool surviving;
    
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
        if (AllDead())
        {
            surviving = false;
        }
    }

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
}
