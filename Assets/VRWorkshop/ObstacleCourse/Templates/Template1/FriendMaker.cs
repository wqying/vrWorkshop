using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMaker : MonoBehaviour
{

    public GameObject friend;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            friend.SetActive(true);
        }
    }
}
