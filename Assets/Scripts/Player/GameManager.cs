using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] LevelsPrefab;
    public GameObject[] WorldsPrefab;
    public int maxLevels;

    void Awake()
    {
        //Reset level # after max levels
        if (GlobalGameSettings.currentLevelId >= maxLevels) 
        { 
            GlobalGameSettings.currentLevelId = 0;
        }

        Debug.Log("Level# = " + GlobalGameSettings.currentLevelId);
        Instantiate(LevelsPrefab[GlobalGameSettings.currentLevelId]);

        if (GlobalGameSettings.currentLevelId < 12)
        {
            Instantiate(WorldsPrefab[0]);
        }
        /*else if (GlobalGameSettings.currentLevelId < 12) 
        {
            Instantiate(WorldsPrefab[1]);
        }*/
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                          