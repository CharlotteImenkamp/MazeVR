using UnityEngine;

public class RegisterBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BallManager.Instance.RegisterBall(gameObject); 
    }
}
