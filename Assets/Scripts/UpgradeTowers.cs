using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowers : MonoBehaviour
{
    [SerializeField] private GameControl gameCtrl;

    private TowerMenus thisTower;

    public void onCancel()
    {
        gameObject.SetActive(false);
        gameCtrl.activeTowerSpotScript = null;
        thisTower = null;
        gameCtrl.UpgradeDisplay.text = "";
    }

    public void UpgradeStrength()
    {
        thisTower = gameCtrl.activeTowerSpotScript;
        if (gameCtrl.cash >= 250 * (thisTower.numUpgrades +1))
        {
            thisTower.towerScript.towerDamage += 5;
            gameCtrl.cash -= (250 * (thisTower.numUpgrades + 1));
            thisTower.numUpgrades++;
            gameCtrl.showCash.text = "$" + gameCtrl.cash;
        }
        onCancel();
    }

    public void UpgradeDistance()
    {
        thisTower = gameCtrl.activeTowerSpotScript;
        if (gameCtrl.cash >= 150 * (thisTower.numUpgrades+1))
        {
            thisTower.towerScript.towerRange += 1;
            thisTower.towerScript.upgradeRadius();
            gameCtrl.cash -= 150 * (thisTower.numUpgrades + 1);
            gameCtrl.showCash.text = "$" + gameCtrl.cash;
            thisTower.numUpgrades++;
        }
        onCancel();
    }
    public void UpgradeSpeed()
    {
        thisTower = gameCtrl.activeTowerSpotScript;

        if (gameCtrl.cash >= 100 * (thisTower.numUpgrades + 1))
        {
            
            if (thisTower.towerScript.shootTime >= 3)
                thisTower.towerScript.shootTime -= 2;
            else
                thisTower.towerScript.shootTime -= .25f;
            
            gameCtrl.cash -= 100 * (thisTower.numUpgrades + 1);
            gameCtrl.showCash.text = "$" + gameCtrl.cash;
            thisTower.numUpgrades++;
        }
        onCancel();
    }
}
