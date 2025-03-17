using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

using System;

public class XRDrawIn3D : MonoBehaviour {

    [Header("Pen Properties")]
    public Transform tipTransform;
    public Material drawingMaterial;
    public Material tipMaterial;

    [Range(0.001f, 0.1f)]
    public float penWidth = 0.01f;

    public Color[] penColors;

    [Header("Hands and Grabbable")]
    public XRGrabInteractable grabbale;

    [Header("Controller elements")]
    public InputActionProperty aButtonAction;
    public InputActionProperty triggerOnRighController;
    public InputActionProperty triggerOnLeftController;
    public InputActionProperty grabbButtonRightController;
    public InputActionProperty grabbButtonLeftController;
    

    private LineRenderer currentDrawing;
    private GameObject drawingContrainer;
    private int index;
    private int currentColorIndex;
    private bool isGrabbed;

    private void Start() {
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
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
        } else if (aButtonAction.action.WasPerformedThisFrame())
        {
            SwitchColor();
        }
    }

    private void SwitchColor() {
        if(currentColorIndex == penColors.Length - 1) {
            currentColorIndex = 0;
        } else {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
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
            currentDrawing.SetPosition(0, tipTransform.position);
        } 
        else {
            var currentPosition = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPosition, tipTransform.position) > 0.01f) {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tipTransform.position);
                // currentDrawing.material = drawingMaterial;
            }
        }
    }
}   



