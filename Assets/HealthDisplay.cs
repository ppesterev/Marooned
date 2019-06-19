using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Slider slider = null;
    private float originalWidth;

    // Start is called before the first frame update
    void Start()
    {
        Unit playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        playerUnit.HealthChanged += UpdateHealthDisplay;
        slider = GetComponent<Slider>();
        slider.value = (playerUnit.Health * 1.0f / playerUnit.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthDisplay(int health, int maxHealth)
    {
        slider.value = (health * 1.0f / maxHealth);        
    }
}
