using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public Transform turretMount;            // Rotating part of the turret
    public float turnSpeed = 5f;
    public float detectionRange = 30f;
    public float yRotationOffset = 0f;       // Manual offset in degrees (e.g. -90 for X-facing turrets)

    private Transform target;
    private TowerBehaviour towerBehaviour;

    void Awake()
    {
        towerBehaviour = GetComponent<TowerBehaviour>();
    }

    void Update()
    {
        FindTargetByType();

        if (target != null)
        {
            Vector3 direction = target.position - turretMount.position;
            direction.y = 0f; // Only rotate around Y

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Quaternion adjustedRotation = lookRotation * Quaternion.Euler(0, yRotationOffset, 0);
                turretMount.rotation = Quaternion.Slerp(turretMount.rotation, adjustedRotation, Time.deltaTime * turnSpeed);
            }
        }
    }

    void FindTargetByType()
    {
        if (towerBehaviour == null) return;

        Enemy enemy = TowerTargeting.GetTarget(towerBehaviour, towerBehaviour.targetType);
        if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= detectionRange)
            target = enemy.transform;
        else
            target = null;
    }
}
