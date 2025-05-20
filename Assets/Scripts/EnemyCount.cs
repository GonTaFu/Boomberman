using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;

    void Start()
    {
        if (enemyCountText == null)
        {
            Debug.LogError("Enemy Count Text is not assigned in the inspector.");
        }
    }

    void Update()
    {
        UpdateEnemyCount();
    }

    void UpdateEnemyCount()
    {
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCountText.text = "Enemies: " + count.ToString();
    }
}
