using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoovPlayer : MonoBehaviour
{
    public Connection _defaultNoyaux; //variable _defaultNoyauxs
    public int currentIndex = 0; //index qui va se d�placer dans la liste
	public List<GameObject> connections = null; //liste de connections
    public Cam�ra noyauxSelect; //variable BackgroundFocus � envoy� � backgroundfocus 
    //d�finie variable qui stocke le noyaux actuellement s�lectionn� dans la liste
    public GameObject _secondNoyauxFocus; //variable _secondNoyauxFocus � envoy� � backgroundfocus
    [SerializeField] private float _speed; //vitesse de d�placement
    [SerializeField] private float timeToMove; //temps de d�placement
    public ChargeNoyau chargeNoyau; //variable chargeNoyau � envoy� � chargeNoyau
    public GameObject light;

    void Start()
    {

       _defaultNoyaux.Actif(); // on active le premier noyau
       currentIndex = 0; // on stocke le premier noyau dans la variable secondNoyauxFocus pour pouvoir le r�utiliser plus tard
       _secondNoyauxFocus = _defaultNoyaux.connections[currentIndex]; // on stocke le premier noyau dans la variable secondNoyauxFocus pour pouvoir le r�utiliser plus tard
        noyauxSelect.ChangeSegondNoyaux(_secondNoyauxFocus);
        //on dit que la lumi�re devient active sur le segondnoyauxfocus     
    }
    IEnumerator MoveToTarget(Vector3 targetPosition, float timeToMove)
    {
        Vector3 startPosition = transform.position; // position de d�part du joueur
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

            yield return null; // Attendre jusqu'� la prochaine frame
        }
        transform.position = targetPosition; // S'assurer que le joueur atteint la position cible
    }

    void Update()
    { 

       if (Input.GetKeyDown(KeyCode.D))
       {
         
            currentIndex++; // pour faire plus 1 dans la liste , on incr�mente quoi. ca va servir � se situer dans la liste des noyaux en connections et donc de changer de noyaux en visuel.

            
		    if (currentIndex > _defaultNoyaux.connections.Count - 1) // Si index plus grand que la taille de la liste le remettre a 0
	        {
			    	currentIndex = 0;// Si index plus grand que la taille de la liste le remettre a 0                     
	        }
            Debug.Log(currentIndex); // pour voir dans la console si ca fonctionne
            _secondNoyauxFocus = _defaultNoyaux.connections[currentIndex];
            noyauxSelect.ChangeSegondNoyaux(_secondNoyauxFocus);// on stocke le noyau actuel dans la variable secondNoyauxFocus pour pouvoir le r�utiliser plus tard
            //on dit que la lumo�re devient active sur le segondnoyauxfocus
           
            Debug.Log("changeSegondNoyaux1"); // pour voir dans la console si ca fonctionne
       }     

       if (Input.GetKeyDown(KeyCode.A))
       {           
             currentIndex--;

             if (currentIndex < 0) // Si le current index est inf�rieur � z�ro = on sort de la liste
             {
                 currentIndex =  _defaultNoyaux.connections.Count - 1;  //le current index retourne en haut de la liste   
             }
             Debug.Log(currentIndex);
             _secondNoyauxFocus = _defaultNoyaux.connections[currentIndex];
             noyauxSelect.ChangeSegondNoyaux(_secondNoyauxFocus);
            //on dit que la lumo�re devient active sur le segondnoyauxfocus           
            Debug.Log("changeSegondNoyaux");
       }   
     

       if (Input.GetKeyDown(KeyCode.Space))
       {                   
                // Mettre � jour la position ou l'�tat du joueur pour refl�ter le nouvel objet s�lectionn� au pr�alable dans la liste  
                _defaultNoyaux.InActif(); // on d�sactive le noyau actuel (celui sur lequel on se trouve
                                          //il doit utiliser speed pour se d�placer d'un noyau � l'autre
                Vector3 targetPosition = _defaultNoyaux.connections[currentIndex].transform.position; // votre logique pour obtenir la position du noyau suivant
                StartCoroutine(MoveToTarget(targetPosition, 1f)); // 1 seconde pour d�placer vers le noyau suivant

                //transform.position = _defaultNoyaux.connections[currentIndex].transform.position; // on change la position du joueur pour qu'il se retrouve sur le noyau suivant
                _defaultNoyaux = _defaultNoyaux.connections[currentIndex].GetComponent<Connection>(); // on change le noyau par le noyau suivant
                _defaultNoyaux.Actif(); // on active le noyau suivant
                _secondNoyauxFocus = _defaultNoyaux.connections[currentIndex];          
       }
       if (_secondNoyauxFocus != null && light != null)
       {
           // D�placer la lumi�re � la position du second noyau
           light.transform.position = _secondNoyauxFocus.transform.position;

           // Activer la lumi�re
           light.SetActive(true);
       }
       else
       {
           Debug.LogError("L'un des objets est nul: _secondNoyauxFocus ou Light");
       }
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            Time.timeScale = 0;
            // R�activez l'image du menu si elle n'est pas d�j� active
            SceneManager.LoadScene("Menu"); 
       }
    }
}
     
    


  