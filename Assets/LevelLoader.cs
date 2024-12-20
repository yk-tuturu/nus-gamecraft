using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Static reference to the single instance
    public static LevelLoader instance { get; private set; }
    public int currentLevel = -1;
    public loadingBar bar;
    public GameObject loadingCanvas;

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Keep the instance across scenes
    }

    // Start is called before the first frame update
    void Start()
    {
        if (currentLevel == 1) {
            Debug.Log("hello from scene manager we are in level 1");
        }
        bar.SetFill(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel(int level) {
        currentLevel = level;
        StartCoroutine(LoadAsync("game"));
    }

    public void LoadNextLevel() {
        if (currentLevel < 3) {
            currentLevel++;
            StartCoroutine(LoadAsync("game"));
        }
    }

    public void Reload() {
        StartCoroutine(LoadAsync("game"));
    }

    public void LoadMenu() {
        StartCoroutine(LoadAsync("titleScreen"));
        currentLevel = -1;
    }

    IEnumerator LoadAsync(string name) {
        Time.timeScale = 0f; 
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        bar.SetFill(0f);
        loadingCanvas.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            bar.SetFill(progress);
            Debug.Log(progress);
            yield return null;
        }

        loadingCanvas.SetActive(false);
    }
}
