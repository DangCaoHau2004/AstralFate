using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera mainCamera;
    public float dragSpeed = 1f;
    private Vector3 lastTouchPos;

    void Start()
    {

        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && mainCamera != null)
        {
            mainCamera.enabled = !mainCamera.enabled;
            Time.timeScale = mainCamera.enabled ? 1f : 0f;

        }

        if (mainCamera != null && !mainCamera.enabled)
        {
            if (Input.GetMouseButtonDown(1))
            {
                lastTouchPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(1))
            {
                Vector3 touchDelta = new Vector3(
                    Input.mousePosition.x - lastTouchPos.x,
                    Input.mousePosition.y - lastTouchPos.y,
                    0f
                );

                Vector3 move = touchDelta * dragSpeed * Time.unscaledDeltaTime;
                transform.position -= move;

                lastTouchPos = Input.mousePosition;
            }

        }
    }
}
