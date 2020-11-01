using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level01MiniBoss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> rockObjects = new List<GameObject>();
    [SerializeField] Transform rockSpawnPoint;
    [SerializeField] GameObject enemySpawner;
    public int damageCount;
    void Start()
    {
        
    }
    private void OnBecameVisible()
    {
        enemySpawner.SetActive(false);
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
        damageCount++;
        if (damageCount >= 40)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().ReduceHealth(100);
        }
    }
}
