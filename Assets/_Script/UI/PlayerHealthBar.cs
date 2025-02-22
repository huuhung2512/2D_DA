using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : SingletonBehavior<PlayerHealthBar>
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Stats stats;
    private float lastHealthValue;

    private void Start()
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = stats.Health.MaxValue;
        healthSlider.value = stats.Health.CurrentValue;
        lastHealthValue = healthSlider.value;
    }

    private void Update()
    {
        if (stats.Health.CurrentValue != lastHealthValue )
        {
            healthSlider.value = stats.Health.CurrentValue;
            lastHealthValue = stats.Health.CurrentValue;
      
        }
    }
    public void SetStats(Stats newStats)
    {
        stats = newStats;
        healthSlider.maxValue = stats.Health.MaxValue;
        healthSlider.value = stats.Health.CurrentValue;
        lastHealthValue = stats.Health.CurrentValue;
    }

    public void UpdateHealthBar()
    {
        stats.Health.Init();
    }
}
