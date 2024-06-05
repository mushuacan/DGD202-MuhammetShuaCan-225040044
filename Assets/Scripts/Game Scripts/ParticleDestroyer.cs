using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public float destroyDelay = 5f; // Partikül efektinin kaç saniye sonra yok edileceði

    void Start()
    {
        // destroyDelay saniye sonra Partikül efektini yok et
        Invoke("DestroyParticleEffect", destroyDelay);
    }

    void DestroyParticleEffect()
    {
        // Partikül efektini yok et
        Destroy(gameObject);
    }
}
