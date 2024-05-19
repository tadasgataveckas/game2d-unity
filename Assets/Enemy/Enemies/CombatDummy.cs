using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticles;
    private Animator animator;

    [SerializeField]
    TMP_Text text;

    [SerializeField]
    GameObject Enemymesh;

    private Coroutine showtext;
    public void Damage(float amount)
    {
        Debug.Log(amount + "Damage taken by: " + this.name);
        //Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        animator.SetTrigger("damage");

        if (showtext != null)
            StopCoroutine(showtext);
        showtext = StartCoroutine(ShowText(amount));
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator ShowText(float amount)
    {
		Rigidbody2D rb = Enemymesh.GetComponent<Rigidbody2D>();
		text.text = "-" + amount.ToString();
        text.gameObject.SetActive(true);
        Vector3 originalPosition = new Vector2(rb.position.x, rb.position.y + 3f);

		float duration = 2f;
        float elapsedTime = 0f;
        Vector3 moveUpAmount = new Vector3(0, 0.5f, 0);

        while (elapsedTime < duration)
        {
            text.transform.position = Vector3.Lerp(originalPosition, originalPosition + moveUpAmount, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.transform.position = originalPosition + moveUpAmount;


        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
        
        text.transform.position = new Vector2(rb.position.x, rb.position.y);
    }
}
