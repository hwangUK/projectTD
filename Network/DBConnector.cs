using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MySql.Data.MySqlClient;
namespace UKH.SQL
{
    public class DBConnector : MonoBehaviour
    {
        [SerializeField] string IP;
        [SerializeField] string DBName;
        [SerializeField] string ID;
        [SerializeField] string PW;

        static string connAdress;
        
        private void Awake()
        {
            connAdress = $"Server={IP};Database={DBName}; Uid={ID}; Pwd={PW}";
            //static MySqlConnection conn = new MySqlConnection(connAdress);

        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
