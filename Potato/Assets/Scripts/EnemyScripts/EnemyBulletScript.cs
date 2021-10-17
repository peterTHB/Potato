using DigitalRuby.LightningBolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private Material material;
    public float fluctuateDuration = 1f;
    private float lastFluctuate;
    public float moveSpeed = 1.5f;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;

        //reset lighting object rotations to 0 to avoid issues
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.TryGetComponent(out LightningBoltScript script))
            {
                child.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update Movement
        transform.Translate(transform.forward * Time.deltaTime, Space.World);

        UpdateEmission();
    }

    private void UpdateEmission()
    {
        float percentage;
        float timeSince = Time.time - lastFluctuate;

        if (timeSince >= fluctuateDuration)
        {
            percentage = 0;
            lastFluctuate = Time.time;
        }
        else
        {
            percentage = timeSince / fluctuateDuration;
        }

        material.SetColor("_EmissionColor", Color.red * percentage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currHealth = PlayerPrefs.GetInt("PlayerHealth") - 1;
            PlayerPrefs.SetInt("PlayerHealth", currHealth);
            PlayerPrefs.SetString("MakeHearts", "YES");

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Arrow"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        GameObject tempExplosion = Instantiate<GameObject>(explosionEffect);
        tempExplosion.transform.position = this.transform.position;
        tempExplosion.transform.position.Scale(new Vector3(0.1f, 0.1f, 0.1f));
        tempExplosion.GetComponent<ParticleSystem>().Play();
        //TODO arrow trigger
    }
}
