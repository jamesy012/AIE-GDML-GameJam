using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    Collider playerCollider;
    public bool m_active;
    public GameObject newLevel;

    private void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(m_active)
        {
            if (other == playerCollider)
            {
                GameObject.Find("Player").GetComponent<PlayerRespawn>().currentLevel = newLevel;
                newLevel.GetComponent<Level>().StartLevel();
                m_active = false;
            }

        }
       
        
    }
}
