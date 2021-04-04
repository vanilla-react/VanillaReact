using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
  public class Kudo
  {
    public int SnippetId { get; set; }
    public Snippet Snippet { get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; }
  }
}