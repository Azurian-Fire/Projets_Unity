using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;
    [SerializeField] TMP_Text currentHealthText;
    [SerializeField] TMP_Text totalHealthText;

    [SerializeField] float width;
    [SerializeField] float height;
    
    public void SetHealthUI(float newHealth, float maxHealth)
    {
        currentHealthText.text = newHealth.ToString();
        
        float newWidth = (newHealth / maxHealth) * width;
        healthBar.sizeDelta = new Vector2(newWidth, height);
    }
}