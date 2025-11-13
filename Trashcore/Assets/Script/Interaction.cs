using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Interaction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactDistance = 3f;
    public float interactRadius = 0.4f;
    public LayerMask interactLayer;

    [Header("UI")]
    public TextMeshProUGUI interactText;

    private Camera cam;
    private PlayerInputActions input;

    private Trash currentTrash;

    void Awake()
    {
        input = new PlayerInputActions();
        input.Player.Interact.performed += ctx => TryInteract();
    }

    void OnEnable() => input.Player.Enable();
    void OnDisable() => input.Player.Disable();

    void Start()
    {
        cam = Camera.main;
        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        DetectObject();
    }

    void DetectObject()
    {
        if (currentTrash != null)
        {
            currentTrash.SetHighlight(false);
            currentTrash = null;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        bool hitSomething = Physics.SphereCast(
            ray,
            interactRadius,
            out hit,
            interactDistance,
            interactLayer
        );

        if (!hitSomething)
        {
            interactText.gameObject.SetActive(false);
            return;
        }

        Trash trash = hit.collider.GetComponent<Trash>();

        if (trash != null)
        {
            trash.SetHighlight(true);
            currentTrash = trash;

            interactText.text = "Press F to Pick Up: " + trash.itemName;
            interactText.gameObject.SetActive(true);
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

    void TryInteract()
    {
        if (currentTrash == null) return;

        currentTrash.OnPick();
        interactText.gameObject.SetActive(false);
        currentTrash = null;
    }
}
