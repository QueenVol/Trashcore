using UnityEngine;

public class TrashInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask trashLayer;

    private Trash currentTrash;

    void Update()
    {
        DetectTrash();

        if (currentTrash != null && Input.GetKeyDown(KeyCode.F))
        {
            TrashManager.Instance.Collect(currentTrash.itemName);
            currentTrash.OnPick();
            currentTrash = null;
        }
    }

    void DetectTrash()
    {
        if (currentTrash != null)
        {
            currentTrash.SetHighlight(false);
            currentTrash = null;
        }

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, trashLayer))
        {
            Trash trash = hit.collider.GetComponent<Trash>();
            if (trash != null)
            {
                currentTrash = trash;
                currentTrash.SetHighlight(true);
            }
        }
    }
}
