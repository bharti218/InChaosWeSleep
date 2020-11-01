using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01MiniBoss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> rockObjects = new List<GameObject>();
    [SerializeField] Transform rockSpawnPoint;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] Slider minibossSlider;
    private float maxHealth = 100;
    private float currentHealth = 100;
    void Start()
    {
        
    }
    private void OnBecameVisible()
    {
        enemySpawner.SetActive(false);
        minibossSlider.gameObject.SetActive(true);
        InvokeRepeating("GenerateRockObjects", 0 ,5);
    }

    private void GenerateRockObjects()
    {
        int randomIndex = Random.Range(0, rockObjects.Count - 1);
        GameObject st = Instantiate(rockObjects[randomIndex]);
        st.transform.position = rockSpawnPoint.position;
    }

    public void TakeDamage()
    {
        currentHealth = currentHealth - 2;
        minibossSlider.value = currentHealth;
        if (currentHealth <= 0)
            Destroy(gameObject);
           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().ReduceHealth(100);
        }
    }
}
