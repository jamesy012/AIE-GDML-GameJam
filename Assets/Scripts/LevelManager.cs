using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [Header("Global Level Properties")]
    public float m_cameraPanTime;

    public Level m_currentLevel;
    public Level m_previousLevel;
    
    public List<Level> m_levels;

    private PlayerMovement m_player;
    [HideInInspector]
    public UIManager m_uiManager;
    private Vector3 m_defaultGravity;
    public Camera m_camera;
    private float m_currentTime;

    // Use this for initialization
    void Start () {
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Level"))
        {
            m_levels.Add(obj.GetComponent<Level>());
        }
        m_defaultGravity = Physics.gravity;
        m_player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        m_uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        m_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        m_currentLevel = m_levels[0];
        SetupLevel();
	}
	
    public void SetupLevel()
    {
        m_uiManager.SetupKeys(m_currentLevel.m_keysInLevel);
        m_uiManager.SetupMoves(m_currentLevel.m_movesAvailable, m_currentLevel.m_movesAvailable);
        StartCoroutine(PanCamera(3));
    }

    public void CompleteLevel()
    {
        if (m_currentLevel.m_movesDone == m_currentLevel.m_platinumStarMoves)
        {
            Debug.Log("Platinum");
        }
        else if (m_currentLevel.m_movesDone <= m_currentLevel.m_goldStarMoves && m_currentLevel.m_movesDone > m_currentLevel.m_platinumStarMoves)
        {
            Debug.Log("Gold");
        }
        else if (m_currentLevel.m_movesDone <= m_currentLevel.m_silverStarMoves && m_currentLevel.m_movesDone > m_currentLevel.m_goldStarMoves)
        {
            Debug.Log("Silver");
        }
        else if (m_currentLevel.m_movesDone <= m_currentLevel.m_bronzeStarMoves && m_currentLevel.m_movesDone > m_currentLevel.m_silverStarMoves)
        {
            Debug.Log("Bronze");
        }
        m_previousLevel = m_currentLevel;
        for(int i = 0; i < m_levels.Count; i++)
        {
            if(m_currentLevel == m_levels[i])
            {
                if (m_currentLevel != m_levels.Last<Level>())
                {
                    m_currentLevel = m_levels[i + 1];
                }
                break;
            }
           
        }
    }

    public void RestartLevel()
    {
        m_player.transform.position = m_currentLevel.m_respawnPoint.transform.position;
        Physics.gravity = m_defaultGravity;
        m_uiManager.SetupKeys(m_currentLevel.m_keysInLevel);
        m_uiManager.SetupMoves(m_currentLevel.m_movesAvailable, m_currentLevel.m_movesAvailable);
        m_player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_currentLevel.m_movesDone = 0;
        m_currentLevel.m_keysCollected = 0;
        m_player.ResetVariables();
        foreach(KeyPickupScript key in GetComponentsInChildren<KeyPickupScript>())
        {
            key.gameObject.SetActive(true);
        }
    }

    public void AddKey()
    {
        m_uiManager.AddKey();
        m_currentLevel.m_keysCollected++;
        if (m_currentLevel.m_keysCollected == m_currentLevel.m_keysInLevel)
        {
           m_currentLevel.OpenDoor();
        }
    }


    IEnumerator PanCamera(float time)
    {
        m_currentTime = 0;
        while (m_currentTime < time)
        {
            m_currentTime += Time.deltaTime;
            if (m_currentLevel.m_levelCamPos != null)
                m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_currentLevel.m_levelCamPos.position, m_currentTime / time);
            yield return null;
        }
    }
}
