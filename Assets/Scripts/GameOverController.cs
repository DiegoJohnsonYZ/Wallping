using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        var asyncOperation = SceneManager.LoadSceneAsync("Test_SampleScene");
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
    }
}
