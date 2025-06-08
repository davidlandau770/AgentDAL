using System;
using System.Collections.Generic;
using c__SQL.BasicConnetion;
using c__SQL.DAL;
using c__SQL.Models;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        // basic connection
        //BasicConnection bc = new BasicConnection();
        //bc.CreateBasicConnection();

        // DAL
        AgentDAL dal = new AgentDAL();
        string query = "CREATE TABLE eagle_eye (codeName INT PRIMARY KEY, name VARCHAR(30), location VARCHAR(40), status VARCHAR(15), number_missions_completed VARCHAR(10));";
        //List<Employee> allEmpList = dal.GetEmployees();
        List<Agent> FilteredAgentsList = dal.GetAgents();
        foreach (Agent agents in FilteredAgentsList)
        {
            agents.PrintDetails();
        }

        //dal.Creat_table("agents", "codeName", "realName", "location", "status", "missionsCompleted");
        //dal.DeleteAllRows("agent");

        //dal.Creat_table("agent");
        //dal.AlertTable("agent", "int", "codeName");
        //dal.AlertTable("agent", "string", "realName");
        //dal.AlertTable("agent", "string", "location");
        //dal.AlertTable("agent", "ENUM('Active', 'Injured', 'Missing', 'Retired')", "status");
        //dal.AlertTable("agent", "int", "missionsCompleted");
        dal.Insert("agent", "codeName, realName, location, status, missionsCompleted", "1, 'david', 'bney brak', 'active', 006");
        dal.Insert("agent", "codeName, realName, location, status, missionsCompleted", "3, 'avishay', 'lod', 'aaactive', 007");
        dal.Update("agent", "status", "Retired", "id IN(2, 4)");
        dal.Delete("agent", "id = 7");
        dal.SearchAgentsByCode("agent", "3");
    }
}
