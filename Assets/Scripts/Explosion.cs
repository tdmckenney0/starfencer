using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public float lifeTime = 0.25f;
    public float scaleFactor = 0.25f;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("EndLife", lifeTime, lifeTime);
    }
	
	// Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }


	void EndLife()
    {
        Object.Destroy(gameObject);
    }
}
