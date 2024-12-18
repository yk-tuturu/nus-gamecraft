using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform customerSpawner; 
    public Transform tableParent;
    public Transform pathParent;
    public Transform customerParent;
    public GameObject customerPrefab;
    public int customersToSpawn = 15;
    public float timeToNextSpawn;
    
    public float minCustomerGap = 10f;
    public float maxCustomerGap = 20f;
    public float doubleChance = 0.3f;

    public List<Transform> paths = new List<Transform>();
    public List<Table> tables = new List<Table>();

    public UnityEvent freezePatience;
    public UnityEvent unfreezePatience;

    public bool queuing = false; 

    public float targetMoney;
    public float currentMoney = 0f;

    public TextMeshProUGUI moneyText;
    public GameObject LevelCompleteText;
    public GameObject LevelFailText;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetMoney = customersToSpawn * 10 * 0.6f;

        foreach (Transform child in tableParent) {
            Table table = child.GetComponent<Table>();
            tables.Add(table);
        }

        timeToNextSpawn = Time.time + 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToNextSpawn && customersToSpawn > 0) {
            SpawnCustomer();
            SetTimeToNextSpawn();
        }

        if (customersToSpawn <= 0 && customerParent.childCount == 0) {
            LevelEnd();
        }
    }

    void SetTimeToNextSpawn() {
        float currentTime = Time.time;
        if (queuing) {
            timeToNextSpawn = currentTime + 5f;
            queuing = false;
        } else {
            float timeGap = Random.Range(minCustomerGap, maxCustomerGap);
            timeToNextSpawn = currentTime + timeGap;
        }   
    }

    void SpawnCustomer() {
        List<Table> unoccupiedTables = new List<Table>();
        foreach (Table table in tables) {
            if (!table.occupied) {
                unoccupiedTables.Add(table);
            }
        }

        if (unoccupiedTables.Count == 0) {
            queuing = true;
            return;
        }

        Debug.Log("spawning customer");
        float rng = Random.Range(0f, 1f);

        if (rng < doubleChance) {
            int tableIndex = Random.Range(0, unoccupiedTables.Count);
            Table targetTable = unoccupiedTables[tableIndex];
            for (int i = 0; i < 2; i++) {
                GameObject customerObj = Instantiate(customerPrefab, customerSpawner.position, Quaternion.identity, customerParent);
                Customer customer = customerObj.GetComponent<Customer>();

                customer.targetTable = targetTable;
                customer.doubled = true;

                if (i == 0) {
                    customer.path = targetTable.pathTowardsLeft;
                } else {
                    customer.path = targetTable.pathTowardsRight;
                }
                customersToSpawn--;
            }
        } else {
            GameObject customerObj = Instantiate(customerPrefab, customerSpawner.position, Quaternion.identity, customerParent);
            Customer customer = customerObj.GetComponent<Customer>();

            int tableIndex = Random.Range(0, unoccupiedTables.Count);
            Table targetTable = unoccupiedTables[tableIndex];

            customer.targetTable = targetTable;
            customer.path = targetTable.pathTowardsLeft;
            customersToSpawn--;
        }
    }

    public void AddMoney(float money) {
        currentMoney += money;
        moneyText.text = "$" + currentMoney.ToString("0.00");
    }

    public int CheckVacancy() {
        int counter = 0;
        for (int i = 0; i < tables.Count; i++) {
            if (!tables[i].occupied) {
                counter++;
            }
        }
        return counter;
    }

    public void LevelEnd() {
        if (currentMoney >= targetMoney) {
            LevelCompleteText.SetActive(true);
        } else {
            LevelFailText.SetActive(true);
        }
    }
}
