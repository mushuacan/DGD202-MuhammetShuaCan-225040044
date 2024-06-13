using UnityEngine;
using System.Collections;

public class EndedGameCameraScript : MonoBehaviour
{
    public Transform target; // Pivot noktasý
    public float distance = 5.0f; // Hedefe olan mesafe
    public float ySpeed = 120.0f; // Yatay eksende dönüþ hýzý
    public float transitionDuration = 2.0f; // Geçiþ süresi

    private float y = 0.0f;
    private bool gameEnded = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        y = angles.x;
    }

    public void EndGame()
    {
        gameEnded = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        y = transform.eulerAngles.y;
        StartCoroutine(TransitionToTarget());
    }

    IEnumerator TransitionToTarget()
    {
        float elapsedTime = 0.0f;
        //Vector3 targetPosition = target.position - target.forward * distance;
        Vector3 targetPosition = Quaternion.Euler(30, y, 0) * new Vector3(0.0f, 0.0f, -distance) + target.position;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - targetPosition);

        while (elapsedTime < transitionDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / transitionDuration);
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / transitionDuration);

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        StartCoroutine(OrbitCamera());
    }

    IEnumerator OrbitCamera()
    {
        while (gameEnded)
        {
            y += ySpeed * Time.unscaledDeltaTime;

            Quaternion rotation = Quaternion.Euler(30, y, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;


            
            transform.rotation = rotation;
            transform.position = position;

            yield return null;
        }
    }
}
