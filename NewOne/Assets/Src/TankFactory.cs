using UnityEngine;
using System.Collections;

public class TankFactory : MonoBehaviour
{

    [SerializeField]
    GameObject tankPrefab;

    [SerializeField]
    float spawnCooldown = 20;
    float spawnTimer = 0;

    [SerializeField]
    int spawnIncStep = 4;

    [SerializeField]
    Transform leftBase;

    [SerializeField]
    Transform rightBase;

    int totalSpawned = 1;

    // Use this for initialization
    void Start()
    {
        if (spawnIncStep == 0)
            spawnIncStep = 1;

        spawnTimer = Random.Range(0.5f, 1.0f) * spawnCooldown;
    }


    // Update is called once per frame
    void Update()
    {
        // don't keep him waiting
        if (GameCore.Instance.Score >= totalSpawned && spawnTimer > 2.0f)
            spawnTimer = 2.0f;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            spawnTimer = Random.Range(0.9f, 1.1f) * spawnCooldown / Mathf.Sqrt(GameCore.Instance.Score / spawnIncStep);
            SpawnTank();
        }
    }

    //
    void SpawnTank()
    {
        var pos = rightBase.transform.position;
        if (GameCore.Instance.Score > 2 && Random.value < 0.5f)
        {
            pos = leftBase.transform.position;
        }

        if (totalSpawned - GameCore.Instance.Score >= 3 + GameCore.Instance.Score / spawnIncStep)
            return;

        pos.z = 0;
        Instantiate(tankPrefab, pos, Quaternion.identity);
        totalSpawned++;
    }
}
