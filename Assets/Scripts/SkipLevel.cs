using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLevel : MonoBehaviour {

    public List<Level> m_levels;
    public Player m_player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_levels[0].StartLevel();
            m_levels[0].ResetLevel();
            m_player.m_currentLevel = m_levels[0];
            m_player.Restart();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_levels[1].StartLevel();
            m_levels[1].ResetLevel();
            m_player.m_currentLevel = m_levels[1];
            m_player.Restart();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_levels[2].StartLevel();
            m_levels[2].ResetLevel();
            m_player.m_currentLevel = m_levels[2];
            m_player.Restart();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_levels[3].StartLevel();
            m_levels[3].ResetLevel();
            m_player.m_currentLevel = m_levels[3];
            m_player.Restart();
        }
    }
}
