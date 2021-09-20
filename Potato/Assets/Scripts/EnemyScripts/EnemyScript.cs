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

        if (transform.parent.parent.gameObject.TryGetComponent<ShielderEnemyMovement>(out ShielderEnemyMovement shielderEnemy))
        {
            shielderEnemy.StopShielding();
        }
        Destroy(transform.parent.parent.gameObject);
    }
}
