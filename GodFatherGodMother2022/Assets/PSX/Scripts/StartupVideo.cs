using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartupVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string firstScene;

    private void Awake() => videoPlayer.loopPointReached += _ => SceneManager.LoadScene(firstScene);

    private void Update()
    {
        if (Input.GetButtonDown("Skip")) SceneManager.LoadScene(firstScene);
    }
}