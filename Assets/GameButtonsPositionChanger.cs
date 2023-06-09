using UnityEngine;

public class GameButtonsPositionChanger : MonoBehaviour
{
    private const float CENTER_POSITION = 0;
    private const float START_POSITION = 222;
    private RectTransform _centerPoint;

    private void Start()
    {
        _centerPoint = GetComponent<RectTransform>();
    }

    public void SetCenterPosition()
    {
        _centerPoint.localPosition = Vector2.zero;
    }

    public void SetStartPosition()
    {
        _centerPoint.localPosition = Vector3.up * START_POSITION;
    }
}
