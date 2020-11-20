using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillationScript : MonoBehaviour
{
    [SerializeField]Vector3 Movementvector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    [Range(0, 1)] [SerializeField] float movementfactor;
    Vector3 startingpos;

    // Start is called before the first frame update
    void Start()
    {
        startingpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / 2;
        const float tau = Mathf.PI * 2f;
        float sinwave = Mathf.Sin(cycles * tau);

        movementfactor = sinwave / 2f;
        Vector3 offset = movementfactor * Movementvector;
        transform.position = startingpos + offset;

    }
}
