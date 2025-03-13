using UnityEngine;

public class sauce_controller : MonoBehaviour
{
    public Material ketchupMaterial;
    public Material mayoMaterial;
    public Material mustardMaterial;

    public MeshRenderer sauceObjectRenderer;

    [Range(0, 2)]
    public int whatToSetup = 0;
    public bool changeSauce = false;

    private void Start()
    {
        SetSaouceOnHotDog("ketchup");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SauceBottle"))
        {
            Debug.Log("The sauce " + other.gameObject.name + " is place on the hotdog");
            SetSaouceOnHotDog(other.gameObject.name.ToString());
        }
    }

    private void Update()
    {
        if (changeSauce)
        {
            if(whatToSetup == 0)
            {
                SetSaouceOnHotDog("ketchup");
            }
            else if (whatToSetup == 1)
            {
                SetSaouceOnHotDog("mayo");
            }
            else if(whatToSetup == 2)
            {
                SetSaouceOnHotDog("mustard");
            }
            changeSauce = false;
        }
    }
    public void OnKetchupSelected()
    {
        whatToSetup = 0;
    }
    public void OnMayoSelected()
    {
        whatToSetup = 1;
    }
    public void OnMustardSelected()
    {
        whatToSetup = 2;
    }

    public void SetSaouceOnHotDog(string sauceName)
    {
        Material sauceToSet = sauceObjectRenderer.material; 
        switch(sauceName)
        {
            case "ketchup":
                sauceToSet = ketchupMaterial;
                break;
            case "mayo":
                sauceToSet = mayoMaterial;
                break;
            case "mustard":
                sauceToSet = mustardMaterial;
                break;
        }
        SetMaterialToRenderer(sauceToSet, sauceObjectRenderer);
    }

    private void SetMaterialToRenderer(Material myMaterial, MeshRenderer myRenderer)
    {
        myRenderer.material = myMaterial;
    }

    public void MakeAChangeOfTheSauce()
    {
        changeSauce = true;
    }
}
