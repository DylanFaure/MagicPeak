using UnityEngine;

public class SelfcastSpell : MonoBehaviour
{
    [HideInInspector] public float activeTime;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= activeTime)
        {
            Destroy(gameObject);
        }
    }
}
