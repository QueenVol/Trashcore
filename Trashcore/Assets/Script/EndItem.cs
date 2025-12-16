using UnityEngine;

public class EndItem : MonoBehaviour
{
    private bool canInteract;

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

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            if (TrashManager.Instance.IsAllCollected())
            {
                Destroy(gameObject); // ≤‚ ‘”√
            }
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
