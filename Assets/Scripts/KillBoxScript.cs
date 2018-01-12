using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour {

    public AudioClip deathSound;
    public LevelManager m_levelManager;
    public ParticleSystem playerDeathParticle;
    Transform playerPosition;

    private void Start()
    {
        m_levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerDeathParticle != null)
            {
                playerPosition = GameObject.Find("Player").transform;
                Instantiate(playerDeathParticle, new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z), playerPosition.transform.rotation);
            }
            m_levelManager.RestartLevel();
            SoundManager.PlaySFX(deathSound);
        }
    }
}
