using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// This class is for the Main Gameplay mechanics including changing the scene and Player Behaviours
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    public static UnityEvent OnSave = new UnityEvent();
    public static UnityEvent OnLoad = new UnityEvent();

    [SerializeField]
    Vector3 SavePosition;                                    //Initialize the Vector3 to Save the players Position whenever ButtonPressed

    [SerializeField]
    Vector3 LoadPosition;                                    //Initialize the Vector3 to Load the players Position whenever ButtonPressed

    [SerializeField]
    private float moveSpeed = 1.0f;
   
    [SerializeField]
    public Vector3 UpdatedPosition;
   
    // Start is called before the first frame update
    void Start()
    {
        PlayerBehaviour.OnSave.AddListener(SavePlayerPosition);
        PlayerBehaviour.OnLoad.AddListener(LoadPlayerPosition);
        //Debug.Log("Game Started..!!");
        //Debug.Log("Kem Cho World..??");
    }

    /// <summary>
    /// This function checks the Input from the player and change the position of the player
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(InputX, InputY, 0) * moveSpeed * Time.deltaTime;
        UpdatedPosition = transform.position;                                                       //Save the Updated Position 
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
        SavePosition = UpdatedPosition;
        OnSave.Invoke();
        PlayerPrefs.Save();

       // Debug.Log("Saved!!");
    }

    public void OnButtonPressLoad()                                         //OnButtonPressLoad Event
    {
        OnLoad.Invoke();
        transform.position = SavePosition;
       // Debug.Log("Load!!");
    }

    void SavePlayerPosition()
    {
        PlayerPrefs.SetString("PlayerPosition", "Save");
        SavePosition = UpdatedPosition;
        Debug.Log("Saved Position : " + SavePosition);
       // Debug.Log("Player Position Saved");
    }

    void LoadPlayerPosition()
    {
        LoadPosition = SavePosition;
        Debug.Log("Loaded Position : " + LoadPosition);
        //string Loaded = PlayerPrefs.GetString("PlayerPosition", "");
       // Debug.Log("Player Position Loaded");// + Loaded);
    }

}
