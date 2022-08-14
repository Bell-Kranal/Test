using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LooseOrWinUIController : MonoBehaviour
{
    [SerializeField] private GameObject _resultUI;
    [SerializeField] private TMP_Text _loseOrWinText;
    [SerializeField] private Image _backgroundColor;
    [SerializeField] private Color _backgroundColorWin;
    [SerializeField] private Color _backgroundColorLose;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        SetValuesAndActivePanel(_backgroundColorWin, "Win!");
    }

    public void Lose()
    {
        SetValuesAndActivePanel(_backgroundColorLose, "Lose!");
    }

    private void SetValuesAndActivePanel(Color backgroundColor, string text)
    {
        _backgroundColor.color = backgroundColor;
        _loseOrWinText.text = text;

        _resultUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
