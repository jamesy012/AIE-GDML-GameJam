using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CoreGameManager : MonoBehaviour {

    public GameObject m_UI;
	// Use this for initialization
	void Start () {
        
        m_UI.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel(string a_level)
    {
        SceneManager.LoadScene(a_level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
