using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolManager : MonoBehaviour {
    public GameObject[] toolPrefabs; // Assign 5 tool prefabs
    public Transform controllerTransform; // Assign dynamically
    public GameObject controllerVisual; // Default controller model
    public InputActionReference grabAction; // Assign in Inspector

    private GameObject[] instantiatedTools;
    private GameObject activeTool;
    private bool isSelecting = false;


    GameObject myVisuals;

    void Start() {
        grabAction.action.started += OnGrabStarted;
        grabAction.action.canceled += OnGrabReleased;
        myVisuals = new GameObject();
    }

    void OnDestroy() {
        grabAction.action.started -= OnGrabStarted;
        grabAction.action.canceled -= OnGrabReleased;
    }

    void OnGrabStarted(InputAction.CallbackContext context) {
        if (!isSelecting) ShowTools();
    }

    void OnGrabReleased(InputAction.CallbackContext context) {
        if (isSelecting) HideTools();
    }

    void ShowTools() {
        isSelecting = true;
        instantiatedTools = new GameObject[toolPrefabs.Length];

        for (int i = 0; i < toolPrefabs.Length; i++) {
            Vector3 toolPosition = controllerTransform.position + controllerTransform.forward * 0.2f + controllerTransform.right * (i - 2) * 0.2f;
            instantiatedTools[i] = Instantiate(toolPrefabs[i], toolPosition, Quaternion.identity);
            instantiatedTools[i].transform.LookAt(controllerTransform);
        }
    }

    void HideTools() {
        foreach (var tool in instantiatedTools) {
            if (tool) Destroy(tool);
        }
        instantiatedTools = null;
        isSelecting = false;
    }

    void Update() {
        if (isSelecting && instantiatedTools != null) {
            foreach (var tool in instantiatedTools) {
                if (Vector3.Distance(controllerTransform.position, tool.transform.position) < 0.05f) {
                    SelectTool(tool);
                    break;
                }
            }
        }
    }

    void SelectTool(GameObject tool) {
        HideTools();
        // hide other tools
        
        DestroyChildren(myVisuals.transform);
        myVisuals.transform.parent = controllerTransform.transform;
        myVisuals.transform.name = "ToolsContainer";
        activeTool = Instantiate(tool, controllerTransform.position, controllerTransform.rotation, myVisuals.transform);
        // activeTool.transform.parent = myVisuals.transform;
        bool a = true;
        if (a) {
            // make only one active
            myVisuals.SetActive(true);
            controllerVisual.SetActive(false);
        } else {
            // destroy all visuals
            myVisuals.SetActive(false);
            controllerVisual.SetActive(true);
        }
        

        // Enable tool-specific input map
        FindObjectOfType<ToolInputManager>().ActivateToolInput();
    }


    public void DestroyChildren(Transform rootTransform) {
        foreach (Transform child in rootTransform) {
            Destroy(child.gameObject);
        }
    }
}
