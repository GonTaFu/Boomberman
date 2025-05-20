using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinnerUI;
    [SerializeField] private GameObject levelLoadUI;
    [SerializeField] private GameObject UIMoblie;
    [SerializeField] private Button buttonPlaceBomb;
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private GameObject player;
    void Start()
    {
        // #if UNITY_ANDROID || UNITY_IOS
        //         UIMoblie.SetActive(true);
        //         Debug.Log("Mobile UI is active");
        // #else
        //     UIMoblie.SetActive(false);
        //     Debug.Log("Mobile UI is inactive");
        // #endif
        if (IsMobilePlatform())
        {
            UIMoblie.SetActive(true);
        }
        else
        {
            UIMoblie.SetActive(false);
        }

        StartCoroutine(LoadLevelLoadingUI());
        LoadLevel();
    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void OnBackMenuButtonClicked()
    {
        SceneManager.LoadScene(0); // Load the menu scene (index 0)
    }

    public void OnRestartButtonClicked()
    {
        GameManager.Instance.ClearLevel();
        SceneManager.LoadScene(1); // Load the main scene (index 1)
    }

    public void OnNetxLevelButtonClicked()
    {
        GameManager.Instance.LoadLevel(); // Load the next level
    }

    private void LoadLevel()
    {
        if (GameManager.Instance == null)
        {
            gameOverUI.SetActive(true);
            Debug.Log("GameManager instance is null. Make sure GameManager is initialized.");
            return;
        }

        GameManager.Instance.ClearLevel();

        GameManager.Instance.AssignTilemap(
            GameObject.Find("Destruction").GetComponent<Tilemap>(),
            GameObject.Find("Indestruction").GetComponent<Tilemap>(),
            GameObject.Find("Floor").GetComponent<Tilemap>()
        );

        GameManager.Instance.AssignGameUI(gameOverUI, gameWinnerUI);
        gameOverUI.SetActive(false);
        gameWinnerUI.SetActive(false);

        GameManager.Instance.AssignPlayer(player);

        buttonPlaceBomb.onClick.AddListener(() =>
        {
            player.GetComponent<BombController>().ButtonPlaceBomb();
        });

        joystickController.player = player.GetComponent<PlayerController>();

        if (IsMobilePlatform())
        {
            mainCamera.AddComponent<CameraFollow>().player = GameObject.FindGameObjectWithTag("Player");
            mainCamera.GetComponent<CameraFollow>().offset = new Vector3(0, 0, -20);
            mainCamera.GetComponent<CameraFollow>().smoothSpeed = 0.125f;
            mainCamera.GetComponent<CameraFollow>().enabled = true;
        }

        GameManager.Instance.LoadLevel();

    }

    IEnumerator LoadLevelLoadingUI()
    {
        levelLoadUI.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        levelLoadUI.SetActive(false);
    }

    bool IsMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android
            || Application.platform == RuntimePlatform.IPhonePlayer;
    }
}
