using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHitPopup : MonoBehaviour
{
    private Image randomPopupImage;
    private float duration = 2.0f;
    private AudioSource audioSource;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = (float)PlayerPrefs.GetInt("PlayerTargetsHit") / 3;
        Sprite[] spriteList = Resources.LoadAll<Sprite>("TargetHitImages");
        randomPopupImage = GetComponentInChildren<Image>();
        randomPopupImage.sprite = spriteList[Random.Range(0, spriteList.Length)];

        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraTransform);
        transform.Rotate(new Vector3(0,180,0));
        transform.Translate(0,Time.deltaTime * 0.3f,0);
        randomPopupImage.CrossFadeAlpha(0, duration, false);
    }
}
