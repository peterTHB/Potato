using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShielderEnemyMovement : MonoBehaviour
{
    public enum ACTION { MOVEUP, MOVEDOWN, IDLE, SHIELD }

    public float rotateAroundSpeed = 0f;
    public float yAxisRange = 20f;
    private float moveSpeed = 0f;
    private float rotationSpeed = 0f;

    private LightningBoltScript lightningBoltScript;
    public GameObject model;
    private GameObject player;
    private GameObject shield;
    private GameObject currTargetProtecting;

    private ACTION currAction = ACTION.IDLE;
    private float lastChangeDirection;
    public float baseActionChangeDelay = 3f;
    public float shieldDelay = 4f;
    private float lastTimeShielded;
    private float adjustedActionDelay;
    private int randomDirection;


    // Start is called before the first frame update
    void Start()
    {
        rotateAroundSpeed = PlayerPrefs.GetFloat("RotateAroundSpeed");
        moveSpeed = PlayerPrefs.GetFloat("MoveSpeed");
        rotationSpeed = PlayerPrefs.GetFloat("RotationSpeed");

        player = GameObject.Find("Player");
        adjustedActionDelay = baseActionChangeDelay;

        lightningBoltScript = GetComponentInChildren<LightningBoltScript>();

        //reset lighting object rotations to 0 to avoid issues
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Shield"))
            {
                shield = child.gameObject;
                break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (model != null)
        {
            model.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

            if (lastChangeDirection + adjustedActionDelay <= Time.time)
            {
                randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;
                //make sure new action is not same as old
                ACTION newAction;
                do
                {
                    newAction = (ACTION)Random.Range(0, System.Enum.GetNames(typeof(ACTION)).Length);
                } while (newAction == currAction);
                adjustedActionDelay = baseActionChangeDelay + Random.Range(-1, 1);
                currAction = newAction;
                lastChangeDirection = Time.time;
            }

            if (transform.position.y >= yAxisRange)
            {
                currAction = ACTION.MOVEDOWN;
                lastChangeDirection = Time.time;
            }
            else if (transform.position.y <= 0)
            {
                currAction = ACTION.MOVEUP;
                lastChangeDirection = Time.time;
            }

            shield.transform.LookAt(player.transform);

            HandleAction();

            if (currTargetProtecting != null)
            {
                float step = 20 * Time.deltaTime; // calculate distance to move
                shield.transform.position = Vector3.MoveTowards(shield.transform.position, currTargetProtecting.transform.position, step);
            }
            else
            {
                float step = 20 * Time.deltaTime; // calculate distance to move
                shield.transform.position = Vector3.MoveTowards(shield.transform.position, this.transform.position, step);
            }
        }
        else
        {
            PlayerPrefs.SetInt("CurrentEnemies", PlayerPrefs.GetInt("CurrentEnemies") - 1);
            Destroy(this.gameObject);
        }

       
    }


    private void HandleAction()
    {
        switch (currAction)
        {
            case ACTION.MOVEDOWN:
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                transform.RotateAround(player.transform.position, new Vector3(0, randomDirection, 0), rotateAroundSpeed * Time.deltaTime);
                break;
            case ACTION.MOVEUP:
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                transform.RotateAround(player.transform.position, new Vector3(0, randomDirection, 0), rotateAroundSpeed * Time.deltaTime);
                break;
            case ACTION.SHIELD:
                ShieldTarget();
                break;
            default:
                break;
        }
    }

    private void ShieldTarget()
    {
        GameObject[] targetList = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject target in targetList)
        {
            if (!target.GetComponent<TargetScript>().isBlocked && currTargetProtecting == null)
            {
                currTargetProtecting = target;
                currTargetProtecting.GetComponent<TargetScript>().isBlocked = true;
                
            }
        }
    }

    public void StopShielding()
    {
        if (currTargetProtecting != null)
        {
            currTargetProtecting.GetComponent<TargetScript>().isBlocked = false;
            currTargetProtecting = null;
        }
    }
}
