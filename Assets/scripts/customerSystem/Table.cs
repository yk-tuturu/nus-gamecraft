using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour, Interactable
{
    public bool occupied = false;
    public List<int> orders = new List<int>();
    public List<Customer> customers = new List<Customer>();

    public Transform pathTowardsLeft;
    public Transform pathTowardsRight;
    public Transform pathAwayLeft;
    public Transform pathAwayRight;

    public GameObject bubble;
    public GameObject doubleBubble;
    public Image bubbleImg;
    public Image doubleBubbleImg0;
    public Image doubleBubbleImg1;

    public loadingBar singleBar;
    public loadingBar doubleBar;
    public loadingBar currentBar;
    public float patience = 30f;
    public float doublePatience = 45f;
    public float currentPatience;
    public float maxPatience;

    public bool paused = false;
    public float currentMoney = 0f;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.freezePatience.AddListener(()=>{paused=true;});
        LevelManager.instance.unfreezePatience.AddListener(()=>{paused=false;});
    }

    // Update is called once per frame
    void Update()
    {
        if (occupied && !paused) {
            currentPatience -= Time.deltaTime;
            float fillAmount = Mathf.Max((float)currentPatience / maxPatience, 0f);
            currentBar.SetFill(fillAmount);
            if (fillAmount <= 0f) {
                Leave();
            }
        }
    }

    public void OccupyTable(int numberOfCustomers, Customer customer) {
        Debug.Log("occupy table called!");
        if (occupied) {
            customers.Add(customer);
            return;
        }

        for (int i = 0; i < numberOfCustomers; i++) {
            int drink = SpriteManager.instance.GetRandomDrink();
            orders.Add(drink);
        }

        if (numberOfCustomers == 2) {
            doubleBubble.SetActive(true);
            doubleBubbleImg0.sprite = SpriteManager.instance.GetSprite(orders[0]);
            doubleBubbleImg1.sprite = SpriteManager.instance.GetSprite(orders[1]);
            currentBar = doubleBar;
        } else {
            bubble.SetActive(true);
            bubbleImg.sprite = SpriteManager.instance.GetSprite(orders[0]);
            currentBar = singleBar;
        }

        occupied = true;
        currentBar.SetFill(1f);
        if (numberOfCustomers == 2) {
            currentPatience = doublePatience;
            maxPatience = doublePatience;
        } else {
            currentPatience = patience;
            maxPatience = patience;
        }
        
        customers.Add(customer);
    }

    public void Leave() {
        for (int i = 0; i < customers.Count; i++) {
            if (customers[i].path == pathTowardsLeft) {
                customers[i].beginMoveAway(pathAwayLeft);
            } else {
                customers[i].beginMoveAway(pathAwayRight);
            }
        }

        orders = new List<int>();
        customers = new List<Customer>();
        currentMoney = 0f;

        bubbleImg.transform.GetChild(0).gameObject.SetActive(false);
        doubleBubbleImg0.transform.GetChild(0).gameObject.SetActive(false);
        doubleBubbleImg1.transform.GetChild(0).gameObject.SetActive(false);

        bubble.SetActive(false);
        doubleBubble.SetActive(false);

        occupied = false;
    }

    public void Interact() {
        List<int> toBeConsumed = new List<int>();

        if (occupied) {
            var tray = InventoryManager.instance.tray;
            for (int i = 0; i < tray.Count; i++) {
                for (int j = 0; j < orders.Count; j++) {
                    if (tray[i].index == orders[j]) {
                        toBeConsumed.Add(tray[i].id);
                        FulfillOrder(tray[i].id, j);
                        break;
                    }
                }
            }
        }
        foreach (int id in toBeConsumed) {
            InventoryManager.instance.ConsumeTray(id);
        }
    }

    void FulfillOrder(int drinkId, int orderIndex) {
        float money = Mathf.Max(InventoryManager.instance.GetDrinkById(drinkId).score, 4f) / 10 + 3f * currentPatience / maxPatience;
        currentMoney += money; 
        
        if (bubble.activeSelf) {
            Transform check = bubbleImg.transform.GetChild(0);
            check.gameObject.SetActive(true);
            LevelManager.instance.AddMoney(currentMoney);
            Leave();
        } else if (doubleBubble.activeSelf) {
            Transform check;
            if (orderIndex == 1) {
                check = doubleBubbleImg1.transform.GetChild(0);
            } else {
                check = doubleBubbleImg0.transform.GetChild(0);
            }
            
            check.gameObject.SetActive(true);

            // if order completely fulfilled, leave, else, set the order to -1 to imply it is complete
            orders[orderIndex] = -1;
            if (CheckOrderComplete()) {
                LevelManager.instance.AddMoney(currentMoney);
                Leave();
            }   
        }
    }

    bool CheckOrderComplete() {
        for (int i = 0; i < orders.Count; i++) {
            if (orders[i] != -1) {
                return false;
            }
        }
        return true;
    }
}
