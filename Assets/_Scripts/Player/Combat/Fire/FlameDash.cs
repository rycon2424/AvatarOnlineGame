using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDash : Moves
{
    public GameObject dashEffect;
    public GameObject flameTrail;
    public int _damage;

    public void EnableEffect()
    {
        dashEffect.SetActive(true);
    }

    public void DisableEffect()
    {
        Invoke("DisableDashEffect", 1.5f);
    }

    void DisableDashEffect()
    {
        dashEffect.SetActive(false);
    }

    void DashDamage()
    {
        RaycastHit hit;

        Vector3 rayOrigin = transform.position + transform.forward * 0.1f + transform.up * 0.5f;
        Debug.DrawRay(rayOrigin, transform.forward * 1.35f, Color.blue, 5);
        if (Physics.Raycast(rayOrigin, transform.forward, out hit, 1.35f))
        {
            if (hit.collider.tag != "")
            {
                Debug.Log("Raycast hit " + hit.collider.name);
                PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(_damage);
                }
            }
        }
    }
    
}