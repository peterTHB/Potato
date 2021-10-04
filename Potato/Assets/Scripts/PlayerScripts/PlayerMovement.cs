using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private static float sensitivity = 1000f;
    public CharacterController controller;
    public float gravity = -9.18f;
    public float speed = 12f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private PlayerInput playerInput;

    Vector3 velocity;
    bool isGrounded;
    bool canMove;

    private GameObject pauseMenu;

    public GameObject normalCamera;
    public GameObject ARCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetInt("Paused", 0);
        PlayerPrefs.SetFloat("PlayerScore", 0);
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        //pauseMenu.SetActive(false);
        PlayerPrefs.SetInt("MaxTargets", 3);
        PlayerPrefs.SetInt("LevelCount", 0);
        playerInput = GetComponent<PlayerInput>();
        PlayerPrefs.SetInt("PlayerTargetsHit", 0);
        Input.gyro.enabled = true;

        if (PlayerPrefs.GetString("ViewingMode").Equals("Normal"))
        {
            ARCamera.SetActive(false);
        }
        else if (PlayerPrefs.GetString("ViewingMode").Equals("AR"))
        {
            normalCamera.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Paused") == 0)
        {
            pauseMenu.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Paused") == 1)
        {
            pauseMenu.SetActive(true);
        }

        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //if (isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}

        // Check if player has died or not
        if (PlayerPrefs.GetInt("PlayerHealth") == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("FinishedScene");
        }

        KeyControls();

        //float xInput = Input.GetAxis("Mouse X");

        //xInput *= sensitivity * Time.deltaTime;

        //transform.Rotate(0f, xInput, 0f);

        //float xMovement = Input.acceleration.x;
        float xMovement = -Input.gyro.rotationRateUnbiased.x;
        float yMovement = -Input.gyro.rotationRateUnbiased.y;
        //float zMovement = -Input.gyro.rotationRateUnbiased.z;
        transform.Rotate(xMovement, yMovement, 0f);

        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 moving = new Vector3(input.x, 0, input.y);
        //moving = moving.x * cameraTransform.right + moving.z * cameraTransform.forward;
        //moving.y = 0f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(moving * speed * Time.deltaTime);
        controller.Move(move * speed * Time.deltaTime);

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        //}

        //velocity.y += gravity * Time.deltaTime;

        //controller.Move(velocity * Time.deltaTime);
    }

    private void KeyControls()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerPrefs.GetInt("Paused") == 1)
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                PlayerPrefs.SetInt("Paused", 0);
                //Cursor.lockState = CursorLockMode.Locked;
            }
            else if (PlayerPrefs.GetInt("Paused") == 0)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                PlayerPrefs.SetInt("Paused", 1);
                //Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            int currHealth = PlayerPrefs.GetInt("PlayerHealth") + 1;
            PlayerPrefs.SetInt("PlayerHealth", currHealth);
            PlayerPrefs.SetString("MakeHearts", "YES");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            int currHealth = PlayerPrefs.GetInt("PlayerHealth") - 1;
            PlayerPrefs.SetInt("PlayerHealth", currHealth);
            PlayerPrefs.SetString("MakeHearts", "YES");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (PlayerPrefs.GetInt("CurrentEnemies") > 0)
            {
                int currEnemies = PlayerPrefs.GetInt("CurrentEnemies") - 1;
                GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] enemyParentObjects = GameObject.FindGameObjectsWithTag("EnemyParent");
                GameObject.Destroy(enemyObjects[currEnemies]);
                GameObject.Destroy(enemyParentObjects[currEnemies]);

                PlayerPrefs.SetInt("CurrentEnemies", currEnemies);
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            int currTargets = PlayerPrefs.GetInt("CurrentTargets") - 1;
            GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("Target");
            GameObject[] targetParentObjects = GameObject.FindGameObjectsWithTag("TargetParent");
            GameObject.Destroy(targetObjects[currTargets]);
            GameObject.Destroy(targetParentObjects[currTargets]);

            if (currTargets == 0)
            {
                int currMaxTargets = PlayerPrefs.GetInt("MaxTargets");
                PlayerPrefs.SetInt("MaxTargets", currMaxTargets + 3);
            }

            PlayerPrefs.SetInt("CurrentTargets", currTargets);
        }
    }

}


