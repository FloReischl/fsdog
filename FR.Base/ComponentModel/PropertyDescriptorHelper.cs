// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.PropertyDescriptorHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FR.ComponentModel
{
  public static class PropertyDescriptorHelper
  {
    private static Dictionary<string, PropertyDescriptor> _properties = new Dictionary<string, PropertyDescriptor>();

    public static PropertyDescriptorCollection GetProperties(
      object component)
    {
      return !(component is ICustomTypeDescriptor customTypeDescriptor) ? TypeDescriptor.GetProvider(component).GetTypeDescriptor(component.GetType(), component).GetProperties() : customTypeDescriptor.GetProperties();
    }

    public static PropertyDescriptorCollection GetProperties(Type type) => TypeDescriptor.GetProvider(type).GetTypeDescriptor(type).GetProperties();

    public static PropertyDescriptor GetProperty(
      object component,
      string propertyName)
    {
      return PropertyDescriptorHelper.GetProperties(component)[propertyName];
    }

    public static PropertyDescriptor GetProperty(Type type, string propertyName) => PropertyDescriptorHelper.GetProperties(type)[propertyName];

    public static PropertyDescriptorCollection GetDynamicProperties(
      PropertyDescriptorCollection properties)
    {
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>(properties.Count);
      for (int index = 0; index < properties.Count; ++index)
      {
        PropertyDescriptor dynamicProperty = PropertyDescriptorHelper.GetDynamicProperty(properties[index], true);
        propertyDescriptorList.Add(dynamicProperty);
      }
      properties = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
      return properties;
    }

    public static PropertyDescriptor GetDynamicProperty(
      PropertyDescriptor template,
      bool checkForReflected)
    {
      return checkForReflected && (template.PropertyType.Namespace != "System.ComponentModel" || template.PropertyType.Name != "ReflectPropertyDescriptor") ? template : new DynamicPropertyCreator(template).Create();
    }
  }
}
