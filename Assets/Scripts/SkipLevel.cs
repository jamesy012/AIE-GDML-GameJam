using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLevel : MonoBehaviour {

    public LevelManager m_levelManager;
    public PlayerMovement m_player;
	// Use this for initialization
	void Start () {
        m_levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        m_player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_levelManager.m_currentLevel = m_levelManager.m_levels[0];
            m_levelManager.SetupLevel();
            m_player.transform.position = m_levelManager.m_currentLevel.m_respawnPoint.transform.position;
            m_player.ResetVariables();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_levelManager.m_currentLevel = m_levelManager.m_levels[1];
            m_levelManager.SetupLevel();
            m_player.transform.position = m_levelManager.m_currentLevel.m_respawnPoint.transform.position;
            m_player.ResetVariables();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_levelManager.m_currentLevel = m_levelManager.m_levels[2];
            m_levelManager.SetupLevel();
            m_player.transform.position = m_levelManager.m_currentLevel.m_respawnPoint.transform.position;
            m_player.ResetVariables();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_levelManager.m_currentLevel = m_levelManager.m_levels[3];
            m_levelManager.SetupLevel();
            m_player.transform.position = m_levelManager.m_currentLevel.m_respawnPoint.transform.position;
            m_player.ResetVariables();  
        }
    }
}
