using UnityEngine.UI;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Character_Health health;
    [SerializeField] Image theHealthBar;

    private void Start()
    {
        health.HealthChanged += Health_HealthChanged;
    }

    private void Health_HealthChanged(object sender, EventArgs e)
    {
        theHealthBar.fillAmount = health.GetHealthNormalized();
    }
}
