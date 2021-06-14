using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class TowerMenus : MonoBehaviour
{
    [SerializeField] GameControl gameCtrl;
    [SerializeField] private GameObject buyMenu;
    [SerializeField] private GameObject upgradeMenu;
    public GameObject towerObject;
    public Tower towerScript;
    public bool hasTower = false;
    public int numUpgrades = 0;

    private void Awake()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
    }
    public void Start()
    {

        buyMenu = gameCtrl.buyMenu;
        upgradeMenu = gameCtrl.upgradeMenu;
    }

    public void onClickTowerSpot()
    {
        Debug.Log("OnClick for spot: " + gameObject.name);
        gameCtrl.activeTowerSpotScript = this;
        
        if (!hasTower)
        {
            buyMenu.SetActive(true);
        }
        else
        {
            //upgrade menu
            if(numUpgrades < 3)
            {
                gameCtrl.UpgradeDisplay.text = "numUpgrades = " + numUpgrades;
                upgradeMenu.SetActive(true);
            }
            
            else
            {
                //upgradeMessage.GetComponent<NotificationManager>().OpenNotification();
                gameCtrl.activeTowerSpotScript = null;
                //close notification
            }
        }
    }


}