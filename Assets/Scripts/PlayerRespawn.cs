using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour{

    public Level currentLevel;

    public void Restart()
    {
        currentLevel.ResetLevel();
    }

}
