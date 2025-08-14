using UnityEngine;

public class LookForCursorController : MonoBehaviour
{
    [SerializeField] private float maxXCursorDistance;
    [SerializeField] private float maxYCursorDistance;
    [SerializeField] private float maxXPositionDistance;
    [SerializeField] private float maxYPositionDistance;

    void Update()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
        transform.localPosition = cursorWorldPosition;
    }
}
