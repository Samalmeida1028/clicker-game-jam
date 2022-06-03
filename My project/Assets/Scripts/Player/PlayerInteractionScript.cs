using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    // private float count = 0;
    public int refreshRate = 10;
    public float interactionRadius = 1;
    public string interactableTag = "Interactable";

    public KeyCode interactionKey;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // count += Time.fixedDeltaTime;
        GameObject closestObj = null;
        // if (count > (float)(1 / refreshRate))
        // {

        //     //TODO once more interactables, make interactable class wrapper
        // }

        if (Input.GetKeyDown(interactionKey))
        {
            closestObj = FindClosestObj(interactableTag);
            if (closestObj)
            {
                closestObj.GetComponent<QuestController>().interact(gameObject);
            }

        }


    }

    public GameObject FindClosestObj(string type)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, interactionRadius);

        GameObject closestObj = null;

        float minDist = Mathf.Infinity;

        foreach (Collider2D collider2D in colliders)
        {
            collider2D.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (collider2D.gameObject.tag.Equals(type))
            {

                Vector2 playerPosition = gameObject.transform.position;
                Vector2 objectPosition = collider2D.gameObject.transform.position;

                float offset = Vector2.Distance(playerPosition, objectPosition);

                if (offset < minDist)
                {

                    closestObj = collider2D.gameObject;
                    minDist = offset;

                }
                else
                {
                    collider2D.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }

            }
        }
        if (closestObj)
        {
            Debug.DrawLine(gameObject.transform.position, closestObj.transform.position);
            Debug.Log(closestObj.transform.position);
            closestObj.GetComponent<SpriteRenderer>().color = Color.green;
        }
        return closestObj;

    }

}
