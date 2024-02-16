using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.FilePathAttribute;
using FMODUnity;

public class ChargeNoyau: MonoBehaviour
{

    [SerializeField] int _speed; //vitesse de rotation de l'objet 
    public float charge; // La charge actuelle (état)
    [SerializeField] private float stableCharge; // La charge stable (état stable) ou le noyaux ne se décharge plus
    [SerializeField] private float oppMinCharge; // La charge minimale est la charge qu'il ne doit pas dépasser sinon le noyau s'éteind
    [SerializeField] private float oppMaxCharge; // La charge minimale est la charge qu'il ne doit pas dépasser sinon le noyau s'éteind
    [SerializeField] private float maxCharge; // La charge maximale est la charge qu'il ne doit pas dépasser sinon le noyau s'éteind
    [SerializeField] private float chargeRate; // La vitesse à laquelle la charge augmente (état instable)
    [SerializeField] private float chargeDecreaseRate; // La vitesse à laquelle la charge diminue (si input n'est pas maintenu jusqu'à l'état stable)
    public float rotationSpeed; //vitesse de rotation de l'objet
    public Transform otherObjectTransform; //récupérer le transform de l'objet
    [SerializeField] private float ennemiCharge; //la charge que va enlever l'ennemi au noyaux
    [SerializeField] private float amiCharge; //la charge que va ajouter l'ami au noyaux
    public GameObject _ami; //récupérer le gameobject du verregentil
    public ListNoyauxStable listNoyaux; //récupérer le script spawnGreen
    public MoovPlayer moovPlayer; //récupérer le script MoovPlayer
    public bool isActif = false; //savoir si l'objet est actif ou non 
    public bool noyauxStable = false; //par default le noyaux n'est pas stable
    public bool noyauSurcharge = false;
    public bool _surcharge = false;
    private float currentIntensity; //intensité de la lumière  
    public  Material materialShader; //shadergraph

    // Start is called before the first frame update
    void Start()
    {
         materialShader = GetComponentInChildren<Renderer>().material;
        currentIntensity = 8f;
        materialShader.SetFloat("_Intensity", currentIntensity);
    }

    // Nouvelles valeurs cibles pour l'intensité et la couleur
    
