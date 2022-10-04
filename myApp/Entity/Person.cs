using System.ComponentModel.DataAnnotations.Schema;

namespace DB_Test.Entity;

public class Person
{
    [Column("person_id")] public int Id { get; set; }

    [Column("first_name")] public string? FirstName { get; set; }

    [Column("second_name")] public string? SecondName { get; set; }

    [Column("middle_name")] public string? MiddleName { get; set; }

    [Column("birth_bate")] public DateOnly BirthDate { get; set; }

    [Column("sex")] public bool Sex { get; set; }
}