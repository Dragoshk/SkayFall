using UnityEngine;
using System.Collections.Generic;

public class Targets : MonoBehaviour
{
    public Player_Controler Cnt;
    public PlayerCharacter pc;
    public bool targetactive;
    public List<Transform> targets;
    public EnemyHealth EH;
    //public EnemyAI ai;
    public GameObject target;
    public RaycastHit hit;
    public Transform selectedTarget;
    private Transform myTransform;
    public Transform selecedTargetByMouse;

    public EnemiesOnScene enemiesReference;

    void Start()
    {
        targetactive = false;
        targets = new List<Transform>();
        selectedTarget = null;

        myTransform = transform;
    }

    public void EnemyDead(GameObject go) // remove os inimigos do array
    {
        target = null;
        EH = null;
        Cnt.inCombat = false;
        if (targets.Count > 0)
        {
            int i = 0;
            foreach (Transform tar in targets.ToArray())
            {
                if (go.transform.name == tar.name)
                {
                    targets.RemoveAt(i);
                    Cnt.inCombat = false;
                }
                i++;
            }
            Destroy(go);
        }
    }
    /*
    public void DealDamage(GameObject go) // da dano no inmigo baseado no target "problemas com o colider e o trigger -.-" 
    {
        if (targets.Count > 0)
        {
            int i = 0;
            foreach (Transform tar in targets.ToArray())
            {
                if (go.transform.name == tar.name)
                {
                    FloatingTextControler.CreateFloatingText(pc.curDmg.ToString(), transform);
                    EH.AdjustCurrentHealth(-pc.curDmg);
                }
                i++;
            }
        }
    }
   */
   
    public void AddTarget(Transform enemy) // adiciona td transform com tag enemy
    {
        targets.Add(enemy);
    }

    public void RemoveTarget(Transform enemy) // remove td transform com tag enemy
    {
        targets.Remove(enemy);
    }
    


    private void SortTargetsByDistance() // seleciona o target pela distancia
    {
        targets.Sort(delegate (Transform t1, Transform t2)
        {
            return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
        });
    }

    private void TargetEnemy()
    {

        if (selectedTarget == null)
        {
            // se nao tiver nem um target pegar o primeiro da lista 
            SortTargetsByDistance();
            selectedTarget = targets[0];
        }


        else
        {
            int index = targets.IndexOf(selectedTarget);
            if (index < targets.Count - 1)
            {
                index++;
            }
            else 
            {
                index = 0;
            }
          //  EH.UI.SetActive(false);
            //EH.Indicator.SetActive(false);
            //perde a selecao do target
            DeselctedTarget();
            selectedTarget = targets[index];
        }
        SelectedTarget();

        //EH.UI.SetActive(true);
        //EH.Indicator.SetActive(true);
        //ai.Aim(EH.UI);
    }

    public void SelectedTarget() // pega o script de vida do inimigo
    {
        if (selecedTargetByMouse != null)
        {
            target = selecedTargetByMouse.gameObject;
            selecedTargetByMouse = null;
        }
        else
        {
            target = selectedTarget.gameObject;
        }

      //  EH = target.gameObject.GetComponent<EnemyHealth>();
        targetactive = true;
    }

    public void DeselctedTarget() // desceleciona o target
    {
        selectedTarget = null;
        target = null;
        targetactive = false;
    }
    
    public void TargetOn()
    {
        if (targets.Count != 0)
        {
            TargetEnemy();
        }
    }

    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, 100)) //checa se a algo a sua frente
        {
            enemiesReference = GameObject.FindGameObjectWithTag("AllEnemies").GetComponent<EnemiesOnScene>();
            AddTarget(enemiesReference.transform);
        }
        
       
        if ( Input.GetMouseButtonDown (0) && targetactive == false)
        { 
           RaycastHit hit;
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

           if ( Physics.Raycast (ray,out hit,100.0f) && hit.transform.tag == "Enemy")
           {
                selecedTargetByMouse = hit.transform;
                TargetEnemy();
                targetactive = true;
           }
        }   
    }  
}
