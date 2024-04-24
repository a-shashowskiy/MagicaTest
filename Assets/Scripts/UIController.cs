using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static public UIController instance;
    public Action<String> OnSpellChange;
    public Action<float> OnHelthChange;
    [SerializeField]private TextMeshProUGUI curentSpell;
    [SerializeField] private Slider helthBar;
    private String curentSpellName;
    private float curentHelth = 0;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        OnSpellChange += UpdateSpell;
        OnHelthChange += UpdateHelth;
    }

    private void UpdateHelth(float obj)
    {
        curentHelth = obj;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        helthBar.value = curentHelth;
    }

    private void UpdateSpell(String spellName)
    {
        curentSpellName = spellName;
        UpdateSpellInUI();
    }
    private void UpdateSpellInUI()
    {
        curentSpell.text = curentSpellName;
    } 
}
