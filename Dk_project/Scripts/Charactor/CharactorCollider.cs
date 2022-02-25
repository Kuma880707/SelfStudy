using UnityEngine;

public class CharactorCollider : MonoBehaviour
{

    private void Awake()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CollisionON");
    }
}
