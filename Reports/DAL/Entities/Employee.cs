using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; }

        public string Name { get; }

        public Employee(string name, Guid supervisorId)
        {
            /*if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }*/

            Id = Guid.NewGuid();
            Name = name;
            SupervisorId = supervisorId;
        }

        public Employee()
        {
        }

        public Guid SupervisorId { get; set; }
    }
}