using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour
{
    public float startHealth = 100f;
    private float currentHealth;
    public Image healthBarFill;

    [Header("Hit Feedback")]
    public float flashTime = 0.1f;
    public Color hitColor = Color.red;

    private SpriteRenderer sprite;
    private Color originalColor;

    private void Start()
    {
        currentHealth = startHealth;
        UpdateHealthBar();

        sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
            originalColor = sprite.color;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (sprite != null)
        {
            StopAllCoroutines();
            StartCoroutine(HitFlash());
        }
    }

    IEnumerator HitFlash()
    {
        sprite.color = hitColor;
        yield return new WaitForSeconds(flashTime);
        sprite.color = originalColor;
    }

    public void HealHealth(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > startHealth)
            currentHealth = startHealth;

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / startHealth;
    }

    void Death()
    {
        Debug.Log("Dead");
        SceneManager.LoadScene("Mads");
    }

    private void Update()
    {
        if (currentHealth <= 0)
            Death();
    }
}
