using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterControls : MonoBehaviour
{
  private Animator anim;
  private bool directionset=true;
  private float m_vertical;
  private float m_horizontal;
  [Header("Character States")]
  public bool fpsmode = false;
  public bool isAiming = false;
  public bool hasWeapon = true;
  [Header("Weapon Objects")]
  public GameObject weapon;
  public GameObject weaponIK;
  private Animator weapon_animator;
  private Animator weaponIK_animator;
    private AudioSource weapon_audio;
  [Header("Weapon Rigs")]
  public GameObject ARaimRig;
  private Rig ARaimRig_rig;
  [Header("Camera Rigs")]
  public GameObject cam;
  public GameObject camfps;
  public GameObject camtps;
  /*[Header("Audio Clips")]
  [Tooltip("The audio clip that is played while walking."), SerializeField]
  private AudioClip walkingSound;
  [Tooltip("The audio clip that is played while running."), SerializeField]
  private AudioClip runningSound;
  */
  private Rigidbody _rigidbody;
  private CapsuleCollider _collider;
  private AudioSource _audioSource;
  private bool _isGrounded;

  private readonly RaycastHit[] _groundCastResults = new RaycastHit[8];
  private readonly RaycastHit[] _wallCastResults = new RaycastHit[8];

  void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
    _collider = GetComponent<CapsuleCollider>();
    _audioSource = GetComponent<AudioSource>();
    weapon_animator = weapon.GetComponent<Animator>();
    weaponIK_animator = weaponIK.GetComponent<Animator>();
    weapon_audio = weapon.GetComponent<AudioSource>();
    anim = GetComponent<Animator>();
    ARaimRig_rig = ARaimRig.GetComponent<Rig>();
    if(fpsmode){
      setCameraWeights(true);
      setLayerWeights(true);
    }else{
      setCameraWeights(false);
      setLayerWeights(true);
    }
  }


  /*  private void OnCollisionStay()
  {
  var bounds = _collider.bounds;
  var extents = bounds.extents;
  var radius = extents.x - 0.01f;
  Physics.SphereCastNonAlloc(bounds.center, radius, Vector3.down,
  _groundCastResults, extents.y - radius * 0.5f, ~0, QueryTriggerInteraction.Ignore);
  if (!_groundCastResults.Any(hit => hit.collider != null && hit.collider != _collider)) return;
  for (var i = 0; i < _groundCastResults.Length; i++)
  {
  _groundCastResults[i] = new RaycastHit();
}

_isGrounded = true;
}*/

// Update is called once per frame
void Update()
{
  float h = Input.GetAxis ("Horizontal");
  float v = Input.GetAxis ("Vertical");
  float mag = new Vector2(h,v).sqrMagnitude;
  anim.SetFloat("magnitude",mag);
  anim.SetFloat("vertical",v);//Check Forward and Back
  anim.SetFloat("horizontal",h);//Check Right and Left
  checkAimTrigger();  //Checks "Fire2" or "Right Click" Aim
  checkRunTrigger();    //Checks "LeftShift" or "Run Trigger" Triggers "RunBool" in the Animator
  setCheckDirections();
  if(fpsmode){ //FirstPerson Block
    //Rotate character to the Camera Forward
    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
  }else{//ThirdPerson Block
    checkInputT(); //The CheckInput for ThirdPerson Mode
  }
  if(!weapon_animator.GetBool("Reloading")){
      switchModecheck();  //Switch to ThirdPerson or FirstPerson Using KeyCode "F"
  }

}
private void checkRunTrigger(){
  if(Input.GetKeyDown(KeyCode.LeftShift)){
    anim.SetBool("RunBool",true);
  }
  if(Input.GetKeyUp(KeyCode.LeftShift)){
    anim.SetBool("RunBool",false);

  }
}
private void checkAimTrigger(){
  if(Input.GetButton("Fire2")){
    anim.SetFloat("directiony",0f);
    anim.SetFloat("directionx",0f);
    setLayerWeightsAim(true);
      isAiming = true;
    anim.SetBool("isAiming",true);
    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
    if(Input.GetKeyDown(KeyCode.R)){
      weapon_animator.SetTrigger("Reload");
      weaponIK_animator.SetTrigger("Reload");
    }
    if(Input.GetButtonDown("Fire1")||Input.GetButton("Fire1")){

      weapon_animator.SetBool("Fire",true);
      weaponIK_animator.SetBool("Fire",true);
    }
    if(Input.GetButtonUp("Fire1")){
      StartCoroutine(stopFire());
    }
  }
  if(Input.GetButtonUp("Fire2")){
    if(weapon_animator.GetBool("Reloading")){
      StartCoroutine(stopAimDelayed());
    }else{
        anim.SetBool("isAiming",false);
            setLayerWeightsAim(false);
    }
    weapon_animator.SetBool("Fire",false);
    weaponIK_animator.SetBool("Fire",false);
    //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
  }
}
private IEnumerator stopAimDelayed() {
  yield return new WaitForSeconds(1);
  isAiming = false;
  anim.SetBool("isAiming",false);
    setLayerWeightsAim(false);
}

