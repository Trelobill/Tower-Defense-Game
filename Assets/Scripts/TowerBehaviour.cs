using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public TowerTargeting.TargetType targetType = TowerTargeting.TargetType.First;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Example: press T to cycle targeting
        {
            targetType = (TowerTargeting.TargetType)(((int)targetType + 1) % System.Enum.GetValues(typeof(TowerTargeting.TargetType)).Length);
            Debug.Log("Targeting mode: " + targetType);
        }

    }
}
