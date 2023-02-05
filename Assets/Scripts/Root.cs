using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;
    private bool _wasTriggered = false;

    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        SetHeight(transform.position.y + objectHeight/2f);
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_wasTriggered) return;
        if (other.tag == "Player")
        {
            _wasTriggered = true;
            StartCoroutine(Dissolve());
        }
    }

    IEnumerator Dissolve()
    {

        var time = 0f;
        var prevHeight = transform.position.y + 0.5f;
        var currHeight = transform.position.y + 0.5f;
        while (true)
        {
            prevHeight = currHeight;
            var t = time * Mathf.PI * 0.5f;
            currHeight = transform.position.y;
            currHeight += Mathf.Cos(t) * (objectHeight / 2.0f);
            SetHeight(currHeight);
            time += Time.deltaTime;
            if (prevHeight < currHeight) break;
            else yield return null;
        }
        Destroy(gameObject);
    }

}
