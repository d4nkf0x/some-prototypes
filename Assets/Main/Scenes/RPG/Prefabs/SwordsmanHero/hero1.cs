using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero1 : MonoBehaviour
{
  private Animator anim;
  private bool directionset=true;
  private float m_vertical;
  private float m_horizontal;
  private float combotime;
  private int combostreak;
  public float JumpHeight = 2f;
  public AudioClip[] audioclips = new AudioClip[2];
  public LayerMask mask;
  /*[Header("Character States")]
  public bool fpsmode = false;
  public bool isAiming = false;
  public bool hasWeapon = true;*/
  /*  [Header("Weapon Objects")]
  public GameObject weapon;
  public GameObject weaponIK;
  private Animator weapon_animator;
  private Animator weaponIK_animator;
  private AudioSource weapon_audio;*/
  /*  [Header("Weapon Rigs")]
  public GameObject ARaimRig;
  private Rig ARaimRig_rig;*/
  /*[Header("Camera Rigs")]
  public GameObject cam;
  public GameObject camfps;
  public GameObject camtps;*/

  private Rigidbody _rigidbody;
  private CapsuleCollider _collider;
  private AudioSource _audioSource;
  private bool _isGrounded;
  private bool jump;
  private bool layerset = false;
  private Vector3 _inputs = Vector3.zero;
  public float Speed = 5f;
  public GameObject bip;
  private float mag;

  private readonly RaycastHit[] _groundCastResults = new RaycastHit[8];
  private readonly RaycastHit[] _wallCastResults = new RaycastHit[8];
  void Start()
  {
    _audioSource = GetComponent<AudioSource>();
    _rigidbody = GetComponent<Rigidbody>();
    _collider = GetComponent<CapsuleCollider>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {

  //  Debug.DrawRay(transform.position, -transform.up * 0.5f, Color.red);
  //Debug.Log(mag);

    if(combotime > 0f){
      combotime -= Time.deltaTime;
    }
    float h = Input.GetAxis ("Horizontal");
    float v = Input.GetAxis ("Vertical");
    _inputs = Vector3.zero;
    _inputs.x = h;
    _inputs.z = v;
    mag = Mathf.Sin(new Vector2(h,v).sqrMagnitude);
    anim.SetFloat("magnitude",mag);
    anim.SetFloat("vertical",v);//Check Forward and Back
    anim.SetFloat("horizontal",h);//Check Left and Right
    setCheckDirections();
    if(Input.GetButtonDown("Fire1")){
      combotime = 0.75f;
      combostreak++;
      anim.SetInteger("combostreak",combostreak);
      //anim.SetFloat("directiony",0f);
      //anim.SetFloat("directionx",0f);
      transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

    }else{
      if(combostreak > 4 || combotime < 0f){
        combostreak = 0;
      }
      checkInputT();
      anim.SetInteger("combostreak",combostreak);
    }
  }
  void FixedUpdate(){
    StartCoroutine(checkGrounded());
    if(jump){
      _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
      jump = false;
    }
    if(!_isGrounded){
    _rigidbody.MovePosition(_rigidbody.position+bip.transform.forward * (Speed*mag) * Time.fixedDeltaTime);
    }
  }
  private IEnumerator checkGrounded(){
    yield return new WaitForSeconds(0.5f);
    _isGrounded = IsGrounded();
  }
  bool IsGrounded()
  {
    RaycastHit hit;
    layerset = false;
    if (Physics.Raycast(transform.position, -transform.up, out hit, 0.3f,mask))
    {
      if(!layerset){
        anim.SetLayerWeight(1,1f);
      anim.SetLayerWeight(2,0f);
      layerset = true;
      }

    //  Debug.Log("Hit");
      return true;
    }
    else
    {
        if(!layerset){
      anim.SetLayerWeight(1,0f);
    anim.SetLayerWeight(2,1f);
      layerset = true;
  }
    //  Debug.Log("Miss");
      return false;
    }
  }

  private void resetCombo(){
    combostreak = 0;
    anim.SetInteger("combostreak",combostreak);
  }

  private void checkInputT(){
    if(Input.GetKeyDown(KeyCode.Space) && _isGrounded){
      anim.SetBool("Jump",true);
      jump = true;
    }
    if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
    //  combostreak = 0;
      transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
      directionset = false;
    }
    if(Input.GetKeyDown(KeyCode.Space)){
      anim.SetBool("Jump",false);
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
  private void swoosh(){
    _audioSource.clip = audioclips[0];
    _audioSource.Play();
  }
  private void Jump(){

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
