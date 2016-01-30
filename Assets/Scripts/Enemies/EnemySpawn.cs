using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    public int numberOfEnemies = 5;
    public float waitForNextEnemy = 2f;
    private float mSpawnTimer = 0f;

    public Object enemy;

    private Path mPath;

	// Use this for initialization
	void Start () {
        mPath = GetComponent<Path>();
    }
	
	// Update is called once per frame
	void Update () {
        mSpawnTimer += Time.deltaTime;
        if(numberOfEnemies > 0 && mSpawnTimer >= waitForNextEnemy)
        {
            mSpawnTimer = 0;
            GameObject prefab = Instantiate(enemy) as GameObject;
            prefab.transform.position = mPath.root.transform.position;
            prefab.transform.rotation = Quaternion.identity;
            EnemyMovement enemyMovement = prefab.GetComponent<EnemyMovement>();
            enemyMovement.StartMoving(mPath.root);
            numberOfEnemies--;
        }
	}
}
