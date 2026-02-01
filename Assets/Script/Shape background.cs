using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class Shapebackground : MonoBehaviour
{
    [SerializeField] private List<GameObject> _shapesToSpawn;
    public float timeBetweenSpawn;
    public float radius;
    public float offsetinfluence = 0.3f;
    
    private void Awake()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(timeBetweenSpawn * (Random.value + 0.5f));
        int random = Random.Range(0, _shapesToSpawn.Count);
        GameObject obj = Instantiate(_shapesToSpawn[random]);
        float randompos = Random.value * 2 * Mathf.PI;
        obj.transform.position = transform.position + new Vector3(Mathf.Cos(randompos), Mathf.Sin(randompos), 0) * radius;
        Vector2 offset = Random.insideUnitCircle;
        obj.GetComponent<MoveShapeMenu>().direction = new Vector3(Mathf.Cos(randompos), Mathf.Sin(randompos), 0) * -1 + new Vector3(offset.x, offset.y, 0) * offsetinfluence;
        StartCoroutine(spawn());
    }
}
