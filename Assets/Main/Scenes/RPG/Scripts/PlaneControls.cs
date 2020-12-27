using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControls : MonoBehaviour
{
  private Animator anim;
  private Rigidbody rb;
  private bool directionset;
  private float m_vertical;
  private float m_horizontal;
  public GameObject cam;
  public float speedmod = 20f;
  public GameObject characterobject;
  void Start()
  {
    anim = characterobject.GetComponent<Animator>();
    rb = characterobject.GetComponent<Rigidbody>();
  }
  // Update is called once per frame
  void Update()
  {
    anim.SetFloat("vertical",Input.GetAxis("planefwd"));
    anim.SetFloat("horizontal",Input.GetAxis("planesides"));
    anim.SetFloat("directiony",Input.GetAxis("planey"));
    anim.SetFloat("directionx",Input.GetAxis("planex"));
//  Vector3 newVector = new Vector3(-Input.GetAxis("planey"),-30f*Input.GetAxis("planesides"),-Input.GetAxis("planex"));
  //  Vector3 forceVector = new Vector3(0f,0f,Input.GetAxis("planefwd"));
    Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
    float forwardSpeed = localVelocity.z;
    rb.drag = 20*forwardSpeed/100;
        Debug.Log(rb.drag);
  //rb.AddForce(forceVector,ForceMode.Acceleration);
    //rb.AddRelativeTorque(newVector);
    transform.Rotate(-Input.GetAxis("planey"),Input.GetAxis("planesides"),-Input.GetAxis("planex"),Space.Self);
  /*  if(transform.localRotation.z>0f){
      transform.Rotate(0f,0f,-0.1f,Space.Self);
    }else if(transform.localRotation.z<0f){
        transform.Rotate(0f,0f,0.1f,Space.Self);
    }else{

    }
    if(transform.localRotation.x>0f){
      transform.Rotate(-0.1f,0f,0f,Space.Self);
    }
    else if(transform.localRotation.x<0f){
        transform.Rotate(0.1f,0f,0f,Space.Self);
    }else{

    }*/
    /*if(anim.GetFloat("vertical") > -0.1 && anim.GetFloat("vertical") < 0.1 &&  anim.GetFloat("horizontal") < 0.1 &&  anim.GetFloat("horizontal") > -0.1 && !directionset){
      m_vertical = anim.GetFloat("vertical");
     m_horizontal = anim.GetFloat("horizontal");
     anim.SetFloat("directiony",m_vertical);
     anim.SetFloat("directionx",m_horizontal);
     Debug.Log(+m_horizontal + " "+m_vertical);
     directionset = true;
    }*/
    /*  if(!directionset){
        m_vertical = Input.GetAxis("Vertical");
       m_horizontal = Input.GetAxis("Horizontal");
       anim.SetFloat("directiony",m_vertical);
       anim.SetFloat("directionx",m_horizontal);
       Debug.Log(+m_horizontal + " "+m_vertical);
       directionset = true;
      }*/

  /*  if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
      if(Input.GetKey(KeyCode.LeftShift)){
        anim.SetTrigger("running");
      }
      transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
      directionset = false;
    }
    if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)){
    }*/


    /*    if(Input.GetKeyUp(KeyCode.W)){
    anim.SetTrigger("reset");
  }
  if(Input.GetKeyUp(KeyCode.S)){
  anim.SetTrigger("reset");
}
if(Input.GetKeyUp(KeyCode.A)){
anim.SetTrigger("reset");
}
if(Input.GetKeyUp(KeyCode.D)){
anim.SetTrigger("reset");
}
if(Input.GetKey(KeyCode.W)){

if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)){
transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y-15, transform.localEulerAngles.z);
lookup();
}
else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)){
transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y+15, transform.localEulerAngles.z);
lookup();
}
else{
transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
lookup();
}
}
else if(Input.GetKey(KeyCode.S)){
if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)){
transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y-(-15+180), transform.localEulerAngles.z);
lookup();
}
else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)){
transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y-(15+180), transform.localEulerAngles.z);
lookup();
}else{
transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y+180, transform.localEulerAngles.z);
//anim.Play("turn180");
lookup();
}
}
else if(Input.GetKey(KeyCode.A)){
transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y-90, transform.localEulerAngles.z);
lookup();
}
else if(Input.GetKey(KeyCode.D)){
transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y+90, transform.localEulerAngles.z);
lookup();
}
else if(Input.GetKey(KeyCode.Space)){
jump();
}*/
}
void lookup(){
  anim.SetTrigger("run");
}

void lookright(){
  anim.Play("runright");
}
void lookleft(){
  anim.Play("runleft");
}
void lookback(){

}
void jump(){
  anim.Play("jump");
}
}
