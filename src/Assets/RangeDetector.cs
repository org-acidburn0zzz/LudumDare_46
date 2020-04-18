using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    [SerializeField] GameObject wizardParent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wizardParent.GetComponent<WizardAttack>().ActivateWizard();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wizardParent.GetComponent<WizardAttack>().DeactivateWizard();
        }
    }
}
