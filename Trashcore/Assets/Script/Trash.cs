using UnityEngine;

public class Trash : MonoBehaviour
{
    public string itemName = "Trash";

    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void SetHighlight(bool state)
    {
        if (state)
            rend.material.color = highlightColor;
        else
            rend.material.color = originalColor;
    }

    public void OnPick()
    {
        Destroy(gameObject);
    }
}
