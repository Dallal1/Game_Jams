using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // مراجع للـ Panels
    public GameObject fullSuccessPanel;
    public GameObject partialSuccessPanel;
    public GameObject losePanel;

    // نص لعرض عدد الحيوانات في partial success
    public TMPro.TextMeshProUGUI partialSuccessText;

    // هل انتهت اللعبة؟
    private bool _isGameOver = false;

    void Start()
    {
        // نتأكد إن كل الـ Panels مقفولة في البداية
        if (fullSuccessPanel != null) fullSuccessPanel.SetActive(false);
        if (partialSuccessPanel != null) partialSuccessPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    // بتتنفذ لما اللاعب يوصل الأرض
    public void PlayerWins()
    {
        if (_isGameOver) return;
        _isGameOver = true;

        // نعد الحيوانات المحررة
        int freedAnimals = CountFreedAnimals();
        int totalAnimals = CountTotalAnimals();

        // نقرر النهاية بناءً على العدد
        if (freedAnimals == totalAnimals)
        {
            // كل الحيوانات اتحررت
            fullSuccessPanel.SetActive(true);
            Debug.Log("Full Success! All animals saved!");
        }
        else
        {
            // بعض الحيوانات فقط
            partialSuccessPanel.SetActive(true);

            // نعرض العدد
            if (partialSuccessText != null)
            {
                partialSuccessText.text = "You saved " + freedAnimals + " of " + totalAnimals + " animals!";
            }

            Debug.Log("Partial Success! Saved " + freedAnimals + "/" + totalAnimals);
        }

        // نوقف اللعبة
        Time.timeScale = 0f;
    }

    // بتتنفذ لما العداد يخلص
    public void PlayerLoses()
    {
        if (_isGameOver) return;
        _isGameOver = true;

        losePanel.SetActive(true);
        Time.timeScale = 0f;

        Debug.Log("Player loses!");
    }

    // بتتنفذ لما اللاعب يدوس Restart
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // نعد الحيوانات المحررة
    private int CountFreedAnimals()
    {
        Animal[] allAnimals = FindObjectsByType<Animal>(FindObjectsSortMode.None);
        int count = 0;

        foreach (Animal animal in allAnimals)
        {
            if (animal.IsFreed()) count++;
        }

        return count;
    }

    // نعد كل الحيوانات في المستوى
    private int CountTotalAnimals()
    {
        Animal[] allAnimals = FindObjectsByType<Animal>(FindObjectsSortMode.None);
        return allAnimals.Length;
    }
}