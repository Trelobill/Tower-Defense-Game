using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public Transform turretMount;            // Rotating part of the turret
    public float turnSpeed = 5f;
    public float detectionRange = 30f;
    public float yRotationOffset = 0f;       // Manual offset in degrees (e.g. -90 for X-facing turrets)

    private Transform target;

    void Update()
    {
        // Find the closest enemy each frame
        FindClosestEnemy();

        // If we found one, rotate toward it
        if (target != null)
        {
            Vector3 direction = target.position - turretMount.position;
            direction.y = 0f; // Only rotate around Y

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                // Apply manual Y-axis offset
                Quaternion adjustedRotation = lookRotation * Quaternion.Euler(0, yRotationOffset, 0);
                turretMount.rotation = Quaternion.Slerp(turretMount.rotation, adjustedRotation, Time.deltaTime * turnSpeed);
            }
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float minDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance && dist <= detectionRange)
            {
                minDistance = dist;
                closest = enemy.transform;
            }
        }

        target = closest;
    }
}
