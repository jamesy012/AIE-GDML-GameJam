using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    GameObject player;
    Collider playerCollider;
    public bool m_active;
    public Level newLevel;
    public bool changeBackgroundMusic;
    public AudioClip newBackGroundMusic;
    public Level prevLevel;
    private int levelNumber;
    public Image m_levelStar;
    

    public References m_references;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerCollider = player.GetComponent<Collider>();
        m_references = GameObject.Find("References").GetComponent<References>();
        levelNumber = int.Parse(transform.parent.name.Remove(0,"Level".Length));
        if(levelNumber != 1)
            m_levelStar = GameObject.Find("StarPanel").transform.GetChild(levelNumber - 2).GetComponent<Image>();
    }

    private void OnTriggerEnter(Collider other)
    {
		if (m_levelStar == null) {
			return;
		}
        if(m_active)
        {
            if (other == playerCollider)
            {
                if (GameObject.Find("Player").GetComponent<Player>().m_currentLevel != null)
                {
                    GameObject.Find("Player").GetComponent<Player>().m_currentLevel.m_door.m_animtor.SetTrigger("Close");
                }
                if (prevLevel != null)
                {
                    if (prevLevel.m_movesDone == prevLevel.platStar)
                    {
                        Debug.Log("help");
                        m_references.m_stars[3].SetActive(true);
                        m_references.m_stars[2].SetActive(false);
                        m_references.m_stars[1].SetActive(false);
                        m_references.m_stars[0].SetActive(false);
                        m_levelStar.sprite = m_references.m_starSprites[3];
                        m_levelStar.color = Color.white;
                        m_references.m_starAnim.SetTrigger("Go");
                    }
                    else if (prevLevel.m_movesDone <= prevLevel.goldStar && prevLevel.m_movesDone > prevLevel.platStar)
                    {
                        Debug.Log("Gold");
                        m_references.m_stars[3].SetActive(false);
                        m_references.m_stars[2].SetActive(true);
                        m_references.m_stars[1].SetActive(false);
                        m_references.m_stars[0].SetActive(false);
                        m_levelStar.sprite = m_references.m_starSprites[2];
                        m_levelStar.color = Color.white;
                        m_references.m_starAnim.SetTrigger("Go");
                    }
                    else if (prevLevel.m_movesDone <= prevLevel.silverStar && prevLevel.m_movesDone > prevLevel.goldStar)
                    {
                        Debug.Log("Silver");
                        m_references.m_stars[3].SetActive(false);
                        m_references.m_stars[2].SetActive(false);
                        m_references.m_stars[1].SetActive(true);
                        m_references.m_stars[0].SetActive(false);
                        m_levelStar.sprite = m_references.m_starSprites[1];
                        m_levelStar.color = Color.white;
                        m_references.m_starAnim.SetTrigger("Go");
                    }
                    else if (prevLevel.m_movesDone <= prevLevel.bronzeStar && prevLevel.m_movesDone > prevLevel.silverStar)
                    {
                        m_references.m_stars[3].SetActive(false);
                        m_references.m_stars[2].SetActive(false);
                        m_references.m_stars[1].SetActive(false);
                        m_references.m_stars[0].SetActive(true);
                        m_levelStar.sprite = m_references.m_starSprites[0];
                        m_levelStar.color = Color.white;
                        m_references.m_starAnim.SetTrigger("Go");
                    }
                }
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
