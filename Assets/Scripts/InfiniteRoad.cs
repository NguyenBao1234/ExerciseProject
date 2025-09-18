using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{

    public Camera playerCamera;
    void Update()
    {
        if (playerCamera.transform.position.z > transform.position.z + 20)
        {
            transform.position += new Vector3(0,0, 12 * 4);
        }
    }
}
