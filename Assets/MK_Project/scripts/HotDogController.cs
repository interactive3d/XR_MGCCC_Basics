using UnityEngine;
using System.Collections;

public class HotDogController : MonoBehaviour {
    [SerializeField] private Renderer sauceRenderer; // Renderer of the sauce object
    [SerializeField] private Material mayoMaterial;
    [SerializeField] private Material ketchupMaterial;
    [SerializeField] private Material mustardMaterial;
    [SerializeField] private float fadeDuration = 1.5f; // Time to fade in

    private Material currentMaterial;
    private Coroutine fadeCoroutine;
    private Color originalColor;

    public bool changeSouce = false;
    [Range(0,2)]
    public int souceType;
    private void Start() {
        if (sauceRenderer == null) {
            Debug.LogError("Sauce Renderer is not assigned!");
            return;
        }

        // Ensure the sauce starts invisible
        originalColor = sauceRenderer.material.color;
        SetSauceVisibility(0);
    }


    private void Update() {
        if (changeSouce) {
            switch (souceType) {
                case 0:
                    OnMayo();
                    break;
                case 1:
                    OnKetchup();
                    break;
                case 2:
                    OnMustard();
                    break;
            }
            changeSouce = false;
        }
        
    }
    public void OnMayo() {
        ChangeSauce(mayoMaterial);
    }

    public void OnKetchup() {
        ChangeSauce(ketchupMaterial);
    }

    public void OnMustard() {
        ChangeSauce(mustardMaterial);
    }

    private void ChangeSauce(Material newMaterial) {
        if (newMaterial == null || sauceRenderer == null)
            return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // Apply new material
        sauceRenderer.material = newMaterial;
        currentMaterial = newMaterial;

        // Start fading in the sauce
        fadeCoroutine = StartCoroutine(FadeInSauce());
    }

    private IEnumerator FadeInSauce() {
        float elapsedTime = 0f;
        Color color = sauceRenderer.material.color;
        color.a = 0f;
        sauceRenderer.material.color = color;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, originalColor.a, elapsedTime / fadeDuration);
            sauceRenderer.material.color = color;
            yield return null;
        }

        color.a = originalColor.a;
        sauceRenderer.material.color = color;
    }

    private void SetSauceVisibility(float alpha) {
        if (sauceRenderer != null && sauceRenderer.material != null) {
            Color color = sauceRenderer.material.color;
            color.a = alpha;
            sauceRenderer.material.color = color;
        }
    }
}
