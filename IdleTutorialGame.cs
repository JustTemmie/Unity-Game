using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IdleTutorialGame : MonoBehaviour
{

    //text boxes
    public Text CoinsText;
    public Text clickValueText;
    public Text coinsPerSecText;
    public Text clickUpgrade1Text;
    public Text clickUpgrade1TextMax;
    public Text clickUpgrade2Text;
    public Text productionUpgrade1Text;
    public Text productionUpgrade2Text;


    public double coins;



    //click upgrades
    public double coinsClickValue;
    public double clickUpgrade1Power;
    public int clickUpgrade1Level;


    //click upgrades 2
    public double clickUpgrade2Cost;
    public double clickUpgrade2Power;
    public int clickUpgrade2Level;

    
    //coins per second
    public double coinsPerSecond;
    public double productionUpgrade1Cost;
    public double productionUpgrade1Power;
    public int productionUpgrade1Level;


    //coins per second 2
    public double productionUpgrade2Cost;
    public double productionUpgrade2Power;
    public int productionUpgrade2Level;


    //prestige system
    public Text GemsText;
    public Text GemsBoostText;
    public Text PrestigeButtonText;

    public double gems;
    public double gemBoost;
    public double gemsToGet;


    //progress bars
    public Image clickUpgrade1Bar;



    public void Start()
    {
        Application.targetFrameRate = 60;
        Load();
        // starts "SaveFunction" in 10 seconds, making it repeat every 5 seconds after that
        InvokeRepeating("SaveFunction", 10f, 5f);
    }

    void SaveFunction()
    {
        Save();
    } 

    public void Load()
    {
        coins = double.Parse(PlayerPrefs.GetString("coins", "0"));

        clickUpgrade1Power = double.Parse(PlayerPrefs.GetString("clickUpgrade1Power", "1"));
        clickUpgrade2Power = double.Parse(PlayerPrefs.GetString("clickUpgrade2Power", "5"));
        clickUpgrade2Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade2Cost", "100"));

        productionUpgrade1Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade1Cost", "25"));
        productionUpgrade2Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade2Cost", "250"));
        productionUpgrade1Power = double.Parse(PlayerPrefs.GetString("productionUpgrade1Power", "1"));
        productionUpgrade2Power = double.Parse(PlayerPrefs.GetString("productionUpgrade2Power", "5"));


        clickUpgrade1Level = PlayerPrefs.GetInt("clickUpgrade1Level", 0);
        clickUpgrade2Level = PlayerPrefs.GetInt("clickUpgrade2Level", 0);
        productionUpgrade1Level = PlayerPrefs.GetInt("productionUpgrade1Level", 0);
        productionUpgrade2Level = PlayerPrefs.GetInt("productionUpgrade2Level", 0);

        gems = double.Parse(PlayerPrefs.GetString("gems", "0"));
    }

    public void Save()
    {   
        PlayerPrefs.SetString("coins", coins.ToString());
        
        PlayerPrefs.SetString("clickUpgrade2Cost", clickUpgrade2Cost.ToString());
        PlayerPrefs.SetString("clickUpgrade1Power", clickUpgrade1Power.ToString());
        PlayerPrefs.SetString("clickUpgrade2Power", clickUpgrade2Power.ToString());

        PlayerPrefs.SetString("productionUpgrade1Cost", productionUpgrade1Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade2Cost", productionUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade1Power", productionUpgrade1Power.ToString());
        PlayerPrefs.SetString("productionUpgrade2Power", productionUpgrade2Power.ToString());


        PlayerPrefs.SetInt("clickUpgrade1Level", clickUpgrade1Level);
        PlayerPrefs.SetInt("clickUpgrade2Level", clickUpgrade2Level);
        PlayerPrefs.SetInt("productionUpgrade1Level", productionUpgrade1Level);
        PlayerPrefs.SetInt("productionUpgrade2Level", productionUpgrade2Level);

        PlayerPrefs.SetString("gems", gems.ToString());
    }

    
    //prestige
    public void Prestige()
    {
        if (gemsToGet >= 1)
        {
            coins = 0;


            clickUpgrade2Cost = 100;
            clickUpgrade1Power = 1;
            clickUpgrade2Power = 5;

            productionUpgrade1Cost = 25;
            productionUpgrade2Cost = 250;
            productionUpgrade1Power = 1;
            productionUpgrade2Power = 5;


            clickUpgrade1Level = 0;
            clickUpgrade2Level = 0;
            productionUpgrade1Level = 0;
            productionUpgrade2Level = 0;

            gems += gemsToGet;
        }
    }


    //buttons
    public void Click()
    {
        coins += coinsClickValue;
    }

    public double BuyClickUpgrade1MaxCount()
    {
        var basecost = 10; //b
        var currency = coins; //c
        var costincrease = 1.07; //r
        var k = clickUpgrade1Level;
        var n = System.Math.Floor(System.Math.Log(currency * (costincrease - 1) / (basecost * System.Math.Pow(costincrease, k)) + 1, costincrease));
        return n;
    }


    public void BuyUpgrade(string upgradeID)
    {
        switch (upgradeID)
        {
            case "Click1":
                var clickUpgrade1Cost = 10 * System.Math.Pow(1.07, clickUpgrade1Level);
                if (coins >= clickUpgrade1Cost)
                {
                    clickUpgrade1Level++;
                    coins -= clickUpgrade1Cost;
                }
                break;


            case "Click1Max":
                var basecost = 10; //b
                var currency = coins; //c
                var costincrease = 1.07; //r
                var k = clickUpgrade1Level;
                var n = System.Math.Floor(System.Math.Log(currency * (costincrease - 1) / (basecost * System.Math.Pow(costincrease, k)) + 1, costincrease));

                var cost = basecost * (System.Math.Pow(costincrease, k) * (System.Math.Pow(costincrease, n) - 1) / (costincrease - 1));

                if(currency >= cost)
                {
                    clickUpgrade1Level += (int)n;
                    coins -= cost;
                }
                break;


            case "Click2":
                if (coins >= clickUpgrade2Cost)
                    {
                        clickUpgrade2Level++;
                        coins -= clickUpgrade2Cost;
                        clickUpgrade2Cost *= 1.15;
                    }
                break;
            
            case "Product1":    
                if (coins >= productionUpgrade1Cost)
                {
                    productionUpgrade1Level++;
                    coins -= productionUpgrade1Cost;
                    productionUpgrade1Cost *= 1.07;
                }
                break;

            case "Product2":
                if (coins >= productionUpgrade2Cost)
                {
                    productionUpgrade2Level++;
                    coins -= productionUpgrade2Cost;
                    productionUpgrade2Cost *= 1.065;
                }
                break;
            default:
                Debug.Log("i'm not assigned to a proper upgrade");
                break;

        }
    }


    public string ScientificMethod(double x, string y)
    {
        if (x >= 1000)
        {
            var exponent = System.Math.Floor(System.Math.Log10(System.Math.Abs(x)));
            var mantissa = x / System.Math.Pow(10, exponent);
            return mantissa.ToString("F2") + "e" + exponent;
        }
        return x.ToString(y);
    }

    public string ScientificMethod(float x, string y)
    {
        if (x >= 1000)
        {
            var exponent = Mathf.Floor(Mathf.Log10(Mathf.Abs(x)));
            var mantissa = x / Mathf.Pow(10, exponent);
            return mantissa.ToString("F2") + "e" + exponent;
        }
        return x.ToString(y);
    }
    

    //update
    public void Update()
    {
         //#Region hidden code

        clickValueText.text = "Click\n+" + ScientificMethod(coinsClickValue, y: "F1") + " Coins";
        CoinsText.text = "Coins: " + ScientificMethod(coins, y: "F1");
        coinsPerSecText.text = ScientificMethod(coinsPerSecond, y: "F1") + " coins/s";
        PrestigeButtonText.text = "Prestige:\n+ " + (ScientificMethod(gemsToGet, y: "F0")) + " gems";
        GemsText.text = "Gems: " + (ScientificMethod(gems, y: "F0"));
        GemsBoostText.text = ScientificMethod(gemBoost, y: "F2") + "x boost";
        clickValueText.text = "Click\n+" + ScientificMethod(coinsClickValue, y: "F1") + " Coins";


        string clickUpgrade1CostString;
        var clickUpgrade1Cost = 10 * System.Math.Pow(1.07, clickUpgrade1Level);
        clickUpgrade1CostString = ScientificMethod(clickUpgrade1Cost, y: "F1");

        string clickUpgrade1LevelString;
        clickUpgrade1LevelString = ScientificMethod(clickUpgrade1Level, y: "F1");

        string clickUpgrade2CostString;
        clickUpgrade2CostString = ScientificMethod(clickUpgrade2Cost, y: "F1");

        string clickUpgrade2LevelString;
        clickUpgrade2LevelString = ScientificMethod(clickUpgrade2Level, y: "F1");


        string productionUpgrade1CostString;
        productionUpgrade1CostString = ScientificMethod(productionUpgrade1Cost, y: "F1");

        string productionUpgrade1LevelString;
        productionUpgrade1LevelString = ScientificMethod(productionUpgrade1Level, y: "F1");

        string productionUpgrade2CostString;
        productionUpgrade2CostString = ScientificMethod(productionUpgrade2Cost, y: "F1");

        string productionUpgrade2LevelString;
        productionUpgrade2LevelString = ScientificMethod(productionUpgrade2Level, y: "F1");

        //#End region hidden code


        gemsToGet = System.Math.Floor((150 * System.Math.Sqrt(coins / 1e7)));
        gemBoost = (gems * 0.05) +1;

        coinsPerSecond = ((productionUpgrade1Power * productionUpgrade1Level) + (productionUpgrade2Power * productionUpgrade2Level)) * gemBoost;
        coinsClickValue = (clickUpgrade1Power * clickUpgrade1Level) + (clickUpgrade2Power * clickUpgrade2Level) + 1;


        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1CostString + " coins\nPower: +1 Click\nLevel: " + clickUpgrade1LevelString;
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2CostString + " coins\nPower: +5 Click\nLevel: " + clickUpgrade2LevelString;

        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1CostString + " coins\nPower: +" + (productionUpgrade1Power * gemBoost) + "coins/s\nLevel: " + productionUpgrade1LevelString;
        productionUpgrade2Text.text = "Production Upgrade 2\nCost: " + productionUpgrade2CostString + " coins\nPower: +" + (productionUpgrade2Power * gemBoost) + " coins/s\nLevel: " + productionUpgrade2LevelString;
        

        coins += coinsPerSecond * Time.deltaTime;

        if(coins / clickUpgrade1Cost < 0.01)
            clickUpgrade1Bar.fillAmount = 0;
        
        else if (coins / clickUpgrade1Cost > 10)
            clickUpgrade1Bar.fillAmount = 1;
        
        else
            clickUpgrade1Bar.fillAmount = (float)(coins / clickUpgrade1Cost);

    clickUpgrade1TextMax.text = "Buy Max (" + BuyClickUpgrade1MaxCount() + ")";
    }
}