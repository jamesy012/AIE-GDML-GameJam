using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public int m_numberOfKeys;
    public int m_keys;

    public Transform m_respawnPoint;

    public int m_moves;
    public int m_movesDone;
    public UIManager m_uiManager;
    private GameObject m_player;
    private Vector3 m_defaultGravity;
    public GameObject m_camera;
    public List<EndOfLevel> m_levelTriggers;
    private float m_currentTime;
    public bool m_levelComplete;
    public float m_panTime;
    public Door m_door;
    public bool m_startFlipped;
    public Transform m_levelCamPos;
    public int platStar;
    public int goldStar;
    public int silverStar;
    public int bronzeStar;



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
        for (int i = 0; i < m_gravityDirection.Length; i++) {
            m_gravityDirection[i].m_allowDirection = true;
        }

        m_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        m_player = GameObject.Find("Player");
        m_camera = GameObject.Find("Main Camera");
        m_defaultGravity = Physics.gravity;
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
        StartCoroutine(PanCamera(3));
        m_uiManager.SetupMoves(m_moves, m_moves);
    }

    public void ResetLevel()
    {
        m_player.transform.position = m_respawnPoint.transform.position;
        Physics.gravity = m_defaultGravity;
        m_uiManager.SetupKeys(m_numberOfKeys);
        StartCoroutine(PanCamera(3));
        m_uiManager.SetupMoves(m_moves, m_moves);
        m_player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_movesDone = 0;
        m_keys = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
           if(this.transform.GetChild(i).GetComponent<KeyPickupScript>())
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

    }

    IEnumerator PanCamera(float time)
    {
        m_currentTime = 0;
        while (m_currentTime < time)
        {
            m_currentTime += Time.deltaTime;
            if(m_levelCamPos != null)
                m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_levelCamPos.position, m_currentTime / time);
            yield return null;
        }
    }

    private void OnValidate() {
        for(int i = 0; i < m_gravityDirection.Length; i++) {
            m_gravityDirection[i].m_name = ((PlayerMovement.Direction)i).ToString().Remove(0,1);
        }
    }
}
