using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    Collider playerCollider;
    public bool m_active;
    public Level newLevel;

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
                if (GameObject.Find("Player").GetComponent<Player>().currentLevel != null)
                {
                    GameObject.Find("Player").GetComponent<Player>().currentLevel.m_door.m_animtor.SetTrigger("Close");
                }
                GameObject.Find("Player").GetComponent<Player>().currentLevel = newLevel;
                newLevel.GetComponent<Level>().StartLevel();
                m_active = false;
            }

        }
       
        
    }
}
