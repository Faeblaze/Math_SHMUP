using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;

    public Vector2Int range = new Vector2Int(1, 50);

    private Rigidbody rb;

    public int Number { get; private set; }
    public int ReachedNumber { get; private set; }

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        Bounds worldBounds = WorldBoundary.Instance.WorldBounds;
        Vector3 center = worldBounds.center;
        Vector3 extents = worldBounds.extents;
        transform.position = center + (Vector3)((new Vector2(Random.value, Random.value) - Vector2.one * .5F) * extents);

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit);

        transform.position += Vector3.back * 100F;
        Vector3 pos = transform.position;
        pos.z = hit.point.z;
        transform.position = pos;

        Number = Random.Range(range.x, range.y + 1);
        GetComponentInChildren<TextMeshPro>().text = Number.ToString();
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            Vector3 heading = target.position - transform.position;
            Vector3 direction = heading / heading.magnitude;
            direction.z = 0F;
            rb.velocity = direction * speed;
        }
    }

    public void Count (int number, GameLogic.Operator op)
    {
        switch (op)
        {
            case GameLogic.Operator.ADD:
                ReachedNumber += number;
                break;
            case GameLogic.Operator.SUBTRACT:
                ReachedNumber -= number;
                break;
            case GameLogic.Operator.MULTIPLY:
                ReachedNumber *= number;
                break;
            case GameLogic.Operator.DIVIDE:
                ReachedNumber /= number;
                break;
        }

        if (ReachedNumber == Number)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
