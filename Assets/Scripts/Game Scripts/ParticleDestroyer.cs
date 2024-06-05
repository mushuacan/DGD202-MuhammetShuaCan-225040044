using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public float destroyDelay = 5f; // Partik�l efektinin ka� saniye sonra yok edilece�i

    void Start()
    {
        // destroyDelay saniye sonra Partik�l efektini yok et
        Invoke("DestroyParticleEffect", destroyDelay);
    }

    void DestroyParticleEffect()
    {
        // Partik�l efektini yok et
        Destroy(gameObject);
    }
}
