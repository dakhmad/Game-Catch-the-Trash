using UnityEngine;

public class MouseDragXZ : MonoBehaviour
{
    private float yPosLocked;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main;
        yPosLocked = transform.position.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;

                    Plane plane = new Plane(Vector3.up, new Vector3(0, yPosLocked, 0));
                    if (plane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        offset = transform.position - hitPoint;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Plane plane = new Plane(Vector3.up, new Vector3(0, yPosLocked, 0));
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 point = ray.GetPoint(enter);
                Vector3 targetPos = point + offset;
                transform.position = new Vector3(targetPos.x, yPosLocked, targetPos.z);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
