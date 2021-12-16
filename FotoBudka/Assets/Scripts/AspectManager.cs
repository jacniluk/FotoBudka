using UnityEngine;

public class AspectManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float minFov;
    [SerializeField] private float maxFov;

    [Header("References")]
    [SerializeField] private Transform model; // temp

    private Vector3 lastMousePosition;

    private void Update()
    {
        UpdateRotation();
        UpdateZoom();
    }

    private void UpdateRotation()
    {
        if (Input.GetMouseButton(0))
        {
            float x = Input.mousePosition.x - lastMousePosition.x;
            float y = Input.mousePosition.y - lastMousePosition.y;

            model.Rotate(new Vector3(-y, x, 0.0f));
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
}
