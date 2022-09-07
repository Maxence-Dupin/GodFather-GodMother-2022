using UnityEngine;

public class Framerate : MonoBehaviour
{
    [SerializeField] private int maxFps;

    private void Awake() => Application.targetFrameRate = maxFps;
}