private IEnumerator stopFire() {

  yield return new WaitForSeconds (0.01f);
  weapon_animator.SetBool("Fire",false);
  weaponIK_animator.SetBool("Fire",false);
}
private void checkInputT(){
  if(isAiming){
    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
  }
  else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
    directionset = false;
  }
}
private void setCheckDirections(){
  if(!directionset){
    m_vertical = Input.GetAxis("Vertical");
    m_horizontal = Input.GetAxis("Horizontal");
    anim.SetFloat("directiony",m_vertical);
    anim.SetFloat("directionx",m_horizontal);
    directionset = true;
  }
}
private void switchModecheck(){

  if(Input.GetKeyDown(KeyCode.F)){
    if(!fpsmode){
      fpsmode=true;
      setCameraWeights(true);
      setLayerWeights(true);
    }
    else{
      fpsmode=false;
      setCameraWeights(false);
      setLayerWeights(false);
    }
  }
}
private void setCameraWeights(bool tofpsmode){
  if(!tofpsmode){

    camtps.SetActive(true);
    camfps.SetActive(false);
  }else{

    camfps.SetActive(true);
    camtps.SetActive(false);
    anim.SetFloat("directiony",0f);
    anim.SetFloat("directionx",0f);
  }
}
private void setLayerWeights(bool tofpsmode){

  if(hasWeapon && !tofpsmode){
    anim.SetLayerWeight(4,0f);
    anim.SetLayerWeight(3,1f);
    anim.SetLayerWeight(1,0f);
    anim.SetLayerWeight(2,0f);
  }else if(!hasWeapon && !tofpsmode){
    anim.SetLayerWeight(4,0f);
    anim.SetLayerWeight(3,0f);
    anim.SetLayerWeight(1,1f);
    anim.SetLayerWeight(2,0f);
  }
  if(hasWeapon && tofpsmode){
    anim.SetLayerWeight(4,1f);
    anim.SetLayerWeight(3,0f);
    anim.SetLayerWeight(1,0f);
    anim.SetLayerWeight(2,0f);
  }else if(!hasWeapon && tofpsmode){
    anim.SetLayerWeight(4,0f);
    anim.SetLayerWeight(3,0f);
    anim.SetLayerWeight(1,0f);
    anim.SetLayerWeight(2,1f);
  }
}
private void setLayerWeightsAim(bool x){
  if(x){
    if(fpsmode){

    }else{
      anim.SetLayerWeight(4,1f);
      anim.SetLayerWeight(3,0f);
    }
  }else{
    if(fpsmode){

    }else{
      anim.SetLayerWeight(4,0f);
      anim.SetLayerWeight(3,1f);
    }
  }
}
private void PlayFootsteps(){
  if(_audioSource.isPlaying){

  }else{
      _audioSource.Play();
  }
}
private void StopFootsteps(){
  _audioSource.Stop();
}
}
