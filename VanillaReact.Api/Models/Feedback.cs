using System.Collections.Generic;

namespace Api.Models
{
  public class Feedback
  {
    public int FeedbackId { get; set; }
    public string Content { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }

    public int SnippetId { get; set; }
    public Snippet Snippet { get; set; }
  }
}