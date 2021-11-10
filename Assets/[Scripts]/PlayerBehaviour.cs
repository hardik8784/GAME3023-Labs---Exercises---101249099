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

    private Rigidbody2D Rigidbody;
    private Animator AnimatorController;


    ////Public Variable
    //[SerializeField]
    //private float Speed = 10.0f;

    //[SerializeField]
    //private float JumpForce = 500.0f;

    //[SerializeField]
    //private float GroundCheckRadius = 0.15f;

    //[SerializeField]
    //private Transform GroundCheck;

    //[SerializeField]
    //private LayerMask GroundMask;

    ////Private Variable
    //private Rigidbody2D rigidBody;

    //[SerializeField]
    //private bool isGrounded = false;

    //Commented Out
    //[SerializeField]
    //private float moveSpeed = 1.0f;

    //[SerializeField]
    //public Vector3 UpdatedPosition;
    // til this one

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
        //float InputX = Input.GetAxisRaw("Horizontal");
        ////float InputY = Input.GetAxisRaw("Vertical");
        //isGrounded = TouchWithGroundCheck();

        ////Jump
        //if(isGrounded && Input.GetAxis("Jump") > 0 )
        //{
        //    rigidBody.AddForce(new Vector2(0.0f, JumpForce));
        //    isGrounded = false;
        //}

        //rigidBody.velocity = new Vector2(InputX * Speed, rigidBody.velocity.y);

    }

    private void Move()
    {
        if (isGrounded)
        {

            float DeltaTime = Time.deltaTime;

            // Keyboard Input
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            float Jump = Input.GetAxisRaw("Jump");

            // Check for Flip

            if (x != 0)
            {
                x = FlipAnimation(x);
                AnimatorController.SetInteger("AnimationState", 1);     //RUN state
            }
            else
            {
                AnimatorController.SetInteger("AnimationState", 0); //IDLE State
            }

            // Touch Input
            Vector2 worldTouch = new Vector2();
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }


            float HorizontalMoveForce = x * HorizontalForce; //* DeltaTime;
            float JumpMoveForce = Jump * VerticalForce;      // * DeltaTime;

            float Mass = Rigidbody.mass * Rigidbody.gravityScale;

            Rigidbody.AddForce(new Vector2(HorizontalMoveForce, JumpMoveForce) * Mass);
            Rigidbody.velocity *= 0.99f;
        }
        else
        {
            AnimatorController.SetInteger("AnimationState", 2);     //JUMP State
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

    //private bool TouchWithGroundCheck()
    //{


    //    //return Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundMask);
    //}


    /// <summary>
    /// This function checks the Input from the player and change the position of the player
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        //Comment Out
        //float InputX = Input.GetAxisRaw("Horizontal");
        //float InputY = Input.GetAxisRaw("Vertical");

        //transform.position += new Vector3(InputX, InputY, 0) * moveSpeed * Time.deltaTime;
        //UpdatedPosition = transform.position;                                                       //Save the Updated Position
        //Till This one
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
        //SavePosition = UpdatedPosition;
        OnSave.Invoke();
        PlayerPrefs.Save();
        // Debug.Log("Saved!!");
    }

    public void OnButtonPressLoad()                                         //OnButtonPressLoad Event
    {
        OnLoad.Invoke();
        //transform.position = SavePosition;
       // Debug.Log("Load!!");
    }

    void SavePlayerPosition()
    {
        //PlayerPrefs.SetString("PlayerPosition", "Save");
        //Comment Out
        //SavePosition = UpdatedPosition;
        //Till This
        Debug.Log("Saved Position : " + SavePosition);
       // Debug.Log("Player Position Saved");
    }

    void LoadPlayerPosition()
    {
        transform.position = SavePosition;
        LoadPosition = SavePosition;

        Debug.Log("Loaded Position : " + LoadPosition);
        //string Loaded = PlayerPrefs.GetString("PlayerPosition", "");
        // Debug.Log("Player Position Loaded");// + Loaded);
    }

}
