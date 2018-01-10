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

    public Transform m_defaultPos, m_flippedPos;
    /// <summary>
    /// movement speed of the player
    /// </summary>
    public float m_moveSpeed = 100;

    /// <summary>
    /// flag to check if the player is grounded
    /// </summary>
    public bool m_isGrounded = false;

    private LayerMask m_playerLayerMask;
    public float m_currentTime;
    public Transform m_graphics;
    public bool m_flipped;

    // Use this for initialization
    void Start() {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerLayerMask = LayerMask.NameToLayer("Player");
        m_animator = GetComponent<Animator>();
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
        if (!m_isGrounded) {
            return;
        }

        //get key inputs
        bool keyLeft = Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow);
        bool keyRight = Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow);
        bool keyGravityFlip = Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.Space);

        //calc direction from key inputs
        int movement = 0;
        if (keyLeft) {
            movement += -1;
        }
        if (keyRight) {
            movement += 1;
        }

        //do movement
        SideMovement(movement);

        //go gravity flip
        if (keyGravityFlip) {
            FlipGravity();
        }

       
         m_animator.SetInteger("Movement", movement);
        

    }

    private void SideMovement(int a_movementDirection) {
        Vector3 velocity = m_rigidbody.velocity;
        velocity.x = a_movementDirection * m_moveSpeed * Time.deltaTime;
        m_rigidbody.velocity = velocity;
    }

    private void FlipGravity() {
        Vector3 gravity = Physics.gravity;
        gravity.y *= -1;
        Physics.gravity = gravity;
        StartCoroutine(FlipPLayer(0.3f));
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
