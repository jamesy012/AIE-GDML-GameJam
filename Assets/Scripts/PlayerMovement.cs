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
    private Player m_player;
    public Transform m_defaultPos, m_flippedPos;

    /// <summary>
    /// movement speed of the player
    /// </summary>
    public float m_moveSpeed = 100;
    public float m_flipSpeed;
    public int m_acceleration;
    public int m_decelleration;
    private int m_direction;

    /// <summary>
    /// flag to check if the player is grounded
    /// </summary>
    private bool m_isGrounded = false;


    /// <summary>
    /// Sound effect storage
    /// </summary>
    public AudioClip m_gravitySwitchAudio;

    private LayerMask m_playerLayerMask;
    public float m_currentTime;
    public Transform m_graphics;
    public bool m_flipped;
    public float m_maxSpeed;

    public enum Direction {
        KLeft,
        KRight,
        KUp,
        KDown
    }

    /// <summary>
    /// which keys are held
    /// use Direction enum as index
    /// </summary>
    private bool[] m_keyDirections = new bool[4];

    // Use this for initialization
    void Start() {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerLayerMask = LayerMask.NameToLayer("Player");
        m_animator = GetComponent<Animator>();
        m_player = GetComponent<Player>();

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

        if (m_isGrounded)
        {
            if (m_player.m_currentLevel != null)
            {
                if (m_player.m_currentLevel.m_movesDone < m_player.m_currentLevel.m_moves)
                {
                    bool keyGravityFlip = Input.GetKey(KeyCode.Space);
                    //go gravity flip
                    if (keyGravityFlip)
                    {

                        //0-3(inclusive) being left,right,up,down keys
                        for (int i = 0; i < 4; i++) {
                            if (m_keyDirections[i] && m_player.m_currentLevel.m_gravityDirection[i].m_allowDirection) {
                                FlipGravity((Direction)i);
                                m_player.m_currentLevel.m_movesDone++;
                                m_player.m_currentLevel.m_uiManager.SetupMoves(m_player.m_currentLevel.m_moves - m_player.m_currentLevel.m_movesDone, m_player.m_currentLevel.m_moves);
                                break;
                            }
                        }

                    }
                }
            }
        }
        //do movement

        if (m_graphics.transform.rotation == m_flippedPos.rotation && !m_flipped)
        {
            StartCoroutine(FlipPLayer(m_flipSpeed));
        }
        if (m_graphics.transform.rotation == m_defaultPos.rotation && m_flipped)
        {
            StartCoroutine(FlipPLayer(m_flipSpeed));
        }
        
        

    }

    public void FixedUpdate()
    {
        //calc grounded
        m_isGrounded = false;//reset
        //work out which way the gravity is going
        float isGravityUp = Mathf.Sign(Physics.gravity.y);
        //the character is offset from being centered, it's anchored to the bottom side of the player
        Vector3 playerCharacterOffset = new Vector3(0, Mathf.Clamp01(Physics.gravity.y) * 2, 0);
        float playerXOffset = 0.5f;
        int[] offsets = { 0, -1, 1 };
        //go between -1 and 1, used as a direction scale
        for (int i = 0; i < 3; i++)
        {
            //NOTE: could probably combine playerCharacterOffset and xOffset into one vector
            //offset based on i(direction)
            float xOffset = offsets[i] * playerXOffset;
            //draw a debug ray to show off where the ray is
            Debug.DrawRay(transform.position + playerCharacterOffset + new Vector3(xOffset, -0.5f * isGravityUp, 0), Vector3.up * isGravityUp * 2);
            //finally work out if the player is on the ground
            m_isGrounded |= Physics.Raycast(transform.position + playerCharacterOffset + new Vector3(xOffset, -0.5f * isGravityUp, 0), Vector3.up * isGravityUp, 2, ~(1<<m_playerLayerMask.value));
            //m_isGrounded = Physics.Raycast(transform.position + offset, Vector3.up * isGravityUp, out hit, 2, ~m_playerLayerMask.value);
            //ok player is grounded, no need to check if other raycasts are hitting the ground/foor
            if (m_isGrounded)
            {
                break;
            }
        }

        //get key inputs
        m_keyDirections[(int)Direction.KLeft] = Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow);
        m_keyDirections[(int)Direction.KRight] = Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow);
        m_keyDirections[(int)Direction.KUp] = Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow);
        m_keyDirections[(int)Direction.KDown] = Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow);

        //calc movement speed
        if (m_keyDirections[(int)Direction.KLeft] && (m_moveSpeed > -m_maxSpeed))
        {
            m_graphics.transform.localScale = new Vector3(-1, 1, 1);
            if (m_moveSpeed > 0)
                m_moveSpeed -= m_acceleration * 4 * Time.deltaTime;
            else if (m_moveSpeed <= 0)
                m_moveSpeed -= m_acceleration * Time.deltaTime;
        }
        else if (m_keyDirections[(int)Direction.KRight])
        {
            m_graphics.transform.localScale = new Vector3(1, 1, 1);
            if (m_moveSpeed < 0)
                m_moveSpeed += m_acceleration * 4 * Time.deltaTime;
            else if (m_moveSpeed >= 0)
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

        //clamp speed
        m_moveSpeed = Mathf.Clamp(m_moveSpeed, -m_maxSpeed, m_maxSpeed);
        SideMovement();
    }

    public void ResetVariables()
    {
        
        StopAllCoroutines();
        m_moveSpeed = 0;
        if (m_player.m_currentLevel.m_startFlipped)
        {
            m_graphics.transform.rotation = m_flippedPos.rotation;
            m_graphics.transform.position = m_flippedPos.position;
            m_flipped = true;
        }
        else
        {
            m_graphics.transform.rotation = m_defaultPos.rotation;
            m_graphics.transform.position = m_defaultPos.position;
            m_flipped = false;
        }

    }

    private void SideMovement() {

        Vector3 velocity = m_rigidbody.velocity;
        velocity.x = m_moveSpeed * 100 * Time.deltaTime;
        m_rigidbody.velocity = velocity;
    }

    private void FlipGravity(Direction a_direction) {
        float gravityValue = Mathf.Max(Mathf.Abs(Physics.gravity.x),Mathf.Max(Mathf.Abs(Physics.gravity.y), Mathf.Abs(Physics.gravity.z)));
        Vector3 gravity = Vector3.zero;
        switch (a_direction) {
            case Direction.KDown:
                gravity.y = -gravityValue;
                break;
            case Direction.KUp:
                gravity.y = gravityValue;
                break;
            case Direction.KLeft:
                gravity.x = -gravityValue;
                break;
            case Direction.KRight:
                gravity.x = gravityValue;
                break;
        }
        Physics.gravity = gravity;
        StartCoroutine(FlipPLayer(m_flipSpeed));
        m_flipped = !m_flipped;

    }

    IEnumerator FlipPLayer(float time)
    {
        SoundManager.PlaySFX(m_gravitySwitchAudio);
        m_currentTime = 0;
     
        while (m_currentTime < time)
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
