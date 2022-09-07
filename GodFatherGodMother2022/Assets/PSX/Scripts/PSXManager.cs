using UnityEngine;

public class PSXManager : MonoBehaviour
{
    [SerializeField] private int maxFps;

    private void Awake() => Application.targetFrameRate = maxFps;
}