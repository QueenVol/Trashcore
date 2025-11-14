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
    public GameObject interactUIPrefab;
    private GameObject currentUI;
    private TextMeshProUGUI currentUIText;

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
    }

    void Update()
    {
        DetectObject();
        UpdateUIPosition();
    }

    void DetectObject()
    {
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
            ClearCurrentTarget();
            return;
        }

        Trash trash = hit.collider.GetComponent<Trash>();

        if (trash == null)
        {
            ClearCurrentTarget();
            return;
        }

        if (trash != currentTrash)
        {
            ClearCurrentTarget();

            currentTrash = trash;
            currentTrash.SetHighlight(true);

            currentUI = Instantiate(interactUIPrefab);
            currentUIText = currentUI.GetComponentInChildren<TextMeshProUGUI>();
        }

        currentUIText.text = "Press F to Pick Up: " + currentTrash.itemName;
        currentUI.SetActive(true);
    }

    void ClearCurrentTarget()
    {
        if (currentTrash != null)
        {
            currentTrash.SetHighlight(false);
            currentTrash = null;
        }

        if (currentUI != null)
        {
            Destroy(currentUI);
            currentUI = null;
            currentUIText = null;
        }
    }

    void UpdateUIPosition()
    {
        if (currentUI == null || currentTrash == null) return;

        Vector3 worldPos = currentTrash.transform.position + Vector3.up * 0.5f;

        currentUI.transform.position = worldPos;
    }

    void TryInteract()
    {
        if (currentTrash == null) return;

        currentTrash.OnPick();
        ClearCurrentTarget();
    }
}
