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
        private int Id { get; set; }
        public int CodeName { get; private set; }
        public string RealName { get; private set; }
        public string Location { get; private set; }
        public string Status { get; private set; }
        public int MissionsCompleted { get; private set; }

        public Agent(int codeName, string name, string location, string status, int nmc)
        {
            CodeName = codeName;
            RealName = name;
            Location = location;
            Status = status;
            MissionsCompleted = nmc;
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
