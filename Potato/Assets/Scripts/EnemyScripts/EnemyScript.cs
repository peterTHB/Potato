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
        GameObject enemyParent = this.gameObject;

        GameObject currParentObject = this.gameObject;
        while(enemyParent.tag != "EnemyParent" && currParentObject != null)
        {
            if (currParentObject.tag == "EnemyParent")
            {
                enemyParent = currParentObject;
            }
            else
            {
                currParentObject = currParentObject.transform.parent.gameObject;
            }
        }

        if (enemyParent.TryGetComponent<ShielderEnemyMovement>(out ShielderEnemyMovement shielderEnemy))
        {
            shielderEnemy.StopShielding();
        }
        Destroy(enemyParent);
    }
}
