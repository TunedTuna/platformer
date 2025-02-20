using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinTracker;
    public Camera cam;
    public Transform debugCubeTransform;
    private int coins;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int timeLeft= 300-(int)(Time.time);
        timerText.text = $"Time:{timeLeft}";

      

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;
            Ray cursorRay = cam.ScreenPointToRay(screenPos);

            bool rayHitSomething = Physics.Raycast(cursorRay,out RaycastHit hitInfo);
            if (rayHitSomething && hitInfo.transform.gameObject.CompareTag("Brick")) 
            {
                Debug.Log("ouch!");
                Destroy(hitInfo.transform.gameObject);
            }
            if(rayHitSomething && hitInfo.transform.gameObject.CompareTag("Question"))
            {
                coins++;
                Debug.Log("ding!");
                StartCoroutine(questionAnimation(hitInfo.transform));
                coinTracker.text = $"\nx{coins}";
            }

        }
    }

    IEnumerator questionAnimation(Transform questionObj)
    {
        Vector3 originalPosition = questionObj.position;
        float moveAmount = 0.1f;
        float moveSpeed = 0.01f; 
        for (int i = 0; i < 5; i++)
        {
            questionObj.position += new Vector3(0, moveAmount, 0);
            yield return new WaitForSeconds(moveSpeed);
        }

        
        for (int i = 0; i < 5; i++)
        {
            questionObj.position -= new Vector3(0, moveAmount, 0);
            yield return new WaitForSeconds(moveSpeed);
        }

        
        questionObj.position = originalPosition;
    }
}
