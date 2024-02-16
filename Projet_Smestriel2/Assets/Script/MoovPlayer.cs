using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoovPlayer : MonoBehaviour
{
    public Connection _defaultNoyaux; //variable _defaultNoyauxs
    public int currentIndex = 0; //index qui va se déplacer dans la liste
	public List<GameObject> connections = null; //liste de connections
    public Caméra noyauxSelect; //variable BackgroundFocus à envoyé à backgroundfocus 
    //définie variable qui stocke le noyaux actuellement sélectionné dans la liste
    public GameObject _secondNoyauxFocus; //variable _secondNoyauxFocus à envoyé à backgroundfocus
    [SerializeField] private float _speed; //vitesse de déplacement
    [SerializeField] private float timeToMove; //temps de déplacement
    public ChargeNoyau chargeNoyau; //variable chargeNoyau à envoyé à chargeNoyau
    public GameObject light;

    void Start()
    {

       _defaultNoyaux.Actif(); // on active le premier noyau
       currentIndex = 0; // on stocke le premier noyau dans la variable secondNoyauxFocus pour pouvoir le réutiliser plus tard
       _secondNoyauxFocus = _defaultNoyaux.connections[currentIndex]; // on stocke le premier noyau dans la variable secondNoyauxFocus pour pouvoir le réutiliser plus tard
        noyauxSelect.ChangeSegondNoyaux(_secondNoyauxFocus);
        //on dit que la lumière devient active sur le segondnoyauxfocus     
    }
    IEnumerator MoveToTarget(Vector3 targetPosition, float timeToMove)
    {
        Vector3 startPosition = transform.position; // position de départ du joueur
        bool isMoving = true;
        float elapsedTime = 0;

        while (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                break;
            }

            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp(elapsedTime / timeToMove, 0, 1);
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            yield return null; // Attendre jusqu'à la prochaine frame
        }
        transform.position = targetPosition; // S'assurer que le joueur atteint la position cible
    }

    void Update()
    { 

       if (Input.GetKeyDown(KeyCode.D))
       {
         
            currentIndex++; // pour faire plus 1 dans la liste , on incrémente quoi. ca va servir à se situer dans la liste des noyaux en connections et donc de changer de noyaux en visuel.

            
		    if (currentIndex > _defaultNoyaux.connections.Count - 1) // Si index plus grand que la taille de la liste le remettre a 0
	        {
			    	currentIndex = 0;// Si index plus grand que la taille de la liste le remettre a 0                     
	        }
            Debug.Log(currentIndex); // pour voir dans la console si ca fonctionne
            _secondNoyauxFocus = _defaultNoyaux.connections[currentIndex];
            noyauxSelect.ChangeSegondNoyaux(_secondNoyauxFocus);// on stocke le noyau actuel dans la variable secondNoyauxFocus pour pouvoir le réutiliser plus tard
            //on dit que la lumoère devient active sur le segondnoyauxfocus
           
            Debug.Log("changeSegondNoyaux1"); // pour voir dans la console si ca fonctionne
       }     

       if (Input.GetKeyDown(KeyCode.A))
       {           
             currentIndex--;

             if (currentIndex < 0) // Si le current index est inférieur à zéro = on sort de la liste
             {
                 currentIndex =  _defaultNoyaux.connections.Count - 1;  //le current index retourne en haut de la liste   
             }
             Debug.Log(currentIndex);
             _secondNoyauxFocus = _defaultNoyaux.connections[currentIndex];
             noyauxSelect.ChangeSegondNoyaux(_secondNoyauxFocus);
            //on dit que la lumoère devient active sur le segondnoyauxfocus           
            Debug.Log("changeSegondNoyaux");
       }   
     

       if (Input.GetKeyDown(KeyCode.Space))
       {                   
                // Mettre à jour la position ou l'état du joueur pour refléter le nouvel objet sélectionné au préalable dans la liste  
                _defaultNoyaux.InActif(); // on désactive le noyau actuel (celui sur lequel on se trouve
                                          //il doit utiliser speed pour se déplacer d'un noyau à l'autre
                Vector3 targetPosition = _defaultNoyaux.connections[currentIndex].transform.position; // votre logique pour obtenir la position du noyau suivant
                StartCoroutine(MoveToTarget(targetPosition, 1f)); // 1 seconde pour déplacer vers le noyau suivant

                //transform.position = _defaultNoyaux.connections[currentIndex].transform.position; // on change la position du joueur pour qu'il se retrouve sur le noyau suivant
                _defaultNoyaux = _defaultNoyaux.connections[currentIndex].GetComponent<Connection>(); // on change le noyau par le noyau suivant
                _defaultNoyaux.Actif(); // on active le noyau suivant
                _secondNoyauxFocus = _defaultNoyaux.connections[currentIndex];          
       }
       if (_secondNoyauxFocus != null && light != null)
       {
           // Déplacer la lumière à la position du second noyau
           light.transform.position = _secondNoyauxFocus.transform.position;

           // Activer la lumière
           light.SetActive(true);
       }
       else
       {
           Debug.LogError("L'un des objets est nul: _secondNoyauxFocus ou Light");
       }
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            Time.timeScale = 0;
            // Réactivez l'image du menu si elle n'est pas déjà active
            SceneManager.LoadScene("Menu"); 
       }
    }
}
     
    


  