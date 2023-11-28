using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    [SerializeField] protected Text _balanceText;
    private int balance;

    void Start()
    {
        balance = PlayerPrefs.GetInt("Coins", 0);
        UpdateCoinText();
    }
    
    void UpdateCoinText()
    {
        _balanceText.text = balance.ToString();
    }
    // Update is called once per frame
}
