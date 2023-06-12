using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour
{
    public LayerMask destroyableLayer; // Assign the layer in the inspector (use LayerMask and select the layer you want).

    void Update()
    {
        // Check for left mouse button click.
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the clicked point.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object.
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is on the specific layer.
                if (((1 << hit.collider.gameObject.layer) & destroyableLayer) != 0)
                {
                    // Destroy the hit object.
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}

