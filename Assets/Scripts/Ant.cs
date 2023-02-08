using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BoardManager;
using UnityEngine.UIElements;

public class Ant : MonoBehaviour
{
    public Cake cake;
    public enum AntState { None, GoToCake, Die, GoToHome}
    public AntState mState = AntState.None;
    public Vector3 mStartPos = Vector3.zero;
    public Vector3 mTargetPos = Vector3.zero;
    public Vector3 mcakePos = Vector3.zero;
    public Animator  mAntAnimator = null;
    public Camera mCamera = null;
    // Start is called before the first frame update
    void Start()
    {
        mAntAnimator = GetComponent<Animator>();    
        SetState(AntState.GoToCake);

       
        mcakePos = cake.GetComponent<Transform>().position;
        SetTargetPos(mcakePos);
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (mState == AntState.GoToCake || mState == AntState.GoToHome)
        {
            UpdateTarget();
            this.transform.Translate(Vector3.up * 0.01f);
           
        }
        
    }

    public void SetState(AntState value)
    {
        mState = value;
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggerEnter!!!! ");
        if (other.gameObject.CompareTag("Cake"))
        {
            Debug.Log("triggerEnter!!!! --> Cake");

            if (other.gameObject.GetComponent<Cake>().GetCakeCount() > 0)
            {
                Debug.Log("ant get cake !!!!! ");
                other.gameObject.GetComponent<Cake>().DisCakeCount(); // ���� �ϳ� ���
               
            }
            else
            {
                Debug.Log("cake empty!!!!! ");
            }
            SetTargetPos(mStartPos); // �� ������ Ÿ�� ���� ����.
            SetState(AntState.GoToHome); //������ !

            
            UpdateTarget();
        }

        if(other.gameObject.CompareTag("Bullet"))
        {
            SetState(AntState.Die);
            Die(); // hp ��°� ���߿�
        }
    }

    public void Die()
    {
        mAntAnimator.SetTrigger("IsDead");

    }
    public void Reposition()
    {
        transform.position = mStartPos;

    }


    public void SetTargetPos(Vector3 targetPos)
    {
        mTargetPos = targetPos;
    }

    public void UpdateTarget()
    {
      
        // Ÿ�������� ������ �Ÿ�
        Vector3 targetDistance = mTargetPos - this.transform.position;

        Vector3 quaternionDistance = Quaternion.Euler(0, 0, 0) * targetDistance;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, quaternionDistance);

        this.transform.rotation = lookRotation;

    }


    // ---------------------- �� ã��� �ٽ� ��������

    public void UpdateMode()
    {
       
        // ���� ������ ��ǥ�� ������ǥ�� ��ȯ
        Vector3 antCurrPos = mCamera.ScreenToWorldPoint(transform.position);

        // ������� �ε����� ��ȯ
        int posX = Mathf.Clamp((int)(antCurrPos.x / UNIT), 0, WIDTH - 1);
        int posY = Mathf.Clamp((int)(antCurrPos.y / UNIT), 0, LENGTH - 1);

        int currIndex = posY * WIDTH + posX;

        antCurrPos.z = 0.0f;
        antCurrPos.x = posX;
        antCurrPos.y = posY;

        List<int> indexs = FindAroundIndex(currIndex);

        while (true)
        {
            int randomIndex = Random.Range(0, indexs.Count + 1);

            // �ֺ� ĭ�߿� �� ���� �� 1�� ��� �̵� 
            if (Board[randomIndex] == 0)
            {
                //SetTargetPos(Board[randomInde)
                return;
            }
            else
            {

            }
        }
               

    }

    public List<int> FindAroundIndex(int currIndex)
    {
        List <int > indexs = new List <int>();

        int index_1 = (currIndex - 1) - WIDTH;
        indexs.Add(index_1);

        int index_2 = currIndex - WIDTH;
        indexs.Add(index_2);

        int index_3 = (currIndex + 1) - WIDTH;
        indexs.Add(index_3);

        int index_4 = currIndex - 1;
        indexs.Add(index_4);

        int index_5 = currIndex + 1;
        indexs.Add(index_5);

        int index_6 = (currIndex - 1) + WIDTH;
        indexs.Add(index_6);

        int index_7 = currIndex + WIDTH;
        indexs.Add(index_7);

        int index_8 = (currIndex + 1) + WIDTH;
        indexs.Add(index_8);

        for(int i = 0; i < indexs.Count;i++)
        {
            if (ValidIndexCheck(indexs[i]) == -1)
            {
                indexs.RemoveAt(i);
                i--;
            }
            
        }

        return indexs;


    }

    public int ValidIndexCheck(int index)
    {
        if (index >= 0 && index <= WIDTH * LENGTH) 
        {
            return index;

        }
        else
        {
            return -1;
        }
    }
}
