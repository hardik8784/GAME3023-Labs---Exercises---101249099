using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Started..!!");
        Debug.Log("Kem Cho World..??");
    }

    // Update is called once per frame
    void Update()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(InputX, InputY, 0) * moveSpeed * Time.deltaTime;

    }
}
