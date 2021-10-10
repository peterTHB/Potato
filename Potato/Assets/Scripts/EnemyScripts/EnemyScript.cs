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
        if (transform.parent.parent.gameObject.TryGetComponent<ShielderEnemyMovement>(out ShielderEnemyMovement shielderEnemy))
        {
            shielderEnemy.StopShielding();
        }
        Destroy(enemyParent);
    }
}
