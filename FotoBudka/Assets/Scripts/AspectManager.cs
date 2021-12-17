using UnityEngine;

public class AspectManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomStep;

    public static AspectManager Instance;

    private bool wasClickedOnUi;
    private Vector3 lastMousePosition;
    private Vector3 mouseOffset;

    private Transform modelTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateRotationAndPosition();
        UpdateZoom();
    }

    private void UpdateRotationAndPosition()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            wasClickedOnUi = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }

        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && wasClickedOnUi == false)
        {
            mouseOffset = Input.mousePosition - lastMousePosition;
            if (Input.GetMouseButton(0))
            {
                UpdateRotation();
            }
            else
            {
                UpdatePosition();
            }
        }

        lastMousePosition = Input.mousePosition;
    }

    private void UpdateRotation()
    {
        float angle = Vector3.Dot(mouseOffset, Camera.main.transform.right);
        if (Vector3.Dot(modelTransform.up, Vector3.up) >= 0)
        {
            angle = -angle;
        }
        modelTransform.Rotate(modelTransform.up, angle, Space.World);
        modelTransform.Rotate(Camera.main.transform.right, Vector3.Dot(mouseOffset, Camera.main.transform.up), Space.World);
    }

    private void UpdatePosition()
    {
        Camera.main.transform.position -= mouseOffset * moveSpeed;
    }

    private void UpdateZoom()
    {
        if (Input.mouseScrollDelta.y != 0.0f)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,
                Mathf.Clamp(Camera.main.transform.position.z + Input.mouseScrollDelta.y * zoomStep, minZoom, maxZoom));
        }
    }

    public void SetModelTransform(Transform _modelTransform)
    {
        modelTransform = _modelTransform;
    }
}
