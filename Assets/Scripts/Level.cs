using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public int m_numberOfKeys;
    public int m_keys;

    public Transform m_respawnPoint;

    public UIManager m_uiManager;
    private GameObject m_player;
   

    public List<EndOfLevel> m_levelTriggers;

    public bool m_levelComplete;

    public Door m_door;

    // Use this for initialization
    void Start()
    {
        m_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        m_player = GameObject.Find("Player");
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).GetComponent<EndOfLevel>())
            {
                m_levelTriggers.Add(this.transform.GetChild(i).GetComponent<EndOfLevel>());
            }
            if (this.transform.GetChild(i).name == "ExitDoor")
            {
                m_levelTriggers.Add(this.transform.GetChild(i).GetComponent<EndOfLevel>());
            }
        }
    }
	
    public void AddKey()
    {
        m_uiManager.AddKey();
        m_keys++;
        if(m_keys == m_numberOfKeys)
        {
            m_levelComplete = true;
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        m_door.m_animtor.SetTrigger("Open");
    }


    public void StartLevel()
    {
        m_uiManager.SetupKeys(m_numberOfKeys);
    }

    public void ResetLevel()
    {
        m_player.transform.position = m_respawnPoint.transform.position;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        m_uiManager.SetupKeys(m_numberOfKeys);
    }
}
