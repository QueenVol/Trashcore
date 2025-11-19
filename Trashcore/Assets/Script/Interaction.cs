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
    private Balloon currentBalloon;

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

        bool hitSomething = Physics.SphereCast(ray, interactRadius, out hit, interactDistance, interactLayer);

        if (!hitSomething)
        {
            ClearCurrentTarget();
            return;
        }

        Trash trash = hit.collider.GetComponent<Trash>();
        Balloon balloon = hit.collider.GetComponent<Balloon>();

        if (trash == null && balloon == null)
        {
            ClearCurrentTarget();
            return;
        }

        if (trash != null && trash != currentTrash)
        {
            ClearCurrentTarget();

            currentTrash = trash;
            currentTrash.SetHighlight(true);

            currentUI = Instantiate(interactUIPrefab);
            currentUIText = currentUI.GetComponentInChildren<TextMeshProUGUI>();
        }

        if (balloon != null && balloon != currentBalloon)
        {
            ClearCurrentTarget();

            currentBalloon = balloon;
            currentBalloon.SetHighlight(true);

            currentUI = Instantiate(interactUIPrefab);
            currentUIText = currentUI.GetComponentInChildren<TextMeshProUGUI>();
        }

        if (currentTrash != null)
            currentUIText.text = "Press F to Pick Up: " + currentTrash.itemName;

        if (currentBalloon != null)
            currentUIText.text = "Press F to Pick Up: " + currentBalloon.itemName;

        currentUI.SetActive(true);
    }

    void ClearCurrentTarget()
    {
        if (currentTrash != null)
        {
            currentTrash.SetHighlight(false);
            currentTrash = null;
        }

        if (currentBalloon != null)
        {
            currentBalloon.SetHighlight(false);
            currentBalloon = null;
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
        if (currentUI == null) return;

        Transform target = null;

        if (currentTrash != null)
            target = currentTrash.transform;
        else if (currentBalloon != null)
            target = currentBalloon.transform;

        if (target == null) return;

        Vector3 worldPos = target.position + Vector3.up * 0.5f;
        currentUI.transform.position = worldPos;
    }

    void TryInteract()
    {
        if (currentTrash != null)
        {
            currentTrash.OnPick();
            ClearCurrentTarget();
            return;
        }

        if (currentBalloon != null)
        {
            var playerFly = GetComponent<Flight>();
            currentBalloon.OnPick(playerFly);
            ClearCurrentTarget();
        }
    }
}
