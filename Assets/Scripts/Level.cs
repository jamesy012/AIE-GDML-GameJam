using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public int m_numberOfKeys;

    public Transform m_respawnPoint;

    private UIManager m_uiManager;
    private GameObject m_player;
	
    // Use this for initialization
	void Start () {
        m_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartLevel()
    {
        m_uiManager.SetupKeys(m_numberOfKeys);
    }

    void ResetLevel()
    {
        m_player.transform.position = m_respawnPoint.transform.position;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        m_uiManager.SetupKeys(m_numberOfKeys);
    }
}
