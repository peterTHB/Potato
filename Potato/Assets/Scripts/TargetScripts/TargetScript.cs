using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool isBlocked = false;

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

    private void GotHit()
    {
        // Set Current entities by -1
        // If current entities are 0,
        //      increase max entities
        //      Increase level difficulty and destroy all current enemies in scene
        // destroy gameobject and parent
    }
}
