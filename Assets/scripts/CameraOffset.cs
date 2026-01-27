using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 1.5f, -3f);

    void LateUpdate() 
    { 
        transform.localPosition = offset; 
    }
}
