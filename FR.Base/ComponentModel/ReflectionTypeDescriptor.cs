// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.ReflectionTypeDescriptor
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace FR.ComponentModel
{
  public class ReflectionTypeDescriptor : ICustomTypeDescriptor
  {
    private Type _objectType;
    private ReflectionDescriptionProvider _provider;
    private object _instance;

    public Type ObjectType => this._objectType;

    public ReflectionDescriptionProvider Provider => this._provider;

    public object Instance => this._instance;

    public ReflectionTypeDescriptor(
      ReflectionDescriptionProvider provider,
      Type objectType,
      object instance)
    {
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      if (objectType == null)
        throw new ArgumentNullException(nameof (objectType));
      this._instance = instance;
      this._objectType = objectType;
      this._provider = provider;
    }

    public virtual AttributeCollection GetAttributes()
    {
      object[] customAttributes = this.ObjectType.GetCustomAttributes(false);
      Attribute[] attributeArray = new Attribute[customAttributes.Length];
      customAttributes.CopyTo((Array) attributeArray, 0);
      return new AttributeCollection(attributeArray);
    }

    public virtual string GetClassName() => this.ObjectType.Name;

    public virtual string GetComponentName() => this.ObjectType.Name;

    public virtual TypeConverter GetConverter()
    {
      object[] customAttributes = this.ObjectType.GetCustomAttributes(typeof (TypeConverterAttribute), false);
      return customAttributes.Length != 0 ? (TypeConverter) Type.GetType(((TypeConverterAttribute) customAttributes[0]).ConverterTypeName).GetConstructor(Type.EmptyTypes).Invoke(new object[0]) : new TypeConverter();
    }

    public virtual EventDescriptor GetDefaultEvent()
    {
      foreach (EventDescriptor defaultEvent in this.GetEvents())
      {
        foreach (Attribute attribute in defaultEvent.Attributes)
        {
          if (attribute is DefaultEventAttribute)
            return defaultEvent;
        }
      }
      return (EventDescriptor) null;
    }

    public virtual PropertyDescriptor GetDefaultProperty()
    {
      foreach (PropertyDescriptor property in this.GetProperties())
      {
        for (int index = 0; index < property.Attributes.Count; ++index)
        {
          if (property.Attributes[index] is DefaultPropertyAttribute)
            return property;
        }
      }
      return (PropertyDescriptor) null;
    }

    public virtual object GetEditor(Type editorBaseType)
    {
      object[] customAttributes = this.ObjectType.GetCustomAttributes(typeof (EditorAttribute), false);
      return customAttributes.Length != 0 ? Type.GetType(((EditorAttribute) customAttributes[0]).EditorTypeName).GetConstructor(Type.EmptyTypes).Invoke(new object[0]) : (object) null;
    }

    public virtual EventDescriptorCollection GetEvents(
      Attribute[] attributes)
    {
      EventDescriptorCollection properties = this.Provider.EventsFilterCache.FindProperties(this.ObjectType, attributes);
      if (properties != null)
        return properties;
      bool flag = attributes != null && attributes.Length > 0;
      EventDescriptorCollection events = new EventDescriptorCollection((EventDescriptor[]) null);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (EventInfo eventInfo in this.ObjectType.GetEvents())
      {
        dictionary.Add(eventInfo.Name, eventInfo.Name);
        ReflectionEventDescriptor reflectionEventDescriptor = new ReflectionEventDescriptor(eventInfo);
        if (!flag || reflectionEventDescriptor.Attributes.Contains(attributes))
          events.Add((EventDescriptor) reflectionEventDescriptor);
      }
      this.Provider.EventsFilterCache.SetProperties(this.ObjectType, attributes, events);
      return events;
    }

    public virtual EventDescriptorCollection GetEvents() => this.GetEvents((Attribute[]) null);

    public virtual object GetPropertyOwner(PropertyDescriptor pd) => this.Instance;

    public virtual PropertyDescriptorCollection GetProperties() => this.GetProperties((Attribute[]) null);

    public virtual PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      PropertyDescriptorCollection properties1 = this.Provider.PropertiesCache.FindProperties(this.ObjectType, attributes);
      if (properties1 != null)
        return properties1;
      bool flag = attributes != null && attributes.Length > 0;
      PropertyDescriptorCollection properties2 = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (PropertyInfo property in this.ObjectType.GetProperties())
      {
        PropertyInfo[] properties3 = property.PropertyType.GetProperties();
        if (!dictionary.ContainsKey(property.Name))
        {
          dictionary.Add(property.Name, property.Name);
          PropertyDescriptor propertyDescriptor = property.PropertyType.HasElementType || properties3.Length <= 0 ? (PropertyDescriptor) new ReflectionPropertyDescriptor(property) : (PropertyDescriptor) new ExpandablePropertyDescriptor(property);
          if (!flag || propertyDescriptor.Attributes.Contains(attributes))
            properties2.Add(propertyDescriptor);
        }
      }
      foreach (Type type in this.ObjectType.GetInterfaces())
      {
        foreach (PropertyInfo property in type.GetProperties())
        {
          PropertyInfo[] properties4 = property.PropertyType.GetProperties();
          if (!dictionary.ContainsKey(property.Name))
          {
            PropertyDescriptor propertyDescriptor = property.PropertyType.HasElementType || properties4.Length <= 0 ? (PropertyDescriptor) new ReflectionPropertyDescriptor(property) : (PropertyDescriptor) new ExpandablePropertyDescriptor(property);
            if (!flag || propertyDescriptor.Attributes.Contains(attributes))
              properties2.Add(propertyDescriptor);
          }
        }
      }
      this.Provider.PropertiesCache.SetProperties(this.ObjectType, attributes, properties2);
      return properties2;
    }

    [DebuggerNonUserCode]
    AttributeCollection ICustomTypeDescriptor.GetAttributes() => this.GetAttributes();

    [DebuggerNonUserCode]
    string ICustomTypeDescriptor.GetClassName() => this.GetClassName();

    [DebuggerNonUserCode]
    string ICustomTypeDescriptor.GetComponentName() => this.GetComponentName();

    [DebuggerNonUserCode]
    TypeConverter ICustomTypeDescriptor.GetConverter() => this.GetConverter();

    [DebuggerNonUserCode]
    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => this.GetDefaultEvent();

    [DebuggerNonUserCode]
    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => this.GetDefaultProperty();

    [DebuggerNonUserCode]
    object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => this.GetEditor(editorBaseType);

    [DebuggerNonUserCode]
    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
      Attribute[] attributes)
    {
      return this.GetEvents(attributes);
    }

    [DebuggerNonUserCode]
    EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => this.GetEvents();

    [DebuggerNonUserCode]
    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
      Attribute[] attributes)
    {
      return this.GetProperties(attributes);
    }

    [DebuggerNonUserCode]
    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => this.GetProperties();

    [DebuggerNonUserCode]
    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => this.GetPropertyOwner(pd);
  }
}
