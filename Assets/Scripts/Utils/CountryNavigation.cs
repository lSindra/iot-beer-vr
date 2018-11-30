using UnityEngine;
using System.Collections;

public class CountryNavigation : MonoBehaviour
{
    public IEnumerator NavigationIterator(Transform from, Transform to)
    {
        Vector3 middle = Vector3.Lerp(from.position, to.position, 0.9f);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 70)
        {
            from.position = Vector3.Slerp(from.position, middle, t);

            yield return null;
        }
    }
}
