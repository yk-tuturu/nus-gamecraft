using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Static reference to the single instance
    public static LevelLoader instance { get; private set; }
    public int currentLevel = -1;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel(int level) {
        currentLevel = level;
        SceneManager.LoadScene("game");
    }

    public void LoadNextLevel() {
        if (currentLevel < 3) {
            currentLevel++;
            SceneManager.LoadScene("game");
        }
    }

    public void Reload() {
        SceneManager.LoadScene("game");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("titleScreen");
        currentLevel = -1;
    }
}
