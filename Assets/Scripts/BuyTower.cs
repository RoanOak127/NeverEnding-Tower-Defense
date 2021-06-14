using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class BuyTower : MonoBehaviour
{

    [SerializeField] private GameObject redTowerPrefab;
    [SerializeField] private GameObject greenTowerPrefab;
    [SerializeField] private GameObject blueTowerPrefab;

    [SerializeField] private GameControl gameCtrl;

    private TowerMenus thisTower;

    public void onCancel()
    {
        gameObject.SetActive(false);
        gameCtrl.activeTowerSpotScript = null;
        thisTower = null;

    }
    public void AddRedTower()
    {
        thisTower = gameCtrl.activeTowerSpotScript;
        if (gameCtrl.cash >= 100)
        {
            thisTower.towerObject = Instantiate(redTowerPrefab, thisTower.transform.position, Quaternion.identity);
            thisTower.hasTower = true;
            thisTower.towerScript = thisTower.towerObject.GetComponent<Tower>();
            gameCtrl.cash -= 100;
            gameCtrl.showCash.text = "$" + gameCtrl.cash;
        }
        onCancel();
    }
    public void AddGreenTower()
    {
        thisTower = gameCtrl.activeTowerSpotScript;
        if (gameCtrl.cash >= 150)
        {
            thisTower.towerObject = Instantiate(greenTowerPrefab, thisTower.transform.position, Quaternion.identity);
            thisTower.hasTower = true;
            thisTower.towerScript = thisTower.towerObject.GetComponent<Tower>();
            gameCtrl.cash -= 150;
            gameCtrl.showCash.text = "$" + gameCtrl.cash;
        }
        onCancel();
    }

    public void AddBlueTower()
    {
        thisTower = gameCtrl.activeTowerSpotScript;
        if (gameCtrl.cash >= 250)
        {
            thisTower.towerObject = Instantiate(blueTowerPrefab, thisTower.transform.position, Quaternion.identity);
            thisTower.hasTower = true;
            thisTower.towerScript = thisTower.towerObject.GetComponent<Tower>();
            gameCtrl.cash -= 250;
            gameCtrl.showCash.text = "$" + gameCtrl.cash;
        }
        onCancel();
    }
}
