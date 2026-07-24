using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 60f;

    // مرجع للـ Text UI عشان نغير الرقم
    public TextMeshProUGUI timerText;

    // اللون لما الوقت يقل عن 10 ثواني
    public Color warningColor = Color.red;

    // الوقت الحالي
    private float _timeLeft;

    // هل انتهى الوقت؟
    private bool _isTimeUp = false;

    void Start()
    {
        _timeLeft = startTime;
    }

    void Update()
    {
        // لو الوقت انتهى، بلاش نكمل
        if (_isTimeUp) return;

        // نقلل الوقت كل frame
        _timeLeft -= Time.deltaTime;

        // نعرض الرقم على الشاشة (نقربه لأقرب عدد صحيح)
        timerText.text = Mathf.CeilToInt(_timeLeft).ToString();

        // لو الوقت أقل من 10، اللون يبقى أحمر
        if (_timeLeft <= 10f)
        {
            timerText.color = warningColor;
        }

        // لو الوقت خلص
        if (_timeLeft <= 0f)
        {
            _timeLeft = 0f;
            _isTimeUp = true;
            OnTimeUp();
        }
    }

    // بتتنفذ لما الوقت يخلص
    private void OnTimeUp()
    {
        Debug.Log("Time's up! Rocket launched!");
        // بعدين هنضيف هنا Game Over screen
    }
}
