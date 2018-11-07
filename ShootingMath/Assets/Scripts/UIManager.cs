using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameLogic logic;
    public Selectable[] operations = { };

    public Slider slider;
    public TextMeshProUGUI calc;
    public TextMeshProUGUI time;

    public TextMeshProUGUI level;
    public TextMeshProUGUI score;

    private string levelFormat;
    private string scoreFormat;

    private readonly BaseEventData ev = new BaseEventData(null);

    private GameLogic.Operator cachedOperator = (GameLogic.Operator)int.MinValue;

    private void Awake()
    {
        levelFormat = level.text;
        scoreFormat = score.text;
    }

    private void Update()
    {
        GameLogic.Operator op = logic.CurrentOperator;
        
        if(op != cachedOperator)
        {
            cachedOperator = op;
            for (int i = 0; i < operations.Length; i++)
            {
                if ((int)op == i)
                    operations[i].OnSelect(ev);
                else
                    operations[i].OnDeselect(ev);
            }
        }

        string num = "0";

        if (logic.HasEnemy)
            num = logic.EnemyNumber.ToString();

        calc.text = "Calc: " + num;

        slider.value = logic.CurrentNumber;

        if (logic.HasEnemy)
            time.text = Mathf.CeilToInt(logic.MaxAliveTime - logic.CurrentAliveTime).ToString();
        else
            time.text = "No Enemy";

        level.text = string.Format(levelFormat, logic.Level);
        score.text = string.Format(scoreFormat, logic.Score, logic.AddedScore);
    }

    private void OnValidate()
    {
        if (operations.Length != System.Enum.GetValues(typeof(GameLogic.Operator)).Length)
            System.Array.Resize(ref operations, System.Enum.GetValues(typeof(GameLogic.Operator)).Length);
    }
}
