using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvtController : MonoBehaviour
{
    [SerializeField] GameObject eyePrefab;
    [SerializeField] Transform eyeParent;
    public float respawnTime = .1f;
    private Vector2 screenBounds;
    public int eyeCount = 0;
    [SerializeField] List<Transform> eyeSpawningPoints = new List<Transform>();
    List<Transform> occupiedPoints = new List<Transform>();
    List<GameObject> eyeObjects = new List<GameObject>();
    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(asteroidWave());
    }

    private void SpawnEye()
    {
        GameObject a = Instantiate(eyePrefab) as GameObject;
        Transform point = GetNearestPoint();
        a.transform.position = point.position;
        occupiedPoints.Add(point);
        
    }

    Transform GetNearestPoint()
    {
        float minDis = 1000;
        Transform nearestPoint = eyeSpawningPoints[0];
        for (int i = 0; i < eyeSpawningPoints.Count; i++)
        {
            float dis =  Vector3.Distance(eyeParent.position, eyeSpawningPoints[i].position);
            
            if(dis<minDis && !occupiedPoints.Contains(eyeSpawningPoints[i]))
            {
                minDis = dis;
                nearestPoint = eyeSpawningPoints[i];
            }
        }
        return nearestPoint;
    }

    IEnumerator asteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEye();
        }
    }


}
