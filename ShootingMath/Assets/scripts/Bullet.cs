using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;

    private GameLogic.Operator op;
    private int number;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Count(number, op);
                Destroy(gameObject);
            }
        } else if (hitInfo.CompareTag("Bound"))
        {
            Destroy(gameObject);
        }

    }

    public void SetValues(GameLogic.Operator op, int number)
    {
        this.op = op;
        this.number = number;
    }
}
