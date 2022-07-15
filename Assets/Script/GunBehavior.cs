using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
   private int numberOfBullet;
   
    // Start is called before the first frame update
    void Start()
    {
        numberOfBullet = Random.Range(1,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
