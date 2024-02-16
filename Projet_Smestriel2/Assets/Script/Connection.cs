using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Connection : MonoBehaviour
{
    public List<GameObject> connections = new List<GameObject>();   
    public ChargeNoyau chargeNoyau; //le noyau qui est connecté à cette connection
    
    public void Actif()
    {
       //le noyaux doit savoir qu'il est Actif
       chargeNoyau.isActif = true;       
    }

    public void InActif()
    {
        chargeNoyau.isActif = false;
		//le noyaux doit savoir qu'il est InActif
	}    
}
