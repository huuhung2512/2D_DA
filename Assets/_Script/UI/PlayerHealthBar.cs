using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : SingletonBehavior<PlayerHealthBar>
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Stats stats; // Để gán trong Inspector nếu cần, nhưng sẽ được cập nhật động
    private float lastHealthValue;

    private void Start()
    {
        InitializeHealthBar();
    }

    private void Update()
    {
        if (stats == null || healthSlider == null)
        {
            FindPlayerStats(); // Tìm lại Stats nếu bị mất
            return;
        }

        if (stats.Health.CurrentValue != lastHealthValue)
        {
            healthSlider.value = stats.Health.CurrentValue;
            lastHealthValue = stats.Health.CurrentValue;
        }
    }

    public void SetStats(Stats newStats)
    {
        stats = newStats;
        if (stats != null && healthSlider != null)
        {
            healthSlider.maxValue = stats.Health.MaxValue;
            healthSlider.value = stats.Health.CurrentValue;
            lastHealthValue = stats.Health.CurrentValue;
        }
    }

    public void UpdateHealthBar()
    {
        if (stats != null)
        {
            stats.Health.Init();
            InitializeHealthBar();
        }
    }

    private void InitializeHealthBar()
    {
        if (stats == null)
        {
            FindPlayerStats();
        }

        if (stats != null && healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.maxValue = stats.Health.MaxValue;
            healthSlider.value = stats.Health.CurrentValue;
            lastHealthValue = stats.Health.CurrentValue;
        }
    }

    private void FindPlayerStats()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            stats = player.GetComponentInChildren<Stats>();
            if (stats != null)
            {
                SetStats(stats); // Cập nhật lại stats và thanh máu
            }
            else
            {
                Debug.LogError("Không tìm thấy Stats trên Player!");
            }
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Player trong scene!");
        }
    }
}