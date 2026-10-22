using UnityEngine;

[DefaultExecutionOrder(-1)]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float rotateSpeed = 30f;

    [SerializeField]
    float zoomSpeed = 5f;

    [SerializeField]
    int minZoom = 5;
    [SerializeField]
    int maxZoom = 20;

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

        transform.position += movement * moveSpeed * Time.unscaledDeltaTime;
    }

    public void Rotate(float rotate)
    {
        transform.Rotate(
            Vector3.up,
            rotate * rotateSpeed * Time.unscaledDeltaTime,
            Space.World
        );
    }

    public void Zoom(float zoom)
    {
        Vector3 position = transform.position;
        position += transform.forward * zoom * zoomSpeed;
        position.y = Mathf.Clamp(position.y, minZoom, maxZoom);
        transform.position = position;
    }
}