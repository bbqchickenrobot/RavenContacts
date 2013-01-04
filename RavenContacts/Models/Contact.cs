using System;
using System.ComponentModel.DataAnnotations;

namespace Raven.Contacts.Models
{
	public class Contact
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }
	}
}