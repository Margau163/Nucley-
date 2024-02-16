using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerreVert : MonoBehaviour
{

    public List<GameObject> StableNoyauxList = null; //liste de noyaux stables
    public ListNoyauxStable listNoyaux; //r�cup�rer le script spawnGreen
    public MoovPlayer moovPlayer; //r�cup�rer le script moovplayer
    public int currentNoyauIndex = 0; // Commencez par le premier noyau
    [SerializeField] private float _speed; //vitesse de d�placement
    public ChargeNoyau chargeNoyau; //le noyau qui est connect� � cette connection

    void Start()
    {
       //r�cup�re la liste de noyaux stable qu'il y a au moment o� il est instanci�
       StableNoyauxList = listNoyaux.StableNoyauxList;
    }

    // Update is called once per frame
    void Update()
    {
     
        MoveToNoyau();
        
    }
    void MoveToNoyau()
    {
        if (listNoyaux.StableNoyauxList.Count == 0)
            return; // Pas de noyaux � suivre

        GameObject targetNoyau = listNoyaux.StableNoyauxList[currentNoyauIndex]; // Obtenez le noyau cible actuel
        transform.position = Vector3.MoveTowards(transform.position, targetNoyau.transform.position, _speed * Time.deltaTime);

        // V�rifiez si le joueur a atteint le noyau cible
        if (Vector3.Distance(transform.position, targetNoyau.transform.position) < 0.1f)
        {
            // Mettez � jour l'indice pour viser le prochain noyau, en bouclant si n�cessaire
            currentNoyauIndex = (currentNoyauIndex + 1) % listNoyaux.StableNoyauxList.Count;
        }
        Debug.Log("j'ai boug�");
    }

    void OnTriggerEnter(Collider other)
    {
        // V�rifiez si l'objet d�clencheur a le tag "verreRouge"
        if (other.CompareTag("Ennemi"))
        {
            Destroy(other.gameObject); // D�truit l'objet avec le tag "ennemi"
            Debug.Log("ennemi touch�");
        }
    }  

}
