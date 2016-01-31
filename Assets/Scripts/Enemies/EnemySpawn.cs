using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    public CameraRotation camera;
    private float waitForNextEnemy = 2f;
    private float mSpawnTimer = 0f;

    public Object[] enemies;

    public int[] enemyIds;
    public float[] enemyTimers;
    private int actualEnemy = 0;

    private Path mPath;

    private ArrayList instantiatedEnemies = new ArrayList();

	// Use this for initialization
	void Start () {
        mPath = GetComponent<Path>();
        actualEnemy = 0;
        waitForNextEnemy = enemyTimers[actualEnemy];
    }
	
	// Update is called once per frame
	void Update () {
        mSpawnTimer += Time.deltaTime;
        if(actualEnemy < enemyIds.Length  && mSpawnTimer >= waitForNextEnemy)
        {

            GameObject prefab = Instantiate(enemies[enemyIds[actualEnemy]]) as GameObject;
            instantiatedEnemies.Add(prefab);
            prefab.transform.position = mPath.root.transform.position;
            prefab.transform.rotation = Quaternion.identity;
            EnemyMovement enemyMovement = prefab.GetComponent<EnemyMovement>();
            enemyMovement.StartMoving(mPath.root, camera, this);
            actualEnemy++;
            mSpawnTimer = 0;
            if(actualEnemy < enemyTimers.Length)
                waitForNextEnemy = enemyTimers[actualEnemy];
        }
        else {
            if(instantiatedEnemies.Count <= 0 && actualEnemy >= enemyIds.Length) {
                GameManager.Instance.GameOver(true);
            }
        }
	}

    public void destroyEnemy(GameObject enemy) {
        instantiatedEnemies.Remove(enemy);
    }
}
