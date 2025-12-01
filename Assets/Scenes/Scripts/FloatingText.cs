using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshPro textMesh;
    public float floatSpeed = 2f;
    public float fadeDuration = 1f;

    private Color originalColor;
    private Camera mainCamera;

    private void Awake()
    {
        if (textMesh == null)
            textMesh = GetComponent<TextMeshPro>();

        originalColor = textMesh.color;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Move upwards
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Face the camera
        if (mainCamera != null)
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

        // Fade out
        float alpha = Mathf.Lerp(originalColor.a, 0, Time.deltaTime / fadeDuration);
        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // Destroy when fully invisible
        if (textMesh.color.a <= 0.05f)
            Destroy(gameObject);
    }

    // Call this to set the text and color dynamically
    public void Setup(string message, Color color)
    {
        textMesh.text = message;
        textMesh.color = color;
        originalColor = color;
    }
}