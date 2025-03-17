using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class AATM_BasicIinteractions : MonoBehaviour
{
    public bool isAtmPositionActive = false;
    public bool isCardVisible = false;

    public GameObject creditCardGO; // reference to Credit card object

    public TeleportationAnchor atmTeleportationAnchor;
    public XRSimpleInteractable cardInteractable;


    private void Start()
    {
        if (atmTeleportationAnchor == null) {
            atmTeleportationAnchor = FindObjectOfType<TeleportationAnchor>();
        }
        
        if (atmTeleportationAnchor != null )
        {
            atmTeleportationAnchor.teleporting.AddListener(OnTeleportToAtm);
        }
        isCardVisible = false;
        creditCardGO.SetActive(isCardVisible);
    }
    private void OnDestroy()
    {
        if (atmTeleportationAnchor !=null)
        {
            atmTeleportationAnchor.teleporting.RemoveListener(OnTeleportToAtm);
        }
    }

    private void Update()
    {
        // if the user enters the AnchorPoint then the other navigation is disables
        // the card becomes visible
        // if the user moves then the settings get reseted :-)

    }

    public void UserEnteredAnchorPoint()
    {
        isAtmPositionActive = true;
        isCardVisible = true;

    }


    public void TriggerCardSlot()
    {
        if (isCardVisible) 
        {

            //creditCardGO.GetComponent<Animation>().Play();
            creditCardGO.GetComponent<Animator>().SetTrigger("CardIn");
        }
    }

    public void OnTeleportToAtm(TeleportingEventArgs args)
    {
        Debug.Log("User teleported to the ATM");
        creditCardGO.SetActive(true); // show the credit card
        isCardVisible = true; // set the bool to true
    }
}
