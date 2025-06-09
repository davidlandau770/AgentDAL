using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c__SQL.DAL;
using MySql.Data.MySqlClient;

namespace c__SQL.Models
{
    internal class Agent
    {
        public int Id { get; private set; }
        public int CodeName { get; private set; }
        public string RealName { get; private set; }
        public string Location { get; private set; }
        public string Status { get; private set; }
        public int MissionsCompleted { get; private set; }

        public Agent(int id, int codeName, string name, string location, string status, int missionsCompleted)
        {
            Id = id;
            CodeName = codeName;
            RealName = name;
            Location = location;
            Status = status;
            MissionsCompleted = missionsCompleted;
        }

        public void PrintDetails()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return $"code name: {CodeName}, name: {RealName}, location: {Location}, status: {Status}, number missions completed: {MissionsCompleted}";
        }
    }
}
