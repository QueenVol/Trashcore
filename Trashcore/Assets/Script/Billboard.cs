using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        Vector3 camDir = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(camDir);
    }
}
