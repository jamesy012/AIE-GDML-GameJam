using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;
    public bool m_active;
    public Level newLevel;
    public bool changeBackgroundMusic;
    public AudioClip newBackGroundMusic;
    public int goldStar;
    public int silverStar;
    public int bronzeStar;
    private Level currentLevel;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerCollider = player.GetComponent<Collider>();
        currentLevel = player.GetComponent<Player>().m_currentLevel.GetComponent<Level>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(m_active)
        {
            if (other == playerCollider)
            {
                if (GameObject.Find("Player").GetComponent<Player>().m_currentLevel != null)
                {
                    GameObject.Find("Player").GetComponent<Player>().m_currentLevel.m_door.m_animtor.SetTrigger("Close");
                }

                //if (currentLevel.m_movesDone <= currentLevel)
                //{
                //    //give player gold star
                //}
                GameObject.Find("Player").GetComponent<Player>().m_currentLevel = newLevel;
                newLevel.GetComponent<Level>().StartLevel();
                m_active = false;
            }
            if (changeBackgroundMusic)
            {
                SoundManager.StopBGM(false, 0);
                SoundManager.PlayBGM(newBackGroundMusic, true, 10.0f);
            }

            
        }

    }
}
