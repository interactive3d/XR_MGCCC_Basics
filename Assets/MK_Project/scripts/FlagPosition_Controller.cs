using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction;

public class FlagPosition_Controller : MonoBehaviour
{
    public Transform targetObject; // Reference to the object to move
    public enum Axis { X, Y, Z } // Enum for axis selection
    public Axis moveAxis = Axis.X; // Default to X-axis

    public float minValue = 0f; // Minimum position value
    public float maxValue = 10f; // Maximum position value

    [Range(0f, 1f)]
    public float inputValue = 0f; // Input value between 0 and 1

    public XRKnob theWheel;

    public AudioSource polishAnthemPlayer;

    void Update() {
        if (targetObject != null) {
            float newPosition = Mathf.Lerp(minValue, maxValue, inputValue);
            Vector3 currentPosition = targetObject.localPosition;

            switch (moveAxis) {
                case Axis.X:
                    currentPosition.x = newPosition;
                    break;
                case Axis.Y:
                    currentPosition.y = newPosition;
                    break;
                case Axis.Z:
                    currentPosition.z = newPosition;
                    break;
            }

            targetObject.localPosition = currentPosition;
        }
    }

    public void SetFlagPosition() {
        inputValue = theWheel.value;
    }

    public void PlayPolishAnthem()
    {
        Debug.Log("Play the anthem");
        if(polishAnthemPlayer != null )
        {
            polishAnthemPlayer.Play();
        }
    }
    public void StopPlayingPolishAnthem()
    {
        polishAnthemPlayer.Stop();
    }
}
