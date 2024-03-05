using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance { get; private set; }
    [SerializeField] private GameObject[] rayGameobjects = new GameObject[10];
    [SerializeField] private GameObject alleyDot;
    [HideInInspector] public int score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        alleyDirection = RayDirection(alleyDot);
        SaveInitialTransforms();

    }

    void Update()
    {
        foreach (var gameObject in rayGameobjects)
        {
            Vector3 quilleDirection = RayDirection(gameObject);
            bool quilleIsStanding = -0.2f < Vector3.Dot(quilleDirection, alleyDirection) && Vector3.Dot(quilleDirection, alleyDirection) < 0.2f;
            if (!quilleIsStanding)
            {
                gameObject.GetComponent<QuilleScript>().isStanding = false;
            }
        }
        if (AreAllQuillesDown() && shouldResetQuilles)
        {
            Debug.Log("tout est tombé");
            shouldResetQuilles = false;
            StartCoroutine(ResetQuille());
        }
    }
    public Vector3 RayDirection(GameObject go)
    {
        Vector3 rayOrigin = go.transform.position;
        Vector3 rayDirection = go.transform.TransformDirection(Vector3.up);
        Ray ray = new Ray(rayOrigin, rayDirection);
        Debug.DrawRay(rayOrigin, rayDirection * 10f, Color.red);
        return ray.direction;
    }


    public bool AreAllQuillesDown()
    {
        foreach (GameObject quille in rayGameobjects)
        {
            QuilleScript quilleScript = quille.GetComponent<QuilleScript>();
            if (quilleScript != null && quilleScript.isStanding)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator ResetQuille()
    {
        yield return new WaitForSeconds(4f);
        for (int i = 0; i < rayGameobjects.Length; i++)
        {
            rayGameobjects[i].transform.position = initialPositions[i];
            rayGameobjects[i].transform.rotation = initialRotations[i];
            rayGameobjects[i].GetComponent<Rigidbody>().freezeRotation = true;
            StartCoroutine(UnfreezeRotations());
            rayGameobjects[i].SetActive(true);
            QuilleScript quilleScript = rayGameobjects[i].GetComponent<QuilleScript>();
            if (quilleScript != null)
            {
                quilleScript.isStanding = true;
                quilleScript.shouldCountPoints = true;
            }
            shouldResetQuilles = true;
        }

    }

    private IEnumerator UnfreezeRotations()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < rayGameobjects.Length; i++)
            rayGameobjects[i].GetComponent<Rigidbody>().freezeRotation = false;
    }
    private void SaveInitialTransforms()
    {
        initialPositions = new Vector3[rayGameobjects.Length];
        initialRotations = new Quaternion[rayGameobjects.Length];
        for (int i = 0; i<rayGameobjects.Length; i++)
        {
            initialPositions[i] = rayGameobjects[i].transform.position;
            initialRotations[i] = rayGameobjects[i].transform.rotation;
        }
    }
    private void OnDrawGizmos()
    {
        foreach (var gameObject in rayGameobjects)
        {
            RayDirection(gameObject);
        }
        RayDirection(alleyDot);
    }

    private Vector3 alleyDirection;
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    private bool shouldResetQuilles = true;
}
