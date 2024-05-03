using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AxeItem : MonoBehaviour
{
    [SerializeField] private int axeDamage = 5;
    [SerializeField] private float hitCooldown = 1f; // Adjust this in the Inspector

    private bool canHit = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canHit)
        {
            StartCoroutine(HitCooldownRoutine());
            Hit();
        }
    }

    IEnumerator HitCooldownRoutine()
    {
        canHit = false;
        yield return new WaitForSeconds(hitCooldown);
        canHit = true;
    }

    void Hit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.GetComponent<TreeHealth>())
            {
                hit.collider.GetComponent<TreeHealth>().takeDamage(axeDamage, transform.root.gameObject);
            }
        }
    }
}
