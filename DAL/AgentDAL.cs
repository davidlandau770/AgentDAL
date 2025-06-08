using c__SQL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using System.Xml.Linq;
using static Mysqlx.Expect.Open.Types;
using System.Linq;

namespace c__SQL.DAL
{
    internal class AgentDAL
    {
        private string connStr = "server=localhost;user=root;password=;database=eagle_eye_db";
        private MySqlConnection _conn;


        public MySqlConnection OpenConnection()
        {
            if (_conn == null)
            {
                _conn = new MySqlConnection(connStr);
            }

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _conn.Open();
                Console.WriteLine("Connection successful.");
            }

            return _conn;
        }

        public AgentDAL()
        {
            try
            {
                OpenConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
                _conn = null;
            }
        }

        public List<Agent> GetAgents(string query = "SELECT * FROM agent")
        {
            List<Agent> agentsList = new List<Agent>();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                OpenConnection();
                cmd = new MySqlCommand(query, _conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    int codeName = reader.GetInt32("codeName");
                    string name = reader.GetString("realName");
                    string location = reader.GetString("location");
                    string status = reader.GetString("status");
                    int nmc = reader.GetInt32("missionsCompleted");

                    Agent agents = new Agent(codeName, name, location, status, nmc);
                    agentsList.Add(agents);
                }
                Console.WriteLine("Connection successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching agents: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                CloseConnection();
            }

            return agentsList;
        }

        public void InsertAgent(Agent agent)
        {
            MySqlCommand cmd = null;
            try
            {
                OpenConnection();

                string query = "INSERT INTO agent (codeName, realName, location, status, missionsCompleted) VALUES (@codeName, @realName, @location, @status, @missionsCompleted);";

                cmd = new MySqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@codeName", agent.CodeName);
                cmd.Parameters.AddWithValue("@realName", agent.RealName);
                cmd.Parameters.AddWithValue("@location", agent.Location);
                cmd.Parameters.AddWithValue("@status", agent.Status);
                cmd.Parameters.AddWithValue("@missionsCompleted", agent.MissionsCompleted);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Agent inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while inserting agent: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void UpdateAgent(Agent agent)
        {
            MySqlCommand cmd = null;
            try
            {
                OpenConnection();

                string query = "UPDATE agent SET codeName = @codeName, realName = @realName, location = @location, status = @status, missionsCompleted = @missionsCompleted WHERE id = @id;";

                cmd = new MySqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@codeName", agent.CodeName);
                cmd.Parameters.AddWithValue("@realName", agent.RealName);
                cmd.Parameters.AddWithValue("@location", agent.Location);
                cmd.Parameters.AddWithValue("@status", agent.Status);
                cmd.Parameters.AddWithValue("@missionsCompleted", agent.MissionsCompleted);
                //cmd.Parameters.AddWithValue("@id", agent.Id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Agent updated successfully.");
                else
                    Console.WriteLine("No agent found with the given ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating agent: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void PrintAgent()
        {
            List<Agent> agents = GetAgents();
            foreach (Agent agent in agents)
            {
                agent.PrintDetails();
            }
        }

        public void Creat_table(string tableName)
        {
            MySqlCommand cmd = null;
            try
            {
                OpenConnection();
                string checkQuery = $"SHOW TABLES LIKE '{tableName}'";
                cmd = new MySqlCommand(checkQuery, _conn);
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    Console.WriteLine($"Table '{tableName}' already exists.");
                    return;
                }

                cmd = new MySqlCommand($"CREATE TABLE IF NOT EXISTS {tableName} (id INT AUTO_INCREMENT PRIMARY KEY)", _conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching agents: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void AlertTable(string tableName, string type, string columnName)
        {
            MySqlCommand cmd = null;
            try
            {
                Console.WriteLine($"{columnName}:");
                OpenConnection();
                string checkQuery = $"SHOW TABLES LIKE '{tableName}'";
                cmd = new MySqlCommand(checkQuery, _conn);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine($"Table '{tableName}' does not exist.");
                    return;
                }

                if (type == "string")
                {
                    type = "varchar(100)";
                }
                cmd = new MySqlCommand($"ALTER TABLE {tableName} ADD {columnName} {type};", _conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching agents: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Insert(string tableName, string columnsName, string values)
        {
            MySqlCommand cmd = null;
            try
            {
                Console.WriteLine($"{columnsName}:");
                OpenConnection();
                string checkQuery = $"SHOW TABLES LIKE '{tableName}'";
                cmd = new MySqlCommand(checkQuery, _conn);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine($"Table '{tableName}' does not exist.");
                    return;
                }
                cmd = new MySqlCommand($"INSERT INTO {tableName} ({columnsName}) VALUES ({values});", _conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Table updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching agents: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(string tableName, string columnName, string newValue, string condition)
        {
            MySqlCommand cmd = null;
            try
            {
                Console.WriteLine($"{columnName}:");
                OpenConnection();
                string checkQuery = $"SHOW TABLES LIKE '{tableName}'";
                cmd = new MySqlCommand(checkQuery, _conn);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine($"Table '{tableName}' does not exist.");
                    return;
                }

                cmd = new MySqlCommand($"UPDATE {tableName} SET {columnName} = '{newValue}' WHERE {condition};", _conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching agents: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Delete(string tableName, string condition)
        {
            MySqlCommand cmd = null;
            try
            {
                OpenConnection();
                string checkQuery = $"SHOW TABLES LIKE '{tableName}'";
                cmd = new MySqlCommand(checkQuery, _conn);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine($"Table '{tableName}' does not exist.");
                    return;
                }
                cmd = new MySqlCommand($"DELETE FROM {tableName} WHERE {condition};", _conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Row deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting row: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeleteAllRows(string tableName)
        {
            MySqlCommand cmd = null;
            try
            {
                OpenConnection();
                string checkQuery = $"SHOW TABLES LIKE '{tableName}'";
                cmd = new MySqlCommand(checkQuery, _conn);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine($"Table '{tableName}' does not exist.");
                    return;
                }
                string deleteQuery = $"DELETE FROM {tableName}";
                cmd = new MySqlCommand(deleteQuery, _conn);
                cmd.ExecuteNonQuery();

                string resetAutoIncrement = $"ALTER TABLE {tableName} AUTO_INCREMENT = 1";
                cmd = new MySqlCommand(resetAutoIncrement, _conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine($"All rows deleted from '{tableName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting all rows: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SearchAgentsByCode(string tableName, string partialCode)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                OpenConnection();
                string checkQuery = $"SHOW TABLES LIKE '{tableName}'";
                cmd = new MySqlCommand(checkQuery, _conn);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine($"Table '{tableName}' does not exist.");
                    return;
                }
                cmd = new MySqlCommand($"SELECT * FROM {tableName} WHERE codeName = '{partialCode}';", _conn);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    int codeName = reader.GetInt32("codeName");
                    string name = reader.GetString("realName");
                    string location = reader.GetString("location");
                    string status = reader.GetString("status");
                    int nmc = reader.GetInt32("missionsCompleted");
                    Console.WriteLine($"codeName: {codeName}, name: {name}, location: {location}, status: {status}, nmc: {nmc}");
                }
                Console.WriteLine("searched successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while searched: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
