using UnityEngine;

public class FPS : MonoBehaviour
{
    public void Awake()
    {
        Application.targetFrameRate = 60;
    }
}