    // Update is called once per frame
    void Update()
    {
        materialShader.SetColor("_EmissionColor", Color.white * 0f);

        // vector 3 pour la rotation
        Vector3 _rota = new Vector3(1, 0, 0);

        // Si Z est pressé, faire tourner l'objet
        if (Input.GetKey(KeyCode.W))
        {
            if (isActif == true) //et si le noyaux est le _defaultnoyau
            {
                otherObjectTransform.Rotate(_rota * Time.deltaTime * _speed); //alors il tourne
            }
        }
        // Augmenter la charge si Z est pressé et que la charge n'est pas maximale
        if (Input.GetKey(KeyCode.W) && charge < maxCharge)
        {
            //si chargenoyaux isactif est vrai alors charge 
            if (isActif == true)
            {
                charge += chargeRate * Time.deltaTime; //la charge augmente

            }
        }
        if (charge == 0)
        {
            currentIntensity = 8f;
            materialShader.SetFloat("_Intensity", currentIntensity);
            Color32 newColor = new Color32(255, 255, 255, 255);
            materialShader.SetColor("_Color", newColor);
        }
            if (charge <= 2 && charge >= 0)
            {              
                currentIntensity = 3f;
                materialShader.SetFloat("_Intensity", currentIntensity);

                Color32 newColor = new Color32(255, 255, 255, 255);
                materialShader.SetColor("_Colo", newColor);
            }
            if (charge <= 30 && charge >= 2)            
            {               
                currentIntensity = 3f;
                materialShader.SetFloat("_Intensity", currentIntensity);

                Color32 newColor = new Color32(69, 99, 236, 255);
                materialShader.SetColor("_Colo", newColor);


            }
            if (charge >= 30 && charge <= 50)
            {
                currentIntensity = 2f;
                materialShader.SetFloat("_Intensity", currentIntensity);

                Color32 newColor = new Color32(0, 223, 255, 255);
                materialShader.SetColor("_Colo", newColor);
            }
            if (charge >= 50 && charge <= 60)
            {
                currentIntensity = 1f;
                materialShader.SetFloat("_Intensity", currentIntensity);

                Color32 newColor = new Color32(130, 255, 131, 255);
                materialShader.SetColor("_Colo", newColor);
            }
            if (charge >= 60 && charge <= 70)
            {
                currentIntensity = 1f;
                materialShader.SetFloat("_Intensity", currentIntensity);

                Color32 newColor = new Color32(255, 240, 70, 255);
                materialShader.SetColor("_Colo", newColor);
            }
            if (charge >= 70 && charge <= 78)

            {
                currentIntensity = 0.5f;
                materialShader.SetFloat("_Intensity", currentIntensity);

                Color32 newColor = new Color32(255, 140, 0, 255);
                materialShader.SetColor("_Colo", newColor); 
            }
            if (charge >= 78 && charge <= 80)
            {
                currentIntensity = 0.5f;
                materialShader.SetFloat("_Intensity", currentIntensity);

                Color32 newColor = new Color32(255, 0, 0, 255);
                materialShader.SetColor("_Colo", newColor);
            }
        // Si la charge atteint le stable, stabiliser la sphère
        if (charge >= stableCharge)
        {
            // Le noyaux continue de tourner
            otherObjectTransform.Rotate(_rota * Time.deltaTime * _speed); //le noyaux continue de tourner

        }
        //si elle n'est pas stable, elle se décharge
        else if (charge <= stableCharge)
        {
            charge -= chargeDecreaseRate * Time.deltaTime; //la charge diminue
            if (currentIntensity <= 0f)
            {
                currentIntensity = 0f;
                materialShader.SetFloat("_Intensity", currentIntensity);
            }
        }
        if (charge <= 0)
        {
            charge = 0; //si la charge est inférieur à 0 alors elle revient à 0

            currentIntensity = 0f;
            materialShader.SetFloat("_Intensity", currentIntensity);
        }
        //elle s'ajoute à al liste de noyaux stables
        if (charge >= stableCharge && noyauxStable == false)
        {
            noyauxStable = true;
            //envoyé la variable qui a changé dans le script stableNoyaux            
            // Ajoute ce GameObject à la liste des noyaux stables
            listNoyaux.StableNoyauxList.Add(gameObject);
        }
        if (charge >= stableCharge && noyauSurcharge == false)
        {
            noyauSurcharge = true;
            listNoyaux.Surcharge.Add(gameObject);
        }

        if (Input.GetKey(KeyCode.W) && charge >= stableCharge && charge <= maxCharge) //si le joueur continue d'appuyer sur Z et que la charge dépassse la charge stable il entre dans la surcharge
        {
            if (isActif == true)
            {
                charge += chargeRate * Time.deltaTime; //la charge augmente
            }
        }

        if (Input.GetKey(KeyCode.Space) && charge > oppMinCharge && charge < oppMaxCharge) //si la charge se situe entre les deux alors fenètre d'opportunité //on rajoute un if pour éviter un bug
        {             
                charge = stableCharge;
                //intantier le verre vert sur le noyaux
                Instantiate(_ami, moovPlayer.transform.position, Quaternion.identity);
                Debug.Log("ami instantié"); //affiche fenetre dans la console
            
        }
        if (charge >= maxCharge)
        {
            _surcharge = true;
            
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Noyau");
            foreach (GameObject obj in objectsWithTag)
            {
                ChargeNoyau chargeNoyauScript = obj.GetComponent<ChargeNoyau>();
                if (chargeNoyauScript != null)
                {
                    // Réinitialiser la charge
                    chargeNoyauScript.charge = 0;
                }
            }
            // Vider la liste Surcharge
            listNoyaux.Surcharge.Clear();
        }
        if (_surcharge == true)
        {
            StartCoroutine(RotateAndTurnOff());
        }      
    }
    public IEnumerator RotateAndTurnOff()
    {
        currentIntensity = 0f;
        materialShader.SetFloat("_Intensity", currentIntensity);

        Color32 newColor = new Color32(255, 126, 126, 255);
        materialShader.SetColor("_Color", newColor);

        float timeElapsed = 0f;
                while (timeElapsed < 2f) // Tourner pendant 2 secondes
                {
                    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Assurez-vous que rotationSpeed est défini
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
            Debug.Log("J'ai tourné"); // Bon pour le débogage
            _surcharge = false;
    }
    
       void OnTriggerEnter (Collider other)
       {
            // Vérifier si l'objet de la collision a le tag "Sphere"
            if (other.CompareTag("Ennemi"))
            {
                // Réduire la charge de 1f
                charge -= ennemiCharge; //la charge est égale à la charge instable
                Debug.Log("Nouvelle charge de Noyaux: " + charge); //affiche dans la console la nouvelle charge
            }
            if (other.CompareTag("Ami") && charge <= stableCharge)
            {   
                if (charge < stableCharge)
                {
                    charge += amiCharge;
                    Debug.Log("Nouvelle charge de Noyaux: " + charge); //affiche dans la console la nouvelle charge                       
                }            
            }
       }
}
