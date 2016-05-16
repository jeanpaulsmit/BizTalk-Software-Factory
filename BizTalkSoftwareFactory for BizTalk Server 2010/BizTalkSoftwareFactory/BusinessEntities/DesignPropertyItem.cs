using System;
using System.Collections.Generic;
using System.Text;

namespace BizTalkSoftwareFactory.BusinessEntities
{
  /// <summary>
  /// </summary>
  public class DesignPropertyItemCollection : List<DesignPropertyItem>
  {
  }
  
  /// <summary>
  /// This is a Business Entity representing a Design Property, used by the Pipeline Component wizard
  /// </summary>
  public class DesignPropertyItem
  {
    public DesignPropertyItem()
    {
    }

      // Constuctor to set the name
    public DesignPropertyItem(string name, string type)
    {
        this.PropertyName = name;
        this.PropertyType = type;
    }

    private string propertyName;

    public string PropertyName
    {
      get { return propertyName; }
      set { propertyName = value; }
    }

    private string propertyType;

    public string PropertyType
    {
      get { return propertyType; }
      set { propertyType = value; }
    }
  }
}
