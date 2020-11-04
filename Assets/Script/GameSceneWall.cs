using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneWall : MonoBehaviour
{
    public class Boundary
    {
        public float xMin;
        public float xMax;
        public float zMin;
        public float zMax;
    }
    public Transform m_Transform;
    private Rigidbody m_Rigidbody;
    public Boundary boundary;

// Start is called before the first frame update
void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        //实例化  Bundary 类 ，并给这些变量赋值；
        boundary = new Boundary() { xMin = -3f, xMax = 3f, zMin = -3f, zMax = 3f };
    }

    // Update is called once per frame
    void Update()
    {
        float fmh = Input.GetAxis("Horizontal");//得到水平移量
        float fmv = Input.GetAxis("Vertical");  //得到垂直移量
        Vector3 move = new Vector3(fmh, 0, fmv);  //形成三维向量
        if (m_Rigidbody != null)
        {
            m_Rigidbody.velocity = move * 10;    //得到刚体的速度
            //限制刚体的移动范围   Mathf.Clamp   一个范围
            float fx = Mathf.Clamp(m_Rigidbody.position.x, boundary.xMin, boundary.xMax);
            float fz = Mathf.Clamp(m_Rigidbody.position.z, boundary.zMin, boundary.zMax);
            m_Rigidbody.position = new Vector3(fx, 0.0F, fz);
            ///物体的倾斜角度
            m_Rigidbody.rotation = Quaternion.Euler(0.0F, 0.0F, m_Rigidbody.velocity.x * 5f);
        }
    }
}
