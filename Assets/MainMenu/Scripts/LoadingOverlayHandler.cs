using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading;

public class LoadingOverlayHandler : MonoBehaviour {
    private static bool isLoading;

    public static bool IsLoading
    {
        get { return isLoading; }
    }

    private static Canvas canvas;

	void Start () {
        canvas = GetComponent<Canvas>();
        isLoading = false;
	}

    private static void toggleCanvas(bool toggle)
    {
        canvas.enabled = toggle;
    }

    public static void LoadNewScene(int sceneIndex)
    {
        toggleCanvas(true);
        isLoading = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneIndex);
    }
}
