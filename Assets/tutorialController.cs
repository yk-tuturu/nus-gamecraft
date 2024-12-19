using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class tutorialController : MonoBehaviour
{
    public static tutorialController instance;
    public GameObject targetMoneyPanel;
    public GameObject tutorialCanvas;
    public List<GameObject> panels = new List<GameObject>();

    public Table tutorialTable;
    public CoffeeStateManager coffee;
    public MilkStateManager milk;
    public CombineStateManager combine;
    public menuManager coffeeMenu;
    public combineMenuManager combineMenu;

    public RectTransform approachCircle;
    public Sequence circleSequence;
    public CanvasGroup score300;
    public CanvasGroup hitCircle;

    public GameObject rhythmEventPrefab;
    public rhythmEventManager currentRhythmEvent;
    public bool rhythmEnd = false;

    public UnityEvent freezePlayer;
    public UnityEvent unfreezePlayer;

    public int currentStep = 0;
    public bool inPanel = true;
    public bool waiting = false;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (LevelLoader.instance.currentLevel != 0) {
            Destroy(tutorialCanvas);
            Destroy(gameObject);
            return;
        }

        targetMoneyPanel.SetActive(false);
        panels[currentStep].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // any checks that need to happen when the panel is active
        if (inPanel) {
            switch (currentStep) {
                case 4: //check when player selects espresso
                    if (coffeeMenu.selected == 1) {
                        Debug.Log("selected espresso");
                        CloseOverlay();
                        NextPanel();
                    }
                    break;
                case 5: // check that go has been pressed and coffee machine is in rhythm state
                    if (coffee.checkCurrentState() == coffee.RhythmState) {
                        CloseOverlay();
                        NextPanel();

                        circleSequence = DOTween.Sequence();
                        circleSequence.Append(hitCircle.DOFade(1, 0.5f))
                                      .Join(approachCircle.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.Linear))
                                      .Append(hitCircle.DOFade(0, 0.1f))
                                      .Append(score300.DOFade(1, 0.05f))
                                      .AppendInterval(0.5f)
                                      .Append(score300.DOFade(0, 0.3f));
                        circleSequence.SetLoops(-1, LoopType.Restart);
                        circleSequence.Play();
                    }
                    break;
                case 11: // check that combine menu has latte as the current drink
                    if (combineMenu.targetDrink == 15) {
                        CloseOverlay();
                        NextPanel();
                    }
                    break;
                case 12: 
                    if (combine.checkCurrentState() == combine.RhythmState) {
                        CloseOverlay();
                        Debug.Log("reached combine rhythm");
                    }
                    break;
                default: 
                    break;
            }
            return;
        }

        // checks that happen when the panel is closed
        switch (currentStep) {
            case 0:
                if (tutorialTable.occupied) {
                    tutorialTable.paused = true;
                    NextPanel();
                }
                break;
            case 1: 
                NextPanel();
                break;
            case 2: 
                NextPanel();
                break;
            case 3: 
                // check when player interacts with coffee machine
                if (coffee.checkCurrentState() == coffee.MenuState) {
                    NextPanel();
                }
                break;
            case 6: 
                if (rhythmEnd && !waiting) {
                    coffee.currentScore = (float)currentRhythmEvent.score / 9f;
                    coffee.ChangeState(coffee.OccupiedState);
                    StartCoroutine(WaitBeforeNextPanel(1f));
                }
                break;
            case 7: 
                NextPanel();
                break;
            case 8: //check when player interacts with milk
                if (milk.checkCurrentState() == milk.DoneState) {
                    NextPanel();
                }
                break;
            case 9: 
                if (milk.checkCurrentState() == milk.IdleState) {
                    NextPanel();
                }
                break;
            case 10: // rememebr to add a check that the player has the correct ingredients
                if (combine.checkCurrentState() == combine.MenuState && InventoryManager.instance.InvContains(1) && InventoryManager.instance.InvContains(5)) {
                    NextPanel();
                }
                break;
            case 12: 
                if (combine.checkCurrentState() == combine.DoneState) {
                    NextPanel();
                }
                break;
            case 13: 
                if (InventoryManager.instance.TrayContains(15)) {
                    NextPanel();
                }
                break;
            case 14: 
                if (!tutorialTable.occupied) {
                    NextPanel();
                }
                break;
            case 15: 
                NextPanel();
                break;
            default:
                break;
        }
    }

    public void CloseOverlay() {
        GameObject currentPanel = panels[currentStep];
        currentPanel.GetComponent<uiTransitions>().UIFadeOut();
        Time.timeScale = 1f;
        inPanel = false;
        unfreezePlayer.Invoke();
        LevelManager.instance.unfreezePatience.Invoke();
        tutorialTable.paused = true;

        // things that happen when this panel is closed
        switch (currentStep) {
            case 0:
                LevelManager.instance.SpawnTutorialCustomer();
                break;
            case 3: 
                coffee.ChangeState(coffee.IdleState);
                break;
            case 6: 
                GameObject rhythm = Object.Instantiate(coffee.rhythmEvent, new Vector3(0, 0, 0), Quaternion.identity);
                currentRhythmEvent = rhythm.GetComponent<rhythmEventManager>();
                currentRhythmEvent.rhythmEnd.AddListener(()=> {rhythmEnd = true;});
                currentRhythmEvent.approachRate = 0.9f;
                
                freezePlayer.Invoke();
                break;
            default:
                break;
        }
    }

    public void NextPanel() {
        inPanel = true;
        freezePlayer.Invoke();
        LevelManager.instance.freezePatience.Invoke();
        currentStep++;

        panels[currentStep].SetActive(true);
        panels[currentStep].GetComponent<uiTransitions>().UIFadeIn();
    }

    IEnumerator WaitBeforeNextPanel(float seconds) {
        waiting = true;
        yield return new WaitForSeconds(seconds);
        NextPanel();
        waiting = false;
    }
}
