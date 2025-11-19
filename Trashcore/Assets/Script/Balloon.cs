using UnityEngine;

public class Balloon : MonoBehaviour
{
    public string itemName = "Jet Pack";
    public float flyDuration = 3f;
    public float flyForce = 6f;

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
        rend.materials = state ? matsHighlighted : matsNormal;
    }

    public void OnPick(Flight player)
    {
        player.ActivateFlight(flyDuration, flyForce);
        Destroy(gameObject);
    }
}
