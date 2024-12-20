using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseManager : MonoBehaviour
{
    public static pauseManager instance;

    public bool isPaused = false;
    public GameObject pauseButton;
    public playerMovement player;
    public GameObject pauseMenu;
    public GameObject recipePanel;
    public GameObject smallRecipeSet;
    public GameObject fullRecipeSet;

    public bool canPause = true; // disable pause during rhythm events

    // Start is called before the first frame update
    void Awake() {
        instance = this;
    }
    void Start()
    {
        if (LevelLoader.instance.currentLevel == 0) {
            pauseButton.SetActive(false);
            recipePanel.SetActive(false);
        } else {
            recipePanel.SetActive(true);
            if (LevelLoader.instance.currentLevel == 1) {
                smallRecipeSet.SetActive(true);
                fullRecipeSet.SetActive(false);
            } else {
                smallRecipeSet.SetActive(false);
                fullRecipeSet.SetActive(true);
            }
            Time.timeScale = 0f;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (LevelLoader.instance.currentLevel == 0) {
                return;
            } else {
                if (!isPaused) {
                    Pause();
                } else {
                    Unpause();                
                }
            }
        }
    }

    public void Pause() {
        if (!canPause) {
            return;
        }

        isPaused = true;
        Time.timeScale = 0f;
        player.FreezePlayer();
        LevelManager.instance.freezePatience.Invoke();
        pauseMenu.SetActive(true);
    }

    public void Unpause() {
        isPaused = false;
        player.UnfreezePlayer();
        Time.timeScale = 1f;
        LevelManager.instance.unfreezePatience.Invoke();
        pauseMenu.SetActive(false);
    }

    public void CloseRecipePanel() {
        if (pauseMenu.activeSelf) {
            recipePanel.SetActive(false);
        } else {
            Time.timeScale = 1f;
            recipePanel.GetComponent<uiTransitions>().UIFadeOut();
        }
    }

    public void OpenRecipePanel() {
        recipePanel.SetActive(true);
        recipePanel.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void EnablePause() {
        canPause = true;
    }

    public void DisablePause() {
        canPause = false;
    }
}
