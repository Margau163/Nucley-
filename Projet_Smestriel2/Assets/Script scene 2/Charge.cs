using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] int _speed; //vitesse de rotation de l'objet 
    public float charge; // La charge actuelle (état)
    [SerializeField] private float stableCharge; // La charge stable (état stable) ou le noyaux ne se décharge plus
    // Start is called before the first frame update
    [SerializeField] private float chargeRate;
    public Transform otherObjectTransform; //récupérer le transform de l'objet
    
    
    
    void Start()
    {
        // Obtenez le Renderer attaché à ce GameObject
        Renderer renderer = GetComponentInChildren<Renderer>();
      
       
    }

    // Update is called once per frame
    void Update()
    {
      

        // vector 3 pour la rotation
        Vector3 _rota = new Vector3(1, 0, 0);

        // Si Z est pressé, faire tourner l'objet
        if (Input.GetKey(KeyCode.W))
        {
          
            otherObjectTransform.Rotate(_rota * Time.deltaTime * _speed); //alors il tourne
    
        }


        // Augmenter la charge si Z est pressé et que la charge n'est pas maximale
        if (Input.GetKey(KeyCode.W) && charge < stableCharge)
        {
            //si chargenoyaux isactif est vrai alors charge 
            
            
                charge += chargeRate * Time.deltaTime; //la charge augmente
                
        }
        if (charge >= stableCharge)
        {
            charge = stableCharge;
            // Assurez-vous que le Renderer et le matériau existent
            if (GetComponentInChildren<Renderer>() != null && GetComponentInChildren<Renderer>().material != null)
            {
                // Accédez au matériau
                Material material = GetComponentInChildren<Renderer>().material;

                // Obtenez la valeur actuelle de l'intensité (remplacez '_Intensity' par le nom exact de la propriété du shader)
                float currentIntensity = material.GetFloat("_Intensity");

                // Modifiez l'intensité comme vous le souhaitez
                float newIntensity = currentIntensity + 1f; // exemple d'augmentation de l'intensité

                // Définissez la nouvelle valeur de l'intensité
                material.SetFloat("_Intensity", newIntensity);
            }
        }
    }
}
