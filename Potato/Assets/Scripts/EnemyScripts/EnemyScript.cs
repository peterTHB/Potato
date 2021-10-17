using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject explosionEffect;

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
        GameObject tempExplosion = Instantiate<GameObject>(explosionEffect);
        tempExplosion.transform.position = this.transform.position;
        tempExplosion.transform.position.Scale(new Vector3(0.1f, 0.1f, 0.1f));
        tempExplosion.GetComponent<ParticleSystem>().Play();

        Destroy(enemyParent);
    }
}
