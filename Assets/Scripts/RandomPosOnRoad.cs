using UnityEngine;

public class RandomPosOnRoad : MonoBehaviour
{
    public Camera playerCamera;
    void Update()
    {
        if (playerCamera.transform.position.z > transform.position.z + 20)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.position += new Vector3(0,0, Random.Range(30, 12 * 6));
            var newPos = transform.position;
            newPos.x = Random.Range(10, 15);
            transform.position = newPos;
        }
    }
}
