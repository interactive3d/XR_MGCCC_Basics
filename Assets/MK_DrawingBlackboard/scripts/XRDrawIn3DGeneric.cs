using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class XRDrawIn3DGeneric : MonoBehaviour {

    [Header("Pen Properties")]
    [Tooltip("Reference to the prefab representing a pen")]
    public GameObject penObject; // reference to the prefab that will be instanciated when a pen tool is choosen
    [Tooltip("This is the point from which all the drawing will appear")]
    public Transform drawingPoint; // position that will be used to draw
    [Tooltip("Preferably a Particle Unlit type material")]
    public Material drawingMaterial; //
    [Tooltip("Visible material representing the color that will be used in the drawing")]
    public Material penMaterial; // material of the pen that will be used to draw and display color

    [Range(0.001f, 0.1f)]
    [Tooltip("Width of the drawn elements as LineRenderer")]
    public float penWidth = 0.01f;

    [Tooltip("Colors array that will be used and triggered when a color is changed")]
    public Color[] penColors;

    [Header("Hands and Grabbable")]
    [Tooltip("Reference to the marker object with XRGrabInteractable")]
    public XRGrabInteractable grabbale; // reference to XR Grab Interactable added to the Pen/Marker in the scene

    [Header("Controller elements")]
    [Tooltip("This reference Action to trigger color change of the market")]
    public InputActionProperty changeColorActionButton; // reference to Action responsible for triggering the next color
    public InputActionProperty clearScreenActionButton; // reference to Action responsible for clearing/destroying all drawing
    public InputActionProperty triggerOnRighController; // action that will trigger the paining on the right controller / hand
    public InputActionProperty triggerOnLeftController; // action that will trigger the paining on the left controller / hand
    public InputActionProperty grabbButtonRightController; // action responsible for grabbing on the right controller / hand
    public InputActionProperty grabbButtonLeftController; // action responsible for grabbing on the left controller / hand

    public InputActionMap drawingActionMap; // potentially the action map that will be activated when a Pen/Marker is holded
    

    private LineRenderer currentDrawing;
    private GameObject drawingContrainer;
    private int index;
    private int currentColorIndex;
    private bool isGrabbed;

    private void Start() {
        currentColorIndex = 0;
        penMaterial.color = penColors[currentColorIndex];
        // grabbale = GetComponent<XRGrabInteractable>();
        drawingContrainer = new GameObject();
        drawingContrainer.name = "Drawing_Container";
    }


    private void Update() {
        bool isRightHandDrawing = isGrabbed && triggerOnRighController.action.IsPressed();   // here also add elements to determine that this is right hand also if trigger is pressed
        bool isLeftHandDrawing = isGrabbed && triggerOnLeftController.action.IsPressed(); // same as above but for the left hand also if trigger is pressed

        if ((grabbButtonRightController.action.IsPressed() || grabbButtonLeftController.action.IsPressed()) && grabbale.interactorsSelecting.Count > 0) {
            isGrabbed = true;
        } else {
            isGrabbed = false;
        }

        if (isRightHandDrawing || isLeftHandDrawing) {
            Draw();
        } else if (currentDrawing != null) {
            currentDrawing = null;
        } else if (changeColorActionButton.action.WasPerformedThisFrame())
        {
            SwitchColor();
        }

        if (isGrabbed && clearScreenActionButton.action.WasCompletedThisFrame()) {
            ClearDrawings();
        }
    }

    private void SwitchColor() {
        if(currentColorIndex == penColors.Length - 1) {
            currentColorIndex = 0;
        } else {
            currentColorIndex++;
        }
        penMaterial.color = penColors[currentColorIndex];
    }

    private void Draw() {
        if(currentDrawing == null) {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.name = "Line" + index;
            currentDrawing.transform.parent = drawingContrainer.transform;
            currentDrawing.material = drawingMaterial;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, drawingPoint.position);
        } 
        else {
            var currentPosition = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPosition, drawingPoint.position) > 0.01f) {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, drawingPoint.position);
                // currentDrawing.material = drawingMaterial;
            }
        }
    }

    public void ClearDrawings() {
        // this should remove all the drawings
        // delete all object in drawingContrainer
        Debug.Log("All elements in drawing container will be removed");
        foreach (Transform child in drawingContrainer.transform) {
            Destroy(child.gameObject);
        }
    }
}   



