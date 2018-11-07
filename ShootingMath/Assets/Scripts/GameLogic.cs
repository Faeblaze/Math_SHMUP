using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public const float EULER = 2.718281828459045235360287471352F;

    public GameObject enemyTemplate;
    public Player player;
    public Weapon weapon;

    public float startAliveTime = 20F;
    public float enemyWait = 2F;

    [Header("Current Number")]
    public float numberSpeed = .25F;

    private float numberTimer = 0F;
    private float enemyTimer = 0F;
    private Enemy currentEnemy;
    private int shotCounter;

    public float MaxAliveTime
    {
        get
        {
            float mat = startAliveTime + 5 * Mathf.Clamp(Level - 1, 0, 4);
            if (Level > 5)
                mat += 15 * (Level - 5);

            return mat;
        }
    }
    public int Level { get; private set; }
    public int Score { get; private set; }
    public float CurrentAliveTime { get; private set; }
    public Operator CurrentOperator { get; private set; }
    public int CurrentNumber { get; private set; }
    public bool HasEnemy
    {
        get
        {
            return currentEnemy;
        }
    }
    public int EnemyNumber
    {
        get
        {
            return currentEnemy.ReachedNumber;
        }
    }
    public int AddedScore
    {
        get
        {
            return Mathf.Max(0, 50 * Level - 2 * shotCounter);
        }
    }

    private void Awake()
    {
        CurrentNumber = 1;
        Level = 1;
    }

    private void Update()
    {
        if (!currentEnemy)
        {
            CurrentAliveTime = 0F;
            enemyTimer += Time.deltaTime;

            if (enemyTimer >= enemyWait)
            {
                enemyTimer = 0;
                currentEnemy = Instantiate(enemyTemplate).GetComponent<Enemy>();
                currentEnemy.logic = this;

                currentEnemy.speed += .1F * Level;
                currentEnemy.range = new Vector2Int(currentEnemy.range.x + Mathf.RoundToInt(Mathf.Pow(EULER, Level)), currentEnemy.range.y + Mathf.RoundToInt(Mathf.Pow(EULER, Level)));

                if (Level <= 7)
                    currentEnemy.transform.localScale = Vector3.one * Mathf.Lerp(1F, .25F, Level / 7F);
                else 
                    currentEnemy.transform.localScale = Vector3.one * Mathf.Lerp(.6F, 1.25F, (Level - 7) / 3F);

                Debug.LogFormat("I am a {0} at level {1}", currentEnemy.range, Level);
            }
        } else
        {
            CurrentAliveTime += Time.deltaTime;

            if(CurrentAliveTime >= MaxAliveTime)
                player.OnDie();
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
            shotCounter++;
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            CurrentOperator = CurrentOperator + 1;
            if (CurrentOperator > Operator.DIVIDE)
                CurrentOperator = 0;
        }

    }

    public void OnEnemyDeath ()
    {
        Score += 50 + AddedScore;
        shotCounter = 0;
        Level++;
    }

    public enum Operator
    {
        ADD,
        SUBTRACT,
        MULTIPLY,
        DIVIDE
    }
}
