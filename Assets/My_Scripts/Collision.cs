using UnityEngine;

public class Collision : MonoBehaviour
{
    public int currListidx; 
    void Start()
    {
        currListidx = GameManager.Instance.currentListIdx; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoldBallTable"))
        {
            GameManager.Instance.ballsValue[currListidx-1] += 1;
            BallManager.Instance.RemoveBall(other.gameObject.transform.GetChild(1).gameObject);
        }
    }
}
