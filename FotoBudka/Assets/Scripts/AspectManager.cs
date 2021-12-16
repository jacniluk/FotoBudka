using UnityEngine;

public class AspectManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform model; // temp

    private Vector3 lastMousePosition;

    private void Update()
    {
        UpdateRotation();
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
}
