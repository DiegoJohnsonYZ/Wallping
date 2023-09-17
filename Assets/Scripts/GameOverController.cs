using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public TMP_Text scoreFinal;

    private void Start()
    {
        scoreFinal.text = PlayerPrefs.GetInt("score").ToString();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);
            if (firstTouch.phase == TouchPhase.Began)
            {

                StartCoroutine(LoadGameScene());
            }
        }
    }

    private IEnumerator LoadGameScene()
    {
        var asyncOperation = SceneManager.LoadSceneAsync("MainGame");
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
    }
}
