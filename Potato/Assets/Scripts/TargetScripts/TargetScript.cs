using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool isBlocked = false;
    public GameObject spawnOnHit;

    private void Start()
    {
        CheckInRadius();
    }

    private void CheckInRadius()
    {
        Vector3 center = transform.position;
        float radius = 5f;

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Target" || hitCollider.gameObject.tag == "Enemy")
            {
                float[] acceptedPositions = new float[] { -6f, -5f, -4f, 4f, 5f, 6f };

                float randomX = acceptedPositions[Random.Range(0, acceptedPositions.Length)];
                float randomY = acceptedPositions[Random.Range(0, acceptedPositions.Length)];
                float randomZ = acceptedPositions[Random.Range(0, acceptedPositions.Length)];

                Vector3 position = new Vector3(randomX, randomY, randomZ);

                transform.position += position;
            }
        }
    }

    public void GotHit()
    {
        GameObject temp = Instantiate<GameObject>(spawnOnHit);
        temp.transform.position = this.transform.position;

        int totalTargetsHit = PlayerPrefs.GetInt("PlayerTargetsHit") + 1;
        PlayerPrefs.SetInt("PlayerTargetsHit", totalTargetsHit);
        Destroy(transform.parent.gameObject);
    }
}
