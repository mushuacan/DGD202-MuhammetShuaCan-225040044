using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Giri� ger�ekle�ti");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player g�r�ld�");
            gameObject.SetActive(false);
        }
    }
}
