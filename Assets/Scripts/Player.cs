using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //, IScoreChange
{
    // Singleton pattern
    public static Player Instance; /**{ get; private set; }**/

    public event EventHandler OnFirstStart;

    [SerializeField] public bool isPlaying;

    private PlayerInputActions playerInputActions;

    public Transform playerVisual;

    private Vector3 curScreenPos;
    private Vector3 WorldPos
    {
        get
        {
            float z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(curScreenPos + new Vector3(0, 0, z));
        }
    }

    //private float playerScore = 0f;
    private float minX, maxX;
    private float boundCoor = 0.5f;

    private bool isDragging;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.ScreenPos.performed += ScreenPos_performed;
    }

    private void Start()
    {
        playerInputActions.Player.Press.performed += Press_performed;

        playerInputActions.Player.Press.canceled += Press_canceled;

        Vector3 worldScreenCoordinate = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = -worldScreenCoordinate.x + boundCoor;
        maxX = worldScreenCoordinate.x - boundCoor;

        OnFirstStart?.Invoke(this, EventArgs.Empty);
        isPlaying = false;

        playerVisual = gameObject.transform.Find("Player_Visual");
        playerVisual.gameObject.SetActive(false);
    }

    private void ScreenPos_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        curScreenPos = obj.ReadValue<Vector2>();
    }

    private void Press_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        StartCoroutine(Drag());
    }

    private void Press_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isDragging = false;
        StopCoroutine(Drag());
    }

    private IEnumerator Drag()
    {
        isDragging = true;

        while (isDragging && isPlaying)
        {
            // dragging player

            transform.position = new Vector3(WorldPos.x, transform.position.y, transform.position.z);

            Vector3 tempPos = transform.position;

            if (tempPos.x > maxX)
            {
                tempPos.x = maxX;
            }

            if (tempPos.x < minX)
            {
                tempPos.x = minX;
            }

            transform.position = tempPos;

            yield return null;
        }
    }
}
