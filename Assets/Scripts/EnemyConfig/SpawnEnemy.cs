using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<GameObject> listEnemySpawn;



    public void Spawn(float delay, Transform parent, Vector3 position, Quaternion rotation)
    {
        StartCoroutine(SpawnAfterDelay(delay, parent, position, rotation));
    }

    IEnumerator SpawnAfterDelay(float delay, Transform parent, Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(delay);
        GameObject enemy = Instantiate(listEnemySpawn[Random.Range(0, listEnemySpawn.Count)], position, rotation);
        enemy.transform.SetParent(parent);
    }

}
