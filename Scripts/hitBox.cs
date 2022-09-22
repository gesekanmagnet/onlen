using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D hitBox)
    {
        if(hitBox.tag == "Enemy")
        {
            Debug.Log("Hit");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
