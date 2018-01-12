﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public Image m_movesBar;
    public Transform m_keyPanel;
    public GameObject m_keyImagePrefab;


    public List<Transform> m_objectsToDestroy;

    public Sprite m_keyOnImage;
    public Sprite m_keyOffImage;

    public Text m_movesLeftText;
    public int m_numberOfKeysActive;

    public GameObject m_canvas;

    public References m_references;
    public List<GameObject> m_keySprites;

    // Use this for initialization
    void Start () {

        m_references = GameObject.Find("References").GetComponent<References>();
	}
	
	// Update is called once per frame
	void Update () {
		
    }


    public void DisplayStar(string a_star, Level a_level)
    {
        switch (a_star)
        {
            case " Platinum":
                break;
        }

    }

    public void SetupKeys(int a_numberOfKeys)
    {
        for(int i = 0; i < m_keyPanel.childCount; i ++)
        {
            m_keySprites.Remove(m_keyPanel.GetChild(i).gameObject);
            Destroy(m_keyPanel.GetChild(i).gameObject);
            m_numberOfKeysActive = 0;
        }
        
        for (int i = 0; i < a_numberOfKeys; i++)
        {
            GameObject Key = Instantiate(m_keyImagePrefab, m_keyPanel) as GameObject;
            Key.GetComponent<Image>().sprite = m_keyOffImage;
           m_keySprites.Add(Key);
        }
    }

    public void AddKey()
    {
        if(m_numberOfKeysActive < m_keySprites.Count)
        {
           m_keySprites[m_numberOfKeysActive].GetComponent<Image>().sprite = m_keyOnImage;
            m_numberOfKeysActive++;
        } 
    }

    public void SetupMoves(int a_moves, int m_maxMoves)
    {
        m_movesLeftText.text = a_moves.ToString();
        m_movesBar.fillAmount = (float)a_moves / m_maxMoves;

        m_canvas.SetActive(true);
      
    }
    



}
