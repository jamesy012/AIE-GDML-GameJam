using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [Header("Level Properties")]
    public int m_keysInLevel;
    public int m_movesAvailable;

    [Header("Current Gameplay")]
    public int m_keysCollected;
    public int m_movesDone;
   
   
   
    [Header("Bools")]
    public bool m_startFlipped;
    public bool m_levelFailed;
    private bool m_levelComplete;

    
    public Transform m_respawnPoint;
    [HideInInspector]
    public Transform m_levelCamPos;
    private Door m_door;
    private LevelManager m_levelManager;
  

    public int m_platinumStarMoves;
    public int m_goldStarMoves;
    public int m_silverStarMoves;
    public int m_bronzeStarMoves;


    [System.Serializable]
    public struct GravityDirection {
        [HideInInspector]
        public string m_name;
        public bool m_allowDirection;
    }

    public GravityDirection[] m_gravityDirection = new GravityDirection[4]; 

    // Use this for initialization
    void Start()
    {
        m_levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        m_door = m_levelManager.m_currentLevel.GetComponentInChildren<Door>();
    }
	
    public void OpenDoor()
    {
        m_door.m_animtor.SetTrigger("Open");
    }


    private void OnValidate() {
        for(int i = 0; i < m_gravityDirection.Length; i++) {
            m_gravityDirection[i].m_name = ((PlayerMovement.Direction)i).ToString().Remove(0,1);
        }
    }
}
