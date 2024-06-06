using UnityEngine;

public class SnakeBodyFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek obje
    public float distance = 5f; // Ýki obje arasýndaki mesafe

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Takip eden objenin konumu
            Vector3 followerPosition = transform.position;

            // Takip edilen objenin konumu
            Vector3 targetPosition = target.position;

            // Takip eden objenin, takip edilen objeye doðru yönelmesi
            transform.LookAt(targetPosition);

            // Takip eden objenin, takip edilen objeye doðru hareket etmesi
            Vector3 newPosition = targetPosition - transform.forward * distance;

            // Yeni konumu uygula
            transform.position = newPosition;
        }
    }
}

