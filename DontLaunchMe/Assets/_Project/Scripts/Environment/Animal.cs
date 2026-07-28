using UnityEngine;

public class Animal : MonoBehaviour
{
    public string animalName = "Animal";
    public Color freedColor = Color.green;

    private bool _isFreed = false;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isFreed)
        {
            FreeAnimal();
        }
    }

    private void FreeAnimal()
    {
        _isFreed = true;

        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = freedColor;
        }

        // نطفي الـ Collider عشان اللاعب يقدر يمر بدون تكرار
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Debug.Log(animalName + " has been freed!");
        // ⚠️ لا Destroy! الحيوان يبقى موجود عشان GameManager يعده
    }

    public bool IsFreed()
    {
        return _isFreed;
    }
}