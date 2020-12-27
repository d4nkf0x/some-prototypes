using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class Inventory : MonoBehaviour
{
    //  [Header("Equipped")]
    //  [Header("Stored")]
    public GameObject equipped;
    private GripsSystem grips;
    void Start()
    {
      grips = GetComponent<GripsSystem>();
      EquipWeapon(equipped);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void EquipWeapon(GameObject weapon){
      WeaponClass weaponclass = weapon.GetComponent<WeaponClass>();
      //map the grips
      //grips.gripsparent.transform.SetParent(weaponclass.gripsparent.transform);
      /*grips.grips[0].transform.SetParent(weaponclass.gripsparent.transform);
      grips.grips[1].transform.SetParent(weaponclass.gripsparent.transform);
      grips.grips[2].transform.SetParent(weaponclass.gripsparent.transform);
      grips.grips[3].transform.SetParent(weaponclass.gripsparent.transform);
      grips.grips[4].transform.SetParent(weaponclass.gripsparent.transform);
      grips.grips[5].transform.SetParent(weaponclass.gripsparent.transform);*/
      grips.grips[0].transform.SetParent(weaponclass.grips[0].transform,false);
      grips.grips[1].transform.SetParent(weaponclass.grips[1].transform,false);
      grips.grips[2].transform.SetParent(weaponclass.grips[2].transform,false);
      grips.grips[3].transform.SetParent(weaponclass.grips[3].transform,false);
      grips.grips[4].transform.SetParent(weaponclass.grips[4].transform,false);
      grips.grips[5].transform.SetParent(weaponclass.grips[5].transform,false);

    /*  grips.grips[0].transform.SetPositionAndRotation(weaponclass.grips[0].transform.position,weaponclass.grips[0].transform.rotation);
      grips.grips[1].transform.SetPositionAndRotation(weaponclass.grips[1].transform.position,weaponclass.grips[1].transform.rotation);
      grips.grips[2].transform.SetPositionAndRotation(weaponclass.grips[2].transform.position,weaponclass.grips[2].transform.rotation);
      grips.grips[3].transform.SetPositionAndRotation(weaponclass.grips[3].transform.position,weaponclass.grips[3].transform.rotation);
      grips.grips[4].transform.SetPositionAndRotation(weaponclass.grips[4].transform.position,weaponclass.grips[4].transform.rotation);
      grips.grips[5].transform.SetPositionAndRotation(weaponclass.grips[5].transform.position,weaponclass.grips[5].transform.rotation);*/
      RigBuilder rb = GetComponent<RigBuilder>();
      rb.Build();


    }
    void DequipWeapon(){

    }
}
