using UnityEngine;

public class Trash : MonoBehaviour
{
    public string itemName = "Trash";

    private Renderer rend;
    private Material baseMaterial;
    private Material outlineMaterial;

    private Material[] matsNormal;
    private Material[] matsHighlighted;

    void Start()
    {
        rend = GetComponent<Renderer>();

        baseMaterial = rend.materials[0];
        outlineMaterial = rend.materials[1];

        matsNormal = new Material[] { baseMaterial };
        matsHighlighted = new Material[] { baseMaterial, outlineMaterial };

        rend.materials = matsNormal;
    }

    public void SetHighlight(bool state)
    {
        if (state)
        {
            rend.materials = matsHighlighted;
        }
        else
        {
            rend.materials = matsNormal;
        }
    }

    public void OnPick()
    {
        Destroy(gameObject);
    }
}
