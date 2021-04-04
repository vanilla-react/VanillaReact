using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VanillaReact.Api.Models
{
  public class Snippet
  {
    public int SnippetId { get; set; }
    public Status Status { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }

    //The JSON content will contain inner keys with "vanilla" and "react"
    public string Content { get; set; }

    //Relationships
    public List<Category> Categories { get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; }
    public List<Feedback> FeedbackItems { get; set; }
    public List<Kudo> Kudos { get; set; }
  }

  public enum Status
  {
    PENDING,
    APPROVED
  }
}