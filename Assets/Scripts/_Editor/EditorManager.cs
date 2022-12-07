using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public PlayerInputActions inputAction;

    public Camera mainCam;
    public Camera editorCam;


    public bool editorMode;

    Vector3 mousePos;
    public GameObject prefab1;
    //public GameObject prefab2;
    public GameObject item;
    public bool instantiated = false;

    ICommand command;

    //UIManager ui;

    public float unit = 10f;
    Vector3 defaultPos;

    //Will send notifications that something has happened to whoever is interested
    Subject subject = new Subject();

    //private void OnEnable()
    //{
    //    inputAction.Enable();
    //}

    //private void OnDisable()
    //{
    //    inputAction.Disable();
    //}

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        defaultPos = new Vector3(0, editorCam.transform.position.y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {


        inputAction = new PlayerInputActions();

        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Editor.EditorMode.performed += cntxt => EnterEditorMode();

        inputAction.Editor.AddItem.performed += cntxt => AddItem(1);
        inputAction.Editor.DropItem.performed += cntxt => DropItem();

        editorMode = false;

        mainCam.enabled = true;
        editorCam.enabled = false;

        //ui = GetComponent<UIManager>();

        unit = 10f;
    }

    public void EnterEditorMode()
    {
        mainCam.enabled = !mainCam.enabled;
        editorCam.enabled = !editorCam.enabled;
        //ui.ToggleEditorUI();
    }

    public void AddItem(int itemId)
    {
        if (editorMode && !instantiated)
        {
            switch (itemId)
            {
                case 1:
                    item = Instantiate(prefab1);
                    //Create boxes that can observe events and give them an event to do
                    Pallet pallet = new Pallet(item, new WhiteMat());
                    //Add the boxes to the list of objects waiting for something to happen
                    subject.AddObserver(pallet);
                    break;
               
                 
                default:
                    break;
            }
            subject.Notify();
            instantiated = true;
        }
    }

    public void DropItem()
    {
        if (editorMode && instantiated)
        {
            if (item.GetComponent<Rigidbody>())
            {
                item.GetComponent<Rigidbody>().useGravity = true;
            }
            item.GetComponent<Collider>().enabled = true;

            command = new PlaceItemCommand(item.transform.position, item.transform);
            CommandInvoke.AddCommand(command);

            instantiated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCam.enabled == false && editorCam.enabled == true)
        {
            editorMode = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            editorMode = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (instantiated)
        {
            mousePos = Mouse.current.position.ReadValue();
            mousePos = new Vector3(mousePos.x, mousePos.y, 50f);

            item.transform.position = editorCam.ScreenToWorldPoint(mousePos);

            subject.Notify();
        }

    }

    public void MoveCameraPos(string direction)
    {
        switch (direction)
        {
            case "Up":
                editorCam.transform.Translate(transform.forward * unit, Space.World);
                break;
            case "Down":
                editorCam.transform.Translate(transform.forward * -unit, Space.World);
                break;
            case "Left":
                editorCam.transform.Translate(transform.right * -unit, Space.World);
                break;
            case "Right":
                editorCam.transform.Translate(transform.right * unit, Space.World);
                break;
            case "Reset":
                editorCam.transform.position = defaultPos;
                break;
            default:
                break;
        }
    }
}
