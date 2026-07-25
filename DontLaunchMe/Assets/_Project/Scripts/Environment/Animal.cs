using UnityEngine;

public class Animal : MonoBehaviour
{
    // اسم الحيوان (يظهر في Console)
    public string animalName = "Animal";

    // اللون بعد التحرير
    public Color freedColor = Color.green;

    // هل الحيوان اتحرر؟
    private bool _isFreed = false;

    // مرجع للاعب لما يكون قريب
    private bool _playerIsNear = false;

    // مرجع لـ SpriteRenderer عشان نغير اللون
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // لو اللاعب قريب وضغط E
        if (_playerIsNear && !_isFreed && Input.GetKeyDown(KeyCode.E))
        {
            FreeAnimal();
        }
    }

    // اللاعب دخل منطقة الحيوان
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = true;
        }
    }

    // اللاعب خرج من منطقة الحيوان
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = false;
        }
    }

    // تحرير الحيوان
    private void FreeAnimal()
    {
        _isFreed = true;

        // نغير لونه
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = freedColor;
        }

        Debug.Log(animalName + " has been freed!");
    }

    // للاستخدام في GameManager بعدين
    public bool IsFreed()
    {
        return _isFreed;
    }
}