using Entities.LinkModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Entities.Models
{
    public class Entity
    {
        public class Company
        {
            [Column("CompanyId")]
            public Guid Id { get; set; }

            [Required(ErrorMessage = "Company name is a required field.")]
            [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
            public string? Name { get; set; }

            [Required(ErrorMessage = "Company address is a required field.")]
            [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters.")]
            public string? Address { get; set; }

            public string? Country { get; set; }

            public ICollection<Employee>? Employees { get; set; }
        }

        public class Employee
        {
            [Column("EmployeeId")]
            public Guid Id { get; set; }

            [Required(ErrorMessage = "Employee name is a required field.")]
            [MaxLength(30, ErrorMessage = "Maximum length for the Name is 60 characters.")]
            public string? Name { get; set; }

            [Required(ErrorMessage = "Age is a required field.")]
            public int Age { get; set; }

            [Required(ErrorMessage = "Position is a required field.")]
            [MaxLength(20, ErrorMessage = "Maximum length for a Position is 20 characters.")]
            public string? Position { get; set; }

            [ForeignKey(nameof(Company))]
            public Guid CompanyId { get; set; }
            public Company? Company { get; set; }
        }

        private void WriteLinksToXml(string key, object value, XmlWriter writer)
        {
            writer.WriteStartElement(key);
            if (value.GetType() == typeof(List<Link>))
            {
                foreach (var val in value as List<Link>)
                {
                    writer.WriteStartElement(nameof(Link));
                    WriteLinksToXml(nameof(val.Href), val.Href, writer);
                    WriteLinksToXml(nameof(val.Method), val.Method, writer);
                    WriteLinksToXml(nameof(val.Rel), val.Rel, writer);
                    writer.WriteEndElement();
                }
            }
            else
            {
                writer.WriteString(value.ToString());
            }
            writer.WriteEndElement();
        }
    }
}
