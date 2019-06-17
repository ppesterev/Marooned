using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour
{
    // how many enemies cam be hit with 1 projectile
    [SerializeField] private int penetration = 2;

    public int damage = 50; // how do i even OOP

    private LineRenderer lineRenderer;

    private void OnEnable()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        int enemiesOnlyMask = 1 << 9; // centralized place for these, like an enumeration?
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, Mathf.Infinity, enemiesOnlyMask);

        if (hits.Length > 0)
        {
            System.Array.Sort(hits, (RaycastHit x, RaycastHit y) => { return x.distance < y.distance ? -1 : 1; });

            for (int i = 0; i < Mathf.Min(hits.Length, penetration); i++)
            {
                Unit hitEnemy = hits[i].collider.GetComponentInParent<Unit>(); // this looks bad
                hitEnemy.TakeDamage(damage);
            }
        }

        Vector3 defaultLineEnd = transform.position + transform.forward * 20f;
        IEnumerator coroutine = RenderLine(hits.Length >= penetration? hits[penetration - 1].point : defaultLineEnd);
        StartCoroutine(coroutine);
    }

    IEnumerator RenderLine(Vector3 lineEnd)
    {
        float alpha = 1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions( new Vector3[]{ transform.position, lineEnd } );

        // maybe figure out how to do this with materials
        Gradient grad = lineRenderer.colorGradient;
        GradientAlphaKey[] aKeys = grad.alphaKeys; 
        while (alpha > 0)
        {
            for (int i = 0; i < aKeys.Length; i++)
            {
                aKeys[i].alpha *= alpha;
            }

            grad.alphaKeys = aKeys;
            lineRenderer.colorGradient = grad;
            alpha -= 0.07f; // magic number
            yield return null;
        }
        Destroy(gameObject);
    }
}
