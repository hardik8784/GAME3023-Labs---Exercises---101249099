using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// This class is for the Main Gameplay mechanics including changing the scene and Player Behaviours
/// Reference : Lecture/Lab Saving and Loading Video
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    public static UnityEvent OnSave = new UnityEvent();
    public static UnityEvent OnLoad = new UnityEvent();

    [SerializeField]
    Vector3 SavePosition;                                    //Initialize the Vector3 to Save the players Position whenever ButtonPressed

    [SerializeField]
    Vector3 LoadPosition;                                    //Initialize the Vector3 to Load the players Position whenever ButtonPressed

    [Header("Movement")]
    public float HorizontalForce;
    public float VerticalForce;
    public bool isGrounded;
    public Transform GroundOrigin;
    public float GroundRadius;
    public LayerMask GroundLayerMask;

    [Range(0.1f, 0.9f)]
    public float AirControlFactor;

    [Header("Animation")]
    public PlayerAnimationState State;

    private Rigidbody2D Rigidbody;
    private Animator AnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        AnimatorController = GetComponent<Animator>();
        PlayerBehaviour.OnSave.AddListener(SavePlayerPosition);
        PlayerBehaviour.OnLoad.AddListener(LoadPlayerPosition);
        //Debug.Log("Game Started..!!");
        //Debug.Log("Kem Cho World..??");
    }

    private void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnSave.Invoke();
            PlayerPrefs.Save();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            OnLoad.Invoke();
        }

    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (isGrounded)
        {

            // Keyboard Input
           
            float y = Input.GetAxisRaw("Vertical");
            float Jump = Input.GetAxisRaw("Jump");

            // Check for Flip

            if (x != 0)
            {
                x = FlipAnimation(x);
                AnimatorController.SetInteger("AnimationState", (int)PlayerAnimationState.RUN);     //RUN State
                State = PlayerAnimationState.RUN;
            }
            else
            {
                AnimatorController.SetInteger("AnimationState", (int)PlayerAnimationState.IDLE);     //IDLE State
                State = PlayerAnimationState.IDLE;
            }

            // Touch Input
            Vector2 worldTouch = new Vector2();
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }


            float HorizontalMoveForce = x * HorizontalForce; 
            float JumpMoveForce = Jump * VerticalForce;      

            float Mass = Rigidbody.mass * Rigidbody.gravityScale;

            Rigidbody.AddForce(new Vector2(HorizontalMoveForce, JumpMoveForce) * Mass);
            Rigidbody.velocity *= 0.99f;
        }
        else // Air Control
        {
            AnimatorController.SetInteger("AnimationState", (int)PlayerAnimationState.JUMP);     //JUMP State
            State = PlayerAnimationState.JUMP;

            if (x != 0)
            {
                x = FlipAnimation(x);

                float HorizontalMoveForce = x * HorizontalForce * AirControlFactor; 
              

                float Mass = Rigidbody.mass * Rigidbody.gravityScale;

                Rigidbody.AddForce(new Vector2(HorizontalMoveForce, 0.0f) * Mass);

            }

        }
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D Hit = Physics2D.CircleCast(GroundOrigin.position, GroundRadius, Vector2.down, GroundRadius, GroundLayerMask);

        isGrounded = (Hit) ? true : false;
    }

    private float FlipAnimation(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundOrigin.position, GroundRadius);
    }

    /// <summary>
    /// This function is used to check the gameObject tagged with Door collides with the other gameObject
    /// If it does, then change to Level2
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Door")
        {
            Debug.Log("Collision with Door");
            SceneManager.LoadScene("Level2");
        }
    }

    public void OnButtonPressedSave()                                       //OnButtonPressedSave Event
    {
        OnSave.Invoke();
        PlayerPrefs.Save();
    }

        


    public void OnButtonPressLoad()                                         //OnButtonPressLoad Event
    {
        OnLoad.Invoke();
    }

    void SavePlayerPosition()
    {
        SavePosition = transform.position;
        Debug.Log("Saved Position : " + SavePosition);
    }

    void LoadPlayerPosition()
    {
        transform.position = SavePosition;
        LoadPosition = SavePosition;
        Debug.Log("Loaded Position : " + LoadPosition);
    }

}
