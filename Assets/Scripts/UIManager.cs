using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public Transform m_keyPanel;
    public GameObject m_keyImagePrefab;
    public List<GameObject> m_keySprites;
    public List<Transform> m_objectsToDestroy;

    public Sprite m_keyOnImage;
    public Sprite m_keyOffImage;

    public int m_numberOfKeysActive;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            SetupKeys(5);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            AddKey();
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

 

}
