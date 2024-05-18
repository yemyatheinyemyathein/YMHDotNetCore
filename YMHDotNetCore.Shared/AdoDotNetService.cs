using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YMHDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService (string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connecton = new SqlConnection(_connectionString);

            connecton.Open();

            SqlCommand cmd = new SqlCommand(query, connecton);
            if(parameters is not null && parameters.Length  > 0)
            {
                //foreach(var item in parameters) { 
                //    cmd.Parameters.AddWithValue(item.Name, item.Value);
                //}

                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());

                var ParameterArrays = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(ParameterArrays);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connecton.Close();

            string json = JsonConvert.SerializeObject(dt); // c# to json
            List<T> lst = JsonConvert.DeserializeObject<List < T >>(json);

            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connecton = new SqlConnection(_connectionString);

            connecton.Open();

            SqlCommand cmd = new SqlCommand(query, connecton);
            if (parameters is not null && parameters.Length > 0)
            {
                //foreach(var item in parameters) { 
                //    cmd.Parameters.AddWithValue(item.Name, item.Value);
                //}

                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());

                var ParameterArrays = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(ParameterArrays);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connecton.Close();

            string json = JsonConvert.SerializeObject(dt); // c# to json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json);

            return lst[0];
        }

        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connecton = new SqlConnection(_connectionString);

            connecton.Open();

            SqlCommand cmd = new SqlCommand(query, connecton);
            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }
            var result = cmd.ExecuteNonQuery();
            connecton.Close();
            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }
        public AdoDotNetParameter(string name, object value) {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
