using System;
using System.Collections.Generic;

namespace HCSampleProject.Database.Models
{
  public class Cfp
  {
    public Guid Id { get; set; }
    public string Title { get; set; }

    public List<Material> Materials { get; set; }
  }
}
