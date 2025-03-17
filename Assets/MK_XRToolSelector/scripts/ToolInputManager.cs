using UnityEngine;
using UnityEngine.InputSystem;

public class ToolInputManager : MonoBehaviour {
    public InputActionAsset inputActionAsset;
    private InputActionMap toolActionMap;

    void Start() {
        toolActionMap = inputActionAsset.FindActionMap("ToolActions");
        toolActionMap.Disable();
    }

    public void ActivateToolInput() {
        toolActionMap.Enable();
    }

    public void DeactivateToolInput() {
        toolActionMap.Disable();
    }
}
