using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject enemyTemplate;
    public Weapon weapon;

    public bool HasEnemy
    {
        get
        {
            return currentEnemy;
        }
    }
    public Operator CurrentOperator { get; private set; }
    public int CurrentNumber { get; private set; }
    public int EnemyNumber
    {
        get
        {
            return currentEnemy.ReachedNumber;
        }
    }

    public float enemyWait = 2F;
    private float enemyTimer = 0F;
    private Enemy currentEnemy;

    [Header("Current Number")]
    public float numberSpeed = .25F;
    private float numberTimer = 0F;

    private void Awake()
    {
        CurrentNumber = 1;
    }

    private void Update()
    {
        if (!currentEnemy)
        {
            enemyTimer += Time.deltaTime;
            if (enemyTimer >= enemyWait)
            {
                enemyTimer = 0;
                currentEnemy = Instantiate(enemyTemplate).GetComponent<Enemy>();
            }
        }

        if (Input.GetButton("Fire1"))
        {
            numberTimer += Time.deltaTime;
            if (numberTimer >= numberSpeed)
            {
                numberTimer = 0;
                CurrentNumber = Mathf.Clamp(CurrentNumber + 1, 1, 10);
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            weapon.Shoot(CurrentNumber, CurrentOperator);
            CurrentNumber = 1;
            numberTimer = 0;
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            CurrentOperator = CurrentOperator + 1;
            if (CurrentOperator > Operator.DIVIDE)
                CurrentOperator = 0;
        }

    }

    public enum Operator
    {
        ADD,
        SUBTRACT,
        MULTIPLY,
        DIVIDE
    }
}
