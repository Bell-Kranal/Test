using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private int _scoreCount;
    
    private void Start()
    {
        SetScoreText(0);
    }

    public void AddMoney()
    {
        SetScoreText(_scoreCount + 1);
    }

    private void SetScoreText(int newScoreValue)
    {
        _scoreCount = newScoreValue;
        _scoreText.text = _scoreCount.ToString();
    }
}
