using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Left/right movement
/// No jumping
/// Flip gravity (flip sprite??)
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    /// <summary>
    /// reference to the rigidbody
    /// </summary>
    private Rigidbody m_rigidbody;
    private Animator m_animator;
    private PlayerRespawn m_player;
    public Transform m_defaultPos, m_flippedPos;
    /// <summary>
    /// movement speed of the player
    /// </summary>
    public float m_moveSpeed = 100;
    public float m_flipSpeed;
    public int m_acceleration;
    public int m_decelleration;

    /// <summary>
    /// flag to check if the player is grounded
    /// </summary>
    public bool m_isGrounded = false;

    private LayerMask m_playerLayerMask;
    public float m_currentTime;
    public Transform m_graphics;
    public bool m_flipped;
    public float m_maxSpeed;

    // Use this for initialization
    void Start() {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerLayerMask = LayerMask.NameToLayer("Player");
        m_animator = GetComponent<Animator>();
        m_player = GetComponent<PlayerRespawn>();

        if(gameObject.layer != m_playerLayerMask) {
            Debug.LogError("Please make the players layer to be 'Player'");
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null) {
            if (collider.material == null || collider.material.name.Contains("Default")) {
                Debug.LogError("The player colldier should have the FrictionLess Physics Material");
            }
        } else {
            Debug.LogError("The player does not have a collider");
        }

        //set up the constraints
        RigidbodyConstraints desiredConstraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        if (m_rigidbody.constraints != desiredConstraints) {
            Debug.LogWarning("Changing Player Constraints - Freeze position Z | Freeze all rotation;");
            m_rigidbody.constraints = desiredConstraints;
        }
    }

    // Update is called once per frame
    void Update() {
        //calc grounded
        m_isGrounded = false;//reset
        //work out which way the gravity is going
        float isGravityUp = Mathf.Sign(Physics.gravity.y);
        //the character is offset from being centered, it's anchored to the bottom side of the player
        Vector3 playerCharacterOffset = new Vector3(0, Mathf.Clamp01(Physics.gravity.y) * 2, 0);
        float playerXOffset = 0.5f;
        //go between -1 and 1, used as a direction scale
        for (int i = -1; i < 2; i++) {
            //NOTE: could probably combine playerCharacterOffset and xOffset into one vector
            //offset based on i(direction)
            float xOffset = i * playerXOffset;
            //draw a debug ray to show off where the ray is
            Debug.DrawRay(transform.position + playerCharacterOffset + new Vector3(xOffset,0,0), Vector3.up * isGravityUp * 2);
            //finally work out if the player is on the ground
            m_isGrounded |= Physics.Raycast(transform.position + playerCharacterOffset + new Vector3(xOffset, 0, 0), Vector3.up * isGravityUp, 2, ~m_playerLayerMask.value);
            //m_isGrounded = Physics.Raycast(transform.position + offset, Vector3.up * isGravityUp, out hit, 2, ~m_playerLayerMask.value);
            //ok player is grounded, no need to check if other raycasts are hitting the ground/foor
            if (m_isGrounded) {
                break;
            }
        }

        //if the player is not grounded dont run the movement/gravity flip code
        
        //if (!m_isGrounded) {
        //    return;
        //}

        //get key inputs
        bool keyLeft = Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow);
        bool keyRight = Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow);
        if(m_isGrounded)
        {
            if (m_player.currentLevel.m_movesDone < m_player.currentLevel.m_moves)
            {
                bool keyGravityFlip = Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.Space);
                //go gravity flip
                if (keyGravityFlip)
                {
                    FlipGravity();
                    m_player.currentLevel.m_movesDone++;
                    m_player.currentLevel.m_uiManager.SetupMoves(m_player.currentLevel.m_moves - m_player.currentLevel.m_movesDone, m_player.currentLevel.m_moves);

                }
            }
        }

   

        //calc direction from key inputs
       
        if (keyLeft && (m_moveSpeed > -m_maxSpeed)) {
          
            if (m_moveSpeed > 0)
                m_moveSpeed -= m_acceleration * 4 * Time.deltaTime;
            else if (m_moveSpeed <= 0)
                m_moveSpeed -= m_acceleration * Time.deltaTime;
        }
        else if (keyRight )
        { 
            if (m_moveSpeed < 0)
                m_moveSpeed += m_acceleration * 4 * Time.deltaTime;
            else if(m_moveSpeed >= 0)
                m_moveSpeed += m_acceleration * Time.deltaTime;
        }
        else
        {
            if (m_moveSpeed > m_decelleration * Time.deltaTime)
                m_moveSpeed -= m_decelleration * Time.deltaTime;
            else if (m_moveSpeed < -m_decelleration * Time.deltaTime)
                m_moveSpeed = m_moveSpeed + m_decelleration * Time.deltaTime;
            else
                m_moveSpeed = 0;
        }

        m_moveSpeed = Mathf.Clamp(m_moveSpeed, -m_maxSpeed, m_maxSpeed);
        //do movement
        SideMovement();
      

    }

    private void SideMovement() {

        Vector3 velocity = m_rigidbody.velocity;
        velocity.x = m_moveSpeed * 100 * Time.deltaTime;
        m_rigidbody.velocity = velocity;
    }

    private void FlipGravity() {
        Vector3 gravity = Physics.gravity;
        gravity.y *= -1;
        Physics.gravity = gravity;
        StartCoroutine(FlipPLayer(m_flipSpeed));
        m_flipped = !m_flipped;
        
    }

    IEnumerator FlipPLayer(float time)
    {
        m_currentTime = 0;
        while(m_currentTime < time)
        {
            
            m_currentTime += Time.deltaTime;
            if(m_flipped)
            {
                m_graphics.transform.position = Vector3.Lerp(m_graphics.transform.position, m_flippedPos.position, m_currentTime / time);
                m_graphics.transform.rotation = Quaternion.Lerp(m_graphics.transform.rotation, m_flippedPos.rotation, m_currentTime / time);
            }
            else
            {
                m_graphics.transform.position = Vector3.Lerp(m_graphics.transform.position, m_defaultPos.position, m_currentTime / time);
                m_graphics.transform.rotation = Quaternion.Lerp(m_graphics.transform.rotation, m_defaultPos.rotation, m_currentTime / time);
            }
           
            yield return null;
        }
    }


}
