using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class MainMenuBehaviour : MonoBehaviour
{
        private StarterAssetsInputs _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.interact)
        {
            Debug.Log("Hola");
            SceneManager.LoadScene("Main");
        }
    }
}
