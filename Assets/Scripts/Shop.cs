using Platformer.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    int currentSelectedItem = -1;
    public int currentItemCost = 0;
    Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null) print("Hey");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!collision.GetComponent<Player>()) return;
        shopUI.SetActive(true);
        UIManager.Instance.UpdateText();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        shopUI.SetActive(false);
    }

    public void SelectItem(int item)
    {
        currentSelectedItem = item;
        switch (item)
        {

            case (0):
                UIManager.Instance.UpdateShopSelection(78);
                currentItemCost = 200;
                break;
            case (1):
                UIManager.Instance.UpdateShopSelection(-47);
                currentItemCost = 400;
                break;
            case (2):
                UIManager.Instance.UpdateShopSelection(-147);
                currentItemCost = 100;
                break;

            default: print("No Item Selected");
                break;
        }
    }
    public void BuyItem()
    {
        if (currentSelectedItem < 0)
        {
            print("Please select item");
            return;
        }
        if(currentItemCost> player.GetGemsAmount())
        {
            print("You don't have enough gems");
            return;
        }
        if (currentSelectedItem == 2)
        {
            if (GameManager.Instance.HasKeyToCastle)
            {
                print("You can't buy more");
                return;
            }
            GameManager.Instance.HasKeyToCastle = true;
        }
        player.AddOrRemoveGems(-currentItemCost);
        UIManager.Instance.UpdateText();
        print("Thanks for purchase, traveller! Your bill: \"You've spent " + currentItemCost + " Gems\"");


    }

}
