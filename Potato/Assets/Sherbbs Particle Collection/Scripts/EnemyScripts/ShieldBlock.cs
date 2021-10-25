using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    public Material material;
    public GameObject ricochetParticles;
    private AudioSource audioSource;
    private AudioClip[] ricochetSoundEffects;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        ricochetSoundEffects = Resources.LoadAll<AudioClip>("RicochetSound");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            audioSource.PlayOneShot(ricochetSoundEffects[Random.Range(0, ricochetSoundEffects.Length)]);
            GameObject tempParticle = Instantiate<GameObject>(ricochetParticles);
            tempParticle.transform.position = collision.transform.position;

            Destroy(collision.gameObject);
            StartCoroutine(ColorChange());
        }
    }

    private IEnumerator ColorChange()
    {
        material.SetColor("_Color", Color.red * 0.5f);

        yield return new WaitForSeconds(1.5f);

        material.SetColor("_Color", Color.blue * 0.5f);
    }
}
