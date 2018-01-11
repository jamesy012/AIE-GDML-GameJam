using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour {

    public GameObject playerCollider;
    public AudioClip deathSound;
    public LevelManager m_levelManager;

    private void Start()
    {
        m_levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_levelManager.RestartLevel();
            SoundManager.PlaySFX(deathSound);
        }
    }
}
