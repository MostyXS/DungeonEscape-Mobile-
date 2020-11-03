using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Platformer.Control;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shopGemCount, gemCountUI;
    [SerializeField] Image selectionImage;
    [SerializeField] GameObject healthBar;
    Player player;

    private static UIManager instance;
    private void Start()
    {
        UpdateText();
    }
    public static UIManager  Instance
    {
        get 
        { 
            return instance; 
        }
    }
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (instance == null) print("111");
        
    }
    public void UpdateText()
    {
        shopGemCount.text = player.GetGemsAmount() +"G";
        gemCountUI.text = player.GetGemsAmount() + "G";
    }
    public void UpdateShopSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
        
    }
    public void UpdateLives(int livesRemaining)
    {
        for (int i =0; i< healthBar.transform.childCount; i++)
        {
            healthBar.transform.GetChild(i).gameObject.SetActive(i < livesRemaining);
        } 
    }
    

    


}
