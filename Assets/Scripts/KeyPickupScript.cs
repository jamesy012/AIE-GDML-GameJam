using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : MonoBehaviour
{
    Collider playerCollider;
    public Level m_level;
    private void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            m_level.m_uiManager.AddKey();
            Destroy(gameObject);
        }
        
    }
}
