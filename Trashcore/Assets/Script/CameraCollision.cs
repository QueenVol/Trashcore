using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target;
    public float maxDistance = 3f;
    public float minDistance = 0.2f;
    public float smooth = 10f;
    public float sphereRadius = 0.3f;
    public LayerMask collisionMask;

    private float currentDistance;

    void Start()
    {
        currentDistance = maxDistance;
    }

    void LateUpdate()
    {
        Vector3 desiredPos = target.position - target.forward * maxDistance;

        Vector3 direction = (desiredPos - target.position).normalized;

        if (Physics.SphereCast(target.position, sphereRadius, direction, out RaycastHit hit, maxDistance, collisionMask))
        {
            currentDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, maxDistance, Time.deltaTime * smooth);
        }

        transform.position = target.position + direction * currentDistance;

        transform.LookAt(target);
    }
}
