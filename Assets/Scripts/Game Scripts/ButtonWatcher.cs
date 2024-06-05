using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWatcher : MonoBehaviour
{
    [SerializeField] private Image imageW;
    [SerializeField] private Image imageA;
    [SerializeField] private Image imageD;
    [SerializeField] private Image imageS;
    [SerializeField] private Image imageSpace;

    [SerializeField] private Color colorNormal;
    [SerializeField] private Color colorPressed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            imageSpace.color = colorPressed;
        }
        else
        {
            imageSpace.color = colorNormal;
        }

        if (Input.GetKey(KeyCode.W))
        {
            imageW.color = colorPressed;
        }
        else
        {
            imageW.color = colorNormal;
        }
        if (Input.GetKey(KeyCode.A))
        {
            imageA.color = colorPressed;
        }
        else
        {
            imageA.color = colorNormal;
        }
        if (Input.GetKey(KeyCode.S))
        {
            imageS.color = colorPressed;
        }
        else
        {
            imageS.color = colorNormal;
        }
        if (Input.GetKey(KeyCode.D))
        {
            imageD.color = colorPressed;
        }
        else
        {
            imageD.color = colorNormal;
        }
    }
}
