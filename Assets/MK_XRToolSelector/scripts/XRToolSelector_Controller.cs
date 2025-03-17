using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class XRToolSelector_Controller : MonoBehaviour
{
    [Header("XR Controllers Data")]
    public GameObject leftHandController;
    public GameObject rightHandController;
    
    public InputActionProperty grabOnLeftController;
    public InputActionProperty grabOnRightController;


    [Header("Tools Data")]
    public GameObject toolsOffsetParent;
    public GameObject[] toolsAvailable;
    public int toolIDSelected;
    public Vector3 toolsOffsetAmount;

    private bool areToolsVisible = false;
    private int amountOfTools;


    private void Start() {
        toolsOffsetParent.transform.localPosition = toolsOffsetAmount;
    }

}
