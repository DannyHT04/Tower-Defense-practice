using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shopping : MonoBehaviour
{
    public int totalMoney = 0;
    private TileClicking tileClickingScript;
    public GameObject archerTower, knightTower, wizardTower;
    private Dictionary<string, int> unitPrices = new Dictionary<string, int>
    {
        { "Archer", 15 },
        { "Knight", 10 },
        { "Wizard", 30 }
    };
    private Dictionary<string, GameObject> spawningTowers;
    private TextMeshProUGUI moneyTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        tileClickingScript = GameObject.Find("GameManager").GetComponent<TileClicking>();   
    }
    void Start()
    {
        moneyTxt = GameObject.Find("Money_Text").GetComponent<TextMeshProUGUI>();
        moneyTxt.text = "$" + totalMoney.ToString();
        spawningTowers = new Dictionary<string, GameObject>
        {
            { "Archer", archerTower },
            { "Knight", knightTower },
            { "Wizard", wizardTower }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyTower(string tower)
    {

        if (totalMoney >= unitPrices[tower])
        {
            totalMoney -= unitPrices[tower];
            moneyTxt.text = "$" + totalMoney;
            Instantiate(spawningTowers[tower], tileClickingScript.spawnPosition, Quaternion.identity);
            tileClickingScript.CloseShop();
        }
    }

    public void UpdateMoney(int Amount)
    {
        totalMoney += Amount;
        moneyTxt.text = "$" + totalMoney.ToString();
    }




}
