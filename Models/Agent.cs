using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__SQL.Models
{
    internal class Agent
    {
        private int Id { get; set; }
        private int CodeName { get; set; }
        private string RealName { get; set; }
        private string Location { get; set; }
        private string Status { get; set; }
        private string MissionsCompleted { get; set; }

        public Agent(int codeName, string name, string location, string status, string nmc)
        {
            CodeName = codeName;
            RealName = name;
            Location = location;
            Status = status;
            MissionsCompleted = nmc;
        }
        public void PrintDetails()
        {
            Console.WriteLine($"id: {Id}, code name: {CodeName}, name: {RealName}, location: {Location}, status: {Status}, number missions completed: {MissionsCompleted}");
        }

    }
}
