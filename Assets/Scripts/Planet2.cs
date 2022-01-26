using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Code for OnMouseDown in the iPhone. Unquote to test.
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                }
            }
        }
    }

    void OnMouseDown()
    {
        StartCoroutine("Explode");
    }

    IEnumerator Explode()
    {
        float scaleDuration = 5;                                //animation duration in seconds
        Vector3 actualScale = transform.localScale;             // scale of the object at the begining of the animation
        Vector3 targetScale = new Vector3(10.5f, 10.5f, 10.5f);     // scale of the object at the end of the animation

        for (float t = 0; t < 1; t += Time.deltaTime / scaleDuration)
        {
            this.transform.localScale = Vector3.Lerp(actualScale, targetScale, t);
            yield return null;
        }
    }
}
