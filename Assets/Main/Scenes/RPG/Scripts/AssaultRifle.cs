using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    public GameObject[] emitters = new GameObject[3];
    public float lightDuration = 0.02f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void TEST(){
      gameObject.GetComponent<AudioSource>().Play();
      emitters[0].GetComponent<ParticleSystem>().Emit (1);
      	StartCoroutine(MuzzleFlashLight());
      emitters[2].GetComponent<ParticleSystem>().Emit (1);
    }
    private IEnumerator MuzzleFlashLight () {

  		  emitters[1].GetComponent<Light>().enabled = true;
  		yield return new WaitForSeconds (lightDuration);
  		 emitters[1].GetComponent<Light>().enabled = false;
  	}

}
