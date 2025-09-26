using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Rats : MonoBehaviour
{
    protected GameManager gameManager;
    protected Button ownButton;
    [SerializeField] protected float timer = 0;
    protected bool beenHit = false;
    protected bool effectOnce = false;
    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ownButton = GetComponent<Button>();
    }

    protected virtual void Update()
    {
        if (!beenHit && gameManager.lives != 0)
        {
            timer += Time.deltaTime * gameManager.increaseActiveSpeed;
        }
        if (timer > gameManager.ratActiveSpeed && !effectOnce)
        {
            effectOnce = true;
            RatEffect();
        }
    }
    public virtual void RatHit()
    {
        beenHit = true;
        ownButton.gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
    }
    protected virtual void RatEffect()
    {
        ownButton.gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
    }

}
