using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject mainMenuContainer;
    [SerializeField] private GameObject creditsContainer;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        creditsButton.onClick.AddListener(ShowCredits);
        backButton.onClick.AddListener(ShowMainMenu);
    }

    private void StartGame()
    {
        StartCoroutine(LoadGameScene());
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

    private void ShowCredits()
    {
        mainMenuContainer.SetActive(false);
        creditsContainer.SetActive(true);
    }

    private void ShowMainMenu()
    {
        mainMenuContainer.SetActive(true);
        creditsContainer.SetActive(false);
    }
}