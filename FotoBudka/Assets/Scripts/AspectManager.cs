using UnityEngine;

public class AspectManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float minFov;
    [SerializeField] private float maxFov;

    public static AspectManager Instance;

    private bool wasClickedOnUi;
    private Vector3 lastMousePosition;
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
            float x = Input.mousePosition.x - lastMousePosition.x;
            float y = Input.mousePosition.y - lastMousePosition.y;

            modelTransform.Rotate(new Vector3(-y, x, 0.0f));
        }

        lastMousePosition = Input.mousePosition;
    }

    private void UpdateZoom()
    {
        if (Input.mouseScrollDelta.y != 0.0f)
        {
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - Input.mouseScrollDelta.y, minFov, maxFov);
        }
    }

    public void SetModelTransform(Transform _modelTransform)
    {
        modelTransform = _modelTransform;
    }
}
