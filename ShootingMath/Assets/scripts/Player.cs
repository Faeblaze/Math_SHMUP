using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5F;

    public GameObject player;
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
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Vector3 point = hit.point;
            point.z = transform.position.z;
            transform.LookAt(point);
        }

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += Vector3.up;
        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right;
        if (Input.GetKey(KeyCode.S))
            direction += Vector3.down;

        rb.velocity = direction.normalized * speed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            OnDie();
    }

    public void OnDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
