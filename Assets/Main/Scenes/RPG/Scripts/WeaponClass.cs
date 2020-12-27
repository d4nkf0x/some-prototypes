using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
  public int id;
  public string name;
  public GameObject gripsparent;
  public GameObject magazine;
  public AudioClip[] audioclips = new AudioClip[4];
  private GameObject clonemag;
  private AudioSource audiosource;
  public GameObject[] grips = new GameObject[6];
  public GameObject[] emitters = new GameObject[3];
  public float lightDuration = 0.02f;
  void Start()
  {
    audiosource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    if(Input.GetKeyDown(KeyCode.Q)){
      EjectMag();
    }
  }
  void FireGun(){
    audiosource.clip = audioclips[0];
    audiosource.Play();
    emitters[0].GetComponent<ParticleSystem>().Emit (1);
    StartCoroutine(MuzzleFlashLight());
    emitters[2].GetComponent<ParticleSystem>().Emit (1);
  }
  void CloneMag(){
    GetComponent<Animator>().SetBool("Reloading",true);
    audiosource.clip = audioclips[1];
    audiosource.Play();
    GameObject clone = Instantiate(magazine,magazine.transform.position,magazine.transform.rotation,gameObject.transform);
    BoxCollider clonecol = clone.GetComponent<BoxCollider>();
    Rigidbody clonerb = clone.GetComponent<Rigidbody>();
    this.clonemag = clone;
  }
  void EjectMag(){
    audiosource.clip = audioclips[2];
    audiosource.Play();
    BoxCollider clonecol = clonemag.GetComponent<BoxCollider>();
    Rigidbody clonerb = clonemag.GetComponent<Rigidbody>();
    clonerb.isKinematic = false;
    clonerb.useGravity = true;
    clonerb.AddForce(transform.forward*70f);
    clonecol.isTrigger = false;
    clonemag.GetComponent<Magazine>().ejected = true;
    clonemag.transform.SetParent(null);
  }
  void PullSlider(){
    audiosource.clip = audioclips[3];
    audiosource.Play();
    StartCoroutine(Reset());
  }
    private IEnumerator Reset () {
      yield return new WaitForSeconds(0.5f);
      GetComponent<Animator>().SetBool("Reloading",false);
    }
  private IEnumerator MuzzleFlashLight () {

      emitters[1].GetComponent<Light>().enabled = true;
    yield return new WaitForSeconds (lightDuration);
     emitters[1].GetComponent<Light>().enabled = false;
  }
}
