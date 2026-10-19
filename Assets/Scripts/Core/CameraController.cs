using UnityEngine;

[DefaultExecutionOrder(-1)]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField]
    float moveSpeed = 5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Move(Vector2 dir)
    {
        Quaternion rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        Vector3 movement = rotation * new Vector3(dir.x, 0, dir.y);

        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    public void Rotate(float rotate)
    {
        // Implement camera rotation logic here
    }

    public void Zoom(float zoom)
    {
        // Implement camera zoom logic here
    }
}