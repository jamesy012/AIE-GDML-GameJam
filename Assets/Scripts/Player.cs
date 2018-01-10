using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerMovement m_player;
    public Level m_currentLevel;
	
    // Use this for initialization
	void Start () {
        m_player = GetComponent<PlayerMovement>();
	}

    public void Restart()
    {
        m_currentLevel.ResetLevel();
        m_player.ResetVariables();
    }
}
