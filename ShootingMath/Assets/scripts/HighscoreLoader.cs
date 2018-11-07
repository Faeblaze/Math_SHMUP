using TMPro;
using UnityEngine;

public class HighscoreLoader : MonoBehaviour {

    private void Awake()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = string.Format(text.text, PlayerPrefs.GetInt("Highscore"));
    }
}
