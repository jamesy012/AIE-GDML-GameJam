﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public int m_numberOfKeys;

    public Transform m_respawnPoint;

    public UIManager m_uiManager;
    private GameObject m_player;
	
    // Use this for initialization
	void Start () {
        m_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        m_player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
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
