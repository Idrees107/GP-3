using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField] private GameObject knife;


    private void Awake()
    {
        
        knife = transform.Find("Knife").gameObject;

        
        if (knife != null)
            knife.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            if (knife != null)
                knife.SetActive(true);

            other.gameObject.SetActive(false); 
        }
    }
}