using UnityEngine;
using System.Collections;

public class MissHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayMiss()
    {
        GetComponent<AudioSource>().Play();
    }
}
