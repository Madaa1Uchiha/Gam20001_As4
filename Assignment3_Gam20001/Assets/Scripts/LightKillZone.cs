using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightKillZone : MonoBehaviour
{
    [SerializeField] private Light2D dangerLight;
    [SerializeField] private float killThreshold = 1f;

        private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (dangerLight.intensity < killThreshold) return;

        // Now check if anything is blocking the light
        Vector2 lightPos = dangerLight.transform.position;
        Vector2 playerPos = other.transform.position;
        Vector2 direction = (playerPos - lightPos).normalized;
        float distance = Vector2.Distance(lightPos, playerPos);

        int blockingMask = LayerMask.GetMask("Ground"); // change this to match your layers

        RaycastHit2D hit = Physics2D.Raycast(lightPos, direction, distance, blockingMask);

        Debug.DrawRay(lightPos, direction * distance, hit.collider == null ? Color.green : Color.red);

        if (hit.collider == null)
        {
            Debug.Log("No obstacle - killing player");
            other.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Blocked by: " + hit.collider.name + " - player safe");
        }
    }
}