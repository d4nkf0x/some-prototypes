using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    private float despawntime = 10f;
    public bool ejected;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      if(ejected){
        StartCoroutine(Despawn());
      }
    }
    private IEnumerator Despawn () {
      yield return new WaitForSeconds (despawntime);
      GameObject.Destroy(gameObject);
    }
}
