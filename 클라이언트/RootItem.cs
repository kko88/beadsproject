using UnityEngine;
using System.Collections;

public class RootItem : MonoBehaviour {

    enum State
    {
        Idle,
        Hit
    }

    State currentState;

    // Use this for initialization
    void Start()
    {
        currentState = State.Idle;
        NextState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        if (currentState == State.Hit)
            return;

        currentState = State.Hit;
    }
    public void ItemDestory()
    {
        Destroy(gameObject);
    }

    IEnumerator IdleState()
    {
        this.gameObject.transform.rotation = Quaternion.identity;

        while (currentState == State.Idle)
        {
            yield return null;
        }

        NextState();
    }


    IEnumerator HitState()
    {
        float angle = Random.Range(270, 360);
        float hitTime = 0.5f;

        while (currentState == State.Hit)
        {
            yield return null;

            this.gameObject.transform.Rotate(Time.deltaTime * angle * Vector3.one);

            if (hitTime <= 0)
            {
                this.currentState = State.Idle;
            } hitTime -= Time.deltaTime;
        }

        NextState();
    }

    void NextState()
    {
        switch (currentState)
        {
            case State.Idle:
                StartCoroutine(IdleState());
                break;
            case State.Hit:
                StartCoroutine(HitState());
                break;
        }
    }
}
