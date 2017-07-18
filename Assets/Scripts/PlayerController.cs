//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//public class PlayerController : MonoBehaviour
//{
//    private Animator playerAnimator;
//    private Rigidbody rigid;
//    private bool isDefence;
//    private AudioSource audioSource;

//    // Use this for initialization
//    void Start()
//    {
//        playerAnimator = GetComponent<Animator>();
//        audioSource = GetComponent<AudioSource>();
//        rigid = GetComponent<Rigidbody>();
//        rigid.useGravity = true;
//        isDefence = false;
//        audioSource.PlayOneShot(ResourcesManager.Instance.GetAsset("Sounds/Start") as AudioClip);
//    }
//    public void Walk(bool value)
//    {
//        playerAnimator.SetBool("Walk", value);
//    }
//    public void Attack()
//    {
//        playerAnimator.SetTrigger("Attack");
//    }
//    public void Defence()
//    {
//        isDefence = true;
//        playerAnimator.SetTrigger("Defence");
//    }
//    public void DefenceEnd(bool value)
//    {
//        isDefence = false;
//        playerAnimator.SetBool("DefenceEnd", value);
//    }
//    public void Death()
//    {
//        audioSource.PlayOneShot(ResourcesManager.Instance.GetAsset("Sounds/PlayerDeath") as AudioClip);
//        rigid.isKinematic = false;
//        rigid.useGravity = true;
//        Destroy(this.gameObject, 3f);
//    }

//    void PlayerDebut()
//    {
//        rigid.useGravity = false;
//        rigid.isKinematic = true;
//        Walk(true);
//        transform.DOMove(new Vector3(-2f, transform.position.y, transform.position.z), 3f).OnComplete(()=> {
//            //GameManager.Instance.SpawnDragon();
//            Walk(false);
//        });
//    }
//    private void OnCollisionEnter(Collision collision)
//    {
//        if(collision.gameObject.name == "Platform")
//        {
//            PlayerDebut();
//            Debug.Log("PlayerDebut");
//        }

//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.name == "Platform")
//        {
//            PlayerDebut();
//            Destroy(other.gameObject);
//            Debug.Log("PlayerDebut");
//        }
//        if (other.tag == "DragonAttack")
//        {
//            if (isDefence)
//            {
//                DefenceEnd(false);
//                Debug.Log("Defence");
//                audioSource.PlayOneShot(ResourcesManager.Instance.GetAsset("Sounds/Defence") as AudioClip);

//            }
//            else
//            {
//                Debug.Log("Damage");
//                //GameManager.Instance.PlayerHP--;
//                if (Random.Range(0, 2) != 0)
//                    audioSource.PlayOneShot(ResourcesManager.Instance.GetAsset("Sounds/PlayerDamage1") as AudioClip);
//                else
//                    audioSource.PlayOneShot(ResourcesManager.Instance.GetAsset("Sounds/PlayerDamage2") as AudioClip);

//            }
//        }
//    }

//    public void Choise()
//    {
//        if (GameManager.Instance.IsChoised)
//        {
//            GameManager.Instance.Choise();
//            Debug.Log("Choise");
//        }
//    }
//    public void AttackEffect(GameObject effect)
//    {
//        GameObject go = Instantiate(effect);
//        Destroy(go, 2f);
//    }
//    public void PlaySound(AudioClip clip)
//    {
//        audioSource.PlayOneShot(clip);
//    }
//}
