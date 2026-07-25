using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // مرجع للـ Panels
    public GameObject winPanel;
    public GameObject losePanel;

    // هل انتهت اللعبة؟
    private bool _isGameOver = false;

    void Start()
    {
        // نتأكد إن الـ Panels مقفولة في البداية
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    // بتتنفذ لما اللاعب يكسب
    public void PlayerWins()
    {
        if (_isGameOver) return;

        _isGameOver = true;
        winPanel.SetActive(true);

        // نوقف اللعبة
        Time.timeScale = 0f;

        Debug.Log("Player wins!");
    }

    // بتتنفذ لما اللاعب يخسر
    public void PlayerLoses()
    {
        if (_isGameOver) return;

        _isGameOver = true;
        losePanel.SetActive(true);

        // نوقف اللعبة
        Time.timeScale = 0f;

        Debug.Log("Player loses!");
    }

    // بتتنفذ لما اللاعب يدوس Restart
    public void RestartGame()
    {
        // نرجع الوقت لسرعته الطبيعية
        Time.timeScale = 1f;

        // نعيد تحميل الـ scene الحالية
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
