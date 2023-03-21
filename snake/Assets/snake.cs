using UnityEngine;
using System.Collections.Generic;


public class snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    public Transform segmentPrefab;
    public int initialsize = 4;


    private void Start()
    {
      _segments = new List<Transform>();// null döndürmüyor
      ResetState();// oyuna res atarak başlıyoruz 
    }

    private void Update()//kendi içine dönmemesi lazım 

//   if (direction.x != 0f){}
//   private void Update()
//   if (direction.x != 0f){
//   if (Input.GetKeyDown(KeyCode.W))
//       {
//           _direction = Vector2.up;
//       }
//       else if (Input.GetKeyDown(KeyCode.S))
//       {
//           _direction = Vector2.down;
//       }
//     }
//     
//     else if (direction.y != 0f){
//       if (Input.GetKeyDown(KeyCode.A))
//       {
//           _direction = Vector2.left;
//       }
//       else if (Input.GetKeyDown(KeyCode.D))
//       {
//           _direction = Vector2.right;
//           }
//       }
//   }

    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    //kuyruk ekleme
    {
      for (int i = _segments.Count -1; i > 0; i--) {
        _segments[i].position = _segments[i - 1].position;
      }


      //hareket
      this.transform.position = new Vector3(
        Mathf.Round(this.transform.position.x) + _direction.x,
        Mathf.Round(this.transform.position.y) + _direction.y
        );
        Time.fixedDeltaTime = 0.1f; // fps düşürerek yvaşlatma
        //Time.fixedDeltaTime = 0.04f;


      //transform.Translate(_direction * Time.deltaTime*8);//hız
      //Mathf.Round(this.transform.position.x) + _direction.x;
      //Mathf.Round(this.transform.position.y) + _direction.y;




    }
    private void Grow()
    //büyüme
    {
      Transform segment = Instantiate(this.segmentPrefab);
      segment.position = _segments[_segments.Count -1].position;
      _segments.Add(segment);
    }
    private void ResetState()
    // 1 ile başlatma
    {
      for (int i = 1; i < _segments.Count; i++) {
        Destroy(_segments[i].gameObject);

      }
      //res alırken kuyrukları silme ve birleme 

      _segments.Clear();
      _segments.Add(this.transform);

      //-1 kafa zaten res alınca var!!! 
      for (int i = 1; i < this.initialsize; i++) {
        _segments.Add(Instantiate(this.segmentPrefab));

      }

      this.transform.position = Vector3.zero;



    }


    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "food")
      {
        Grow();
      }
      else if (other.tag == "obstacle") {
        ResetState();

      }
    }
}
