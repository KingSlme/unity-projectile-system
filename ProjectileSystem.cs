using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    public static void CreateProjectile(Transform projectilePrefab, Vector3 startPosition, Vector3 targetPosition)
    {
        Transform projectileObject = Instantiate(projectilePrefab, startPosition, Quaternion.identity);
        if (projectileObject.TryGetComponent(out Projectile projectile))
            projectile.Setup(startPosition, targetPosition);
        else
            Debug.LogWarning($"{projectilePrefab.name} has no Projectile component!");
    }
}