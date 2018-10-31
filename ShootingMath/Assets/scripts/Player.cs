using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{   
    public GameObject player;
    public Enemy enemy;
    private Rigidbody rb;
      
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
     
    }

    private void Update()
    {
        Movement();       
    }

    // moving only w,a,d, forward left and right- constant movement while holding down button 
    //Adding S because on gameplay aspects, can remove later

    public void Movement()
    {
        if (Input.GetKey(KeyCode.W))
           rb.velocity = (Vector3.up * 5f);
        if (Input.GetKey(KeyCode.A))
            rb.velocity = (Vector3.left * 5f);
        if (Input.GetKey(KeyCode.D))
            rb.velocity = (Vector3.right * 5f);
        if (Input.GetKey(KeyCode.S))
            rb.velocity = (Vector3.down * 5f);
    }


}
