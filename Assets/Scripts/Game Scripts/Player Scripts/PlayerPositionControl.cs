using UnityEngine;

public class PlayerPositionControl : MonoBehaviour
{
    private float minX = -13f;
    private float maxX = 13f;
    private float minZ = -13f;
    private float maxZ = 13f;
    private float minY = -5f;
    private float maxY = 15f;

    private Vector3 respawnPosition = new Vector3(0f, 2f, 0f);

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        // X ekseni s�n�rlar� kontrol�
        if (currentPosition.x < minX || currentPosition.x > maxX)
            Respawn();

        // Z ekseni s�n�rlar� kontrol�
        if (currentPosition.z < minZ || currentPosition.z > maxZ)
            Respawn();

        // Y ekseni s�n�rlar� kontrol�
        if (currentPosition.y < minY || currentPosition.y > maxY)
            Respawn();
    }

    private void Respawn()
    {
        // Oyuncuyu yeniden konumland�r
        transform.position = respawnPosition;
    }
}
