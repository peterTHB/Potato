using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float sensitivity;

    private PlayerInput playerInput;

    private GameObject pauseMenu;

    public GameObject normalCamera;
    public GameObject ARCamera;
    public GameObject ShootingButton;
    public GameObject MovingButton;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerPrefs.SetInt("Paused", 0);
        PlayerPrefs.SetFloat("PlayerScore", 0);
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
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
        else if (PlayerPrefs.GetString("ViewingMode").Equals(""))
        {
            ARCamera.SetActive(false);
            PlayerPrefs.SetString("ViewingMode", "Normal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Paused") == 0)
        {
            pauseMenu.SetActive(false);
            ShootingButton.SetActive(true);
            MovingButton.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Paused") == 1)
        {
            pauseMenu.SetActive(true);
            ShootingButton.SetActive(false);
            MovingButton.SetActive(false);
        }

        // Check if player has died or not
        if (PlayerPrefs.GetInt("PlayerHealth") == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("FinishedScene");
        }

        if (PlayerPrefs.GetInt("Paused") == 0) {
            KeyControls();

            float xMovement = -Input.gyro.rotationRateUnbiased.x * sensitivity * Time.deltaTime;
            float yMovement = -Input.gyro.rotationRateUnbiased.y * sensitivity * Time.deltaTime;

            // Mouse
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            if (PlayerPrefs.GetString("ViewingMode").Equals("Normal"))
            {
                normalCamera.transform.Rotate(xMovement, 0f, 0f);
                normalCamera.transform.Rotate(-mouseY, 0f, 0f);
            }
            else if (PlayerPrefs.GetString("ViewingMode").Equals("AR"))
            {
                ARCamera.transform.Rotate(xMovement, 0f, 0f);
                ARCamera.transform.Rotate(-mouseY, 0f, 0f);
            }

            transform.Rotate(0f, yMovement, 0f);
            transform.Rotate(0f, mouseX, 0f);

            Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
            Vector3 moving = transform.right * input.x + transform.forward * input.y;
            controller.Move(moving * speed * Time.deltaTime);

            // Keyboard
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
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
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (PlayerPrefs.GetInt("Paused") == 0)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                PlayerPrefs.SetInt("Paused", 1);
                Cursor.lockState = CursorLockMode.None;
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

        if (Input.GetMouseButtonDown(0))
        {
          PlayerPrefs.SetString("Shooting", "Yes");
        }
    }

}


