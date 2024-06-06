using UnityEngine;

public class SnakeBodyFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek obje
    public float distance = 5f; // �ki obje aras�ndaki mesafe

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Takip eden objenin konumu
            Vector3 followerPosition = transform.position;

            // Takip edilen objenin konumu
            Vector3 targetPosition = target.position;

            // Takip eden objenin, takip edilen objeye do�ru y�nelmesi
            transform.LookAt(targetPosition);

            // Takip eden objenin, takip edilen objeye do�ru hareket etmesi
            Vector3 newPosition = targetPosition - transform.forward * distance;

            // Yeni konumu uygula
            transform.position = newPosition;
        }
    }
}

