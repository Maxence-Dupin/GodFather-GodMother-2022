using UnityEngine;

public class EditorOnlyGO : MonoBehaviour
{
#if !UNITY_EDITOR
    private void Awake() => Destroy(gameObject);
#endif
}