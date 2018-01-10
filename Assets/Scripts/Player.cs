using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public Level currentLevel;
    public PlayerMovement m_player;

    public void Restart()
    {
        m_player.ResetVariables();
        currentLevel.ResetLevel();
    }

}
