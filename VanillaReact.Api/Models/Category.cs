using System.Collections.Generic;

namespace Api.Models
{
  public class Category
  {
    public int CategoryId { get; set; }
    public string Name { get; set; }

    public List<Snippet> Snippets { get; set; }

  }
}