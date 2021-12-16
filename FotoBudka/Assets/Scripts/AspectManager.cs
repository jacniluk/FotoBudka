using UnityEngine;

public class AspectManager : MonoBehaviour
{
    [Header("Settings")]
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
        UpdateRotation();
        UpdateZoom();
    }

    private void UpdateRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            wasClickedOnUi = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }

        if (Input.GetMouseButton(0) && wasClickedOnUi == false)
        {
            mouseOffset = Input.mousePosition - lastMousePosition;
            float angle = Vector3.Dot(mouseOffset, Camera.main.transform.right);
            if (Vector3.Dot(modelTransform.up, Vector3.up) >= 0)
            {
                angle = -angle;
            }
            modelTransform.Rotate(modelTransform.up, angle, Space.World);
            modelTransform.Rotate(Camera.main.transform.right, Vector3.Dot(mouseOffset, Camera.main.transform.up), Space.World);
        }

        lastMousePosition = Input.mousePosition;
    }

    private void UpdateZoom()
    {
        if (Input.mouseScrollDelta.y != 0.0f)
        {
            Camera.main.transform.position = new Vector3(0.0f, 0.0f, Mathf.Clamp(Camera.main.transform.position.z + Input.mouseScrollDelta.y * zoomStep, minZoom, maxZoom));
        }
    }

    public void SetModelTransform(Transform _modelTransform)
    {
        modelTransform = _modelTransform;
    }
}
