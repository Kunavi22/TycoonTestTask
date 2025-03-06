using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float xMin = -25;
    private float xMax = 75;

    private float zMin = -25;
    private float zMax = 25;


    [SerializeField]
    float movementSpeed;

    void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveVector = moveDir * movementSpeed * Time.deltaTime;

        Vector3 nextPos = transform.position + moveVector;

        nextPos.x = Mathf.Clamp(nextPos.x, xMin, xMax);
        nextPos.z = Mathf.Clamp(nextPos.z, zMin, zMax);

        transform.position = nextPos;

    }

    public void UnlockNewChunk(Vector3 chunkPos)
    {
        xMin = Mathf.Min(xMin, chunkPos.x - 25);
        zMin = Mathf.Min(zMin, chunkPos.z - 25);

        xMax = Mathf.Max(xMax, chunkPos.x + 75);
        zMax = Mathf.Max(zMax, chunkPos.z + 25);
    }
}
