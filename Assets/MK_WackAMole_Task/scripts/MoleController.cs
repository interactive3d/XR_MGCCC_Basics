using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour {
    public float popUpTime = 1.5f; // Time mole stays up
    public float popDownTime = 1.0f; // Time before next pop up
    public Animator animator;
    public bool isUp = false;
    public bool canBeHit = false;
    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.Instance;
        PopUp();
        // StartCoroutine(PopUpRoutine());
    }


    private IEnumerator PopUpRoutine() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(1f, popDownTime)); // Random delay
            if (gameManager.CanMoleAppear()) {
                PopUp();
                yield return new WaitForSeconds(popUpTime);
                if (isUp) {
                    gameManager.MoleMissed();
                }
               PopDown();
            }
        }
    }

    private void PopUp() {
        isUp = true;
        animator.SetTrigger("PopUp");
        canBeHit = true;
        // gameManager.MoleAppeared();
    }

    private void PopDown() {
        isUp = false;
        canBeHit = false;
        animator.SetTrigger("PopDown");
        // gameManager.MoleDisappeared();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hammer") && canBeHit) {
            canBeHit = false;
            gameManager.MoleHit();
            PopDown();
        }
    }
}
