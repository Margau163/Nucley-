using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerreVert : MonoBehaviour
{

    public List<GameObject> StableNoyauxList = null; //liste de noyaux stables
    public ListNoyauxStable listNoyaux; //récupérer le script spawnGreen
    public MoovPlayer moovPlayer; //récupérer le script moovplayer
    public int currentNoyauIndex = 0; // Commencez par le premier noyau
    [SerializeField] private float _speed; //vitesse de déplacement
    public ChargeNoyau chargeNoyau; //le noyau qui est connecté à cette connection

    void Start()
    {
       //récupére la liste de noyaux stable qu'il y a au moment où il est instancié
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
            return; // Pas de noyaux à suivre

        GameObject targetNoyau = listNoyaux.StableNoyauxList[currentNoyauIndex]; // Obtenez le noyau cible actuel
        transform.position = Vector3.MoveTowards(transform.position, targetNoyau.transform.position, _speed * Time.deltaTime);

        // Vérifiez si le joueur a atteint le noyau cible
        if (Vector3.Distance(transform.position, targetNoyau.transform.position) < 0.1f)
        {
            // Mettez à jour l'indice pour viser le prochain noyau, en bouclant si nécessaire
            currentNoyauIndex = (currentNoyauIndex + 1) % listNoyaux.StableNoyauxList.Count;
        }
        Debug.Log("j'ai bougé");
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifiez si l'objet déclencheur a le tag "verreRouge"
        if (other.CompareTag("Ennemi"))
        {
            Destroy(other.gameObject); // Détruit l'objet avec le tag "ennemi"
            Debug.Log("ennemi touché");
        }
    }  

}
