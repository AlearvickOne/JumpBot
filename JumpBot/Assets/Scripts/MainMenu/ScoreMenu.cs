using UnityEngine;
using TMPro;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] TMP_Text textMenuScore;
    void Start()
    {
        ScriptableParametrs.scoreStatic = PlayerPrefs.GetInt("ScorePoint");
        textMenuScore = GetComponent<TMP_Text>();
        textMenuScore.text = "Score: " + ScriptableParametrs.scoreStatic.ToString();
    }
}
