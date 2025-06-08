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
        AgentDAL dal = new AgentDAL();
        dal.Creat_table("agents");

        Agent agent1 = new Agent(101, "david", "bney brak", "Active", 12);
        Agent agent2 = new Agent(102, "avishay", "lod", "Injured", 5);
        Agent agent3 = new Agent(103, "landau", "new york", "Missing", 5);

        dal.InsertAgent(agent1);
        dal.InsertAgent(agent2);
        dal.InsertAgent(agent3);

        dal.PrintAgent();

        // ישן - הכל עובד אבל בלי שימוש גורף בקלאס אג'נט. מתאים לכל סוג וגודל של טבלה
        //dal.DeleteAllRows("agent");
        //dal.Creat_table("agent");
        //dal.AlertTable("agent", "int", "codeName");
        //dal.AlertTable("agent", "string", "realName");
        //dal.AlertTable("agent", "string", "location");
        //dal.AlertTable("agent", "ENUM('Active', 'Injured', 'Missing', 'Retired')", "status");
        //dal.AlertTable("agent", "int", "missionsCompleted");
        //dal.Insert("agent", "codeName, realName, location, status, missionsCompleted", "1, 'david', 'bney brak', 'active', 006");
        //dal.Insert("agent", "codeName, realName, location, status, missionsCompleted", "3, 'avishay', 'lod', 'aaactive', 007");
        //dal.Update("agent", "status", "Retired", "id IN(2, 4)");
        //dal.Delete("agent", "id = 7");
        //dal.SearchAgentsByCode("agent", "3");
    }
}
