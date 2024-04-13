using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    [SerializeField]
    private Rect touchArea;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private bool isDebug = false;

    [SerializeField]
    private Color rectColor = Color.green;

    public bool TouchDetected { get; private set; } = false;

    private void Update()
    {
        TouchDetected = GetScreenTouch();
    }

    public bool GetScreenTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            if (touchArea.Contains(new Vector2(touchPosition.x, touchPosition.y)))
            {
                //Debug.Log("Touch in touch area!");
                return true;
            }
            else
                return false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 clockPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (touchArea.Contains(clockPosition))
            {
                //Debug.Log("Click in touch area!");
                return true;
            }
            else
                return false;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (isDebug)
        {
            Gizmos.color = rectColor;
            Gizmos.DrawWireCube(touchArea.center, touchArea.size);
        }
    }

}