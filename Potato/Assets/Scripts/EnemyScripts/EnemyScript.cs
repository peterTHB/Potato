using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private void Start()
    {

    }

    public void GotHit()
    {
        // Set Current entities by -1
        // If current entities are 0, increase max entities
        // destroy gameobject and parent
        GameObject enemyParent = null;
        Transform t = transform;
        while (enemyParent == null && t.parent != null)
        {
            if (t.parent.CompareTag("EnemyParent"))
            {
                enemyParent = t.parent.gameObject;
            }
            t = t.parent.transform;
        }


        if (enemyParent.TryGetComponent<ShielderEnemyMovement>(out ShielderEnemyMovement shielderEnemy))
        {
            shielderEnemy.StopShielding();
        }
        Destroy(enemyParent);
    }
}
